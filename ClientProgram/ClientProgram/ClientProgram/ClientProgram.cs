using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProgram
{
    public partial class ClientProgram : Form
    {
        #region First header checkbox setting
        private void dgvPidList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;

                int nWidth = 15;
                int nHeight = 15;
                int offsetX = (e.CellBounds.Width - nWidth) / 2;
                int offsetY = (e.CellBounds.Height - nHeight) / 2;

                pt.X += offsetX;
                pt.Y += offsetY;

                CheckBox cb = new CheckBox();
                cb.Size = new Size(nWidth, nHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(dgvPidList_CheckedChanged);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }

        private void dgvPidList_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvPidList.Rows)
            {
                r.Cells["checkboxColumn"].Value = ((CheckBox)sender).Checked;
                dgvPidList.EndEdit();
            }
        }
        #endregion

        private BarcodeReader barcodeRdr = new BarcodeReader();
        private SerialPortManager serialPortManager = new SerialPortManager();
        private XmlParser xmlParser = new XmlParser();
        private IpAddressSettings ipAddressSettings = new IpAddressSettings();
        private SqliteManager sqlite = new SqliteManager();
        private readonly LogManager logManager = new LogManager("Client_Log");

        public ClientProgram()
        {
            InitializeComponent();

            UserInitialization();
        }

        void UserInitialization()
        {
            barcodeRdr.FormSendEvent += new BarcodeReader.FormSendDataHandler(FormReceiver);
            barcodeRdr.ShowDialog();            
            dgvPidList_Draw();

            ipAddressSettings.LogMsgEvent += new IpAddressSettings.LogMsgHandler(LogMsgReceiver);

            string ipAddress = ipAddressSettings.LoadIpAddress().ToString();
            int portNo = 8080;
            txtIpAddress.Text = ipAddress;
            txtPortNo.Text = portNo.ToString();

            ipAddressSettings.RequestConnection(ipAddress, portNo);
            ipAddressSettings.SendData(txtBarcodeNo.Text);

            sqlite.SaveSqlite(txtBarcodeNo.Text, txtModelName.Text);
        }

        /// <summary>
        /// BarcodeReader.cs에서 전달받은 바코드 번호, 모델 이름을 UI에 설정
        /// </summary>
        /// <param name="info"></param>
        private void FormReceiver(string[] info)
        {
            string barcodeNo = info[0];
            string modelName = info[1];

            txtBarcodeNo.Text = barcodeNo;
            txtModelName.Text = modelName;
        }

        /// <summary>
        /// IpAddressSettings.cs에서 전달받은 로그를 UI에 설정
        /// </summary>
        /// <param name="msg"></param>
        private void LogMsgReceiver(string msg)
        {
            tslStatus.Text = msg;

            var stringWriter = string.Format(msg);
            this.logManager.WriteLine(stringWriter);
        }

        private void dgvPidList_Draw()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlParser.XmlParser_SetPath());
            dataSet.Tables["PID"].Columns["ID"].Unique = true;

            dgvPidList.DataSource = dataSet;
            dgvPidList.DataMember = "PID";
            dgvPidList.Columns["Id"].DisplayIndex = 1;
            dgvPidList.Columns["Id"].HeaderText = "PID";
            dgvPidList.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPidList.Columns["Text"].DisplayIndex = 2;
            dgvPidList.Columns["Text"].HeaderText = "Description";
            dgvPidList.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            txtPidCount.Text = dgvPidList.RowCount.ToString();

            dgvPidList.Columns[1].ReadOnly = true;
            dgvPidList.Columns[2].ReadOnly = true;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            LogMsgReceiver("Restart barcode reader");
            barcodeRdr.Close();
            Thread.Sleep(500);
            barcodeRdr.RestartForm();
        }
    }
}
