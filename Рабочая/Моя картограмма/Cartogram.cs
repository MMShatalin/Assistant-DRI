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
    /// 
    [Serializable]
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
        private double _VisotaOtmetka;
        public double VisotaOtmetka
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
        /// 
  
        private Color color;
        public Color Color
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
    [Serializable]
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


            if ((this._fuelRect.Width != 0) && (this._fuelRect.Height != 0)) ///иначе LinearGradientBrush ругается и прога пожет повиснуть если юзер будет ресайзить форму
            {
                LinearGradientBrush lgb = new LinearGradientBrush(this._fuelRect, Color.DarkBlue, Color.LightBlue, (float)0);
                g.FillRectangle(lgb, this.FuelRect);
            }
        }
    }


    //сигнализатор уровня
    class SU
    {
        Bitmap mySUPoint;
        private Point _cord;
        private Point _nativeCord;
        
        public Point NativeCord
        {
            get { return _nativeCord; }
            set { _nativeCord = value; }
        }

        public Point Cord
        {
            get { return _cord; }
            set { _cord = value; }
        }

/// <summary>
/// Конструктор СИГНАЛИЗАТОРА УРОВНЯ
/// </summary>
/// <param name="mypoint">Координата </param>
        public SU(Point mypoint, int FA, int FSTEP, Point XYTVS)
        {     
           Image i = new Bitmap("IMG/su8.png");
            //Image i = new Bitmap("IMG/test.gif");
         //this.mySUPoint = new Bitmap(
           this.mySUPoint = new Bitmap(i,(int)(FA),(int)(2*FSTEP));
           this._cord = new Point((int)(mypoint.X - this.mySUPoint.Width / 2), (int)(mypoint.Y - FSTEP));
            this._nativeCord = XYTVS;
        }


        /// <summary>
        /// Будет ставит новые координаты после вращения 
        /// </summary>
        /// <param name="newCord"></param>
        /// <param name="FA"></param>
        public void setCord(Point newCord, int FA)
        {
            this._cord = new Point((int)(newCord.X - this.mySUPoint.Width / 2), (int)(newCord.Y - FA));        
        }
        public void ReDraw(Graphics g)
        {
            g.DrawImage(this.mySUPoint, this._cord);
        }
    }
   

    [Serializable]
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
            Font tempFont = new Font("Lucida",(float)MyConst.FontTechAxis);
            g.DrawString(this._descr, tempFont, Brushes.Black, this._center);       
        }    
    }
    

    [Serializable]
    class Cartogram
    {

        /// <summary>
        /// свойство необходимое только для сериализации для сохранения этого параметра
        /// </summary>
        int mynextTVS;

        public int MynextTVS
        {
            get { return mynextTVS; }
            set { mynextTVS = value; }
        }


        FuelBath fuelBath;
        DriTable driTable;
        IonChambers ionChambers;
                    
        [NonSerialized]
        Graphics g;

         [NonSerialized]
        PictureBox pic;

         [NonSerialized]
         Pen myPen;


        /// <summary>
        /// эта как раз и есть градиентная кисточка для закраски
        /// </summary>
         [NonSerialized]
         LinearGradientBrush linGrBrush;

         [NonSerialized]
         Pen MyGoalPen;

        List<oneTVS> _zona;
        OneTechAxis[] _listtechAxis;

        /// <summary>
        /// угол поворота стола
        /// </summary>
        List<int>[] faza = new List<int>[4];

        /// <summary>
        /// Инициализируем занова несериализцемые компонеты
        /// </summary>
        /// <param name="ng"></param>
        /// <param name="npic"></param>
        public void InitComponentsAfterDeserialize(Graphics ng,PictureBox npic, Pen npen)
        {
            this.g = ng;
            this.pic = npic;

            this.myPen = npen;



            this.MyGoalPen = new Pen(Color.Red);
            //this.MyGoalPen.Color = Color.DarkRed;
            this.MyGoalPen.Width = 6;

            //linGrBrush = new LinearGradientBrush(new Point(0, 0), new Point(this.pic.Width - 100, this.pic.Height), Color.Gold, Color.Brown);

            linGrBrush = new LinearGradientBrush(
    new Point(0, 0),
    new Point(this.pic.Width - 100, this.pic.Height),
    Color.LightBlue,   // Opaque red
    Color.Blue);  // Opaque blue


        }




        int ScreanAngle;

        public int ScreanAngle1
        {
            get { return ScreanAngle; }
            set { ScreanAngle = value; }
        }

        public List<oneTVS>  Zona
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
        /// Прорисовка осей. Goal - нужен чтобы выделять цели на координатной сетке
        /// </summary>
        /// <param name="goal">Индекс ТВС Куда будет загружена следующая</param>
        public void DrawGrid(int goal)
        {
            oneTVS temp = new oneTVS();
            ///Получим реальный объект целефой ТВС
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
            Pen myDefaultPen = new Pen(Brushes.Black);
            myDefaultPen.DashStyle = DashStyle.Dot;
            myDefaultPen.Width = 1;

            Pen myDefaultPenGoal = new Pen(Brushes.Red);
            myDefaultPenGoal.DashStyle = DashStyle.Dot;
            myDefaultPenGoal.Width = 1;

            Font myDefaultFont = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, MyConst.MyAxisFontSize, FontStyle.Regular);
            Font myDefaultGoalFont = new Font(System.Windows.Forms.Control.DefaultFont.FontFamily, MyConst.MyAxisFontGoalSize, FontStyle.Regular);
            ///При каждой конкретно повороте окна нужно рисовать либо справа либо слева,
            ///чтобы не накладывалорсь на стол ДРИ
            ///Ну чтоже, слева уже рисует всегд, сделаем, так
            ///чтобы СЛЕВА рисовал только в ДВУХ НУжних случаях:
            ///
            if ((this.ScreanAngle == 2) || (this.ScreanAngle == 3))
            {
                foreach (int item in faza[this.ScreanAngle])
                {
                    temp = this.getTVSByTVS360Number(item);
                    //if ((goalTvs.Cord.X == temp.Cord.X)||(goalTvs.Cord.Y == temp.Cord.Y))
                    //{
                    //    g.DrawLine(myDefaultPenGoal, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);
                    //}
                    //else
                    //{
                    //    g.DrawLine(myDefaultPen, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);
                    //}
                    switch (this.ScreanAngle)
                    {
                        case 2:
                            val = temp.Cord.X;
                            break;
                        //case 1:
                        //    val = temp.Cord.Y;
                        //    break;
                        //case 2:
                        //    val = temp.Cord.X;
                        //    break;
                        case 3:
                            val = temp.Cord.Y;
                            break;

                        default:
                            break;
                    }
                    if ((val == goalTvs.Cord.X) || (val == goalTvs.Cord.Y))
                    {
                        g.DrawLine(myDefaultPenGoal, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultGoalFont, Brushes.Red, 0, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultGoalFont, Brushes.Red, 0, temp.CanvasCord.Y - 14);
                    }
                    else
                    {
                        g.DrawLine(myDefaultPen, 40, temp.CanvasCord.Y, temp.CanvasCord.X - this.FA, temp.CanvasCord.Y);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Black, 0, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Black, 0, temp.CanvasCord.Y - 14);
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
                    //if ((goalTvs.Cord.X == temp.Cord.X) || (goalTvs.Cord.Y == temp.Cord.Y))
                    //{
                    //    g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X + this.FA, temp.CanvasCord.Y, this.pic.Width - 40, temp.CanvasCord.Y);
                    //}
                    //else
                    //{
                    //    g.DrawLine(myDefaultPen, temp.CanvasCord.X + this.FA, temp.CanvasCord.Y, this.pic.Width - 40, temp.CanvasCord.Y);
                    //}            
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
                        g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X + this.FA, temp.CanvasCord.Y, this.pic.Width - 40, temp.CanvasCord.Y);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultGoalFont, Brushes.Red, this.pic.Width-40, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultGoalFont, Brushes.Red, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                    }
                    else
                    {
                        g.DrawLine(myDefaultPen, temp.CanvasCord.X + this.FA, temp.CanvasCord.Y, this.pic.Width - 40, temp.CanvasCord.Y);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Black, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Black, this.pic.Width - 40, temp.CanvasCord.Y - 14);
                    }
                }
            }

            //ЭТИ УСЛОВИЯ КОГДА НУЖНО ЧЕРТИТЬ ВВЕРХУ
            if ((this.ScreanAngle == 0) || (this.ScreanAngle == 3))
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
                    //if ((goalTvs.Cord.X == temp.Cord.X) || (goalTvs.Cord.Y == temp.Cord.Y))
                    //{
                    //    g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);
                    //}
                    //else
                    //{
                    //    g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);
                    //}
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
                        g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultGoalFont, Brushes.Red, temp.CanvasCord.X - 15, 0);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultGoalFont, Brushes.Red, temp.CanvasCord.X - 15, 0);
                    }
                    else
                    {
                        g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y - this.FA, temp.CanvasCord.X, 40);
                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Black, temp.CanvasCord.X - 15, 0);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Black, temp.CanvasCord.X - 15, 0);
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

                    //if ((goalTvs.Cord.X == temp.Cord.X) || (goalTvs.Cord.Y == temp.Cord.Y))
                    //{
                    //    g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X, temp.CanvasCord.Y + this.FA, temp.CanvasCord.X, this.pic.Width - 40);
                    //}
                    //else
                    //{
                    //    g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y + this.FA, temp.CanvasCord.X, this.pic.Width - 40);
                    //}

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
                        g.DrawLine(myDefaultPenGoal, temp.CanvasCord.X, temp.CanvasCord.Y + this.FA, temp.CanvasCord.X, this.pic.Width - 40);

                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultGoalFont, Brushes.Red, temp.CanvasCord.X-15, this.pic.Width-40);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultGoalFont, Brushes.Red, temp.CanvasCord.X-15, this.pic.Width - 40);
                    }
                    else
                    {
                        g.DrawLine(myDefaultPen, temp.CanvasCord.X, temp.CanvasCord.Y + this.FA, temp.CanvasCord.X, this.pic.Width - 40);

                        if (val < 10)
                        {
                            g.DrawString("0" + val.ToString(), myDefaultFont, Brushes.Black, temp.CanvasCord.X-15, this.pic.Width - 40);
                        }
                        else
                            g.DrawString(val.ToString(), myDefaultFont, Brushes.Black, temp.CanvasCord.X-15, this.pic.Width - 40);
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
        //public void Show()
        //{
        //    this.CalcCord(this.pic.Width);
            
        //    foreach (oneTVS item in this.Zona)
        //    {
        //        this.g.FillPolygon(Brushes.White, item.Hex);
        //        this.g.DrawPolygon(this.myPen, item.Hex);
        //    }



            
        //  //  this.g.DrawImage(mybmp, new Point(this.pic.Width / 2, this.pic.Width / 2));

        //    this.fuelBath.Draw(this.g);

        //    this.driTable.ReDraw(this.g, this.ScreanAngle, this.pic.Width);
        //    this.ionChambers.ReDraw(this.g, this.ScreanAngle, this.pic.Width,this);

        //    pic.Refresh();

        //}

        /// <summary>
        /// Рисует технологические оси (пожелания БОрдея)
        /// </summary>
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
        public void RePaintAll(bool isFirst60, bool isIC, bool TechAxis)
        {
            
            this.g.FillRectangle(MyConst.MyBackgroundBrush, 0, 0, this.pic.Width, this.pic.Width);
            for (int i = 0; i < this.Zona.Count; i++)
            {
                this.SetTVSSolidColor(Color.White, i, false);
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
            if (isIC)
            {
                this.ionChambers.ReDraw(this.g, this.ScreanAngle, this.pic.Width, this);
            }
            if (TechAxis)
            {
                this.DrawAllAxis();
            }
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
        public void SetTVSSolidColor(Color brush, int tvsnumber, bool refresh)
        {

            this.g.FillPolygon(new SolidBrush(brush), this.Zona[tvsnumber].Hex);

            g.DrawPolygon(this.myPen, this.Zona[tvsnumber].Hex);
            // this.Show();
            //   this.UpdateLoadNumber(first60);
            if (refresh) this.pic.Refresh();
        }


        /// <summary>
        /// Закрашивает выбранную ТВС определенным цветом по номеру от 1 до 163
        /// </summary>
        /// <param name="brush">кисточка со цветом</param>
        /// <param name="tvsnumber">номер твс</param>
        /// <param name="first60">обновить надписи, первые 60 или нет</param>
        public void SetTVSSolidColorBy360Number(Brush brush, int tvsnumber, bool refresh)
        {

            this.g.FillPolygon(brush, this.getTVSByTVS360Number(tvsnumber).Hex);

            g.DrawPolygon(this.myPen, this.getTVSByTVS360Number(tvsnumber).Hex);
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
            Font myF = new Font("Lucida", MyConst.FontSizeInTVS, FontStyle.Regular);

            foreach (oneTVS item in this.Zona)
            {
                if (!first60)
                {
                    this.g.DrawString(item.LoadNumber.ToString(), myF, MyConst.FontColorInTVS, item.CanvasCord.X - (int)(0.4*this.FA), item.CanvasCord.Y - this.FA / 3 );
                }
                else
                {

                    if (item.LoadNumber < 62)
                    {
                        this.g.DrawString(item.LoadNumber.ToString(), myF, MyConst.FontColorInTVS, item.CanvasCord.X - (int)(0.4 * this.FA), item.CanvasCord.Y - this.FA / 3);
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

            this.Fstep = (int)(SideLength / MyConst.CartogramSize); //так как максимум 14 твс по ширине а fstep это половинка одной, но прибавим еще 2 ТВС на подписи и все такое

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
     /// Возвращает ТВС по Point координате X-Y
     /// </summary>
     /// <param name="x"></param>
     /// <param name="y"></param>
     /// <returns></returns>
        public oneTVS getTVSByPointXY(Point cor)
        {

            foreach (oneTVS item in this.Zona)
            {
                if (item.Cord == cor)
                {
                    return item;
                }

            }
            return null;

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
            
            this._zona = new List<oneTVS>();




            this._listtechAxis = new OneTechAxis[4];
            this._listtechAxis[0] = new OneTechAxis("I");
            this._listtechAxis[1] = new OneTechAxis("II");
            this._listtechAxis[2] = new OneTechAxis("III");
            this._listtechAxis[3] = new OneTechAxis("IV");


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
            this.ionChambers = new IonChambers();
            

            ////Эта кисть будет потом все решать при загрузке
            //           linGrBrush = new LinearGradientBrush(
            //new Point(0, 0),
            //new Point(this.pic.Width - 100, this.pic.Height),
            //Color.Gold,   // Opaque red
            //Color.Brown);  // Opaque blue
            linGrBrush = new LinearGradientBrush(
new Point(0, 0),
new Point(this.pic.Width - 100, this.pic.Height),
Color.LightBlue,   // Opaque red
Color.Blue);  // Opaque blue






            ////в этом массиве лежат номера граничных ТВС, чтобы от их координат рисовать координатную сетку
            faza[0] = new List<int> { 0, 6, 15, 25, 36, 48, 61, 75, 88, 102, 115, 127, 138, 148, 157 };
            faza[1] = new List<int> { 74, 60, 47, 35, 24, 14, 13, 5, 4, 3, 2, 1, 0, 7, 6, 15, 25, 36, 48, 61, 23, 12, 11, 10, 9, 8, 16 };
            faza[2] = new List<int> { 162, 156, 147, 137, 126, 114, 101, 87, 74, 60, 47, 35, 24, 14, 5 };
            faza[3] = new List<int> { 101, 114, 126, 137, 147, 156, 146, 155, 162, 154, 161, 153, 160, 152, 159, 151, 158, 150, 157, 149, 139, 148, 138, 127, 115, 102, 88 };




            for (int i = 0; i < 163; i++)
            {
                this._zona.Add(new oneTVS());

                _zona[i].Cord = MyConst.setka[i];
                _zona[i].Color = Color.White;

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
