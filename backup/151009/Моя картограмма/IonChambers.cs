using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Моя_картограмма
{

    [Serializable]
    class IonChambers
    {



        private Point _cordSKP1;
        private Point _cordSKP2;
        private Point _cordSKP3;
        private Point _cordSKP4;
        private Point _cordSKP5;
        private Point _cordSKP6;

        private Point _cordAFP8;
        private Point _cordAFP16;


        private Point _cordDI2;
        private Point _cordDI6;
        private Point _cordDI12;

        private Point _cordDI4;
        private Point _cordDI10;
        private Point _cordDI14;

        public void ReDraw(Graphics g, int screanState, int PicWidth, Cartogram mycart)
            {
                switch (screanState)
                {
                    //Закоментено то что было для В-320
                    //case 0:
                    //    this._cord = new Point((int)(PicWidth - 0.04 * PicWidth), (int)(PicWidth * 0.2));
                    //    break;
                    //case 1:
                    //    this._cord = new Point((int)(PicWidth - PicWidth * 0.2), (int)(PicWidth - 0.09 * PicWidth));
                    //    break;
                    //case 2:
                    //    this._cord = new Point(0, (int)(PicWidth - PicWidth * 0.2));
                    //    break;
                    //case 3:
                    //    this._cord = new Point((int)(PicWidth * 0.2), 0);
                    //    break;
                    //default:
                    //    break;
                    case 0:
                    //  this._cordSKP2 = new Point((int)(0.04 * PicWidth), (int)(PicWidth * 0.5));

                    ///КООРДИНАТЫ SKP1 определены правильно
                    this._cordSKP1.X = mycart.getTVSByTVS360Number(0).CanvasCord.X - mycart.getFstep()*2;
                    this._cordSKP1.Y = mycart.getTVSByTVS360Number(0).CanvasCord.Y;


                    this._cordSKP2.X = mycart.getTVSByTVS360Number(75).CanvasCord.X - mycart.getFstep() * 2;
                    this._cordSKP2.Y = mycart.getTVSByTVS360Number(75).CanvasCord.Y;

                    this._cordSKP3.X = mycart.getTVSByTVS360Number(157).CanvasCord.X - mycart.getFstep() * 2;
                    this._cordSKP3.Y = mycart.getTVSByTVS360Number(157).CanvasCord.Y;

                    this._cordSKP4.X = mycart.getTVSByTVS360Number(162).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP4.Y = mycart.getTVSByTVS360Number(162).CanvasCord.Y;

                    this._cordSKP5.X = mycart.getTVSByTVS360Number(87).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP5.Y = mycart.getTVSByTVS360Number(87).CanvasCord.Y;

                    this._cordSKP6.X = mycart.getTVSByTVS360Number(5).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP6.Y = mycart.getTVSByTVS360Number(5).CanvasCord.Y;

                    this._cordAFP16.X = mycart.getTVSByTVS360Number(14).CanvasCord.X;
                    this._cordAFP16.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y + (int)(mycart.getFstep() * 2.5);

                    this._cordAFP8.X = mycart.getTVSByTVS360Number(149).CanvasCord.X;
                    this._cordAFP8.Y = mycart.getTVSByTVS360Number(149).CanvasCord.Y - (int)(mycart.getFstep() * 3.3);



                    this._cordDI14.X = mycart.getTVSByTVS360Number(14).CanvasCord.X+ (int)(mycart.getFstep() * 3);
                    this._cordDI14.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y;


                    this._cordDI10.X = mycart.getTVSByTVS360Number(156).CanvasCord.X + (int)(mycart.getFstep() * 3);
                    this._cordDI10.Y = mycart.getTVSByTVS360Number(156).CanvasCord.Y;

                    this._cordDI12.X = mycart.getTVSByTVS360Number(101).CanvasCord.X + (int)(mycart.getFstep() * 2.3);
                    this._cordDI12.Y = mycart.getTVSByTVS360Number(101).CanvasCord.Y;

                    //this._cordSKP2.X = mycart.getTVSByTVS360Number(88).Hex[2].X-((int)mycart.getFstep()/2);
                    //this._cordSKP2.Y = mycart.getTVSByTVS360Number(88).Hex[2].Y + ((int)mycart.getFstep() / 2);

                    // this._cordSKP2.X = this.

                    break;
                    case 1:

                    this._cordSKP1.X = mycart.getTVSByTVS360Number(0).CanvasCord.X;
                    this._cordSKP1.Y = mycart.getTVSByTVS360Number(0).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordSKP2.X = mycart.getTVSByTVS360Number(75).CanvasCord.X;
                    this._cordSKP2.Y = mycart.getTVSByTVS360Number(75).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordSKP3.X = mycart.getTVSByTVS360Number(157).CanvasCord.X;
                    this._cordSKP3.Y = mycart.getTVSByTVS360Number(157).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordSKP4.X = mycart.getTVSByTVS360Number(162).CanvasCord.X;
                    this._cordSKP4.Y = mycart.getTVSByTVS360Number(162).CanvasCord.Y + mycart.getFstep() * 2;

                    this._cordSKP5.X = mycart.getTVSByTVS360Number(87).CanvasCord.X;
                    this._cordSKP5.Y = mycart.getTVSByTVS360Number(87).CanvasCord.Y + mycart.getFstep() * 2;

                    this._cordSKP6.X = mycart.getTVSByTVS360Number(5).CanvasCord.X;
                    this._cordSKP6.Y = mycart.getTVSByTVS360Number(5).CanvasCord.Y + mycart.getFstep() * 2;


                    this._cordAFP16.X = mycart.getTVSByTVS360Number(14).CanvasCord.X- (int)(mycart.getFstep() * 2.5);
                    this._cordAFP16.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y;

                    this._cordAFP8.X = mycart.getTVSByTVS360Number(149).CanvasCord.X + (int)(mycart.getFstep() * 3.3);
                    this._cordAFP8.Y = mycart.getTVSByTVS360Number(149).CanvasCord.Y;

                    this._cordDI14.X = mycart.getTVSByTVS360Number(14).CanvasCord.X;
                    this._cordDI14.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y + (int)(mycart.getFstep() * 3);


                    this._cordDI10.X = mycart.getTVSByTVS360Number(156).CanvasCord.X;
                    this._cordDI10.Y = mycart.getTVSByTVS360Number(156).CanvasCord.Y + (int)(mycart.getFstep() * 3);

                    this._cordDI12.X = mycart.getTVSByTVS360Number(101).CanvasCord.X;
                    this._cordDI12.Y = mycart.getTVSByTVS360Number(101).CanvasCord.Y + (int)(mycart.getFstep() * 2.3);


                    //this._cordSKP2 = new Point((int)(PicWidth - PicWidth * 0.2), 0);
                    break;
                    case 2:


                    this._cordSKP1.X = mycart.getTVSByTVS360Number(0).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP1.Y = mycart.getTVSByTVS360Number(0).CanvasCord.Y;

                    this._cordSKP2.X = mycart.getTVSByTVS360Number(75).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP2.Y = mycart.getTVSByTVS360Number(75).CanvasCord.Y;

                    this._cordSKP3.X = mycart.getTVSByTVS360Number(157).CanvasCord.X + mycart.getFstep() * 2;
                    this._cordSKP3.Y = mycart.getTVSByTVS360Number(157).CanvasCord.Y;

                    this._cordSKP4.X = mycart.getTVSByTVS360Number(162).CanvasCord.X-mycart.getFstep() * 2;
                    this._cordSKP4.Y = mycart.getTVSByTVS360Number(162).CanvasCord.Y ;

                    this._cordSKP5.X = mycart.getTVSByTVS360Number(87).CanvasCord.X - mycart.getFstep() * 2;
                    this._cordSKP5.Y = mycart.getTVSByTVS360Number(87).CanvasCord.Y;


                    this._cordSKP6.X = mycart.getTVSByTVS360Number(5).CanvasCord.X - mycart.getFstep() * 2;
                    this._cordSKP6.Y = mycart.getTVSByTVS360Number(5).CanvasCord.Y;


                    this._cordAFP16.X = mycart.getTVSByTVS360Number(14).CanvasCord.X;
                    this._cordAFP16.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y - (int)(mycart.getFstep() * 2.5);

                    this._cordAFP8.X = mycart.getTVSByTVS360Number(149).CanvasCord.X;
                    this._cordAFP8.Y = mycart.getTVSByTVS360Number(149).CanvasCord.Y + (int)(mycart.getFstep() * 3.3);

                    this._cordDI14.X = mycart.getTVSByTVS360Number(14).CanvasCord.X - (int)(mycart.getFstep() * 3);
                    this._cordDI14.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y;


                    this._cordDI10.X = mycart.getTVSByTVS360Number(156).CanvasCord.X- (int)(mycart.getFstep() * 3);
                    this._cordDI10.Y = mycart.getTVSByTVS360Number(156).CanvasCord.Y;

                    this._cordDI12.X = mycart.getTVSByTVS360Number(101).CanvasCord.X- (int)(mycart.getFstep() * 2.3);
                    this._cordDI12.Y = mycart.getTVSByTVS360Number(101).CanvasCord.Y;

                    //this._cordSKP2 = new Point((int)(PicWidth * 0.9), (int)(PicWidth * 0.8));
                    break;

                    ///По всей видимости на НВАЭС-2 эта будет основной режим.
                    case 3:


                    this._cordSKP1.X = mycart.getTVSByTVS360Number(0).CanvasCord.X;
                    this._cordSKP1.Y = mycart.getTVSByTVS360Number(0).CanvasCord.Y+ mycart.getFstep() * 2;

                    this._cordSKP2.X = mycart.getTVSByTVS360Number(75).CanvasCord.X;
                    this._cordSKP2.Y = mycart.getTVSByTVS360Number(75).CanvasCord.Y + mycart.getFstep() * 2;

                    this._cordSKP3.X = mycart.getTVSByTVS360Number(157).CanvasCord.X;
                    this._cordSKP3.Y = mycart.getTVSByTVS360Number(157).CanvasCord.Y + mycart.getFstep() * 2;

                    this._cordSKP4.X = mycart.getTVSByTVS360Number(162).CanvasCord.X;
                    this._cordSKP4.Y = mycart.getTVSByTVS360Number(162).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordSKP5.X = mycart.getTVSByTVS360Number(87).CanvasCord.X;
                    this._cordSKP5.Y = mycart.getTVSByTVS360Number(87).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordSKP6.X = mycart.getTVSByTVS360Number(5).CanvasCord.X;
                    this._cordSKP6.Y = mycart.getTVSByTVS360Number(5).CanvasCord.Y - mycart.getFstep() * 2;

                    this._cordAFP16.X = mycart.getTVSByTVS360Number(14).CanvasCord.X + (int)(mycart.getFstep() * 2.5);
                    this._cordAFP16.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y;

                    this._cordAFP8.X = mycart.getTVSByTVS360Number(149).CanvasCord.X - (int)(mycart.getFstep() * 3.3);
                    this._cordAFP8.Y = mycart.getTVSByTVS360Number(149).CanvasCord.Y;

                    this._cordDI14.X = mycart.getTVSByTVS360Number(14).CanvasCord.X;
                    this._cordDI14.Y = mycart.getTVSByTVS360Number(14).CanvasCord.Y - (int)(mycart.getFstep() * 3);

                    this._cordDI10.X = mycart.getTVSByTVS360Number(156).CanvasCord.X;
                    this._cordDI10.Y = mycart.getTVSByTVS360Number(156).CanvasCord.Y - (int)(mycart.getFstep() * 3);

                    this._cordDI12.X = mycart.getTVSByTVS360Number(101).CanvasCord.X;
                    this._cordDI12.Y = mycart.getTVSByTVS360Number(101).CanvasCord.Y - (int)(mycart.getFstep() * 2.3);


                    //this._cordSKP2 = new Point((int)(PicWidth * 0.2), (int)(PicWidth * 0.88));
                    break;
                    default:
                        break;
                }
            //g.DrawImage(this.mySKP1, this._cordSKP1);
            // g.DrawImage(this.mySKP2, this._cordSKP2);

            Pen mySKPPen = new Pen(Brushes.Red);
            Pen myAFPPen = new Pen(Brushes.Blue);
            Pen myDIPen = new Pen(Brushes.Black);

            g.FillEllipse(MyConst.SKP1Brush, this._cordSKP1.X - MyConst.SKPsize, this._cordSKP1.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP2Brush, this._cordSKP2.X - MyConst.SKPsize, this._cordSKP2.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP3Brush, this._cordSKP3.X - MyConst.SKPsize, this._cordSKP3.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);

            g.FillEllipse(MyConst.SKP4Brush, this._cordSKP4.X - MyConst.SKPsize, this._cordSKP4.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP5Brush, this._cordSKP5.X - MyConst.SKPsize, this._cordSKP5.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP6Brush, this._cordSKP6.X - MyConst.SKPsize, this._cordSKP6.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);


            g.DrawEllipse(mySKPPen, this._cordSKP1.X - MyConst.SKPsize, this._cordSKP1.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.DrawEllipse(mySKPPen, this._cordSKP2.X - MyConst.SKPsize, this._cordSKP2.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.DrawEllipse(mySKPPen, this._cordSKP3.X - MyConst.SKPsize, this._cordSKP3.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);

            g.DrawEllipse(mySKPPen, this._cordSKP4.X - MyConst.SKPsize, this._cordSKP4.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.DrawEllipse(mySKPPen, this._cordSKP5.X - MyConst.SKPsize, this._cordSKP5.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.DrawEllipse(mySKPPen, this._cordSKP6.X - MyConst.SKPsize, this._cordSKP6.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);



            g.FillEllipse(MyConst.AFP16Brush, this._cordAFP16.X - MyConst.AFPsize, this._cordAFP16.Y - MyConst.AFPsize, MyConst.AFPsize * 2, MyConst.AFPsize * 2);
            g.DrawEllipse(myAFPPen, this._cordAFP16.X - MyConst.AFPsize, this._cordAFP16.Y - MyConst.AFPsize, MyConst.AFPsize * 2, MyConst.AFPsize * 2);

            g.FillEllipse(MyConst.AFP8Brush, this._cordAFP8.X - MyConst.AFPsize, this._cordAFP8.Y - MyConst.AFPsize, MyConst.AFPsize * 2, MyConst.AFPsize * 2);
            g.DrawEllipse(myAFPPen, this._cordAFP8.X - MyConst.AFPsize, this._cordAFP8.Y - MyConst.AFPsize, MyConst.AFPsize * 2, MyConst.AFPsize * 2);


            g.FillEllipse(MyConst.DI14Brush, this._cordDI14.X - MyConst.DIsize, this._cordDI14.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);
            g.DrawEllipse(myDIPen, this._cordDI14.X - MyConst.DIsize, this._cordDI14.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);

            g.FillEllipse(MyConst.DI12Brush, this._cordDI12.X - MyConst.DIsize, this._cordDI12.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);
            g.DrawEllipse(myDIPen, this._cordDI12.X - MyConst.DIsize, this._cordDI12.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);

            g.FillEllipse(MyConst.DI10Brush, this._cordDI10.X - MyConst.DIsize, this._cordDI10.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);
            g.DrawEllipse(myDIPen, this._cordDI10.X - MyConst.DIsize, this._cordDI10.Y - MyConst.DIsize, MyConst.DIsize * 2, MyConst.DIsize * 2);



            Pen myDefaultPen = new Pen(Brushes.White);
            myDefaultPen.DashStyle = DashStyle.Dash;
            myDefaultPen.DashCap = DashCap.Triangle;
            myDefaultPen.Width = 1;

           // g.DrawEllipse(Pens.LightGray, 7, 7, PicWidth - 14, PicWidth - 14);
           // g.DrawPie(Pens.LightGray, 7, 7, PicWidth - 14, PicWidth - 14,0,270);

        }
        }

    }

