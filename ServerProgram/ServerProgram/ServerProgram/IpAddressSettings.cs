using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace ServerProgram
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

        int serverPort;
        Socket mainSocket;

        // Log delegate
        public delegate void LogMsgHandler(string msg);
        public event LogMsgHandler LogMsgEvent;
        void AppendText(string msg)
        {
            if(LogMsgEvent != null)
                LogMsgEvent(msg);
        }

        // Database delegate
        public delegate void MsSqlHandler(string time, string msg, string ip);
        public event MsSqlHandler MsSqlEvent;
        void AppendDatabase(string time, string msg, string ip)
        {
            if (MsSqlEvent != null)
                MsSqlEvent(time, msg, ip);
        }

        /// <summary>
        /// IP 주소 탐색 후 리턴
        /// </summary>
        /// <returns></returns>
        public IPAddress LoadIpAddress()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress defaultHostAddress = null;
            // 처음으로 발견되는 IPv4 주소 사용
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

        public void RequestConnection(int portNo)
        {
            serverPort = portNo;

            // Socket 인스턴스 초기화
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버의 주소, 포트 번호 지정
            // 생성한 serverEp의 주소, 포트 번호를 이용해 소켓에 연결
            // 접속 허용자 수 20
            IPEndPoint serverEp = new IPEndPoint(IPAddress.Any, portNo);
            mainSocket.Bind(serverEp);
            mainSocket.Listen(20);

            AppendText("Server started");

            SocketAsyncEventArgs AsyncEvent = new SocketAsyncEventArgs();
            // 클라이언트가 접속했을 때 이벤트
            // 클라이언트 접속 대기
            AsyncEvent.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptCompleted);
            mainSocket.AcceptAsync(AsyncEvent);
        }

        // 클라이언트의 갯수 만큼 소켓을 생성하기 위한 List
        List<Socket> connectedClients = new List<Socket>();
        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            Socket client = e.AcceptSocket;

            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = client;

            // 클라이언트가 접속하면 클라이언트 소켓을 리스트에 추가
            connectedClients.Add(client);

            if(connectedClients != null)
            {
                AppendText(string.Format("Connected with client(@ {0})", client.RemoteEndPoint));

                // 리시브 인스턴스 생성
                // 데이터 버퍼 설정
                // 소켓과 연결된 클라이언트(사용자) 개체 설정
                SocketAsyncEventArgs AsyncEvent = new SocketAsyncEventArgs();
                AsyncEvent.SetBuffer(obj.Buffer, 0, 4096);
                AsyncEvent.UserToken = connectedClients;
                // 클라이언트에게서 넘어온 데이터를 받았을 때 이벤트
                // 클라이언트 데이터 수신 시작
                AsyncEvent.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveCompleted);
                obj.WorkingSocket.ReceiveAsync(AsyncEvent);
            }

            // 소켓 초기화
            // 다시 클라이언트 접속 대기
            e.AcceptSocket = null;
            mainSocket.AcceptAsync(e);
        }

        private void ReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            Socket client = (Socket)sender;

            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = client;

            // 클라이언트가 연결, 받은 데이터가 있을 때
            if(client.Connected && e.BytesTransferred > 0)
            {
                obj.Buffer = e.Buffer;

                // 수신한 데이터를  텍스트로 변환
                string txt = Encoding.UTF8.GetString(obj.Buffer);

                // 0x01 기준으로 분할
                string[] tokens = txt.Split('\x01');
                string ip = tokens[0];
                string msg = tokens[1];

                // MsSql에 Update하기 위한 시간, 메세지
                string replacedMsg = msg.Replace("\0", "");
                string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                // Log delegate로 전달
                AppendText(string.Format("[RECV] {0} - {1}", ip, replacedMsg));

                e.SetBuffer(obj.Buffer, 0, 4096);
                client.ReceiveAsync(e);

                // Database delegate로 전달
                AppendDatabase(time, replacedMsg, ip);
            }
            else
            {
                // 클라이언트가 연결이 안됐을 때
                // 클라이언트 연결 해제, 리스트에서 해당 클라이언트 소켓 제거
                client.Disconnect(false);
                connectedClients.Remove(client);
                AppendText(string.Format("Lost connection with {0}", client.RemoteEndPoint.ToString()));
                Thread.Sleep(1000);
                AppendText("Restart server program...");
                Thread.Sleep(1000);
                Application.Restart();
            }
        }

                
        //public void StartServer(IPAddress ipAddress, string portNo)
        //{
        //    int port;
        //    if(!int.TryParse(portNo, out port))
        //    {
        //        MsgBoxHelper.Error("포트 번호가 잘못 입력되었거나 입력되지 않았습니다!");
        //        return;
        //    }
            
        //    // 서버에서 클라이언트의 연결 요청을 대기하기 위해 소켓을 열어둠
        //    IPEndPoint serverEp = new IPEndPoint(ipAddress, port);
        //    mainSocket.Bind(serverEp);
        //    mainSocket.Listen(20);

        //    AppendText("Server started");

        //    // 비동기적으로 클라이언트의 연결 요청을 수신
        //    mainSocket.BeginAccept(AcceptCallback, null);
        //}

        //// 접속된 클라이언트 소켓 리스트
        //List<Socket> connectedClients = new List<Socket>();
        //void AcceptCallback(IAsyncResult ar)
        //{
        //    Socket client = mainSocket.EndAccept(ar);
        //    // 클라이언트 연결 요청 수락, 다른 클라이언트 연결 대기
        //    mainSocket.BeginAccept(AcceptCallback, null);

        //    AsyncObject obj = new AsyncObject(4096);
        //    obj.WorkingSocket = client;

        //    // 연결된 클라이언트를 리스트에 추가
        //    connectedClients.Add(client);

        //    AppendText(string.Format("Client(@ {0}) is connected", client.RemoteEndPoint));

        //    // 클라이언트 데이터 수신
        //    client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        //}

        //public void DataReceived(IAsyncResult ar)
        //{
        //    // BeginReceive에서 추가로 수신한 데이터를 AsyncObject로 변환
        //    AsyncObject obj = (AsyncObject)ar.AsyncState;

        //    // 데이터 수신 종료
        //    int received = obj.WorkingSocket.EndReceive(ar);

        //    // 수신한 데이터가 없으면 종료
        //    if (received <= 0)
        //    {
        //        obj.WorkingSocket.Close();
        //        return;
        //    }

        //    // 수신한 데이터를  텍스트로 변환
        //    string txt = Encoding.UTF8.GetString(obj.Buffer);

        //    // 0x01 기준으로 분할
        //    string[] tokens = txt.Split('\x01');
        //    string ip = tokens[0];
        //    string msg = tokens[1];

        //    // 로그 delegate로 전달
        //    AppendText(string.Format("[RECV] {0}: {1}", ip, msg));

        //    // 역순으로 클라이언트에게 Echo 전달
        //    for (int i = connectedClients.Count - 1; i >= 0; i--)
        //    {
        //        Socket socket = connectedClients[i];
        //        if (socket != obj.WorkingSocket)
        //        {
        //            try { socket.Send(obj.Buffer); }
        //            catch
        //            {
        //                // 오류 발생 시 전송 취소 후 클라이언트 리스트에서 삭제
        //                try { socket.Dispose(); }
        //                catch { }
        //                connectedClients.RemoveAt(i);
        //            }
        //        }
        //    }

        //    // 데이터 수신 후 버퍼를 비운 뒤, 다시 클라이언트 수신 대기
        //    obj.ClearBuffer();
        //    obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);

        //    ServerProgram serverProgram = new ServerProgram();

        //    string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //    string replacedMsg = msg.Replace("\0", "");

        //    serverProgram.ConnectMsSql(time, replacedMsg, ip);
        //}

        //public void SendData(string msg)
        //{
        //    // 서버가 대기중인지 확인
        //    if (!mainSocket.IsBound)
        //    {
        //        MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다.");
        //        return;
        //    }

        //    // 전달할 텍스트
        //    string tts = msg;
        //    if (string.IsNullOrEmpty(tts))
        //    {
        //        MsgBoxHelper.Warn("텍스트가 입력되지 않았습니다.");
        //        return;
        //    }

        //    // 서버 ip 주소와 전달할 텍스트를 UTF8 형식의 바이트로 변환
        //    byte[] bDts = Encoding.UTF8.GetBytes(serverIp.ToString() + '\x01' + tts);

        //    // 연결된 모든 클라이언트에게 전송
        //    for (int i = connectedClients.Count - 1; i >= 0; i--)
        //    {
        //        Socket socket = connectedClients[i];
        //        try { socket.Send(bDts); }
        //        catch
        //        {
        //            // 오류 발생 시 전송 취소 후 클라이언트 리스트에서 삭제
        //            try { socket.Dispose(); }
        //            catch { }
        //            connectedClients.RemoveAt(i);
        //        }
        //    }
        //}
    }
}
