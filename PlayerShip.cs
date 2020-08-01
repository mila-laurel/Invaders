using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    class PlayerShip
    {
        private const int HorizontalInterval = 10;
        private int deadShipHeight;
        private Bitmap image;
        private DateTime then;
        private bool alive;
        public bool Alive
        {
            get { return alive; }
            set
            {
                then = DateTime.Now;
                alive = value;
            }
        }
        private void Go()
        {
            if (DateTime.Now - then == TimeSpan.FromSeconds(3.0))
                Alive = true;
            else
                return;
        }

        public Point Location { get; private set; }
        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }
        internal void Draw(Graphics g)
        {
            if (Alive)
            {
                deadShipHeight = Area.Size.Height;
                g.DrawImage(Properties.Resources.player, new Rectangle(Location, new Size(Area.Size.Width, deadShipHeight)));
            }
            else
            {
                if (DateTime.Now - then < TimeSpan.FromSeconds(3.0))
                {
                    if (deadShipHeight > 0)
                    {
                        deadShipHeight--;
                        g.DrawImage(Properties.Resources.player, Location);
                    }
                }
                else
                {
                    Alive = true;
                }
            }
        }

        internal void Move(Direction direction)
        {
            if (direction == Direction.Right)
                Location = new Point(Location.X + HorizontalInterval, Location.Y);
            else
                Location = new Point(Location.X - HorizontalInterval, Location.Y);
        }
    }
}
