using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Моя_картограмма
{
    public partial class Form1 : Form
    {

      

        Bitmap myBitmap;
        Cartogram MyCartogram;
        Graphics g;
        Pen myPen;
        Brush mybrash;

        String smena;


        /// <summary>
        ///порядковый номер загрузки 
        /// </summary>
        int NextTvs;

        /// <summary>
        /// Сколько всего загружено в данный момент в зоне
        /// </summary>
        int totalInZOne;

        /// <summary>
        /// Сколько загружено ТВС в смену
        /// </summary>
        int totalInDuty;

        /// <summary>
        /// Индекс предпоследней загруженной ТВС, для рассчета текущей скорости 
        /// </summary>
        int lastTVS;

        int z = 2;

        public Form1()
        {
            InitializeComponent();
            this.smena = "";


            if (MyConst.InputBoxZam("Внимание. Введите данные о смене", "Пожалуйста введите ФИО ДРИ", ref this.smena) == DialogResult.OK)
            {

                MessageBox.Show("Удачной Вам дежурства, " + this.smena);
                //MyCartogram.Zona[NextTvs].Zamechanie = value;
                label7.Text = this.smena;

            }



        }






        private void Form1_Load(object sender, EventArgs e)
        {








            button1.BackColor = Color.CadetBlue;
            button3.BackColor = Color.DarkOrange;

            this.MaximizeBox = false;
            //this.MinimizeBox = false;
            
            pictureBox1.Dock = DockStyle.Fill;


            ////Этой хренькой я рисую границы ТВСок....Черной хренькой
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            myPen.Width = 4;




            myBitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);


            g = Graphics.FromImage(myBitmap);


            MyCartogram = new Cartogram(g, pictureBox1, myPen);

            pictureBox1.Image = myBitmap;



            LinearGradientBrush lgbfon = new LinearGradientBrush(
                                new Point(0, 0),
                                new Point(pictureBox1.Width, pictureBox1.Height),
                                Color.White,
                                Color.Silver);


            g.FillRectangle(lgbfon, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);





            LinearGradientBrush lgb = new LinearGradientBrush(
                                       new Point(0, 0),
                                       new Point(1500, 0),
                                       Color.White,
                                       Color.Blue);


            //   LinearGradientBrush lgb = new LinearGradientBrush()

            /// g.FillRectangle(lgb, this.ClientRectangle);

            //  g.FillRectangle(Brushes.RoyalBlue, (int)(0.25 * this.pictureBox1.Width), (int)(0.95 * this.pictureBox1.Height), (int)(0.5 * this.pictureBox1.Width), (int)(0.05 * this.pictureBox1.Height));
            //ЗАКРАСКА ФОНОМ БАССЕЙНА
            g.FillRectangle(lgb, (int)(0.25 * this.pictureBox1.Width), (int)(0.95 * this.pictureBox1.Height), (int)(0.5 * this.pictureBox1.Width), (int)(0.05 * this.pictureBox1.Height));


            Font myF = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 14, FontStyle.Bold);
            g.DrawString("БАССЕЙН", myF, Brushes.White, (int)(0.48 * this.pictureBox1.Width), (int)(0.97 * this.pictureBox1.Height));



            LinearGradientBrush lgbdri = new LinearGradientBrush(
                                   new Point(0, 0),
                                   new Point(100, 0),
                                   Color.White,
                                   Color.Chocolate);

            //СТОЛ ДРИ
            g.FillRectangle(lgbdri, (int)(0.85 * this.pictureBox1.Width), (int)(0.1 * this.pictureBox1.Height), (int)(0.05 * this.pictureBox1.Width), (int)(0.20 * this.pictureBox1.Height));
            g.DrawString("ДРИ", myF, Brushes.Black, (int)(0.86 * this.pictureBox1.Width), (int)(0.18 * this.pictureBox1.Height));

            // g.FillRectangle(Brushes.Blue, (int)0.25 * this.pictureBox1.Width, (int)0.9 * this.pictureBox1.Height, (int)0.5 * this.pictureBox1.Width, (int)0.1 * this.pictureBox1.Height);



            mybrash = new SolidBrush(Color.Chocolate);

          
               //упорядочили все ТВС в порядке очередности загрузки



            //MessageBox.Show(Application.StartupPath + "\\draw.ini");
            //MessageBox.Show(YYY.ToString());


            MyCartogram.Show();

            MyCartogram.UpdateLoadNumber(checkBox1.Checked);
            //MyCartogram.Zona.Sort(); 

          //  MyCartogram.UpdateTVSNumber();
            MyCartogram.DrawGrid();

            MyCartogram.Zona.Sort();

           

            //ЭТО БЫЛО НУЖНО ДО ТОГО КАК Я УПОРЯДОЧИЛ МАССИВ ТВС В ПОРЯДке ПОСЛЕДОВАТЕЛЬНОСТИ ЗАГРУЗКИ

            //            var query =
            //from tvs in MyCartogram.Zona
            //where tvs.LoadNumber == 1
            //select tvs;

            //            foreach (var item in query)
            //            {
            //                this.NextTvs = item.TVSnumber;
            //            }
            this.NextTvs = 0;// MyCartogram.Zona[2].TVSnumber; 
            this.totalInDuty = 0;

            label13.Text = "цель: " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString();



            timer1.Enabled = true;


            ///ЛОГГЕР ОШИБОЧНО ЗАГРУЖЕННЫХ ТВС
            ///

            





        }



        /// <summary>
        /// Нужно вызывать если ТВС загружена успешно
        /// </summary>
        private void updateFieldsIfYes()
        {
            



            string value = "10897.5";


            if (MyConst.InputBox("Повысотная отметка", "Пожалуйста введите повысотную отметку", ref value) == DialogResult.OK)
            {

                MyCartogram.Zona[NextTvs].VisotaOtmetka = value;
            }


            //DateTime myNow = DateTime.Now;
            DateTime MyTime = DateTime.Now;

            if (MyConst.InputBoxTime("Повысотная отметка", "Пожалуйста введите время загрузки ТВС", ref MyTime) == DialogResult.OK)
            {                
                MyCartogram.Zona[NextTvs].LoadTime = MyTime;         
            }
            
            // MyCartogram.SetTVSColor(mybrash,,checkBox1.Checked);

            ///так как мы закрасили нужно сверху перерисовать текст
           

           

            MyCartogram.Zona[NextTvs].IsLoaded = true;

            //MyCartogram.Zona[NextTvs].LoadTime = DateTime.Now;

            if (totalInZOne>0)
            {
                TimeSpan temp = MyCartogram.Zona[NextTvs].LoadTime - MyCartogram.Zona[lastTVS].LoadTime;

                float minut = temp.Hours * 60 + temp.Minutes + (float)temp.Seconds / (float)60;
                minut = (float)Math.Round(minut, 2);
                label11.Text = minut.ToString() + " минут";

                
                
                float ostalos = minut * (163 - totalInZOne)/(float)60;
                ostalos = (float)System.Math.Round(ostalos, 2);
                label12.Text = ostalos.ToString() + " часа";
                
            }

            totalInZOne++;
            totalInDuty++;
            this.label8.Text = totalInZOne.ToString();
            this.label9.Text = (163 - totalInZOne).ToString();
            this.label10.Text = totalInDuty.ToString();

            MyCartogram.Zona[NextTvs].Smena = this.smena;

            //  MyCartogram.Zona[NextTvs].VisotaOtmetka 

            ///Так как возможны дозагрузки раннее не загруженных ТВС
            this.lastTVS = NextTvs;
          

        }


        private void button1_Click_2(object sender, EventArgs e)
        {

           // MessageBox.Show(MyCartogram.Zona[1].Cord.X.ToString() + "-" + MyCartogram.Zona[10].Cord.Y.ToString());
            timer1.Enabled = false;

            if ((totalInZOne < 163) && (NextTvs<163))
            {
                               

                const string message =
         "ТВС Загружена без замечаний ?";
                const string caption = "Подтверждение";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.Yes)
                {
      
                    updateFieldsIfYes();

                    MyCartogram.SetTVSColor(NextTvs, true);
                    MyCartogram.Zona[NextTvs].Color = mybrash;
                    MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                    this.NextTvs++;
                    if (NextTvs<163)
                    {
                        label13.Text = "цель: " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString();    
                    }

                    if (NextTvs+1<164)
                    {
                        numericUpDown1.Value = NextTvs + 1;  
                    }

                } ///ТВС Загружена без замечаний
                else
                {

                    var isload = MessageBox.Show("Загружена ли ТВС? (ДА - загружена, НЕТ -  временно отложена)", caption,
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);

                    if (isload == DialogResult.Yes)
                    {
                        ///сюда будут писаться замечания если имеются
                        ///
                 


                        string value = ""; ;


                        if (MyConst.InputBoxZam("Введите замечание", "Пожалуйста введите причину замечания", ref value) == DialogResult.OK)
                        {
                            MyCartogram.Zona[NextTvs].Zamechanie = value;
                            MyCartogram.Zona[NextTvs].Redmark = true;

                        }

                        updateFieldsIfYes();
                        MyCartogram.SetTVSSolidColor(Brushes.Brown, NextTvs, false);
                        MyCartogram.Zona[NextTvs].Color = Brushes.Brown;
                        MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                        this.NextTvs++;

                        if (NextTvs < 163)
                        {
                            label13.Text = "цель: " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString();
                        }
                        if (NextTvs + 1 < 164)
                        {
                            numericUpDown1.Value = NextTvs + 1;
                        }
                      

                        //            var query =
                        //from tvs in MyCartogram.Zona
                        //where tvs.LoadNumber == numericUpDown1.Value
                        //select tvs;

                        //            foreach (var item in query)
                        //            {
                        //                this.NextTvs = item.TVSnumber;
                        //            }

                        
                    }
                    else
                    {
                        ///ТУТ ТЕКСТ ЕСЛИ ТВС ВООБЩЕ НЕ ЗАГРУЖЕНА
                        ///


                        MyCartogram.SetTVSSolidColor(Brushes.Red, NextTvs, false);

                        MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                        MyCartogram.Zona[NextTvs].IsLoaded = false;
                        MyCartogram.Zona[NextTvs].Color = Brushes.Red;
                        MyCartogram.Zona[NextTvs].Smena = this.smena;


                        string value = ""; ;


                        if (MyConst.InputBoxZam("Введите замечание", "Пожалуйста введите причину замечания", ref value) == DialogResult.OK)
                        {
                            MyCartogram.Zona[NextTvs].Zamechanie = value;
                            MyCartogram.Zona[NextTvs].Redmark = true;

                        }


                        listBox1.Items.Add((NextTvs + 1).ToString() + " в коор " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[NextTvs].Zamechanie);




                        StreamWriter errorLogger = new StreamWriter("LOGS/"+DateTime.Now.ToString().Replace(":","_").Replace(",","_")+ ".csv", false, Encoding.GetEncoding("Windows-1251"));
                        errorLogger.WriteLine((NextTvs + 1).ToString() + " в коор " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[NextTvs].Zamechanie + "в " + DateTime.Now.ToString() + "в смену ы" + this.smena);
                        errorLogger.Close();
                        //            var query =
                        //from tvs in MyCartogram.Zona
                        //where tvs.LoadNumber == numericUpDown1.Value
                        //select tvs;

                        //            foreach (var item in query)
                        //            {
                        //                this.NextTvs = item.TVSnumber;
                        //            }

                        this.NextTvs++;
                        if (NextTvs < 163)
                        {
                            label13.Text = "цель: " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString();
                        }
                        if (NextTvs + 1 < 164)
                        {
                            numericUpDown1.Value = NextTvs + 1;
                        }

                        timer1.Enabled = true;
                    }




                }

                /////////////////////////
                /// ОТЧЕТ ДЛЯ ОНЛАЙН ПРОРИСОВКИ

                dataGridView1.Rows.Add(NextTvs, MyCartogram.Zona[NextTvs - 1].TVSnumber, MyCartogram.Zona[NextTvs - 1].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs - 1].Cord.Y.ToString(), MyCartogram.Zona[NextTvs - 1].IsLoaded.ToString(), MyCartogram.Zona[NextTvs - 1].LoadTime, MyCartogram.Zona[NextTvs - 1].Smena, MyCartogram.Zona[NextTvs - 1].VisotaOtmetka, MyCartogram.Zona[NextTvs - 1].Zamechanie);

                if (MyCartogram.Zona[NextTvs - 1].IsLoaded)
                {
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[3].Style.BackColor = Color.LightGreen;
                }
                else dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Style.BackColor = Color.Red;
                





                /////////////////////////



            } //сколько всего в зоне 
            else
            { 

            if (totalInZOne == 163)
            {
                MessageBox.Show("Поздравляем, Вы загрузили все ТВС в активную зону");


            }
            else {

                MessageBox.Show("Некоторые ТВС быди ранне не загружены. Пожалуйста выберете их как только они будут готовы к загрузке");
            }
            }
            timer1.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                // button2.BackColor = colorDialog1.Color;
                mybrash = new SolidBrush(colorDialog1.Color);

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MyCartogram.RePaintAll(checkBox1.Checked);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (NextTvs<163)
            {



                if (z == 2)
                {
                    MyCartogram.GoalTVSByIndex(NextTvs);
                    //MyCartogram.SetTVSSolidColor(Brushes.Black,NextTvs, checkBox1.Checked);
                  label13.ForeColor = Color.Red;

                }
                else
                {
                    MyCartogram.SetTVSSolidColor(Brushes.White, NextTvs, true);
                    label13.ForeColor = Color.Blue;
                }


            MyCartogram.updateLoadNumberByTVSNumber(NextTvs);

            //  z = 3;
            z = 7 - z;
            } 

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message =
"Вы точно уверены, что хотите закрыть форму";
            const string caption = "Подтверждение";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                result = MessageBox.Show("Вы хорошо подумали?", "Еще раз подумайте",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {

                    e.Cancel = true;
                }
                else

                { 
                
               
                                //
                StreamWriter sw = new StreamWriter("crash" + DateTime.Now.ToString().Replace(':','_') + ".csv" , false, Encoding.GetEncoding("Windows-1251"));

                sw.WriteLine("ПОРЯДКОВЫЙ НОМЕР ЗАГРУЗКИ; НОМЕР ТВС; КООРДИНАТА X; КООРДИНАТА Y; СТАТУС;  ВРЕМЯ ЗАГРУЗКИ; СМЕНА; ПОВЫСОТНАЯ ОТМЕТКА; ЗАМЕЧАНИЕ ");


                foreach (oneTVS item in MyCartogram.Zona)
                {

                    sw.WriteLine(item.LoadNumber + ";"
                        + item.TVSnumber + ";" +
                          item.Cord.X + ";" + item.Cord.Y + ";" +
                         item.IsLoaded + ";" +
                         item.LoadTime.ToString() + ";" +

                         item.Smena + ";" +
                         item.VisotaOtmetka + ";" +
                         item.Zamechanie + ";");


                }


                sw.Close();

            }
                
                
                }


            }



      

        private void СдатьПринятьСмену_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Спасибо за дежурство, " + this.smena);

            if (MyConst.InputBoxZam("Внимание. Введите данные о смене", "Пожалуйста введите ФИО ДРИ", ref this.smena) == DialogResult.OK)
            {

                MessageBox.Show("Удачной Вам дежурства, " + this.smena);
                //MyCartogram.Zona[NextTvs].Zamechanie = value;

                label7.Text = this.smena;
                totalInDuty = 0;
                this.label10.Text = "0";
            }


        }

        private void печатьОтчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding("Windows-1251"));

                sw.WriteLine("ПОРЯДКОВЫЙ НОМЕР ЗАГРУЗКИ; НОМЕР ТВС; КООРДИНАТА X; КООРДИНАТА Y; СТАТУС;  ВРЕМЯ ЗАГРУЗКИ; СМЕНА; ПОВЫСОТНАЯ ОТМЕТКА; ЗАМЕЧАНИЕ ");


                foreach (oneTVS item in MyCartogram.Zona)
                {

                    sw.WriteLine(item.LoadNumber + ";"
                        + item.TVSnumber + ";" +
                          item.Cord.X + ";" + item.Cord.Y + ";" +
                         item.IsLoaded + ";" +
                         item.LoadTime.ToString() + ";" +

                         item.Smena + ";" +
                         item.VisotaOtmetka + ";" +
                         item.Zamechanie + ";");


                }


                sw.Close();

            }
}
        

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {


                int temp = NextTvs;
                
                string str = listBox1.Items[listBox1.SelectedIndex].ToString();
               // this.NextTvs = int.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());

                this.NextTvs = int.Parse(str.Split(' ')[0])-1;

                if (MessageBox.Show("Вы уверены, что хотите загрузить ТВС с порядковым номером загрузки " + (NextTvs+1).ToString() + ", которая не была загружена по причине " + MyCartogram.Zona[NextTvs].Zamechanie, "Пожалуйств подтвердите операцию", MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    button1_Click_2(sender, EventArgs.Empty);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }

                //button1_Click_2(this, this);
                NextTvs = temp;
                if (NextTvs + 1 < 164)
                {
                    numericUpDown1.Value = NextTvs + 1;
                }


            }
            else
            {
                if (listBox1.Items.Count > 0)
                {
                    MessageBox.Show("Выберите сперва ТВС по порядковому номеру загрузки, которая была раннее не загружена");
                }
                else {
                    MessageBox.Show("Отсутсвуют ТВС, которые можно было дозагрузить");
                }
            
            
            }
            button1.Focus();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //this.Size = myOldSize;
           // this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void скопироватьРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetImage(pictureBox1.Image);
                MessageBox.Show("Рисунок скопирован в буфер обмена. Откройте, например, Word и нажмите Ctrl+C");
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось скопировать рисунок в буффер обмена");
                throw;
            }
          
            
        }

   

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (tabControl1.SelectedIndex == 1)
            {
                //MessageBox.Show("rfgergjk");

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {

                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "False")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                    }
                    else
                   {
                       if (dataGridView1.Rows[i].Cells[7].Value.ToString() != "")
                       {
                           dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;

                       } else dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    
                    } 


                }



            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About myForm2 = new About();
            myForm2.ShowDialog();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process(); // Приложение, которое будем запускать 
            proc.StartInfo.FileName = "help.pdf"; 
            proc.Start(); 
        }



    }
}
