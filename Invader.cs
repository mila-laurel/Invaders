using System.Drawing;

namespace Invaders
{
    class Invader
    {
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 40;
        private Bitmap image;
        public Point Location { get; private set; }
        public ShipType InvaderType { get; private set; }
        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }
        public int Score { get; private set; }
        public Invader(ShipType invaderType, Point location, int score)
        {
            InvaderType = invaderType;
            Location = location;
            Score = score;
            image = InvaderImage(0);
        }

        private Bitmap InvaderImage(int animationCell)
        {
            Bitmap[] images = new Bitmap[4];
            switch (InvaderType)
            {
                case ShipType.Bug:
                    images[0] = Properties.Resources.bug1;
                    images[1] = Properties.Resources.bug2;
                    images[2] = Properties.Resources.bug3;
                    images[3] = Properties.Resources.bug4;
                    break;
                case ShipType.Satellite:
                    images[0] = Properties.Resources.satellite1;
                    images[1] = Properties.Resources.satellite2;
                    images[2] = Properties.Resources.satellite3;
                    images[3] = Properties.Resources.satellite4;
                    break;
                case ShipType.Saucer:
                    images[0] = Properties.Resources.flyingsaucer1;
                    images[1] = Properties.Resources.flyingsaucer2;
                    images[2] = Properties.Resources.flyingsaucer3;
                    images[3] = Properties.Resources.flyingsaucer4;
                    break;
                case ShipType.Spaceship:
                    images[0] = Properties.Resources.spaceship1;
                    images[1] = Properties.Resources.spaceship2;
                    images[2] = Properties.Resources.spaceship3;
                    images[3] = Properties.Resources.spaceship4;
                    break;
                case ShipType.Star:
                    images[0] = Properties.Resources.star1;
                    images[1] = Properties.Resources.star2;
                    images[2] = Properties.Resources.star3;
                    images[3] = Properties.Resources.star4;
                    break;
            }
            return images[animationCell];
        }

        internal void Draw(Graphics g, int animationCell)
        {
            g.DrawImage(InvaderImage(animationCell), Location);
        }

        internal void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Location = new Point(Location.X + HorizontalInterval, Location.Y);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - HorizontalInterval, Location.Y);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + VerticalInterval);
                    break;
                default:
                    break;
            }
        }
    }
}
