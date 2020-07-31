using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    class Invader
    {
        public Rectangle Area { get; set; }
        internal void Draw(Graphics g, int animationCell)
        {
            throw new NotImplementedException();
        }

        internal void Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
