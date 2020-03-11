using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace ClientProgram
{
    class IpAddressSettings
    {
        /// <summary>
        /// 비동기 작업에서 사용하는 소켓과 해당 작업에 대한 데이터 버퍼를 저장하는 클래스
        /// </summary>
        public class AsyncObject
        {
            public byte[] Buffer;
            public Socket WorkingSocket;
            public readonly int BufferSize;
            public AsyncObject(int bufferSize)
            {
                BufferSize = bufferSize;
                Buffer = new byte[BufferSize];
            }

            public void ClearBuffer()
            {
                Array.Clear(Buffer, 0, BufferSize);
            }
        }

        string clientIp;
        int clientPort;
        Socket mainSocket;

        // Log delegate
        public delegate void LogMsgHandler(string msg);
        public event LogMsgHandler LogMsgEvent;
        void AppendText(string msg)
        {
            LogMsgEvent(msg);
        }

        /// <summary>
        /// IP 주소 탐색 후 리턴
        /// </summary>
        /// <returns></returns>
        public IPAddress LoadIpAddress()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            // 처음으로 발견되는 IPv4 주소 사용
            IPAddress defaultHostAddress = null;
            foreach(IPAddress addr in he.AddressList)
            {
                if(addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    defaultHostAddress = addr;
                    break;
                }
            }

            // 주소가 없다면 로컬호스트 주소 사용
            if (defaultHostAddress == null)
                defaultHostAddress = IPAddress.Loopback;

            return defaultHostAddress;
        }

        public void RequestConnection(string ipAddress, int portNo)
        {
            clientIp = ipAddress;
            clientPort = portNo;

            // Socket 인스턴스 초기화
            // 서버 연결
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.StartClient(clientIp, clientPort);
        }

        public void StartClient(string ipAddress, int portNo)
        {
            try
            {
                AppendText("Waiting for server connection...");
                mainSocket.BeginConnect(ipAddress, portNo, new AsyncCallback(ConnectedCallback), mainSocket);
            }
            catch
            {
                // 서버 접속 실패 시, 재접속 시도
                AppendText("Lost connection with server. Reconnecting...");
                this.RequestConnection(clientIp, clientPort);
            }
        }

        /// <summary>
        /// 연결 시도 Callback
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectedCallback(IAsyncResult ar)
        {
            try
            {
                // 4096 바이트 크기의 배열을 가진 AsyncObject 클래스 생성
                // 작업 중인 소켓을 저장
                // 비동기 작업의 원격 IP 엔드 포인트 저장
                AsyncObject obj = new AsyncObject(4096);
                obj.WorkingSocket = mainSocket;
                IPEndPoint serverEp = (IPEndPoint)obj.WorkingSocket.RemoteEndPoint;

                AppendText("Connected to server.");

                // 보류 중인 연결 완성
                obj.WorkingSocket.EndConnect(ar);
                mainSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), obj);
            }
            catch(SocketException se)
            {
                if(se.SocketErrorCode == SocketError.NotConnected)
                {
                    // 서버 접속 실패 시, 재접속 시도
                    AppendText("Lost connection with server. Reconnecting...");
                    this.StartClient(clientIp, clientPort);
                }
            }
        }
        
        public void Receive(IAsyncResult ar)
        {
            AsyncObject obj = (AsyncObject)ar.AsyncState;

            mainSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, new AsyncCallback(ReceivedCallback), obj);
        }

        /// <summary>
        /// 데이터 수신 Callback
        /// </summary>
        /// <param name="ar"></param>
        private void ReceivedCallback(IAsyncResult ar)
        {
            // BeginReceive에서 수신한 데이터를 AsyncObject로 변환
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            try
            {
                int received = obj.WorkingSocket.EndReceive(ar);

                // 수신한 데이터가 없으면 종료
                if (received <= 0)
                {
                    obj.WorkingSocket.Close();
                    return;
                }

                // 수신한 데이터를 텍스트로 변환
                string txt = Encoding.UTF8.GetString(obj.Buffer);

                // 0x01 기준으로 분할
                string[] tokens = txt.Split('\x01');
                string ip = tokens[0];
                string msg = tokens[1];

                // 로그 delegate로 전달
                AppendText(string.Format("[RECV] {0}: {1}", ip, msg));

                // 버퍼를 비운 뒤, 다시 서버 수신 대기
                obj.ClearBuffer();
                this.Receive(ar);
            }
            catch (SocketException se)
            {
                if (se.SocketErrorCode == SocketError.ConnectionReset)
                {
                    this.StartClient(clientIp, clientPort);
                }
            }
        }

        public void SendData(string msg)
        {
            try
            {
                if(mainSocket.Connected)
                {
                    string tts = msg;
                    if (string.IsNullOrEmpty(msg))
                    {
                        MessageBox.Show("텍스트가 입력되지 않았습니다.");
                        return;
                    }

                    // 클라이언트 ip 주소와 메세지를 변수에 저장
                    IPEndPoint ip = (IPEndPoint)mainSocket.LocalEndPoint;
                    string addr = ip.Address.ToString();
                    string port = ip.Port.ToString();

                    // ip 주소와 전달할 텍스트를 UTF8 형식의 바이트로 변환
                    byte[] bDts = Encoding.UTF8.GetBytes(addr + ':' + port + '\x01' + tts);

                    // 서버 전달
                    mainSocket.BeginSend(bDts, 0, bDts.Length, SocketFlags.None, new AsyncCallback(SendCallback), msg);
                }
            }
            catch(SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// 데이터 전송 Callback
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            string msg = (string)ar.AsyncState;
            // Log delegate로 전달
            AppendText(string.Format("[SEND] {0} - {1}", clientIp, msg));
        }
    }
}