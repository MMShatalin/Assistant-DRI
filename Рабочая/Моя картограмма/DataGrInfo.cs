using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Моя_картограмма
{
    public partial class DataGrInfo : Form
    {
    

        public DataGrInfo()
        {
            InitializeComponent();
        }
        private void AddRowsToDgv()
        {
        
        }


        private void DataGrInfo_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {

                if ((string)dataGridView1.Rows[i].Cells[6].Value == "False")
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;

                else if ((string)dataGridView1.Rows[i].Cells[7].Value == "False") dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                else dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;

            }
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {

                if ((string)dataGridView1.Rows[i].Cells[6].Value == "False" && (string)dataGridView1.Rows[i].Cells[7].Value == "False")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    // dataGridView1.Rows[i].Visible = false ;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                //DataGridViewCellStyle rowRed = new DataGridViewCellStyle();
               // rowRed.BackColor = Color.Red;

                worksheet.Rows[1].Columns[1] = "Порядковый номер загрузки";
                worksheet.Rows[1].Columns[2] = "Номер ТВС";
                worksheet.Rows[1].Columns[3] = "Повысотная отметка";
                worksheet.Rows[1].Columns[4] = "Время";
                worksheet.Rows[1].Columns[5] = "Смена";
                worksheet.Rows[1].Columns[6] = "Подробности";
                worksheet.Rows[1].Columns[7] = "Замечание (True-Есть/False-Нет)";
                worksheet.Rows[1].Columns[8] = "Загрузка ТВС (True-Есть/False-Нет)";
              //  worksheet.Rows[1].Columns[5] = "Замечание";

                for (int i = 1; i < dataGridView1.RowCount + 1; i++)
                {               
                    for (int j = 1; j < dataGridView1.ColumnCount + 1; j++)
                    {              
                            worksheet.Rows[i + 1].Columns[j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value;    
                    }
                }
                

                worksheet.Columns.AutoFit();

                excelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(saveFileDialog1.FileName);
                excelapp.Quit();
            }
        }
    }
}
