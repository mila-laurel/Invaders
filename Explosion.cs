using System;
using System.Drawing;

namespace Invaders
{
    class Explosion
    {

        public Point Location { get; private set; }
        public DateTime Start { get; private set; }

        public Explosion(Point location)
        {
            Location = location;
            Start = DateTime.Now;
        }
        public void Draw(Graphics g, int animationCell)
        {
            switch (animationCell)
            {
                case 0:
                    g.DrawImage(new Bitmap(Properties.Resources.explosion1), Location);
                    break;
                case 1:
                    g.DrawImage(new Bitmap(Properties.Resources.explosion2), Location);
                    break;
                case 2:
                    g.DrawImage(new Bitmap(Properties.Resources.explosion3), Location);
                    break;
                default:
                    g.DrawImage(new Bitmap(Properties.Resources.explosion4), Location);
                    break;
            }
        }
    }
}
