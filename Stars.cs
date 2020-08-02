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
                g.DrawPolygon(stars[i].pen, new Point[] { stars[i].point, new Point(stars[i].point.X + 4, stars[i].point.Y), new Point(stars[i].point.X, stars[i].point.Y + 2), new Point(stars[i].point.X + 2, stars[i].point.Y - 2), new Point(stars[i].point.X + 4, stars[i].point.Y + 2)});
            }
            for (int i = 100; i < 200; i++)
            {
                g.DrawPolygon(stars[i].pen, new Point[] { stars[i].point, new Point(stars[i].point.X + 1, stars[i].point.Y - 1), new Point(stars[i].point.X + 2, stars[i].point.Y - 2), new Point(stars[i].point.X + 3, stars[i].point.Y - 1), new Point(stars[i].point.X + 4, stars[i].point.Y), new Point(stars[i].point.X + 3, stars[i].point.Y + 1), new Point(stars[i].point.X + 2, stars[i].point.Y + 2), new Point(stars[i].point.X + 1, stars[i].point.Y + 1)});
            }
            for (int i = 200; i < 300; i++)
            {
                g.DrawLine(stars[i].pen, stars[i].point, new Point(stars[i].point.X + 8, stars[i].point.Y));
                g.DrawLine(stars[i].pen, new Point(stars[i].point.X + 4, stars[i].point.Y - 4), new Point(stars[i].point.X + 4, stars[i].point.Y + 4));
                g.DrawLine(stars[i].pen, new Point(stars[i].point.X + 2, stars[i].point.Y - 2), new Point(stars[i].point.X + 6, stars[i].point.Y + 2));
                g.DrawLine(stars[i].pen, new Point(stars[i].point.X + 2, stars[i].point.Y + 2), new Point(stars[i].point.X + 6, stars[i].point.Y - 2));
            }
        }

        internal void Twinkle(Random random)
        {
            Star[] starsToRemove = new Star[5];
            for (int i = 0; i < 5; i++)
            {
                starsToRemove[i] = stars[random.Next(stars.Count)];
                stars.Remove(starsToRemove[i]);
                stars.Add(new Star(starsToRemove[i].point, RandomPen(random)));
            }                              
        }        
    }
}
