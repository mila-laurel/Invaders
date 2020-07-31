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
        public bool Alive { get; set; }
        public Rectangle Area { get; set; }
        internal void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        internal void Move(Direction direction)
        {
            throw new NotImplementedException();
        }

        internal void Die()
        {
            throw new NotImplementedException();
        }
    }
}
