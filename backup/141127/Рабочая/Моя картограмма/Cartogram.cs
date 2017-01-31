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
    class oneTVS:IComparable
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

        /// <summary>
        /// тут будут такие поля как текст, цвет, но самое главное коордлинаты 6 углов
        /// </summary>

        ////тут лежат координаты 6 углов
        private Point[] _Hex; //= new Point[5];

        /// <summary>
        /// Конструктор
        /// </summary>
        public oneTVS()
        {
            this._cord = new Point();
            this._canvasCord = new Point();
            this._Hex = new Point[6];
            this._redmark = false; //по умолчанию все ТВС загружены без замечания
            this._zamechanie = "";


        }

        public Point CanvasCord
        {
            get { return _canvasCord; }
            set { _canvasCord = value; }
        }

        public Point[] Hex
        {
            get { return _Hex; }
            set { _Hex = value; }
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

        //private oneTVS[] _zona;// = new oneTVS[162];

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
        private int Fcenter;




        /// <summary>
        /// Функция для нарисовки координатной линии сетки
        /// </summary>
        public void DrawGrid()
        {

            // --- Coordinate Paint--
            // --------- Height (Left)
            //Brush.Color:=Self.Color;
            //Font.size:=f;A
            //Font.Name:='BankGothic Lt BT';
            //if Font.size > 12 then Font.size:=12;
            //// -- labels --

            //for i :=1  to 9 do
            //TextOut(0,Fcentre-round(FA*(12-i*1.5))-(fA DIV 2),'0'+IntToStr(i));

           // int Ystart = this.getTVSByTVS360Number(157).CanvasCord.Y;
           // int Ystep = (this.getTVSByTVS360Number(140).CanvasCord.Y - this.getTVSByTVS360Number(157).CanvasCord.Y)/2; 
           // for (int i = 1; i < 16; i++)
           //{
           //    g.DrawString(i.ToString(), System.Windows.Forms.Control.DefaultFont, Brushes.Black, 0, Ystart);
           //    Ystart += Ystep;
           //}

          oneTVS temp =  new oneTVS();


          int[] tempY = { 0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 };

          int i=15;


          Pen myDefaultPen = new Pen(Brushes.Green);
          //myDefault.Color = Color.Green;
          myDefaultPen.DashStyle = DashStyle.Dot;
          myDefaultPen.Width = 3;

          Font myDefaultFont = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 14, FontStyle.Bold);

          foreach (int item in tempY)
          {
              temp = this.getTVSByTVS360Number(item);
              g.DrawLine(myDefaultPen, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);
              if (i<10)
              {
                  g.DrawString("0" + i.ToString(), myDefaultFont, Brushes.DarkGreen, 0, temp.CanvasCord.Y-14);    
              } else
                  g.DrawString(i.ToString(), myDefaultFont, Brushes.DarkGreen, 0, temp.CanvasCord.Y-14);          
              i--;
          }
            


            ///Для прорисовки вертикальи пройдемся по всем пятым точкам
            ///
          int[] tempX1 = { 102, 115, 127, 138, 148, 149, 157, 156,158,159,160,161,162,156};

          foreach (int item in tempX1)
          {
              temp = this.getTVSByTVS360Number(item);
              g.DrawLine(myDefaultPen, temp.Hex[1].X, temp.Hex[1].Y, temp.Hex[1].X, 40);
              //    if (i < 10)
              //    {
              //        g.DrawString("0" + i.ToString(), myDefaultFont, Brushes.Green, 0, temp.CanvasCord.Y - 10);
              //    }
              //    else
              //        g.DrawString(i.ToString(), myDefaultFont, Brushes.Green, 0, temp.CanvasCord.Y - 10);
              //    i++;
              //}

          }


          int[] tempX0 = { 148,157,158, 159,160,161,162,156 };

          foreach (int item in tempX0)
          {
              temp = this.getTVSByTVS360Number(item);
              g.DrawLine(myDefaultPen, temp.Hex[0].X, temp.Hex[0].Y, temp.Hex[0].X, 40);
          }


          int[] tempX5 = { 162,156,147,137,126,114};

          foreach (int item in tempX5)
          {
              temp = this.getTVSByTVS360Number(item);
              g.DrawLine(myDefaultPen, temp.Hex[5].X, temp.Hex[5].Y, temp.Hex[5].X, 40);
          }






            ///ПОдпишим циферки оси X
            ///
          int stepX = this.getTVSByTVS360Number(115).Hex[1].X - this.getTVSByTVS360Number(102).Hex[1].X;
          int startX = this.getTVSByTVS360Number(102).Hex[1].X-20;
          for (int ii = 16; ii < 43; ii++)
          {
              if (ii%2==0)
              {
                  g.DrawString(ii.ToString(), myDefaultFont, Brushes.DarkGreen, startX, 0);    
              }
              
              
              
              startX += stepX;
          }

          // g.DrawString("центр", System.Windows.Forms.Control.DefaultFont, Brushes.Black, 0, this.Fcenter);
            //for i :=10  to 15 do
            //TextOut(0,Fcentre-round(FA*(12-i*1.5))-(fA DIV 2),IntToStr(i));


            //for (int i = 10; i < 16; i++)
            //{
            //    g.DrawString(i.ToString(), System.Windows.Forms.Control.DefaultFont, Brushes.Black, 0, (int)(this.Fcenter - Math.Round(this.FA * (12 - i * 1.5)) - (int)((float)this.FA / (float)2)));
            //}


            //// -- lines --
            //for i :=1  to 15 do
            //begin
            //case i > 8 of
            //  False:
            //   begin
            //    MoveTo(round(TextWidth('00')),Fcentre-round(FA*(12-i*1.5)));
            //    LineTo(Fcentre -(8+i)*FStep,Fcentre-round(FA*(12-i*1.5)));
            //   end;
            //  True:
            //  begin
            //    MoveTo(round(TextWidth('00')),Fcentre-round(FA*(12-i*1.5)));
            //    LineTo(Fcentre +(i-24)*FStep,Fcentre-round(FA*(12-i*1.5)));
            //  end;
            //end;
            //   if ( (Fcentre -(15)*FStep )> (round(1.5*TextWidth('00')))) then
            //   begin
            //   MoveTo(round(1.5*TextWidth('00')),Fcentre);
            //   LineTo(Fcentre -(15)*FStep,Fcentre);
            //   end;

            //end;


            // --------- Width (top)  ----
//for i:=0 to 13 do
// TextOut(Fcentre -FStep*(13-2*i)-(Canvas.TextWidth('42')div 2) ,0,IntToStr(2*i+16));
            //for (int i = 0; i < 14; i++)
            //{
            //    g.DrawString((2*i+16).ToString(), System.Windows.Forms.Control.DefaultFont, Brushes.Black, this.Fcenter -this.Fstep*(5-2*i)-1, 0);
            //}




//for i:=0 to 2 do
//begin
//MoveTo(Fcentre -FStep*(13-2*i),TextHeight('0'));
//LineTo(Fcentre -FStep*(13-2*i),Fcentre -FA*(4+3*i));
//end;
//MoveTo(Fcentre -FStep*(7),TextHeight('0'));
//LineTo(Fcentre -FStep*(7),Fcentre -FA*10);
//for i:=1 to 6 do
//begin
//MoveTo(Fcentre -FStep*(7-2*i),TextHeight('0'));
//LineTo(Fcentre -FStep*(7-2*i),Fcentre -FA*12);
//end;
//MoveTo(Fcentre +FStep*7,TextHeight('0'));
//LineTo(Fcentre +FStep*7,Fcentre -FA*10);
//for i:=0 to 2 do
//begin
//MoveTo(Fcentre +FStep*(9+ 2*i),TextHeight('0'));
//LineTo(Fcentre +FStep*(9+ 2*i),Fcentre -FA*(10-3*i));
//end;
//// Height (Right)
//// -- labels --
//for i :=1  to 9 do
// TextOut(Width-TextWidth('00'),Fcentre-round(FA*(12-i*1.5))-(fA DIV 2),'0'+IntToStr(i));

//for i:=10 to 15 do
// TextOut(Width-TextWidth('00') ,Fcentre-round(FA*(12-i*1.5))-(fA DIV 2),IntToStr(i));
//// -- lines --
//for i :=1  to 15 do
//begin
//case i > 8 of
//  False:
//   begin
//    MoveTo(Width-TextWidth('00'),Fcentre-round(FA*(12-i*1.5)));
//    LineTo(Fcentre +(8+i)*FStep,Fcentre-round(FA*(12-i*1.5)));
//   end;
//  True:
//  begin
//    MoveTo(Width-TextWidth('00'),Fcentre-round(FA*(12-i*1.5)));
//    LineTo(Fcentre +(24-i)*FStep,Fcentre-round(FA*(12-i*1.5)));
//  end;
//end;
//   MoveTo(Width-TextWidth('00'),Fcentre);
//   LineTo(Fcentre +(15)*FStep,Fcentre);
//end;



            pic.Refresh();
        }


        /// <summary>
        /// Основная функция проприсовки картограммы
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mypic"></param>
        /// <param name="myPen"></param>
        public void Show()
        {

            //CalcCord(this.pic.Width);
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



            IniFile ini = new IniFile(Application.StartupPath + "\\draw.ini");

            int SideL = ini.ReadInt("Cord", "SIDE", 28);         //("main", "Info", "Тест");
            int XXX = ini.ReadInt("Cord", "XXX", 29);
            int YYY = ini.ReadInt("Cord", "YYY", 8);


          // this.FA = (int)(SideLength / 44); //42

            this.FA = (int)(SideLength / SideL);

            ////Math.Pi/180 для перевода градусов в радианы
            this.Fstep = (int)(this.FA * Math.Cos(30 * Math.PI / 180));

            this.Fcenter = (int)(SideLength / 2);


            for (int i = 0; i < _zona.Count; i++)
            {
             // int X = this.Fcenter + (int)((_zona[i].Cord.Y - 32) * this.Fstep); //21 29
               int X = this.Fcenter + (int)((_zona[i].Cord.Y - XXX) * this.Fstep); //21 29

             // int Y = this.Fcenter + (int)((_zona[i].Cord.X - 14) * this.FA * 1.5); //8 13
              int Y = this.Fcenter + (int)((_zona[i].Cord.X - YYY) * this.FA * 1.5); //8 13

                _zona[i].CanvasCord = new Point(X, Y);

                _zona[i].Hex[0] = new Point(X, Y - this.FA);
                _zona[i].Hex[1] = new Point(X - this.Fstep, Y - (int)(0.5 * this.FA));
                _zona[i].Hex[2] = new Point(X - this.Fstep, Y + (int)(0.5 * this.FA));
                _zona[i].Hex[3] = new Point(X, Y + this.FA);
                _zona[i].Hex[4] = new Point(X + this.Fstep, Y + (int)(0.5 * this.FA));
                _zona[i].Hex[5] = new Point(X + this.Fstep, Y - (int)(0.5 * this.FA));

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
                if (item.TVSnumber==TVSNumber)
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



