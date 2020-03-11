using System;
using System.Data.SQLite;

namespace ClientProgram
{
    class SqliteManager
    {
        public void SaveSqlite(string barcodeNo, string modelName)
        {
            if(!string.IsNullOrEmpty(barcodeNo) && !string.IsNullOrEmpty(modelName))
            {
                string strConn = @"DataSource=D:\SQLiteTest\Test.db";
                string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    conn.Open();
                    string sql = "INSERT INTO BarcodeData VALUES ('" + time + "', '" + barcodeNo + "', '" + modelName + "')";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
