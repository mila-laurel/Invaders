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
        public Point Location { get; set; }
        public Shot(Point location)
        {
            Location = location;
        }
        internal void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        public bool Move(Direction direction, Rectangle rectangle)
        {
            if (rectangle.Contains(Location))
                return false;
            else
            {
                return true;
            }
        }
    }
}
