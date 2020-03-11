using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ServerProgram
{
    class ExcelManager
    {
        public void ExcelExport(DataGridView dgv)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add(1);
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];

            // Sheet 이름 변경
            ws.Name = "Exported from gridview";

            ws.Rows.HorizontalAlignment = HorizontalAlignment.Center;
            // 엑셀 Header 부분 쌓기
            for(int i = 1; i < dgv.Columns.Count + 1; i++)
            {
                ws.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
            }

            // 행렬 부분 쌓기
            for(int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                    ws.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
            }

            // 셀 크기 자동 정렬
            ws.Cells.EntireColumn.AutoFit();

            // 저장
            try
            {
                wb.SaveAs(Environment.CurrentDirectory + "\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("{0}" + Environment.NewLine + "사용자가 저장을 취소했습니다", ex.Message);
            }

            // 종료
            app.Quit();
        }
    }
}
