
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Моя_картограмма
{
    [Serializable]
    public partial class Form1 : Form
    {

        //   public List<double> VisOtmetok = new List<double>();
        List<SU> signalizatori;
        Bitmap myBitmap;
        Cartogram MyCartogram;
        Graphics g;
        Pen myPen;
        Color mybrash;
        String smena;





        //PictureBox pictureBox1;


        /// <summary>
        ///порядковый номер загрузки 
        /// </summary>
        public int NextTvs;

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
            this.signalizatori = new List<SU>();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

           // totalInZOne = DataGrInfo.MyRealTotalZone;
            // Bitmap OnePicture = new Bitmap("D:\\Загрузка ред 02\\Безымянный.png");
            //  ImageList imageList1 = new ImageList();
            // imageList1.Images.Add(OnePicture);
            button4.BackColor = Color.Transparent; //прозрачный цвет фона
            // button4.BackgroundImage = imageList1.Images[0];
            // button4.BackgroundImageLayout = ImageLayout.Center; //выравниваем её по центру
            button4.FlatStyle = FlatStyle.Flat;
            //  button4.Text = "АФП";
            button4.TextAlign = ContentAlignment.TopCenter; //позиция текста - внизу по центру
            button4.FlatAppearance.BorderSize = 0;//ширина рамки = 0
            // button4.TextImageRelation = TextImageRelation.ImageAboveText; //картинка над текстом
            button4.TabStop = false;//делаем так, что бы при потере фокуса, вокруг кнопки не оставалась черная рамка

            button5.BackColor = Color.Transparent; //прозрачный цвет фона                                                  
            button5.FlatStyle = FlatStyle.Flat;
            button5.TextAlign = ContentAlignment.TopCenter; //позиция текста - внизу по центру
            button5.FlatAppearance.BorderSize = 0;//ширина рамки = 0                                         
            button5.TabStop = false;//делаем так, что бы при потере фокуса, вокруг кнопки не оставалась черная рамка


            button6.BackColor = Color.Transparent; //прозрачный цвет фона                                                  
            button6.FlatStyle = FlatStyle.Flat;
            button6.TextAlign = ContentAlignment.TopCenter; //позиция текста - внизу по центру
            button6.FlatAppearance.BorderSize = 0;//ширина рамки = 0                                         
            button6.TabStop = false;//делае


            //  button2.BackColor = Color.Transparent; //прозрачный цвет фона
            // // button4.BackgroundImage = imageList1.Images[0];
            // // button4.BackgroundImageLayout = ImageLayout.Center; //выравниваем её по центру
            //  button2.FlatStyle = FlatStyle.Flat;
            ////  button4.Text = "АФП";
            ////  button4.TextAlign = ContentAlignment.TopCenter; //позиция текста - внизу по центру
            //  button2.FlatAppearance.BorderSize = 0;//ширина рамки = 0
            // // button4.TextImageRelation = TextImageRelation.ImageAboveText; //картинка над текстом
            //  button2.TabStop = false;
            ////  button2.BackColor = Color.Transparent;









            toolTip1.SetToolTip(button2, "Поворот по часовой");
            toolTip1.SetToolTip(button3, "Поворот против часовой");

            pictureBox1.Height = Math.Min(pictureBox1.Parent.Width, pictureBox1.Parent.Height);
            pictureBox1.Width = pictureBox1.Height;

            pictureBox1.Left = (pictureBox1.Parent.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = 0;

            //  pictureBox1.z

            //  button4.Left = (tableLayoutPanel3.Width - button4.Width) / 2;

            pictureBox1.Parent.BackColor = MyConst.MyBackground;
            pictureBox1.BackColor = MyConst.MyBackground;
            pictureBox2.BackColor = MyConst.MyBackground;

            tableLayoutPanel1.BackColor = MyConst.MyBackground;
            tableLayoutPanel2.BackColor = MyConst.MyBackground;
            tableLayoutPanel3.BackColor = MyConst.MyBackground;
            //tableLayoutPanel4.BackColor = MyConst.MyBackground;
            //tableLayoutPanel5.BackColor = MyConst.MyBackground;






            button1.BackColor = Color.CadetBlue;

            myBitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            pictureBox1.Image = myBitmap;
            g = Graphics.FromImage(myBitmap);

            myPen = new System.Drawing.Pen(Color.ForestGreen);
            myPen.Width = 2;
            MyCartogram = new Cartogram(g, pictureBox1, myPen);
            g.FillRectangle(Brushes.DimGray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);

            //LinearGradientBrush lgbfon = new LinearGradientBrush(
            //                    new Point(0, 0),
            //                    new Point(pictureBox1.Width, pictureBox1.Height),
            //                    Color.White,
            //                    Color.Silver);


            //g.FillRectangle(lgbfon, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);




            ///для бассейна
            ///
            //LinearGradientBrush lgb = new LinearGradientBrush(
            //                           new Point(0, 0),
            //                           new Point(1500, 0),
            //                           Color.White,
            //                           Color.Blue);


            //ЗАКРАСКА ФОНОМ БАССЕЙНА
            //     g.FillRectangle(lgb, (int)(0.25 * this.pictureBox1.Width), (int)(0.95 * this.pictureBox1.Height), (int)(0.5 * this.pictureBox1.Width), (int)(0.05 * this.pictureBox1.Height));


            //Font myF = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 14, FontStyle.Bold);
            //g.DrawString("БАССЕЙН", myF, Brushes.White, (int)(0.48 * this.pictureBox1.Width), (int)(0.97 * this.pictureBox1.Height));



            //   LinearGradientBrush lgbdri = new LinearGradientBrush(
            //                          new Point((int)(0.95 * this.pictureBox1.Width), 0),
            //                          new Point(this.pictureBox1.Width, 0),
            //                          Color.White,
            //                          Color.Chocolate);

            //   //СТОЛ ДРИ

            //   g.FillRectangle(lgbdri, (int)(0.95 * this.pictureBox1.Width)+1, (int)(0.1 * this.pictureBox1.Height), (int)(0.05 * this.pictureBox1.Width), (int)(0.20 * this.pictureBox1.Height));
            ////g.DrawString(`)
            //   Font myF1 = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 10, FontStyle.Bold);
            //   g.DrawString("ДРИ", myF1, Brushes.Black, (int)(0.95 * this.pictureBox1.Width), (int)(0.18 * this.pictureBox1.Height));

            // g.FillRectangle(Brushes.Blue, (int)0.25 * this.pictureBox1.Width, (int)0.9 * this.pictureBox1.Height, (int)0.5 * this.pictureBox1.Width, (int)0.1 * this.pictureBox1.Height);



            mybrash = Color.Chocolate;


            //упорядочили все ТВС в порядке очередности загрузки



            //MessageBox.Show(Application.StartupPath + "\\draw.ini");
            //MessageBox.Show(YYY.ToString());


            //MyCartogram.Show();
            //MyCartogram.UpdateLoadNumber(checkBox1.Checked);

            MyCartogram.CalcCord(pictureBox1.Width);

            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);




            //        //отсортированные все ТВС в колекции по порядковому номеру загрузки
            MyCartogram.Zona.Sort();

            //  MyCartogram.UpdateTVSNumber();
            // MyCartogram.DrawAllAxis();
            MyCartogram.DrawGrid(NextTvs);

            //MyCartogram.Zona.Sort();



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
            //this.totalInDuty = 0;

            //   label13.Text = "цель: " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString();









            //timer2.Enabled = true;



            //for (int i = 0; i < 20; i++)
            //{
            //    RotateRight();
            //    Thread.Sleep(100);
            //}


            //for (int i = 0; i < 20; i++)
            //{
            //    RotateLeft();
            //    Thread.Sleep(100);
            //}


            ///ЭТО ЧИСТО ДЛЯ ВЫБОРА ЯЧЕЙКИ ГДЕ БУДЕТ СИГНАЛИЗАТОР
            //comboBox1.Items.Clear();
            //foreach (Point item in MyConst.setka)
            //{
            //    comboBox1.Items.Add(item);
            //}
            //comboBox1.SelectedIndex = 0;

            RotateRight();
            /// RotateLeft();

            timer1.Enabled = true;




            if (File.Exists("currstateCartogram.bin"))
            {

                //string message =
                //    "Обнаружен файл с предысторией. Восстановить состяние (ДА) или начать новое (НЕТ) ?";
                //           const string caption = "Внимание!";
                //           var result = MessageBox.Show(message, caption,
                //                                        MessageBoxButtons.YesNo,
                //                                        MessageBoxIcon.Question);
                //           // If the no button was pressed ...
                //           if (result == DialogResult.Yes)
                //           {

                //               загрузитьПоследнееСостояниеToolStripMenuItem_Click(this, EventArgs.Empty);
                //           }

                if (MyConst.Prodolzhit() == DialogResult.OK)
                {
                    загрузитьПоследнееСостояниеToolStripMenuItem_Click(this, EventArgs.Empty);

                    int cc = 0;
                    foreach (oneTVS item in MyCartogram.Zona)
                    {
                        if (item.IsLoaded)
                        {
                            cc++;
                        }
                        // label7.Text = item.Smena.ToString();
                    }


                    totalInZOne = cc;
                    label8.Text = cc.ToString();
                    //   label10.Text = cc.ToString();
                    label9.Text = (163 - cc).ToString();

                    if ((NextTvs - 1) >= 0)
                    {
                        label7.Text = MyCartogram.Zona[NextTvs - 1].Smena.ToString();

                        //else
                        //{
                        //    label7.Text = "";
                        //}
                        //   label7.Text = MyCartogram.Zona[NextTvs - 1].Smena.ToString();



                        int uu = 0;
                        for (int i = 0; i < MyCartogram.Zona.Count; i++)
                        {
                            if (MyCartogram.Zona[i].Smena == MyCartogram.Zona[NextTvs - 1].Smena)
                            {
                                //    MyCartogram.Zona[NextTvs - 1].Smena = MyCartogram.Zona[i].Smena;
                                if (MyCartogram.Zona[i].IsLoaded)
                                {
                                    uu++;
                                }
                            }
                        }
                        //  MyCartogram.Zona[NextTvs].Smena = MyCartogram.Zona[NextTvs - 1].Smena;
                        totalInDuty = uu;
                        label10.Text = uu.ToString();
                        //  MyCartogram.Zona[NextTvs].Smena = uu.ToString();
                        //    label10.Text = totalInDuty.ToString();
                        // label7.Text = MyCartogram.Zona[]

                    }
                    else
                    {
                        label7.Text = "";
                    }
                }



            } //Существует файл с предысторией работы

            
        }



        /// <summary>
        /// Нужно вызывать если ТВС загружена успешно
        /// </summary>
        double value;

        private void updateFieldsIfYes()
        {
            for (int i = MyCartogram.Zona[NextTvs].LoadNumber; i < totalInZOne; i++)
            {

                value = MyCartogram.Zona[MyCartogram.Zona[NextTvs].LoadNumber].VisotaOtmetka;
            }

            try
            {
                if (MyConst.InputBox("Повысотная отметка", "Пожалуйста введите повысотную отметку", ref value) == DialogResult.OK)
                {
                    MyCartogram.Zona[NextTvs].VisotaOtmetka = value;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Необходимо числовое выражение повысотной отметки!!!");
                if (MyConst.InputBox("Повысотная отметка", "Пожалуйста введите повысотную отметку", ref value) == DialogResult.OK)
                {
                    MyCartogram.Zona[NextTvs].VisotaOtmetka = value;
                }
            }


            DateTime MyTime = DateTime.Now;

            if (MyConst.InputBoxTime("Повысотная отметка", "Пожалуйста введите время загрузки ТВС", ref MyTime) == DialogResult.OK)
            {
                MyCartogram.Zona[NextTvs].LoadTime = MyTime;
            }
            // MyCartogram.SetTVSColor(mybrash,,checkBox1.Checked);

            ///так как мы закрасили нужно сверху перерисовать текст




            MyCartogram.Zona[NextTvs].IsLoaded = true;

            //MyCartogram.Zona[NextTvs].LoadTime = DateTime.Now;

            if (totalInZOne > 0)
            {
                TimeSpan temp = MyCartogram.Zona[NextTvs].LoadTime - MyCartogram.Zona[lastTVS].LoadTime;

                float minut = temp.Hours * 60 + temp.Minutes + (float)temp.Seconds / (float)60;
                minut = (float)Math.Round(minut, 2);
                label11.Text = minut.ToString() + " минут";



                float ostalos = minut * (163 - totalInZOne) / (float)60;
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

        /// <summary>
        /// по нажатию на кнопку ЗАВЕРШИТЬ
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //      Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
        //exApp.Workbooks.Add();
        // Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
        List<int> Help = new List<int>();
        private void button1_Click_2(object sender, EventArgs e)
        {

         //  MessageBox.Show(DataGrInfo.totalinZonenew.ToString());

            // MessageBox.Show(MyCartogram.Zona[1].Cord.X.ToString() + "-" + MyCartogram.Zona[10].Cord.Y.ToString());
            timer1.Enabled = false;

            if ((totalInZOne < 163) && (NextTvs < 163))
            {
                const string ask = "ТВС загружена без замечаний ?";
               // const string ask1 = "Имитатор загружен без замечаний ?";
                string message = ask;
                //if ((totalInZOne <= 108) && (NextTvs <= 108))
                //{
                //    message = ask1;
                //}
                const string caption = "Подтверждение";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.Yes)
                {

                    updateFieldsIfYes();

                    //  ZamechaniyList.Add("Без замечаний");                
                    //if (NextTvs > 108)
                    //{
                    //    //MyCartogram.SetTVSColor(NextTvs, true);
                    //    MyCartogram.SetTVSSolidColor(Color.LightGreen, NextTvs, false);
                    //    MyCartogram.Zona[NextTvs].Color = Color.LightGreen;
                    //}
                    ///Конкретно закрашиваем сейчас
                    MyCartogram.SetTVSSolidColor(Color.LightGreen, NextTvs, false);
                    MyCartogram.Zona[NextTvs].Color = Color.LightGreen;


                    //if (NextTvs <= 108)
                    //{
                    //    MyCartogram.SetTVSSolidColor(Color.DarkGray, NextTvs, false);
                    //    MyCartogram.Zona[NextTvs].Color = Color.DarkGray;
                    //}


                    ///запоминаем для будущего если например восстанавливать после сбоя
                    //  MyCartogram.Zona[NextTvs].Color = mybrash;
                    // MyNextTVSlist.Add(NextTvs);


                    MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                    this.NextTvs++;


                    if (NextTvs + 1 < 164)
                    {
                        numericUpDown1.Value = NextTvs + 1;
                    }

                } ///ТВС Загружена без замечаний
                else
                {
                    string ask2 = "Загружена ли ТВС? (ДА - загружена, НЕТ -  временно отложена)";
                    string ask3 = "Загружен ли Имитатор? (ДА - загружен, НЕТ -  временно отложен)";

                    string message1 = ask2;

                    //if (NextTvs <= 109)
                    //{
                    //    message1 = ask3;
                    //}

                    var isload = MessageBox.Show(message1, caption,
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
                            //  ZamechaniyList.Add(MyCartogram.Zona[NextTvs].Zamechanie);
                            //                    List<string> ZamechaniyList = new List<string>();


                            // MyCartogram.Zona[NextTvs].Redmark = true;

                        }
                        updateFieldsIfYes();

                        //if (NextTvs > 109)
                        //{
                        //    MyCartogram.SetTVSSolidColor(Color.Brown, NextTvs, false);
                        //    MyCartogram.Zona[NextTvs].Color = Color.Brown;
                        //}
                        MyCartogram.SetTVSSolidColor(Color.Brown, NextTvs, false);
                        MyCartogram.Zona[NextTvs].Color = Color.Brown;
                        // updateFieldsIfYes();

                        //if (NextTvs <= 109)
                        //{
                        //    MyCartogram.SetTVSSolidColor(Color.Gray, NextTvs, false);
                        //    MyCartogram.Zona[NextTvs].Color = Color.Gray;
                        //}



                        MyCartogram.UpdateLoadNumber(checkBox1.Checked);

                        this.NextTvs++;
                        // VisOtmetok.Add(MyCartogram.Zona[NextTvs].VisotaOtmetka);
                        //  MyTimeList.Add(MyCartogram.Zona[NextTvs].LoadTime);
                        //  SmenList.Add(MyCartogram.Zona[NextTvs].Smena);
                        //MyNextTVSlist.Add(NextTvs);


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


                        //if (NextTvs > 109)
                        //{
                        //    MyCartogram.SetTVSSolidColor(Color.Red, NextTvs, false);
                        //    MyCartogram.Zona[NextTvs].Color = Color.Red;
                        //}
                        MyCartogram.SetTVSSolidColor(Color.Red, NextTvs, false);
                        //MyCartogram.SetTVSSolidColor(Color.Red, NextTvs, false);
                        MyCartogram.Zona[NextTvs].Color = Color.Red;

                        //if (NextTvs <= 109)
                        //{
                        //    MyCartogram.SetTVSSolidColor(Color.LightGray, NextTvs, false);
                        //    MyCartogram.Zona[NextTvs].Color = Color.LightGray;
                        //}

                        MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                        MyCartogram.Zona[NextTvs].IsLoaded = false;
                        //    MyCartogram.Zona[NextTvs].Color = Color.Red;
                        MyCartogram.Zona[NextTvs].Smena = this.smena;


                        string value = ""; ;

                        DateTime MyTime = DateTime.Now;
                        if (MyConst.InputBoxZam("Введите замечание", "Пожалуйста введите причину замечания", ref value) == DialogResult.OK)
                        {
                            MyCartogram.Zona[NextTvs].Zamechanie = value;
                            MyCartogram.Zona[NextTvs].Redmark = true;
                            MyCartogram.Zona[NextTvs].LoadTime = MyTime;
                        }
                        listBox1.Items.Add((NextTvs + 1).ToString() + " в коор " + MyCartogram.Zona[NextTvs].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[NextTvs].Zamechanie);

                        StreamWriter errorLogger = new StreamWriter("LOGS/" + DateTime.Now.ToString().Replace(":", "_").Replace(",", "_") + ".csv", false, Encoding.GetEncoding("Windows-1251"));
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
                        // MyNextTVSlist.Add(NextTvs);
                        // MyTimeList.Add(MyCartogram.Zona[NextTvs].LoadTime);

                        if (NextTvs + 1 < 164)
                        {
                            numericUpDown1.Value = NextTvs + 1;
                        }

                        timer1.Enabled = true;

                    }
                    //   MessageBox.Show(totalInZOne.ToString() + "" + NextTvs.ToString());

                }

                /////////////////////////
                /// ОТЧЕТ ДЛЯ ОНЛАЙН ПРОРИСОВКИ

                //dataGridView1.Rows.Add(NextTvs, MyCartogram.Zona[NextTvs - 1].TVSnumber, MyCartogram.Zona[NextTvs - 1].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs - 1].Cord.Y.ToString(), MyCartogram.Zona[NextTvs - 1].IsLoaded.ToString(), MyCartogram.Zona[NextTvs - 1].LoadTime, MyCartogram.Zona[NextTvs - 1].Smena, MyCartogram.Zona[NextTvs - 1].VisotaOtmetka, MyCartogram.Zona[NextTvs - 1].Zamechanie);

                //if (MyCartogram.Zona[NextTvs - 1].IsLoaded)
                //{
                //    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[3].Style.BackColor = Color.LightGreen;
                //}
                //else dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Style.BackColor = Color.Red;
                /////////////////////////
                //сколько всего в зоне 
              //  Help.Add(totalInZOne);

             //   Cartogram.SaveTotalInZone(totalInZOne);
              //Cartogram.SaveTotalInZone(totalInZOne);
            }
               
            else
            {
                if (totalInZOne == 163)
                {
                    MessageBox.Show("Поздравляем, Вы загрузили все ТВС в активную зону");
                }

                else
                {
                    //MessageBox.Show(totalInZOne.ToString());
                    MessageBox.Show("Некоторые ТВС быди ранне не загружены. Пожалуйста выберете их как только они будут готовы к загрузке");
                }
            }
           // MessageBox.Show(totalInZOne.ToString() + " " + NextTvs.ToString());
            timer1.Enabled = true;
            //   g.FillRectangle(Brushes.Gray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);
            //   MyCartogram.RePaintAll(checkBox1.Checked);
            MyCartogram.DrawGrid(NextTvs);
            ///проверим, не лежит ли сигнализатор уровня в ячейке куда собрались грузить
            CheckSUandGoal();

            ReDrawSU();
            //if(NextTvs!=0 && totalInZOne ==0)
            //{
            //    totalInZOne = 
            //}
        }


        private void CheckSUandGoal()
        {
            foreach (SU item in this.signalizatori)
            {
                if (item.NativeCord == MyCartogram.Zona[NextTvs].Cord)
                {
                    MessageBox.Show("Внимание убери сигнализатор");
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (NextTvs < 163)
            {

                if (z == 2)
                {
                    MyCartogram.GoalTVSByIndex(NextTvs);
                    //MyCartogram.SetTVSSolidColor(Brushes.Black,NextTvs, checkBox1.Checked);

                }
                else
                {
                    MyCartogram.SetTVSSolidColor(Color.White, NextTvs, true);
                }


                MyCartogram.updateLoadNumberByTVSNumber(NextTvs);

                //  z = 3;
                z = 7 - z;
            }



            ///Нужно отмечать месторасположение пробоотборников линейным градиентом, который плавно меняется
            ///
            //LinearGradientBrush lgb = new LinearGradientBrush(this._fuelRect, Color.Blue, Color.LightBlue, (float)0);




            //if (this.currentColor ==255)
            //{
            //    k = -5;               
            //}
            //if (this.currentColor ==130) 
            //{
            //    k = 5;
            //}

            //MyCartogram.SetTVSSolidColorBy360Number(new SolidBrush(Color.FromArgb(255, this.currentColor, this.currentColor)), 5, true);
            //MyCartogram.SetTVSSolidColorBy360Number(new SolidBrush(Color.FromArgb(255, this.currentColor, this.currentColor)), 157, true);

            //this.currentColor = this.currentColor + k;     


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


                    сохранитьСостояниеToolStripMenuItem_Click(this, EventArgs.Empty);




                    //
                    StreamWriter sw = new StreamWriter("LOGS/crash_" + DateTime.Now.ToString().Replace(':', '_') + ".csv", false, Encoding.GetEncoding("Windows-1251"));

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

                MessageBox.Show("Удачного Вам дежурства, " + this.smena);
                //MyCartogram.Zona[NextTvs].Zamechanie = value;
                this.smena = 
                label7.Text = this.smena;
                totalInDuty = 0;
                this.label10.Text = "0";
            }


        }

        private void печатьОтчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void скопироватьРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }



        //private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //    if (tabControl1.SelectedIndex == 1)
        //    {
        //        //MessageBox.Show("rfgergjk");

        //        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
        //        {

        //            if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "False")
        //            {
        //                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;

        //            }
        //            else
        //           {
        //               if (dataGridView1.Rows[i].Cells[7].Value.ToString() != "")
        //               {
        //                   dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;

        //               } else dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;

        //            } 


        //        }



        //    }
        //}

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About myForm2 = new About();
            myForm2.ShowDialog();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process(); // Приложение, которое будем запускать 
            proc.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "/HELP/help.pdf";
            proc.Start();
        }





        private void RotateRight()
        {
            timer1.Enabled = false;
            //  g.FillRectangle(Brushes.DimGray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);
            MyCartogram.RotateCartogram(1);
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            // MyCartogram.DrawAllAxis();
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            SetNewSuCord();
            ReDrawSU();
            timer1.Enabled = true;
        }


        private void RotateLeft()
        {
            timer1.Enabled = false;
            //     g.FillRectangle(Brushes.DimGray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);
            MyCartogram.RotateCartogram(2);
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            //  MyCartogram.DrawAllAxis();
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            SetNewSuCord();
            ReDrawSU();
            timer1.Enabled = true;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Height = Math.Min(pictureBox1.Parent.Height, pictureBox1.Parent.Width);
            pictureBox1.Width = pictureBox1.Height;
            pictureBox1.Left = (pictureBox1.Parent.Height - pictureBox1.Width) / 2;
            pictureBox1.Top = 0;
            if (g != null)
            {
                g.FillRectangle(Brushes.Gray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);
                int currentstate = MyCartogram.ScreanAngle1;
                ///изначально рисуем и расчитывает координаты в нулевой фазе
                MyCartogram.CalcCord(pictureBox1.Width);
                MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);


                MyCartogram.ScreanAngle1 = 0;
                if (currentstate != 0)
                {
                    int k = 0;
                    while (k != currentstate)
                    {
                        RotateRight();
                        k++;
                    }
                }
                else
                {
                    MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
                    if (checkBox4.Checked)
                    {
                        MyCartogram.DrawGrid(this.NextTvs);
                    }

                    SetNewSuCord();
                    ReDrawSU();
                }





            }



        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void повернутьНа90ПротивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void повернутьНа90ПоЧасовойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void копироватьРисВБуферToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void дозагрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {


                int temp = NextTvs;

                string str = listBox1.Items[listBox1.SelectedIndex].ToString();
                // this.NextTvs = int.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());

                this.NextTvs = int.Parse(str.Split(' ')[0]) - 1;

                if (MessageBox.Show("Вы уверены, что хотите загрузить ТВС с порядковым номером загрузки " + (NextTvs + 1).ToString() + ", которая не была загружена по причине " + MyCartogram.Zona[NextTvs].Zamechanie, "Пожалуйств подтвердите операцию", MessageBoxButtons.YesNo,
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
                else
                {
                    MessageBox.Show("Отсутсвуют ТВС, которые можно было дозагрузить");
                }


            }
            button1.Focus();
        }

        private void сохранитьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

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


        private void SetNewSuCord()
        {
            foreach (SU item in this.signalizatori)
            {
                item.setCord(MyCartogram.getTVSByPointXY(item.NativeCord).CanvasCord, MyCartogram.getFstep());
            }

        }



        private void ReDrawSU()
        {

            foreach (SU item in this.signalizatori)
            {
                item.ReDraw(this.g);
            }

        }

        /// <summary>
        /// Кнопка по которой происходит добавление валидации на форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void button4_Click(object sender, EventArgs e)
        //{
        //               /// думаю уже тут необходима валидация
        //   /// 

        //    this.listBox2.Items.Add(comboBox1.SelectedItem);


        //    this.signalizatori.Add(new SU(MyCartogram.getTVSByPointXY((Point)comboBox1.SelectedItem).CanvasCord, MyCartogram.getFA(), MyCartogram.getFstep(), (Point)comboBox1.SelectedItem));


        //   this.signalizatori[this.signalizatori.Count - 1].ReDraw(this.g);


        //   tabControl1.SelectedIndex = 0;
        //}


        ////Кнопка по которой происхожит удаление  сигнализаторв с картограммы
        //private void button5_Click(object sender, EventArgs e)
        //{

        //    int tempint = listBox2.SelectedIndex;
        //    listBox2.Items.RemoveAt(tempint);

        //    this.signalizatori.RemoveAt(tempint);

        //    timer1.Enabled = false;
        //    g.FillRectangle(Brushes.Gray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);

        //    MyCartogram.RePaintAll(checkBox1.Checked);
        //    MyCartogram.DrawAllAxis();
        //    MyCartogram.DrawGrid(NextTvs);


        //    SetNewSuCord();
        //    ReDrawSU();

        //    timer1.Enabled = true;
        //    tabControl1.SelectedIndex = 0;

        //}

        private void сохранитьСостояниеToolStripMenuItem_Click(object sender, EventArgs e)
        {


            BinaryFormatter binFormat = new BinaryFormatter();

            Stream fStream = new FileStream("currstateCartogram.bin", FileMode.Create, FileAccess.Write, FileShare.None);


            this.MyCartogram.MynextTVS = NextTvs;

            binFormat.Serialize(fStream, this.MyCartogram);
            // binFormat.Serialize()
            fStream.Close();

        }

        private void загрузитьПоследнееСостояниеToolStripMenuItem_Click(object sender, EventArgs e)
        {




            BinaryFormatter binFormat = new BinaryFormatter();

            Stream fStream = File.OpenRead("currstateCartogram.bin");

            //       JamesBondClass carFromDisk =
            //(JamesBondClass)binFormat.Deserialize(fStream);

            this.MyCartogram = (Cartogram)binFormat.Deserialize(fStream);


            fStream.Close();

            this.g.Clear(Color.DimGray);

            NextTvs = this.MyCartogram.MynextTVS;

            
                this.MyCartogram.InitComponentsAfterDeserialize(this.g, this.pictureBox1, this.myPen);

            
          

            this.MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            // MyCartogram.DrawAllAxis();
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            SetNewSuCord();
            ReDrawSU();
        }

        private void грПоЧасовойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateRight();

        }

        private void грПротивЧасойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void оживитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            // MyCartogram.DrawAllAxis();
            ReDrawSU();
            pictureBox1.Refresh();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            // MyCartogram.DrawAllAxis();
            ReDrawSU();
            pictureBox1.Refresh();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }

            //  MyCartogram.DrawAllAxis();
            ReDrawSU();
            pictureBox1.Refresh();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);

            if (checkBox4.Checked)
            {
                MyCartogram.DrawGrid(this.NextTvs);
            }


            //  MyCartogram.DrawAllAxis();
            ReDrawSU();
            pictureBox1.Refresh();
        }

        private void информацияПоЗагрузкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrInfo f = new DataGrInfo();
            DataTable VibotTochki = new DataTable();

            VibotTochki.Columns.Add("Порядковый номер загрузки");
            VibotTochki.Columns.Add("Номер ТВС");
            VibotTochki.Columns.Add("Повысотная отметка");
            VibotTochki.Columns.Add("Время");
            VibotTochki.Columns.Add("Смена");
            VibotTochki.Columns.Add("Подробности");
            VibotTochki.Columns.Add("Замечание (True-Есть/False-Нет)");
            VibotTochki.Columns.Add("Загрузка ТВС (True-Есть/False-Нет)");

            f.dataGridView1.DataSource = VibotTochki;
           

            foreach (oneTVS item in MyCartogram.Zona)
            {
                if (item.Smena == "")
                {
                    item.Smena = label7.Text;
                }
                VibotTochki.Rows.Add(item.LoadNumber, item.TVSnumber, item.VisotaOtmetka, item.LoadTime.ToString(), item.Smena, item.Zamechanie, item.Redmark.ToString(), item.IsLoaded.ToString());
              
                if (VibotTochki.Rows.Count == NextTvs) break;
            }

            f.Owner = this;
            f.ShowDialog();
        }

        private void сохранитьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Bitmap AFP =  new Bitmap("IMG/AFP.png");

            System.Diagnostics.Process.Start(Application.StartupPath + "\\IMG\\AFP.png");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\DI-SKP.png");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\IMG\\SKP.png");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\IMG\\DI-SKP.png");
        }

        private void изменитьИнформациюПоЗагрузкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int value = 0;
            double povisotka = 0;
            DateTime mynewtime = DateTime.Now;
            string smenachange = "";
            string podrobnostichange = "";
            string parol = "";
            string myParol = "Шаталин";

            try
            {
                if (MyConst.InputBoxZam("Пароль", "Введите пароль:", ref parol) == DialogResult.OK)
                {
                    if (parol == myParol)
                    {
                        
                        if (MyConst.InputBoxZamNew("Изменение информации", "Введите порядковый номер загрузки", ref value) == DialogResult.OK)
                        {
                            if (value <= NextTvs)
                            {
                                string message = "Изменить повысотную отметку?";
                                string message1 = "Изменить время?";
                                string message2 = "Изменить смену?";                               
                                string message3 = "Изменить замечание?";
                                string message4 = "Загружена ли?";
        
                                const string caption = "Подтверждение";
                                var result4 = MessageBox.Show(message4, caption,
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);

                                if (result4 == DialogResult.Yes)
                                {

                                    MyCartogram.Zona[value - 1].IsLoaded = true;

                                    if (value > 109)
                                    {
                                        MyCartogram.SetTVSSolidColor(Color.LightGreen, value - 1, false);
                                        MyCartogram.Zona[value - 1].Color = Color.LightGreen;
                                        totalInZOne++;
                                        if (HelpF.textBox1.Text != "")
                                        {
                                            HelpF.textBox1.Text = (int.Parse(HelpF.textBox1.Text) + 1).ToString();
                                        }
                                        label8.Text = totalInZOne.ToString();
                                        label9.Text = (163 - totalInZOne).ToString();
                                       // label10.Text = (int.Parse(HelpF.textBox1.Text) + 1).ToString();
                                        listBox1.Items.Clear();
                                    }
                                    else
                                    {
                                        MyCartogram.SetTVSSolidColor(Color.DarkGray, value - 1, false);
                                        MyCartogram.Zona[value - 1].Color = Color.DarkGray;
                                        totalInZOne++;
                                        if (HelpF.textBox3.Text != "")
                                        {
                                            HelpF.textBox3.Text = (int.Parse(HelpF.textBox3.Text) + 1).ToString();
                                        }
                                        label8.Text = totalInZOne.ToString();
                                        label9.Text = (163 - totalInZOne).ToString();
                                        listBox1.Items.Clear();
                                    //    listBox1.Items.Add(((value - 1) + 1).ToString() + " в коор " + MyCartogram.Zona[(value - 1)].Cord.X.ToString() + "-" + MyCartogram.Zona[(value - 1)].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[(value - 1)].Zamechanie);
                                    }
                                    totalInZOne++;

                                    var result = MessageBox.Show(message, caption,
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        if (MyConst.InputBoxZamNew("Изменение повысотной отметки", "Введите повысотную отметку", ref povisotka) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].VisotaOtmetka = povisotka;
                                        }
                                    }

                                    var result1 = MessageBox.Show(message1, caption,
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);

                                    if (result1 == DialogResult.Yes)
                                    {
                                        if (MyConst.InputBoxTime("Изменение времени", "Введите время", ref mynewtime) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].LoadTime = mynewtime;
                                        }
                                    }
                                    var result2 = MessageBox.Show(message2, caption,
                                                                 MessageBoxButtons.YesNo,
                                                                 MessageBoxIcon.Question);

                                    if (result2 == DialogResult.Yes)
                                    {
                                        if (MyConst.InputBoxZam("Изменение смены", "Введите смену", ref smenachange) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].Smena = smenachange;
                                        }
                                    }

                                    var result3 = MessageBox.Show(message3, caption,
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question);
                                    MyCartogram.Zona[value - 1].Redmark = true;
                                    if (result3 == DialogResult.Yes)
                                    {
                                        MyCartogram.Zona[value - 1].Redmark = true;
                                 
                                        if (MyConst.InputBoxZam("Изменение замечания", "Введите замечание", ref podrobnostichange) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].Zamechanie = podrobnostichange;
                                            if (MyCartogram.Zona[value - 1].Zamechanie!=null)
                                            {

                                                if (value > 109)
                                                 {
                                                     MyCartogram.SetTVSSolidColor(Color.Brown, value - 1, false);
                                                     MyCartogram.Zona[value - 1].Color = Color.Brown;
                                                 }
                                                 else
                                                 {
                                                     MyCartogram.SetTVSSolidColor(Color.Gray, value - 1, false);
                                                     MyCartogram.Zona[value - 1].Color = Color.Gray;
                                                 }
                                            }
                                        }
                                    }
                                    if (result3 == DialogResult.No)
                                    {
                                        MyCartogram.Zona[value - 1].Redmark = false;

                                        MyCartogram.Zona[value - 1].Zamechanie = null;

                                    }

                                }
                                if (result4 == DialogResult.No)
                                {
                             
                                    MyCartogram.Zona[value - 1].IsLoaded = false;

                                    if (value > 109)
                                    {
                                        MyCartogram.SetTVSSolidColor(Color.Red, value - 1, false);
                                        MyCartogram.Zona[value - 1].Color = Color.Red;
                                        totalInZOne--;
                                        if (HelpF.textBox2.Text != "")
                                        {
                                            HelpF.textBox2.Text = (int.Parse(HelpF.textBox2.Text) - 1).ToString();
                                        }
                                        label8.Text = totalInZOne.ToString();
                                        label9.Text = (163 - totalInZOne).ToString();
                                        listBox1.Items.Add(((value - 1) + 1).ToString() + " в коор " + MyCartogram.Zona[(value - 1)].Cord.X.ToString() + "-" + MyCartogram.Zona[(value - 1)].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[(value - 1)].Zamechanie);

                                    }
                                    else
                                    {
                                        MyCartogram.SetTVSSolidColor(Color.LightGray, value - 1, false);
                                        MyCartogram.Zona[value - 1].Color = Color.LightGray;
                                        totalInZOne--;
                                        if (HelpF.textBox4.Text != "")
                                        {
                                            HelpF.textBox4.Text = (int.Parse(HelpF.textBox4.Text) - 1).ToString();
                                        }
                                        label8.Text = totalInZOne.ToString();
                                        label9.Text = (163 - totalInZOne).ToString();
                                        listBox1.Items.Add(((value - 1) + 1).ToString() + " в коор " + MyCartogram.Zona[(value - 1)].Cord.X.ToString() + "-" + MyCartogram.Zona[(value - 1)].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[(value - 1)].Zamechanie);
                                    }

                                    var result1 = MessageBox.Show(message1, caption,
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);
                                    if (result1 == DialogResult.Yes)
                                    {
                                        if (MyConst.InputBoxTime("Изменение времени", "Введите время", ref mynewtime) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].LoadTime = mynewtime;
                                        }
                                    }

                                    var result2 = MessageBox.Show(message2, caption,
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

                                    if (result2 == DialogResult.Yes)
                                    {
                                        if (MyConst.InputBoxZam("Изменение смены", "Введите смену", ref smenachange) == DialogResult.OK)
                                        {
                                            MyCartogram.Zona[value - 1].Smena = smenachange;
                                        }
                                    }                                     
                                            MyCartogram.Zona[value - 1].Redmark = true;
                                            if (MyConst.InputBoxZam("Изменение замечания", "Введите замечание", ref podrobnostichange) == DialogResult.OK)
                                            {
                                                MyCartogram.Zona[value - 1].Zamechanie = podrobnostichange;
                                                //totalInZOne--;
                                                //HelpF.textBox4.Text = (int.Parse(HelpF.textBox4.Text) - 1).ToString();
                                                //label8.Text = totalInZOne.ToString();                              
                                                //label9.Text = (163 - totalInZOne).ToString();
                                                //listBox1.Items.Add(((value - 1) + 1).ToString() + " в коор " + MyCartogram.Zona[(value - 1)].Cord.X.ToString() + "-" + MyCartogram.Zona[(value - 1)].Cord.Y.ToString() + " из-за " + MyCartogram.Zona[(value - 1)].Zamechanie);
                                   
                                            }
                                        
                                      
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не загружен(а)!!! Будьте внимательней!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль!");
                    }

                }
          
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }

            if(NextTvs == 163)
            {
                MyCartogram.RePaintAll(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
             
                    MyCartogram.DrawGrid(this.NextTvs);
              

                // MyCartogram.DrawAllAxis();
                ReDrawSU();
                pictureBox1.Refresh();
            }
           
        }

        private void извлечьИммитаторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            
                const string ask = "Выгрузить 108-ую ИТВС ?";
               // const string ask1 = "Выгрузить 109 ИТВС ?";
                string message = ask;
               // string message1 = ask1;

                const string caption = "Подтверждение";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.Yes)
                {

                    

                        MyCartogram.SetTVSSolidColor(Color.Yellow, 107, false);
                        MyCartogram.Zona[108].Color = Color.Yellow;
                    
            


                    MyCartogram.UpdateLoadNumber(checkBox1.Checked);
                 //   this.NextTvs++;


                    //if (NextTvs + 1 < 164)
                    //{
                    //    numericUpDown1.Value = NextTvs + 1;
                    //}

                } ///ТВС Загружена без замечаний
                



                
                /////////////////////////
                /// ОТЧЕТ ДЛЯ ОНЛАЙН ПРОРИСОВКИ

                //dataGridView1.Rows.Add(NextTvs, MyCartogram.Zona[NextTvs - 1].TVSnumber, MyCartogram.Zona[NextTvs - 1].Cord.X.ToString() + "-" + MyCartogram.Zona[NextTvs - 1].Cord.Y.ToString(), MyCartogram.Zona[NextTvs - 1].IsLoaded.ToString(), MyCartogram.Zona[NextTvs - 1].LoadTime, MyCartogram.Zona[NextTvs - 1].Smena, MyCartogram.Zona[NextTvs - 1].VisotaOtmetka, MyCartogram.Zona[NextTvs - 1].Zamechanie);

                //if (MyCartogram.Zona[NextTvs - 1].IsLoaded)
                //{
                //    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[3].Style.BackColor = Color.LightGreen;
                //}
                //else dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Style.BackColor = Color.Red;
                /////////////////////////
        
            timer1.Enabled = true;
            //   g.FillRectangle(Brushes.Gray, 0, 0, this.pictureBox1.Width, this.pictureBox1.Width);
            //   MyCartogram.RePaintAll(checkBox1.Checked);
            MyCartogram.DrawGrid(NextTvs);
            ///проверим, не лежит ли сигнализатор уровня в ячейке куда собрались грузить
            CheckSUandGoal();

            ReDrawSU();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\IMG\\Инструкция по цветам.png");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        Загруженность_зоны HelpF = new Загруженность_зоны();
        private void button8_Click(object sender, EventArgs e)
        {
           // Загруженность_зоны HelpF = new Загруженность_зоны();

            HelpF.label1.Text = "ТВС загружено";
            HelpF.label2.Text = "ТВС не загружено";
            HelpF.label3.Text = "ИТВС загружено";
            HelpF.label4.Text = "ИТВС не загружено";


            HelpF.textBox1.Text = 0.ToString();
            HelpF.textBox2.Text = 0.ToString();
           
                int y1 = 0;
                int y2 = 0;

                for (int i = 109; i < MyCartogram.Zona.Count; i++)
                {
                    if (MyCartogram.Zona[i].IsLoaded)
                    {
                        y1++;
                    }
                    else
                    {
                        y2++;
                    }
                }
                HelpF.textBox1.Text = y1.ToString();
                HelpF.textBox2.Text = y2.ToString();
            
       
           
                int y3 = 0;
                int y4 = 0;

                for (int i = 0; i < 109; i++)
                {
                    if (MyCartogram.Zona[i].IsLoaded)
                    {
                        y3++;
                    }
                    else
                    {
                        y4++;
                    }
                }
                HelpF.textBox3.Text = y3.ToString();
                HelpF.textBox4.Text = y4.ToString();
            
    
            HelpF.Owner = this;
            HelpF.ShowDialog();
        }
    }
}
