using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Моя_картограмма
{
    /// <summary>
    /// Класс описывающий одну ТВС на картограмме
    /// </summary>
    class oneTVS : IComparable
    {

        /// <summary>
        /// Условное обозначение смены в которую была загружена ТВС
        /// </summary>
        private string _smena;

        public string Smena
        {
            get { return _smena; }
            set { _smena = value; }
        }

        /// <summary>
        /// Информация, введенная пользователем о повысотной отметки
        /// </summary>
        private string _VisotaOtmetka;

        public string VisotaOtmetka
        {
            get { return _VisotaOtmetka; }
            set { _VisotaOtmetka = value; }
        }

        /// <summary>
        /// флаг символизирующий загруженна ТВС или нет
        /// </summary>
        private bool _isLoaded;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set { _isLoaded = value; }
        }

        /// <summary>
        /// Булевская метка о том что было замечание при загрузке топлива у кажой ТВС
        /// </summary>
        private bool _redmark;

        public bool Redmark
        {
            get { return _redmark; }
            set { _redmark = value; }
        }


        private string _zamechanie;
        /// <summary>
        /// Замечание возникшее при загрузке кассеты
        /// </summary>
        public string Zamechanie
        {
            get { return _zamechanie; }
            set { _zamechanie = value; }
        }




        /// <summary>
        /// Порядковый номер ТВС от 1...163, иногда нужен для цикла форич
        /// </summary>
        private int _TVSnumber;

        public int TVSnumber
        {
            get { return _TVSnumber; }
            set { _TVSnumber = value; }
        }

        /// <summary>
        /// Цвет закраски твс
        /// </summary>
        private Brush color;

        public Brush Color
        {
            get { return color; }
            set { this.color = value; }

        }

        private DateTime loadTime;

        /// <summary>
        /// Порядковый номер загрузки от 1 до 163
        /// </summary>
        private int _loadNumber;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            oneTVS other = obj as oneTVS;
            if (other != null)
                return this._loadNumber.CompareTo(other._loadNumber);
            else
                throw new ArgumentException("Object is not a Temperature");
        }



        /// <summary>
        /// порпядковый номер загрузки в зону
        /// </summary>
        public int LoadNumber
        {
            get { return this._loadNumber; }
            set { this._loadNumber = value; }
        }

        public DateTime LoadTime
        {
            get { return loadTime; }
            set { loadTime = value; }
        }



        /// <summary>
        /// Кордината ТВС, например 15-24
        /// </summary>
        private Point _cord;// = new Point();

        /// <summary>
        /// Координата центра ТВС на конве
        /// </summary>
        private Point _canvasCord;

        //так как будет вращение нужно гле то хранить исходные координаты. чтобы разрабатывать 3 правила а не комбинации
        private Point _inititial_canvasCord;

        /// <summary>
        /// тут будут такие поля как текст, цвет, но самое главное коордлинаты 6 углов
        /// </summary>

        ////тут лежат координаты 6 углов
        private Point[] _Hex; //= new Point[5];

        //так как будет вращение нужно гле то хранить исходные координаты. чтобы разрабатывать 3 правила а не комбинации
        private Point[] _inititial_Hex;

        /// <summary>
        /// Конструктор
        /// </summary>
        public oneTVS()
        {
            this._cord = new Point();

            this._canvasCord = new Point();
            this._inititial_canvasCord = new Point();

            this._Hex = new Point[6];
            this._inititial_Hex = new Point[6];

            this._redmark = false; //по умолчанию все ТВС загружены без замечания
            this._zamechanie = "";


        }

        public Point CanvasCord
        {
            get { return _canvasCord; }
            set { _canvasCord = value; }
        }


        public Point initialCanvasCord
        {
            get { return _inititial_canvasCord; }
            set { _inititial_canvasCord = value; }
        }


        public Point[] Hex
        {
            get { return _Hex; }
            set { _Hex = value; }
        }

        public Point[] initialHex
        {
            get { return _inititial_Hex; }
            set { _inititial_Hex = value; }
        }

        public Point Cord
        {
            get { return _cord; }
            set { _cord = value; }
        }




    }


    class Cartogram
    {




        Graphics g;

        PictureBox pic;
        Pen myPen;

        LinearGradientBrush linGrBrush;

        Pen MyGoalPen;

        List<oneTVS> _zona;

        
        List<int>[] faza = new List<int>[4];

        int ScreanAngle;

        public int ScreanAngle1
        {
            get { return ScreanAngle; }
            set { ScreanAngle = value; }
        }

        public List<oneTVS> Zona
        {
            get { return _zona; }
            set { _zona = value; }
        }

        private int FA;

        public int getFA()
        {
            return this.FA;
        }

        private int Fstep;

        public int getFstep()
        {
            return this.Fstep;
        }

        private int Fcenter;

        /// <summary>
        /// Изменяет все координаты в зависмости от поворота пользоватея. Тоесть из initialCanvasCord и initialHex, перещитывается в просто CanvasCord и Hex
        /// </summary>
        /// <param name="rezhim">1  - 90 по часовой, 2 - 90 градусов пртив</param>
        public void RotateCartogram(int rezhim)
        {

            ///Итак правило при повороте на 90 градусов по часовой стрелке следующие
            /// Пуская для квадрата L
            /// У нас есть точка с координатами a и b
            /// после поворота новые координы этой точки будут соответвенно (L-b,a)
            /// 

            ///Поэтому пройдемся по всем имеющимся объектам и исправим их координаты
            ///Во-первых это центры всех ТВС
            ///

            switch (rezhim)
            {
                case 1:
                    for (int i = 0; i < this._zona.Count; i++)
                    {
                        this._zona[i].CanvasCord = new Point(this.pic.Width - this._zona[i].CanvasCord.Y, this._zona[i].CanvasCord.X);

                        for (int k = 0; k < 6; k++)
                        {
                            this._zona[i].Hex[k] = new Point(this.pic.Width - this._zona[i].Hex[k].Y, this._zona[i].Hex[k].X);
                        }
                    }


                    if (this.ScreanAngle == 3)
                    {
                        this.ScreanAngle = 0;
                    }
                    else this.ScreanAngle++;

                    //this.X0 

                    break;

                case 2:
                    for (int i = 0; i < this._zona.Count; i++)
                    {
                        this._zona[i].CanvasCord = new Point(this._zona[i].CanvasCord.Y, this.pic.Width -this._zona[i].CanvasCord.X);

                        for (int k = 0; k < 6; k++)
                        {
                            this._zona[i].Hex[k] = new Point(this._zona[i].Hex[k].Y, this.pic.Width - this._zona[i].Hex[k].X);
                        }
                    }

                    if (this.ScreanAngle ==0)
                    {
                        this.ScreanAngle = 3;
                    }
                    else this.ScreanAngle--;

                    break;


                default:
                    break;
            }








        }

         

        /// <summary>
        /// Функция для нарисовки координатной линии сетки
        /// </summary>
        public void DrawGrid(int goal)
        {


            oneTVS temp = new oneTVS();


            oneTVS goalTvs = new oneTVS();
            goalTvs = this.Zona[goal];
            //int[] a = new int[4];

            


                //= { {0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 }, { 0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 } };

           // int[] tempY = { 0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 };

            //int i = 15;
            int val = new int();

            Pen myDefaultPen = new Pen(Brushes.White);
           

            myDefaultPen.DashStyle = DashStyle.Dot;
            myDefaultPen.Width = 1;

            Font myDefaultFont = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 14, FontStyle.Regular);

            foreach (int item in faza[this.ScreanAngle])
            {
                temp = this.getTVSByTVS360Number(item);

               
                g.DrawLine(myDefaultPen, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);


                switch (this.ScreanAngle)
                {
                    case 0:
                        val = temp.Cord.X;
                        break;
                    case 1:
                        val = temp.Cord.Y;
                        break;
                    case  2:
                        val = temp.Cord.X;
                        break;
                    case 3:
                        val = temp.Cord.Y;
                        break;

                    default:
                        break;
                }




                if ((val == goalTvs.Cord.X) || (val == goalTvs.Cord.Y))
                {

                    if (val < 10)
                    {
                        g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Red, 0, temp.CanvasCord.Y - 14);
                    }
                    else
                        g.DrawString(val.ToString(), myDefaultFont, Brushes.Red, 0, temp.CanvasCord.Y - 14);


                }
                else
                {
                    if (val < 10)
                    {
                        g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.WhiteSmoke, 0, temp.CanvasCord.Y - 14);
                    }
                    else
                        g.DrawString(val.ToString(), myDefaultFont, Brushes.WhiteSmoke, 0, temp.CanvasCord.Y - 14);
                }
            }



            ///Для прорисовки верхний координат
            ///
            int fazV = this.ScreanAngle - 1;
            if (fazV<0)
            {
                fazV = 3;
            }

            foreach (int item in faza[fazV])
            {
                temp = this.getTVSByTVS360Number(item);

                g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);

                //g.DrawLine(myDefaultPen, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);


                switch (fazV)
                {
                    case 0:
                        val = temp.Cord.X;
                        break;
                    case 1:
                        val = temp.Cord.Y;
                        break;
                    case 2:
                        val = temp.Cord.X;
                        break;
                    case 3:
                        val = temp.Cord.Y;
                        break;

                    default:
                        break;
                }


                if ((val == goalTvs.Cord.X) || (val == goalTvs.Cord.Y))
                {
                    if (val < 10)
                    {
                        g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X - 14, 0);
                    }
                    else
                        g.DrawString(val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X - 14, 0);
                }
                else
                {
                    if (val < 10)
                    {
                        g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X - 14, 0);
                    }
                    else
                        g.DrawString(val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X - 14, 0);

                }
            }


            /////Для прорисовки вертикальи пройдемся по всем пятым точкам
            /////
            //int[] tempX1 = { 102, 115, 127, 138, 148, 149, 157, 156, 158, 159, 160, 161, 162, 156 };

            //foreach (int item in tempX1)
            //{
            //    temp = this.getTVSByTVS360Number(item);
            //    g.DrawLine(myDefaultPen, temp.Hex[1].X, temp.Hex[1].Y, temp.Hex[1].X, 40);


            //}


            //int[] tempX0 = { 148, 157, 158, 159, 160, 161, 162, 156 };

            //foreach (int item in tempX0)
            //{
            //    temp = this.getTVSByTVS360Number(item);
            //    g.DrawLine(myDefaultPen, temp.Hex[0].X, temp.Hex[0].Y, temp.Hex[0].X, 40);
            //}


            //int[] tempX5 = { 162, 156, 147, 137, 126, 114 };

            //foreach (int item in tempX5)
            //{
            //    temp = this.getTVSByTVS360Number(item);
            //    g.DrawLine(myDefaultPen, temp.Hex[5].X, temp.Hex[5].Y, temp.Hex[5].X, 40);
            //}






            /////ПОдпишим циферки оси X
            /////
            //int stepX = this.getTVSByTVS360Number(115).Hex[1].X - this.getTVSByTVS360Number(102).Hex[1].X;
            //int startX = this.getTVSByTVS360Number(102).Hex[1].X - 20;
            //for (int ii = 16; ii < 43; ii++)
            //{
            //    if (ii % 2 == 0)
            //    {
            //        g.DrawString(ii.ToString(), myDefaultFont, Brushes.WhiteSmoke, startX,0);
            //    }



            //    startX += stepX;
            //}

   

            pic.Refresh();
        }


        /// <summary>
        /// Основная функция проприсовки картограммы. Должна запускаться один раз при запуске Программы раз так как CalcCord вносит и первоначальные данные в два места
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mypic"></param>
        /// <param name="myPen"></param>
        public void Show()
        {

            this.CalcCord(this.pic.Width);

            foreach (oneTVS item in this.Zona)
            {

                this.g.FillPolygon(Brushes.White, item.Hex);

                this.g.DrawPolygon(this.myPen, item.Hex);
            }

            pic.Refresh();

        }

        /// <summary>
        /// предлагаю все закрасить и нарисовать заново
        /// Например когда включить выключить первые 60 
        /// </summary>
        public void RePaintAll(bool isFirst60)
        {
            for (int i = 0; i < this.Zona.Count; i++)
            {

                this.SetTVSSolidColor(Brushes.White, i, false);


                if (this.Zona[i].IsLoaded)
                {



                    if (!this.Zona[i].Redmark)
                    {
                        this.SetTVSColor(i, false);
                    }
                    else
                    {
                        this.SetTVSSolidColor(this.Zona[i].Color, i, false);

                    };

                }
                else
                {
                    if (this.Zona[i].Redmark)
                    {
                        this.SetTVSSolidColor(this.Zona[i].Color, i, false);
                    }
                };

            }

       this.UpdateLoadNumber(isFirst60);

        }



        /// <summary>
        /// Закрашивает выбранную ТВС определенным цветом
        /// </summary>
        /// <param name="brush">кисточка со цветом</param>
        /// <param name="tvsnumber">номер твс</param>
        /// <param name="first60">обновить надписи, первые 60 или нет</param>
        ///  
        public void SetTVSColor(int tvsnumber, bool refresh)
        {

            this.g.FillPolygon(this.linGrBrush, this.Zona[tvsnumber].Hex);

            g.DrawPolygon(this.myPen, this.Zona[tvsnumber].Hex);
            // this.Show();
            //   this.UpdateLoadNumber(first60);
            if (refresh) this.pic.Refresh();
        }



        /// <summary>
        /// Закрашивает выбранную ТВС определенным цветом
        /// </summary>
        /// <param name="brush">кисточка со цветом</param>
        /// <param name="tvsnumber">номер твс</param>
        /// <param name="first60">обновить надписи, первые 60 или нет</param>
        public void SetTVSSolidColor(Brush brush, int tvsnumber, bool refresh)
        {

            this.g.FillPolygon(brush, this.Zona[tvsnumber].Hex);

            g.DrawPolygon(this.myPen, this.Zona[tvsnumber].Hex);
            // this.Show();
            //   this.UpdateLoadNumber(first60);
            if (refresh) this.pic.Refresh();
        }

        public void GoalTVSByIndex(int tvsindex)
        {


            g.DrawEllipse(this.MyGoalPen, new Rectangle(this.Zona[tvsindex].CanvasCord.X - (int)(0.8 * this.FA), this.Zona[tvsindex].CanvasCord.Y - (int)(0.8 * this.FA), (int)(1.6 * this.FA), (int)(1.6 * this.FA)));

            g.DrawLine(this.MyGoalPen, this.Zona[tvsindex].CanvasCord.X - (int)(0.8 * this.FA), this.Zona[tvsindex].CanvasCord.Y, this.Zona[tvsindex].CanvasCord.X + (int)(0.8 * this.FA), this.Zona[tvsindex].CanvasCord.Y);
            // g.DrawLine(this.MyGoalPen, this.Zona[tvsindex].CanvasCord.X - (int)(1 * this.FA), this.Zona[tvsindex].CanvasCord.Y, this.Zona[tvsindex].CanvasCord.X + (int)(1 * this.FA), this.Zona[tvsindex].CanvasCord.Y);
            g.DrawLine(this.MyGoalPen, this.Zona[tvsindex].CanvasCord.X, this.Zona[tvsindex].CanvasCord.Y - (int)(0.8 * this.FA), this.Zona[tvsindex].CanvasCord.X, this.Zona[tvsindex].CanvasCord.Y + (int)(0.8 * this.FA));

            this.pic.Refresh();
        }


        /// <summary>
        /// Обновляет надпись у одной твс по ее номеру
        /// </summary>
        /// <param name="tvsnumber"></param>
        public void updateLoadNumberByTVSNumber(int tvsnumber)
        {
            Font myF = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 10, FontStyle.Bold);

            this.g.DrawString(this.Zona[tvsnumber].LoadNumber.ToString(), myF, Brushes.Black, this.Zona[tvsnumber].CanvasCord.X - this.FA / 2, this.Zona[tvsnumber].CanvasCord.Y - this.FA / 2);

            this.pic.Refresh();
        }

        /// <summary>
        /// Рисует текст внутри ТВС. В данном случае номер ТВС
        /// </summary>

        public void UpdateTVSNumber()
        {
            Font myF = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 10, FontStyle.Bold);

            foreach (oneTVS item in this.Zona)
            {
                this.g.DrawString(item.TVSnumber.ToString(), myF, Brushes.Black, item.CanvasCord.X - this.FA / 2, item.CanvasCord.Y - this.FA / 2);

            }
            this.pic.Refresh();
        }

        /// <summary>
        /// Рисует текст внутри ТВС. В данном случае порядковый номер загрузки
        /// </summary>
        /// <param name="first60"></param>
        public void UpdateLoadNumber(bool first60)
        {
            Font myF = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 10, FontStyle.Bold);

            foreach (oneTVS item in this.Zona)
            {
                if (!first60)
                {
                    this.g.DrawString(item.LoadNumber.ToString(), myF, Brushes.Black, item.CanvasCord.X - this.FA / 2, item.CanvasCord.Y - this.FA / 2);
                }
                else
                {

                    if (item.LoadNumber < 61)
                    {
                        this.g.DrawString(item.LoadNumber.ToString(), myF, Brushes.Black, item.CanvasCord.X - this.FA / 2, item.CanvasCord.Y - this.FA / 2);
                    }

                }

            }
            this.pic.Refresh();
        }

        /// <summary>
        /// Для расчета 6 координат
        /// </summary>
        /// <param name="SideLength">Длина одной стороны</param>
        public void CalcCord(int SideLength)
        {



            //IniFile ini = new IniFile(Application.StartupPath + "\\draw.ini");

            //int SideL = ini.ReadInt("Cord", "SIDE", 28);         //("main", "Info", "Тест");

            ////Это грубо говоря координаты центра картограммы
            //int XXX = ini.ReadInt("Cord", "XXX", 29);
            //int YYY = ini.ReadInt("Cord", "YYY", 8);




            //      this.FA = (int)(SideLength / SideL);

            ////Math.Pi/180 для перевода градусов в радианы
            //    this.Fstep = (int)(this.FA * Math.Cos(30 * Math.PI / 180));


            this.Fstep = (int)(SideLength / 31); //так как максимум 14 твс по ширине а fstep это половинка одной, но прибавим еще 2 ТВС на подписи и все такое

            if (!(this.Fstep%2==0))
            {
                this.Fstep++;
            }

            this.FA = (int)(this.Fstep / Math.Cos(30 * Math.PI / 180));

            if (!(this.FA % 2 == 0))
            {
                this.FA++;
            }

            this.Fcenter = (int)(SideLength / 2);

            for (int i = 0; i < _zona.Count; i++)
            {

                int X = this.Fcenter + (int)((_zona[i].Cord.Y - 29) * this.Fstep); // Так как центр кортограммы имееть координату 08-29
                int Y = this.Fcenter + (int)((_zona[i].Cord.X - 8) * this.FA * 1.5);

                _zona[i].CanvasCord = new Point(X, Y);
               // _zona[i].initialCanvasCord = new Point(X, Y);

                _zona[i].Hex[0] = new Point(X, Y - this.FA);
                _zona[i].Hex[1] = new Point(X - this.Fstep, Y - (int)(0.5 * this.FA));
                _zona[i].Hex[2] = new Point(X - this.Fstep, Y + (int)(0.5 * this.FA));
                _zona[i].Hex[3] = new Point(X, Y + this.FA);
                _zona[i].Hex[4] = new Point(X + this.Fstep, Y + (int)(0.5 * this.FA));
                _zona[i].Hex[5] = new Point(X + this.Fstep, Y - (int)(0.5 * this.FA));

                //_zona[i].initialHex[0] = new Point(X, Y - this.FA);
                //_zona[i].initialHex[1] = new Point(X - this.Fstep, Y - (int)(0.5 * this.FA));
                //_zona[i].initialHex[2] = new Point(X - this.Fstep, Y + (int)(0.5 * this.FA));
                //_zona[i].initialHex[3] = new Point(X, Y + this.FA);
                //_zona[i].initialHex[4] = new Point(X + this.Fstep, Y + (int)(0.5 * this.FA));
                //_zona[i].initialHex[5] = new Point(X + this.Fstep, Y - (int)(0.5 * this.FA));
                               

            }

        }

        private void WriteString(string p1, string p2, string S)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Процедура возвращает ТВС по ее номеру в 360 градусной симметрии
        /// </summary>
        /// <param name="TVSNumber"></param>
        /// <returns></returns>
        public oneTVS getTVSByTVS360Number(int TVSNumber)
        {

            foreach (oneTVS item in this.Zona)
            {
                if (item.TVSnumber == TVSNumber)
                {
                    return item;
                }

            }
            return null;

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Cartogram(Graphics g, PictureBox mypic, Pen pen)
        {
            // this._zona = new oneTVS[MyConst.setka.Length];
            this._zona = new List<oneTVS>();

            //for (int i = 0; i < 163; i++)
            //{
            // this._zona.Add(new oneTVS());
            //}

            this.FA = new int();
            this.Fstep = new int();
            this.Fcenter = new int();
            this.myPen = pen;

            this.MyGoalPen = new Pen(Color.Red);
            //this.MyGoalPen.Color = Color.DarkRed;
            this.MyGoalPen.Width = 10;
            this.g = g;
            this.pic = mypic;

            this.ScreanAngle = 0;

            //for (int i = 0; i < 3; i++)
            //{
            //    //faza[i] = new List<int>();
            //}

            faza[0] = new List<int>{0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157};
            faza[1] = new List<int> { 74,60,47,35,24,14,13,5,4,3,2,1,0,7,6,15,25,36,48,61,23,12,11,10,9,8,16};
            faza[2] = new List<int> { 162,156,147,137,126,114,101,87,74,60,47,35,24,14,5};
            faza[3] = new List<int> { 101,114,126,137,147,156,146,155,162,154,161,153,160,152,159,151,158,150,157,149,139,148,138,127,115,102,88 };

            linGrBrush = new LinearGradientBrush(
             new Point(0, 0),
             new Point(this.pic.Width - 100, this.pic.Height),
             Color.Gold,   // Opaque red
             Color.Brown);  // Opaque blue


            for (int i = 0; i < 163; i++)
            {
                this._zona.Add(new oneTVS());
                //_zona[i] = new oneTVS();
                //занесли координату центра из статического массива констант
                _zona[i].Cord = MyConst.setka[i];
                _zona[i].Color = Brushes.White;

                _zona[i].TVSnumber = i;
                _zona[i].IsLoaded = false;
                //  _zona[i].LoadNumber = i;

            }


            StreamReader sr = new StreamReader("fuel_load_sequence.dat");


            string line;
            line = sr.ReadLine();
            int k = 0;
            while (line != null)
            {
                _zona[k].LoadNumber = int.Parse(line);
                line = sr.ReadLine();
                k++;


            }

            sr.Close();
        }
    }
}



