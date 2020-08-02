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
        private const int HorizontalInterval = 15;
        private int deadShipHeight;
        private Bitmap image;
        private DateTime then;
        private Rectangle playArea;
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
        public Point Location { get; private set; }
        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }
        public PlayerShip(Rectangle playArea)
        {
            image = Properties.Resources.player;
            Alive = true;
            this.playArea = playArea;
            Location = new Point(playArea.Width / 2 - Area.Size.Width / 2, playArea.Bottom - (Area.Size.Height + 5));
        }
        internal void Draw(Graphics g)
        {
            if (Alive)
            {
                deadShipHeight = Area.Size.Height;
                g.DrawImage(image, new Rectangle(Location, new Size(Area.Size.Width, deadShipHeight)));
            }
            else
            {
                if (DateTime.Now - then < TimeSpan.FromSeconds(3.0))
                {
                    if (deadShipHeight > 0)
                    {
                        deadShipHeight--;
                        g.DrawImage(image, new Rectangle(Location, new Size(Area.Size.Width, deadShipHeight)));
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
            {
                if (playArea.Width - Area.Right > 0)
                    Location = new Point(Location.X + HorizontalInterval, Location.Y);
                else
                    return;
            }
            else
            {
                if (Location.X > 0)
                    Location = new Point(Location.X - HorizontalInterval, Location.Y);
                else
                    return;
            }
        }
    }
}
