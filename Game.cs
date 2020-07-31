using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invaders
{
    class Game
    {
        private int score = 0;
        private int livesLeft = 2;
        private int wave = 0;
        private int framesSkipped = 0;
        private Rectangle boundaries;
        private Random random;
        private Direction invaderDirection;
        private List<Invader> invaders;
        private PlayerShip playerShip;
        public Point PlayerShipLocation { get { return new Point(playerShip.Area.X + playerShip.Area.Width / 2, playerShip.Area.Y); } }
        private List<Shot> playerShots;
        private List<Shot> invaderShots;
        private Stars stars;

        public Game(Random random, Rectangle playArea)
        {
            boundaries = playArea;
            this.random = random;
            NextWave();
            playerShip = new PlayerShip();
            playerShots = new List<Shot>();
            invaderShots = new List<Shot>();
            stars = new Stars();
        }
        public void FireShot(Point location)
        {
            if (playerShots.Count < 2)
                playerShots.Add(new Shot(location));
        }

        public void Go()
        {
            if (playerShip.Alive)
            {
                foreach (Shot shot in playerShots)
                {
                    if (!shot.Move(Direction.Up, boundaries))
                        playerShots.Remove(shot);
                }
                foreach (Shot shot in invaderShots)
                {
                    if (!shot.Move(Direction.Down, boundaries))
                        invaderShots.Remove(shot);
                }
                if (framesSkipped == 7 - wave)
                {
                    foreach (Invader invader in invaders)
                    {
                        if (new Rectangle(0, 0, 1, boundaries.Height).Contains(invader.Area) || new Rectangle(boundaries.Right - 1, 0, 1, boundaries.Height).Contains(invader.Area))
                        {
                            invader.Move(Direction.Down);
                            if (invaderDirection == Direction.Right)
                                invaderDirection = Direction.Left;
                            else
                                invaderDirection = Direction.Right;
                        }
                        invader.Move(invaderDirection);
                        if (invaderShots.Count < 2)
                        {
                            Invader shootingInvader = invaders[random.Next(invaders.Count)];
                            invaderShots.Add(new Shot(new Point(shootingInvader.Area.X + shootingInvader.Area.Width / 2, shootingInvader.Area.Y)));
                            if (invaderShots.Count < 2)
                            {
                                shootingInvader = invaders[random.Next(invaders.Count)];
                                invaderShots.Add(new Shot(new Point(shootingInvader.Area.X + shootingInvader.Area.Width / 2, shootingInvader.Area.Y)));
                            }
                        }
                    }
                    framesSkipped = -1;
                }
                framesSkipped++;
            }
            else
            {
                playerShip.Die();
                return;
            }
        }

        private void NextWave()
        {
            invaders = new List<Invader>();
            wave++;
            invaderDirection = Direction.Right;
            framesSkipped = 0;
        }

        private void MoveInvaders()
        {
            if (framesSkipped == 7 - wave)
            {
                foreach (Invader invader in invaders)
                {

                    if (new Rectangle(100, 0, 1, boundaries.Height).Contains(invader.Area) || new Rectangle(boundaries.Right - 100, 0, 1, boundaries.Height).Contains(invader.Area))
                    {
                        invader.Move(Direction.Down);
                        if (invaderDirection == Direction.Right)
                            invaderDirection = Direction.Left;
                        else
                            invaderDirection = Direction.Right;
                    }
                    invader.Move(invaderDirection);
                }
                framesSkipped = 0;
            }
            else
            {
                framesSkipped++;
                return;
            }
        }

        public void MovePlayer(Direction direction)
        {
            if (playerShip.Alive)
                playerShip.Move(direction);
        }

        public event EventHandler GameOver;
        private void OnGameOver()
        {
            EventHandler gameOver = GameOver;
            if (gameOver != null)
                gameOver(this, new EventArgs());
        }

        public void Draw(Graphics g, int animationCell)
        {
            g.FillRectangle(Brushes.Black, boundaries);
            stars.Draw(g);
            foreach (Invader invader in invaders)
                invader.Draw(g, animationCell);
            playerShip.Draw(g);
            foreach (Shot shot in playerShots)
                shot.Draw(g);
            foreach (Shot shot in invaderShots)
                shot.Draw(g);
            if (livesLeft < 0)
            {
                using (Font font = new Font("Arial", 32, FontStyle.Bold))
                {
                    g.DrawString("GAME OVER", font, Brushes.Yellow, 210, 230);
                    g.DrawString("Press S to start a new game or Q to quit", font, Brushes.White, 630, 420);
                }
            }
        }

        public void Twinkle()
        {
            stars.Twinkle();
        }
    }
}
