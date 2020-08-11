using System.Drawing;

namespace Invaders
{
    class Explosion
    {
       
        public Point Location { get; set; }
        public int Frame { get; set; }

        public Explosion(Point location)
        {
           Location = location;
        }
        public void Draw(Graphics g, int animationCell)
        {
            Frame = animationCell;
            switch(Frame)
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
