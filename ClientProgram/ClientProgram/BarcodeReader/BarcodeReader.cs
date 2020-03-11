using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;

namespace ClientProgram
{
    public partial class BarcodeReader : Form
    {
        #region Form Style
        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        #endregion

        private XmlParser xmlParser = new XmlParser();
        private SerialPortManager serialPortManager = new SerialPortManager();
    
        // 바코드 번호, 모델 이름을 ClientProgram에 전달하기 위한 델리게이트 선언
        public delegate void FormSendDataHandler(string[] str);
        public event FormSendDataHandler FormSendEvent;

        public BarcodeReader()
        {
            InitializeComponent();

            UserInitialization();
        }

        public void RestartForm()
        {
            Application.Restart();
            serialPortManager.StopListening();
        }

        private void UserInitialization()
        {
            serialPortManager.RecvMsgEvent += new SerialPortManager.RecvMsgHandler(RecvMsgReceiver);

            // 시리얼 포트 값(Xml) 설정
            // 시리얼 포트 연결
            xmlParser.SerialPortSettings();
            serialPortManager.StartListening();
        }

        /// <summary>
        /// SerialPortManger에서 전달한 델리게이트 이벤트
        /// 크로스 스레드 작업을 위해 Invoke 사용
        /// </summary>
        /// <param name="msg"></param>
        void RecvMsgReceiver(string msg)
        {
            this.Invoke(new EventHandler(delegate
            {
                btnEnter.Enabled = true;
                txtRecvMsg.Text = msg;
                tslStatus.Text = "Data Received";
            }));
        }

        /// <summary>
        /// 바코드 데이터를 ClientProgram.cs로 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            string barcodeNo;
            string modelName;

            barcodeNo = txtRecvMsg.Text;
            // Xml 파싱해 모델 이름 가져오기
            modelName = xmlParser.GetModelName(barcodeNo);
            string[] info = { barcodeNo, modelName };

            // delegate를 이용해 전달
            if(FormSendEvent != null)
                this.FormSendEvent(info);

            this.Close();
        }
    }
}