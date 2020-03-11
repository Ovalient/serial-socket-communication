using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace ServerProgram
{
    class MsSqlManager
    {
        /// <summary>
        /// SELECT DATE, BARCODE, ADDRESS FROM dbo.SERVER
        /// </summary>
        /// <param name="dgv"></param>
        public void LoadDatabase(DataGridView dgv)
        {
            DataSet dataSet = new DataSet();

            string path = Environment.CurrentDirectory + "\\DBMSConfig.xml";
            XElement xElem = XElement.Load(path);

            var result = from xe in xElem.Elements("CONN")
                         select xe;

            var dbms = result.FirstOrDefault();
            if (dbms != null)
            {
                string strConn = dbms.Element("STRCONN").Value;
                string sql = dbms.Element("LOADCMD").Value;
                SqlConnection conn = new SqlConnection(strConn);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, strConn);
                adapter.Fill(dataSet);

                dgv.Invoke(new EventHandler(delegate
                {
                    dgv.DataSource = dataSet.Tables[0];
                    dgv.Columns["DATE"].HeaderText = "Date";
                    dgv.Columns["DATE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["BARCODE"].HeaderText = "Barcode";
                    dgv.Columns["BARCODE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv.Columns["ADDRESS"].HeaderText = "IP Address";
                    dgv.Columns["ADDRESS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }));
            }
        }

        /// <summary>
        /// INSERT INTO dbo.SERVER (DATE, BARCODE, ADDRESS) VALUES (@param1, @param2, @param3)
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dateTime"></param>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        public void InsertDatabase(DataGridView dgv, string dateTime, string msg, string ip)
        {
            string path = Environment.CurrentDirectory + "\\DBMSConfig.xml";
            XElement xElem = XElement.Load(path);

            var result = from xe in xElem.Elements("CONN")
                         select xe;

            var dbms = result.FirstOrDefault();
            if (dbms != null)
            {
                string strConn = dbms.Element("STRCONN").Value;
                SqlConnection conn = new SqlConnection(strConn);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = dbms.Element("UPDATECMD").Value;

                cmd.Parameters.AddWithValue("@param1", dateTime);
                cmd.Parameters.AddWithValue("@param2", msg);
                cmd.Parameters.AddWithValue("@param3", ip);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadDatabase(dgv);
            }
        }
    }
}
