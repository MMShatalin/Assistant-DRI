using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Моя_картограмма
{
    class MyConst
    {
        /// <summary>
        /// Цвета СКП каналов последовательно от 1 до 6
        /// </summary>
        public static Brush SKP1Brush = Brushes.Orange;
        public static Brush SKP2Brush = Brushes.LightGreen;
        public static Brush SKP3Brush = Brushes.DeepSkyBlue;
        public static Brush SKP4Brush = Brushes.Yellow;
        public static Brush SKP5Brush = Brushes.Violet;
        public static Brush SKP6Brush = Brushes.MediumSeaGreen;

        public static Brush AFP16Brush = Brushes.DeepSkyBlue;
        public static Brush AFP8Brush = Brushes.LightGreen;

        public static Brush DI2Brush = Brushes.Orange;
        public static Brush DI6Brush = Brushes.DeepSkyBlue;
        public static Brush DI12Brush = Brushes.Violet;
        public static Brush DI4Brush = Brushes.LightGreen;
        public static Brush DI10Brush = Brushes.Yellow;
        public static Brush DI14Brush = Brushes.MediumSeaGreen;


        public static int FontSizeInTVS = 8;
        public static Brush FontColorInTVS = Brushes.DarkGreen;

        public static int FontTechAxis = 16;

        /// <summary>
        /// Размер датчика ДИ
        /// </summary>
        public static int DIsize = 14;


        /// <summary>
        /// размер кружочка под СКП
        /// </summary>
        public static int SKPsize = 10;


        /// <summary>
        /// Размер датчиков АФП
        /// </summary>
        public static int AFPsize = 14;
        /// <summary>
        /// чем больше это число, тем меньше сама картограмма получиться
        /// </summary>
        public static int CartogramSize = 31;

        public static int MyAxisFontGoalSize = 12;
        public static int MyAxisFontSize=12;

        /// <summary>
        /// Цвет фона всего пространства
        /// </summary>
        public static Color MyBackground = Color.WhiteSmoke;
        public static Brush MyBackgroundBrush = Brushes.WhiteSmoke;

        //        Cart:CartogramVector =

        // (x : 14; y : 21),
        // (x : 14; y : 23),
        // (x : 14; y : 25),
        // (x : 14; y : 27),
        // (x : 14; y : 29),
        // (x : 14; y : 31),
        // (x : 14; y : 33),
        // (x : 14; y : 35),
        // (x : 14; y : 37),
        // (x : 13; y : 20),
        // (x : 13; y : 22),
        // (x : 13; y : 24),
        // (x : 13; y : 26),
        // (x : 13; y : 28),
        // (x : 13; y : 30),
        // (x : 13; y : 32),
        // (x : 13; y : 34),
        // (x : 13; y : 36),
        // (x : 13; y : 38),
        // (x : 12; y : 19),
        // (x : 12; y : 21),
        // (x : 12; y : 23),
        // (x : 12; y : 25),
        // (x : 12; y : 27),
        // (x : 12; y : 29),
        // (x : 12; y : 31),
        // (x : 12; y : 33),
        // (x : 12; y : 35),
        // (x : 12; y : 37),
        // (x : 12; y : 39),
        // (x : 11; y : 18),
        // (x : 11; y : 20),
        // (x : 11; y : 22),
        // (x : 11; y : 24),
        // (x : 11; y : 26),
        // (x : 11; y : 28),
        // (x : 11; y : 30),
        // (x : 11; y : 32),
        // (x : 11; y : 34),
        // (x : 11; y : 36),
        // (x : 11; y : 38),
        // (x : 11; y : 40),
        // (x : 10; y : 17),
        // (x : 10; y : 19),
        // (x : 10; y : 21),
        // (x : 10; y : 23),
        // (x : 10; y : 25),
        // (x : 10; y : 27),
        // (x : 10; y : 29),
        // (x : 10; y : 31),
        // (x : 10; y : 33),
        // (x : 10; y : 35),
        // (x : 10; y : 37),
        // (x : 10; y : 39),
        // (x : 10; y : 41),
        // (x : 09; y : 16),
        // (x : 09; y : 18),
        // (x : 09; y : 20),
        // (x : 09; y : 22),
        // (x : 09; y : 24),
        // (x : 09; y : 26),
        // (x : 09; y : 28),
        // (x : 09; y : 30),
        // (x : 09; y : 32),
        // (x : 09; y : 34),
        // (x : 09; y : 36),
        // (x : 09; y : 38),
        // (x : 09; y : 40),
        // (x : 09; y : 42),
        // (x : 08; y : 17),
        // (x : 08; y : 19),
        // (x : 08; y : 21),
        // (x : 08; y : 23),
        // (x : 08; y : 25),
        // (x : 08; y : 27),
        // (x : 08; y : 29),
        // (x : 08; y : 31),
        // (x : 08; y : 33),
        // (x : 08; y : 35),
        // (x : 08; y : 37),
        // (x : 08; y : 39),
        // (x : 08; y : 41),


        public static DialogResult Prodolzhit()
        {
            Form form = new Form();
            //Label label = new Label();

            
            // TextBox textBox = new TextBox();
         


            Button buttonOk = new Button();
            Button buttonNo = new Button();

            form.Text = "Внимание";
           //label.Text = promptText;
            // textBox.Text = value;

            buttonOk.Text = "Продолжить старую загрузку";
            buttonNo.Text = "Начать новую загрузку";
            buttonOk.DialogResult = DialogResult.OK;
            buttonNo.DialogResult = DialogResult.Cancel;

            
            buttonOk.SetBounds(10,10, 200, 30);
            buttonNo.SetBounds(240, 10, 200, 30);

          //  label.AutoSize = true;
            //    textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(450, 50);
            form.Controls.AddRange(new Control[] { buttonOk, buttonNo });
            form.ClientSize = new Size(450, form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonNo;

            DialogResult dialogResult = form.ShowDialog();            

            return dialogResult;
        }

        /// <summary>
        /// Это окно отображается когда необходимо ввести время при удачно загруженной ТВС
        /// </summary>
        /// <param name="title">Заголовок Окна</param>
        /// <param name="promptText">Текст внутри</param>
        /// <param name="value">Значение </param>
        /// <returns></returns>
        public static DialogResult InputBoxTime(string title, string promptText, ref DateTime value)
        {
            Form form = new Form();
            Label label = new Label();

            DateTimePicker dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Time;


            // TextBox textBox = new TextBox();
            dtp.Value = value;


            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            // textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
          //  textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
        //    textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, dtp, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = dtp.Value;

            return dialogResult;
        }




        /// <summary>
        /// Это окно отображается когда необходимо ввести повысотную отметку при удачно загруженной ТВС
        /// </summary>
        /// <param name="title">Заголовок Окна</param>
        /// <param name="promptText">Текст внутри</param>
        /// <param name="value">Значение </param>
        /// <returns></returns>
        /// 
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != ',' || (((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","))) && e.KeyChar != (char)Keys.Back && (e.KeyChar != '-' || ((TextBox)sender).SelectionStart != 0 || (((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-")));
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show("Не удалось вставить содержимое буфера обмена");
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref double value)
        {
         
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();

               
               
                textBox.Text = value.ToString();

                  
                

                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                label.Text = promptText;
                // textBox.Text = value;

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                DialogResult dialogResult = form.ShowDialog();
                value = Convert.ToDouble(textBox.Text.Replace('.', ','));
                return dialogResult;
            
            
    }


        /// <summary>
        /// Это окно отображается когда ТВС загружена но есть замечания, которые нужно ввести или вввести обозначения смены
        /// </summary>
        /// <param name="title">заголовк окна</param>
        /// <param name="promptText">текст</param>
        /// <param name="value">текст замечвания</param>
        /// <returns></returns>
        public static DialogResult InputBoxZam(string title, string promptText, ref string value)
        {
            Form form = new Form();

            //NumericUpDown nm = new NumericUpDown();

            //nm.Minimum = 11000;
            //nm.Maximum = 13000;

           // nm.Value = 11751;

            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }



        /// <summary>
        /// ЭТОТ МАССИВ ИСПОЛЬЗУЕТСЯ ТОЛЬКО ДЛЯ РИСОВАНИЯ КООРДИНАТНОЙ СЕТКИ ПО ГРАНИЧНЫМ ТВСкам
        /// </summary>
        public static Point[] setka = {
                                      new Point(15,24),
                                      new Point(15,26),
                                      new Point(15,28),
                                      new Point(15,30),
                                      new Point(15,32),
                                      new Point(15,34),

                                       //((x : 15; y : 24),
 // (x : 15; y : 26),
 // (x : 15; y : 28),
 // (x : 15; y : 30),
 // (x : 15; y : 32),
 // (x : 15; y : 34),


                                      new Point(14,21),
                                      new Point(14,23),
                                      new Point(14,25),
                                      new Point(14,27),
                                      new Point(14,29),
                                      new Point(14,31),
                                      new Point(14,33),
                                      new Point(14,35),
                                      new Point(14,37),

                                      new Point(13,20),
                                      new Point(13,22),
                                      new Point(13,24),
                                      new Point(13,26),
                                      new Point(13,28),
                                      new Point(13,30),
                                      new Point(13,32),
                                      new Point(13,34),
                                      new Point(13,36),
                                      new Point(13,38),

                                      new Point(12,19),
                                      new Point(12,21),
                                      new Point(12,23),
                                      new Point(12,25),
                                      new Point(12,27),
                                      new Point(12,29),
                                      new Point(12,31),
                                      new Point(12,33),
                                      new Point(12,35),
                                      new Point(12,37),
                                      new Point(12,39),

                                      new Point(11,18),
                                      new Point(11,20),
                                      new Point(11,22),
                                      new Point(11,24),
                                      new Point(11,26),
                                      new Point(11,28),
                                      new Point(11,30),
                                      new Point(11,32),
                                      new Point(11,34),
                                      new Point(11,36),
                                      new Point(11,38),
                                      new Point(11,40),

                                      new Point(10,17),
                                      new Point(10,19),
                                      new Point(10,21),
                                      new Point(10,23),
                                      new Point(10,25),
                                      new Point(10,27),
                                      new Point(10,29),
                                      new Point(10,31),
                                      new Point(10,33),
                                      new Point(10,35),
                                      new Point(10,37),
                                      new Point(10,39),
                                      new Point(10,41),

                                      new Point(09,16),
                                      new Point(09,18),
                                      new Point(09,20),
                                      new Point(09,22),
                                      new Point(09,24),
                                      new Point(09,26),
                                      new Point(09,28),
                                      new Point(09,30),
                                      new Point(09,32),
                                      new Point(09,34),
                                      new Point(09,36),
                                      new Point(09,38),
                                      new Point(09,40),
                                      new Point(09,42),

                                      new Point(08,17),
                                      new Point(08,19),
                                      new Point(08,21),
                                      new Point(08,23),
                                      new Point(08,25),
                                      new Point(08,27),
                                      new Point(08,29),
                                      new Point(08,31),
                                      new Point(08,33),
                                      new Point(08,35),
                                      new Point(08,37),
                                      new Point(08,39),
                                      new Point(08,41),

                                      new Point(07,16),
                                      new Point(07,18),
                                      new Point(07,20),
                                      new Point(07,22),
                                      new Point(07,24),
                                      new Point(07,26),
                                      new Point(07,28),
                                      new Point(07,30),
                                      new Point(07,32),
                                      new Point(07,34),
                                      new Point(07,36),
                                      new Point(07,38),
                                      new Point(07,40),
                                      new Point(07,42),
                                       // (x : 07; y : 16),
 // (x : 07; y : 18),
 // (x : 07; y : 20),
 // (x : 07; y : 22),
 // (x : 07; y : 24),
 // (x : 07; y : 26),
 // (x : 07; y : 28),
 // (x : 07; y : 30),
 // (x : 07; y : 32),
 // (x : 07; y : 34),
 // (x : 07; y : 36),
 // (x : 07; y : 38),
 // (x : 07; y : 40),
 // (x : 07; y : 42),

                                      new Point(06,17),
                                      new Point(06,19),
                                      new Point(06,21),
                                      new Point(06,23),
                                      new Point(06,25),
                                      new Point(06,27),
                                      new Point(06,29),
                                      new Point(06,31),
                                      new Point(06,33),
                                      new Point(06,35),
                                      new Point(06,37),
                                      new Point(06,39),
                                      new Point(06,41),
 // (x : 06; y : 17),
 // (x : 06; y : 19),
 // (x : 06; y : 21),
 // (x : 06; y : 23),
 // (x : 06; y : 25),
 // (x : 06; y : 27),
 // (x : 06; y : 29),
 // (x : 06; y : 31),
 // (x : 06; y : 33),
 // (x : 06; y : 35),
 // (x : 06; y : 37),
 // (x : 06; y : 39),
 // (x : 06; y : 41),






                                      new Point(05,18),
                                      new Point(05,20),
                                      new Point(05,22),
                                      new Point(05,24),
                                      new Point(05,26),
                                      new Point(05,28),
                                      new Point(05,30),
                                      new Point(05,32),
                                      new Point(05,34),
                                      new Point(05,36),
                                      new Point(05,38),
                                      new Point(05,40),



                                       // (x : 05; y : 18),
 // (x : 05; y : 20),
 // (x : 05; y : 22),
 // (x : 05; y : 24),
 // (x : 05; y : 26),
 // (x : 05; y : 28),
 // (x : 05; y : 30),
 // (x : 05; y : 32),
 // (x : 05; y : 34),
 // (x : 05; y : 36),
 // (x : 05; y : 38),
 // (x : 05; y : 40),

                                      new Point(04,19),
                                      new Point(04,21),
                                      new Point(04,23),
                                      new Point(04,25),
                                      new Point(04,27),
                                      new Point(04,29),
                                      new Point(04,31),
                                      new Point(04,33),
                                      new Point(04,35),
                                      new Point(04,37),
                                      new Point(04,39),
                                  
                                       // (x : 04; y : 19),
 // (x : 04; y : 21),
 // (x : 04; y : 23),
 // (x : 04; y : 25),
 // (x : 04; y : 27),
 // (x : 04; y : 29),
 // (x : 04; y : 31),
 // (x : 04; y : 33),
 // (x : 04; y : 35),
 // (x : 04; y : 37),
 // (x : 04; y : 39),



                                      new Point(03,20),
                                      new Point(03,22),
                                      new Point(03,24),
                                      new Point(03,26),
                                      new Point(03,28),
                                      new Point(03,30),
                                      new Point(03,32),
                                      new Point(03,34),
                                      new Point(03,36),
                                      new Point(03,38),
                                       // (x : 03; y : 20),
 // (x : 03; y : 22),
 // (x : 03; y : 24),
 // (x : 03; y : 26),
 // (x : 03; y : 28),
 // (x : 03; y : 30),
 // (x : 03; y : 32),
 // (x : 03; y : 34),
 // (x : 03; y : 36),
 // (x : 03; y : 38),

                                      new Point(02, 21),
                                      new Point(02, 23),
                                      new Point(02, 25),
                                      new Point(02, 27),
                                      new Point(02, 29),
                                      new Point(02, 31),
                                      new Point(02, 33),
                                      new Point(02, 35),
                                      new Point(02, 37),
                                       // (x : 02; y : 21),
 // (x : 02; y : 23),
 // (x : 02; y : 25),
 // (x : 02; y : 27),
 // (x : 02; y : 29),
 // (x : 02; y : 31),
 // (x : 02; y : 33),
 // (x : 02; y : 35),
 // (x : 02; y : 37),

                                      new Point(01, 24),
                                      new Point(01, 26),
                                      new Point(01, 28),
                                      new Point(01, 30),
                                      new Point(01, 32),
                                      new Point(01, 34),

                                       // (x : 01; y : 24),
 // (x : 01; y : 26),
 // (x : 01; y : 28),
 // (x : 01; y : 30),
 // (x : 01; y : 32),
 // (x : 01; y : 34)) ;
     };
    }
}
