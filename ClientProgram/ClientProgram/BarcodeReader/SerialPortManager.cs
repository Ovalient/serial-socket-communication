using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.IO.Ports;
using System.Threading;

namespace ClientProgram
{
    class SerialPortManager
    {
        private SerialPort serialPort;
        private XmlParser xmlParser = new XmlParser();
        
        /// <summary>
        /// XmlParser_SerialPort 세팅으로 시리얼 포트 연결 시도
        /// </summary>
        public void StartListening()
        {
            // 시리얼 포트가 열려있으면 종료
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();

            string portName = SerialSettings.Instance.PortName;
            int baudRate = SerialSettings.Instance.BaudRate;
            Parity parity = SerialSettings.Instance.Parity;
            int dataBits = SerialSettings.Instance.DataBits;
            StopBits stopBits = SerialSettings.Instance.StopBits;

             // 시리얼 포트 세팅
             serialPort = new SerialPort(
                 portName,
                 baudRate,
                 parity,
                 dataBits,
                 stopBits);

            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            serialPort.Open();
        }

        /// <summary>
        /// 시리얼 포트 종료
        /// </summary>
        public void StopListening()
        {
            serialPort.Close();
        }

        // 수신 받은 시리얼 통신 데이터를 전달하기 위한 델리게이트 선언
        public delegate void RecvMsgHandler(string msg);
        public event RecvMsgHandler RecvMsgEvent;
        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string msg = serialPort.ReadExisting();
            // 델리게이트를 통해 msg를 BarcodeReader.cs로 전달
            this.RecvMsgEvent(msg);
        }
    }
}
