using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Моя_картограмма
{

    [Serializable]
    class IonChambers
    {


        Bitmap mySKP1;
        Bitmap mySKP2;
        Bitmap mySKP3;
        Bitmap mySKP4;
        Bitmap mySKP5;
        Bitmap mySKP6;
        public IonChambers()
            {
            this.mySKP1 = new Bitmap("IC/1.png");
            this.mySKP2 = new Bitmap("IC/2.png");
            this.mySKP3 = new Bitmap("IC/3.png");
            this.mySKP4 = new Bitmap("IC/4.png");
            this.mySKP5 = new Bitmap("IC/5.png");
            this.mySKP6 = new Bitmap("IC/6.png");
        }
        private Point _cordSKP1;
        private Point _cordSKP2;
        private Point _cordSKP3;
        private Point _cordSKP4;
        private Point _cordSKP5;
        private Point _cordSKP6;


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



                    //this._cordSKP2 = new Point((int)(PicWidth * 0.2), (int)(PicWidth * 0.88));
                    break;
                    default:
                        break;
                }
            //g.DrawImage(this.mySKP1, this._cordSKP1);
            // g.DrawImage(this.mySKP2, this._cordSKP2);
            g.FillEllipse(MyConst.SKP1Brush, this._cordSKP1.X - MyConst.SKPsize, this._cordSKP1.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP2Brush, this._cordSKP2.X - MyConst.SKPsize, this._cordSKP2.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP3Brush, this._cordSKP3.X - MyConst.SKPsize, this._cordSKP3.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);

            g.FillEllipse(MyConst.SKP4Brush, this._cordSKP4.X - MyConst.SKPsize, this._cordSKP4.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP5Brush, this._cordSKP5.X - MyConst.SKPsize, this._cordSKP5.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);
            g.FillEllipse(MyConst.SKP6Brush, this._cordSKP6.X - MyConst.SKPsize, this._cordSKP6.Y - MyConst.SKPsize, MyConst.SKPsize * 2, MyConst.SKPsize * 2);

        }
        }

    }

