using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Моя_картограмма
{
    [Serializable]
    class DriTable
    {
        Bitmap myDriPoint;
        public DriTable()
        {
            this.myDriPoint = new Bitmap("IMG/dri.png");
        }
        private Point _cord;
        public void ReDraw(Graphics g, int screanState, int PicWidth)
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
                    this._cord = new Point((int)(0.04 * PicWidth), (int)(PicWidth * 0.15));
                    break;
                case 1:
                    this._cord = new Point((int)(PicWidth - PicWidth * 0.2), 0);
                    break;
                case 2:
                    this._cord = new Point((int)(PicWidth * 0.9), (int)(PicWidth * 0.8));
                    break;

                ///По всей видимости на НВАЭС-2 эта будет основной режим.
                case 3:
                    this._cord = new Point((int)(PicWidth * 0.1), (int)(PicWidth * 0.88));
                    break;
                default:
                    break;
            }
            g.DrawImage(this.myDriPoint, this._cord);
        }
    }
}
