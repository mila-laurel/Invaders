using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    class Shot
    {
        private const int MoveInterval = 20;
        private const int width = 5;
        private const int height = 15;
        public Point Location { get; private set; }
        private Direction direction;
        private Rectangle boundaries;
        public Shot(Point location, Direction direction, Rectangle boundaries)
        {
            Location = location;
            this.direction = direction;
            this.boundaries = boundaries;
        }
        internal void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, new Rectangle(Location, new Size(width, height)));
        }

        public bool Move(Direction direction, Rectangle rectangle)
        {
            if (rectangle.Contains(Location))
            {
                if (direction == Direction.Down)
                    Location = new Point(Location.X, Location.Y + MoveInterval);
                else
                    Location = new Point(Location.X, Location.Y - MoveInterval);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
