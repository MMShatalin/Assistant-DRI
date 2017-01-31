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

    class FuelBath
    {
        public void initializeComponent(int screanState, int PicWidth)
        {
            switch (screanState)
            {
                case 0:
                    this._fuelRect = new Rectangle((int)(PicWidth * 0.25), (int)(PicWidth - 0.05 * PicWidth), (int)(PicWidth * 0.5), (int)(PicWidth * 0.05));
                    break;
                case 1:
                    this._fuelRect = new Rectangle(0, (int)(PicWidth * 0.25), (int)(PicWidth * 0.05), (int)(PicWidth * 0.5));
                    break;
                case 2:
                    this._fuelRect = new Rectangle((int)(PicWidth * 0.25), 0, (int)(PicWidth * 0.5), (int)(PicWidth * 0.05));
                    break;
                case 3:
                    this._fuelRect = new Rectangle((int)(PicWidth - 0.05 * PicWidth), (int)(PicWidth * 0.25), (int)(PicWidth * 0.05), (int)(PicWidth * 0.5));
                    break;
                default:
                    break;
            }
        }

        public FuelBath(int screanState, int PicWidth)
        {
            this.initializeComponent(screanState, PicWidth);

            this.descrip = "FAK";
        }

        private Rectangle _fuelRect;
        public Rectangle FuelRect
        {
            get { return _fuelRect; }
            set { _fuelRect = value; }
        }
        private String descrip;
        public String Descrip
        {
            get { return descrip; }
            set { descrip = value; }
        }

        public void Draw(Graphics g)
        {
            //LinearGradientBrush lgb = new LinearGradientBrush(
            //               new Point(0, 0),
            //               new Point(600, 600),
            //               Color.Blue,
            //               Color.BlueViolet);


            LinearGradientBrush lgb = new LinearGradientBrush(this._fuelRect, Color.Blue, Color.LightBlue, (float)0);


            g.FillRectangle(lgb, this.FuelRect);
        }
    }


    class DriTable
    {


        Bitmap myDriPoint;

 
        public DriTable()
        {
             this.myDriPoint= new Bitmap("dri.png");
        }

        private Point _cord;


        public void ReDraw(Graphics g, int screanState, int PicWidth)
        {

            switch (screanState)
            {
                case 0:
                    this._cord = new Point((int)(PicWidth - 0.04 * PicWidth), (int)(PicWidth * 0.2));
                    break;
                case 1:
                    this._cord = new Point((int)(PicWidth - PicWidth * 0.2), (int)(PicWidth - 0.09 * PicWidth));
                    break;
                case 2:
                    this._cord = new Point(0, (int)(PicWidth - PicWidth * 0.2));
                    break;
                case 3:
                    this._cord = new Point((int)(PicWidth * 0.2), 0);
                    break;
                default:
                    break;
            }
           // g.FillRectangle(Brushes.SandyBrown, this._driRect);

            g.DrawImage(this.myDriPoint, this._cord); 

        }
    }

    class OneTechAxis
    {
        private Point _center;

        public Point Center
        {
            get { return _center; }
            set { _center = value; }
        }

        private string _descr;

        public string Descr
        {
            get { return _descr; }
            set { _descr = value; }
        }

       
        public OneTechAxis(string name)
        {
            this._descr = name;
               
    
        }

        public void Draw(Graphics g)
        {

            Font tempFont = new Font("Arial",(float)16);
         
            //   tempFont.Style = FontStyle.Bold;

           // Font tempFont = new Font(SystemFonts.StatusFont, FontStyle.Bold);
           
            g.DrawString(this._descr, tempFont, Brushes.Black, this._center);
        
        }
    
    }
    


    class Cartogram
    {

        FuelBath fuelBath;

        DriTable driTable;

     

        Graphics g;

        PictureBox pic;
        Pen myPen;

        LinearGradientBrush linGrBrush;

        Pen MyGoalPen;

        List<oneTVS> _zona;

        OneTechAxis[] _listtechAxis;




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


                    for (int i = 0; i < 4; i++)
                    {
                        this._listtechAxis[i].Center = new Point(this.pic.Width - this._listtechAxis[i].Center.Y, this._listtechAxis[i].Center.X);
                    }



                    if (this.ScreanAngle == 3)
                    {
                        this.ScreanAngle = 0;
                    }
                    else this.ScreanAngle++;

                    //this.X0 


                    // this.FuelBath.initializeComponent(this.ScreanAngle, this.pic.Width);

                    // this.FuelBath.FuelRectangle = new Rectangle(this.pic.Width - this.FuelBath.FuelRectangle.Location.Y - this.FuelBath.FuelRectangle.Width, this.FuelBath.FuelRectangle.Location.X, this.FuelBath.FuelRectangle.Height, this.FuelBath.FuelRectangle.Width);


                    break;

                case 2:
                    for (int i = 0; i < this._zona.Count; i++)
                    {
                        this._zona[i].CanvasCord = new Point(this._zona[i].CanvasCord.Y, this.pic.Width - this._zona[i].CanvasCord.X);

                        for (int k = 0; k < 6; k++)
                        {
                            this._zona[i].Hex[k] = new Point(this._zona[i].Hex[k].Y, this.pic.Width - this._zona[i].Hex[k].X);
                        }
                    }




                    for (int i = 0; i < 4; i++)
                    {
                        this._listtechAxis[i].Center = new Point(this._listtechAxis[i].Center.Y, this.pic.Width - this._listtechAxis[i].Center.X);
                    }


                    if (this.ScreanAngle == 0)
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
        /// Прорисовка осей. Goal - нужен чтобы 
        /// </summary>
        /// <param name="goal"></param>
        public void DrawGrid(int goal)
        {

            oneTVS temp = new oneTVS();


            oneTVS goalTvs = new oneTVS();

            if (goal < 162)
            {
                goalTvs = this.Zona[goal];
            }
            else
            {
                goalTvs = this.Zona[162];
            }
            int val = new int();

            Pen myDefaultPen = new Pen(Brushes.White);


            myDefaultPen.DashStyle = DashStyle.Dot;
            myDefaultPen.Width = 1;

            Font myDefaultFont = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, 14, FontStyle.Regular);



            
            ///При каждой конкретно повороте окна нужно рисовать либо справа либо слева,
            ///чтобы не накладывалорсь на стол ДРИ
            ///Ну чтоже, слева уже рисует всегд, сделаем, так
            ///чтобы СЛЕВА рисовал только в ДВУХ НУжних случаях:
            ///


            if ((this.ScreanAngle == 0) || (this.ScreanAngle == 3))
            {
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

            }

            else //если нужно чертить справа
            {
                int fazV = this.ScreanAngle +2;
                if (fazV > 3)
                {
                    fazV = 0;
                }

                foreach (int item in faza[fazV])
                {
                    temp = this.getTVSByTVS360Number(item);
                    g.DrawLine(myDefaultPen, temp.CanvasCord.X + this.FA, temp.CanvasCord.Y,this.pic.Width-40, temp.CanvasCord.Y);
                    switch (this.ScreanAngle)
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
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Red, this.pic.Width-40, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Red, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                    }
                    else
                    {
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.WhiteSmoke, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.WhiteSmoke, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                    }
                }
            
            
            
            
            }

            //ЭТИ УСЛОВИЯ КОГДА НУЖНО ЧЕРТИТЬ ВВЕРХУ
            if ((this.ScreanAngle == 0) || (this.ScreanAngle == 1))
            {

                ///Для прорисовки верхний координат
                ///
                ///fazV - эта махинация нужна чтобы неперебивать массив констант из периферийных ТВС, а его заюзать но сдругой фазой для верхних координат
                int fazV = this.ScreanAngle - 1;
                if (fazV < 0)
                {
                    fazV = 3;
                }

                foreach (int item in faza[fazV])
                {
                    temp = this.getTVSByTVS360Number(item);
                    g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);
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
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X - 15, 0);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X - 15, 0);
                    }
                    else
                    {
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X - 15, 0);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X - 15, 0);
                    }
                }

            } // If screanAngle 0 or 1
            else
            { //Если нужн чертить внизу 
                ///Для прорисовки верхний координат
                ///
                ///fazV - эта махинация нужна чтобы неперебивать массив констант из периферийных ТВС, а его заюзать но сдругой фазой для верхних координат
                int fazV = this.ScreanAngle + 1;
                if (fazV > 3)
                {
                    fazV = 0;
                }

                foreach (int item in faza[fazV])
                {
                    temp = this.getTVSByTVS360Number(item);
                    g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y + this.FA, temp.CanvasCord.X, this.pic.Width-40);
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
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X-15, this.pic.Width-40);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Red, temp.CanvasCord.X-15, this.pic.Width - 40);
                    }
                    else
                    {
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X-15, this.pic.Width - 40);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.WhiteSmoke, temp.CanvasCord.X-15, this.pic.Width - 40);
                    }
                }
            
            
            
            
            }

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



            
          //  this.g.DrawImage(mybmp, new Point(this.pic.Width / 2, this.pic.Width / 2));

            this.fuelBath.Draw(this.g);

            this.driTable.ReDraw(this.g, this.ScreanAngle, this.pic.Width);

            pic.Refresh();

        }


        public void DrawAllAxis()
        {
        
        


            switch (this.ScreanAngle)
            {

                case 0:
                    this._listtechAxis[0].Center = new Point((int)(0.05 * this.pic.Width), (int)(0.49*this.pic.Width));
                    this._listtechAxis[1].Center = new Point((int)(0.49 * this.pic.Width),this.pic.Width - (int)(0.08 * this.pic.Width));
                    this._listtechAxis[2].Center = new Point(this.pic.Width - (int)(0.08 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[3].Center = new Point((int)(0.49 * this.pic.Width), (int)(0.05 * this.pic.Width));

                    break;
                case 1:
                    this._listtechAxis[1].Center = new Point((int)(0.05 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[2].Center = new Point((int)(0.49 * this.pic.Width), this.pic.Width - (int)(0.08 * this.pic.Width));
                    this._listtechAxis[3].Center = new Point(this.pic.Width - (int)(0.08 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[0].Center = new Point((int)(0.49 * this.pic.Width), (int)(0.05 * this.pic.Width));
                    break;
                case 2:
                    this._listtechAxis[2].Center = new Point((int)(0.05 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[3].Center = new Point((int)(0.49 * this.pic.Width), this.pic.Width - (int)(0.08 * this.pic.Width));
                    this._listtechAxis[0].Center = new Point(this.pic.Width - (int)(0.08 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[1].Center = new Point((int)(0.49 * this.pic.Width), (int)(0.05 * this.pic.Width));
                    break;
                case 3:
                    this._listtechAxis[3].Center = new Point((int)(0.05 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[0].Center = new Point((int)(0.49 * this.pic.Width), this.pic.Width - (int)(0.08 * this.pic.Width));
                    this._listtechAxis[1].Center = new Point(this.pic.Width - (int)(0.08 * this.pic.Width), (int)(0.49 * this.pic.Width));
                    this._listtechAxis[2].Center = new Point((int)(0.49 * this.pic.Width), (int)(0.05 * this.pic.Width));
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                this._listtechAxis[i].Draw(this.g);
            }
        
        
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
                    }
                }
                else
                {
                    if (this.Zona[i].Redmark)
                    {
                        this.SetTVSSolidColor(this.Zona[i].Color, i, false);
                    }
                }
            }
            this.UpdateLoadNumber(isFirst60);
          
            this.fuelBath.initializeComponent(this.ScreanAngle, this.pic.Width);
            this.fuelBath.Draw(this.g);

           // this.driTable.initializeComponent(this.ScreanAngle, this.pic.Width);     
            this.driTable.ReDraw(this.g,this.ScreanAngle,this.pic.Width);



        }
        /// <summary>
        /// Закрашивает выбранную ТВС определенным цветом
        /// </summary>
        /// <param name="brush">кисточка со цветом</param>
        /// <param name="tvsnumber">номер твс</param>
        /// <param name="first60">обновить надписи, первые 60 или нет</param>
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


            g.DrawEllipse(this.MyGoalPen, new Rectangle(this.Zona[tvsindex].CanvasCord.X - (int)(0.7 * this.FA), this.Zona[tvsindex].CanvasCord.Y - (int)(0.7 * this.FA), (int)(1.3 * this.FA), (int)(1.3 * this.FA)));

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

            this.Fstep = (int)(SideLength / 31); //так как максимум 14 твс по ширине а fstep это половинка одной, но прибавим еще 2 ТВС на подписи и все такое

            if (!(this.Fstep % 2 == 0))
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

            }

        }



        /// <summary>
        /// Процедура возвращает ТВС по ее номеру в 360 градусной симметрии
        /// </summary>
        /// <param name="TVSNumber">Введите номер ТВС от 1 до 163</param>
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

            this._listtechAxis = new OneTechAxis[4];

            this._listtechAxis[0] = new OneTechAxis("I");
            this._listtechAxis[1] = new OneTechAxis("II");
            this._listtechAxis[2] = new OneTechAxis("III");
            this._listtechAxis[3] = new OneTechAxis("IV");

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
            this.MyGoalPen.Width = 6;
           // this.MyGoalPen.DashStyle = DashStyle.Dot;
            this.g = g;
            this.pic = mypic;

            this.ScreanAngle = 0;


            this.fuelBath = new FuelBath(this.ScreanAngle, this.pic.Width);
            this.driTable = new DriTable();

   

            faza[0] = new List<int> { 0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 };
            faza[1] = new List<int> { 74, 60, 47, 35, 24, 14, 13, 5, 4, 3, 2, 1, 0, 7, 6, 15, 25, 36, 48, 61, 23, 12, 11, 10, 9, 8, 16 };
            faza[2] = new List<int> { 162, 156, 147, 137, 126, 114, 101, 87, 74, 60, 47, 35, 24, 14, 5 };
            faza[3] = new List<int> { 101, 114, 126, 137, 147, 156, 146, 155, 162, 154, 161, 153, 160, 152, 159, 151, 158, 150, 157, 149, 139, 148, 138, 127, 115, 102, 88 };

            linGrBrush = new LinearGradientBrush(
             new Point(0, 0),
             new Point(this.pic.Width - 100, this.pic.Height),
             Color.Gold,   // Opaque red
             Color.Brown);  // Opaque blue


            for (int i = 0; i < 163; i++)
            {
                this._zona.Add(new oneTVS());

                _zona[i].Cord = MyConst.setka[i];
                _zona[i].Color = Brushes.White;

                _zona[i].TVSnumber = i;
                _zona[i].IsLoaded = false;
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
