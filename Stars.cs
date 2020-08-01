using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    struct Star
    {
        public Point point;
        public Pen pen;
        public Star(Point point, Pen pen)
        {
            this.point = point;
            this.pen = pen;
        }
    }
    class Stars
    {
        private List<Star> stars;
        
        public Stars(Rectangle boundaries, Random random)
        {
            stars = new List<Star>();
            while(stars.Count < 300)
            {
                stars.Add(new Star(new Point(random.Next(boundaries.Width), random.Next(boundaries.Height)), RandomPen(random)));
            }
        }

        private Pen RandomPen(Random random)
        {
            Pen pen = Pens.Silver;
            switch(random.Next(4))
            {
                case 0:
                    pen = Pens.Silver;
                    break;
                case 1:
                    pen = Pens.Coral;
                    break;
                case 2:
                    pen = Pens.Gold;
                    break;
                default:
                    pen = Pens.Lavender;
                    break;                    
            }
            return pen;
        }

        internal void Draw(Graphics g)
        {
            for (int i = 0; i < 100; i++)
            {
                g.DrawPolygon(stars[i].pen, new Point[] { stars[i].point, });
            }

        }

        internal void Twinkle()
        {
            throw new NotImplementedException();
        }

        
    }
}
