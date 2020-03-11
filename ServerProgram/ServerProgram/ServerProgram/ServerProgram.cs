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
using System.Net;
using System.Net.Sockets;

using System.Data.SqlClient;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace ServerProgram
{
    public partial class ServerProgram : Form
    {
        private IpAddressSettings ipAddressSettings = new IpAddressSettings();
        private MsSqlManager msSqlManager = new MsSqlManager();
        private ExcelManager excelManager = new ExcelManager();
        private readonly LogManager logManager = new LogManager("Server_Log");

        public ServerProgram()
        {
            InitializeComponent();

            UserInitialization();
        }

        void UserInitialization()
        {
            // Log delegate
            ipAddressSettings.LogMsgEvent += new IpAddressSettings.LogMsgHandler(LogMsgReceiver);
            // Database delegate
            ipAddressSettings.MsSqlEvent += new IpAddressSettings.MsSqlHandler(MsSqlReceiver);

            IPAddress ipAddress = ipAddressSettings.LoadIpAddress();
            int portNo = 8080;
            txtIpAddress.Text = ipAddress.ToString();
            txtPortNo.Text = portNo.ToString();
            // 서버 연결 실행
            ipAddressSettings.RequestConnection(portNo);
        }

        /// <summary>
        /// Log delegate
        /// </summary>
        /// <param name="msg"></param>
        private void LogMsgReceiver(string msg)
        {
            tslStatus.Text = msg;

            // LogManager.cs로 로그 저장
            var stringWriter = string.Format(msg);
            this.logManager.WriteLine(stringWriter);
        }

        /// <summary>
        /// Database delegate
        /// </summary>
        /// <param name="time"></param>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        private void MsSqlReceiver(string time, string msg, string ip)
        {
            msSqlManager.InsertDatabase(dgvRecvMsg, time, msg, ip);
        }

        private void ServerProgram_Load(object sender, EventArgs e)
        {
            msSqlManager.LoadDatabase(dgvRecvMsg);
            cbbFilter.SelectedIndex = 0;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dgvRecvMsg.DataSource;
            switch (cbbFilter.SelectedIndex)
            {
                // Date
                case 0:
                    dataTable.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", dgvRecvMsg.Columns[1].DataPropertyName, txtFilter.Text.Replace("'", "''"));
                    break;
                // Barcode
                case 1:
                    dataTable.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", dgvRecvMsg.Columns[2].DataPropertyName, txtFilter.Text.Replace("'", "''"));
                    break;
                // IP address
                case 2:
                    dataTable.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", dgvRecvMsg.Columns[3].DataPropertyName, txtFilter.Text.Replace("'", "''"));
                    break;
            }
            dgvRecvMsg.Refresh();
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            excelManager.ExcelExport(dgvRecvMsg);
            this.Enabled = true;
        }
    }
}
