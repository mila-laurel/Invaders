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
                MoveInvaders();
                ReturnFire();
                CheckForPlayerCollisions();
                CheckForInvaderCollisions();
            }
            else
                return;
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
                if (invaderDirection == Direction.Right)
                {
                    var invaderNextToRightBoundary = from invader in invaders
                                                     where invader.Area.X >= boundaries.Right - 100
                                                     select invader;
                    if (invaderNextToRightBoundary.Any())
                    {
                        foreach (Invader invader in invaders)
                            invader.Move(Direction.Down);
                        invaderDirection = Direction.Left;
                    }
                }
                else
                {
                    var invaderNextToLeftBoundary = from invader in invaders
                                                    where invader.Area.X <= 100
                                                    select invader;
                    if (invaderNextToLeftBoundary.Any())
                    {
                        foreach (Invader invader in invaders)
                            invader.Move(Direction.Down);
                        invaderDirection = Direction.Right;
                    }
                }
                foreach (Invader invader in invaders)
                    invader.Move(invaderDirection);
            }
            else
            {
                framesSkipped++;
                return;
            }
        }

        private void ReturnFire()
        {
            if (invaderShots.Count >= wave + 1 || random.Next(10) < 10 - wave)
                return;
            else
            {
                var groupedInvaders = from invader in invaders
                                      group invader by invader.Area.X
                                      into invaderXcoordinate
                                      orderby invaderXcoordinate.Key descending
                                      select invaderXcoordinate;
                var randGroup = groupedInvaders.ElementAt(random.Next(groupedInvaders.Count()));
                Invader shootingInvader = randGroup.First();
                invaderShots.Add(new Shot(new Point(shootingInvader.Area.X + shootingInvader.Area.Width / 2, shootingInvader.Area.Y)));
                var invaderAtTheBottom = from invaderGroup in groupedInvaders
                                         where invaderGroup.First().Area.Bottom == boundaries.Bottom
                                         select invaderGroup;
                if (invaderAtTheBottom.Any())
                    OnGameOver();
            }
        }

        private void CheckForInvaderCollisions()
        {
            for (int i = 0; i < playerShots.Count; i++)
            {
                var deadInvaders = from invader in invaders
                                   where invader.Area.Contains(playerShots[i].Location)
                                   select invader;
                for (int c = 0; c < deadInvaders.Count(); c++)
                {
                    if (playerShots[i].Location.Equals(deadInvaders.ElementAt(c).Area))
                        playerShots.Remove(playerShots[i]);
                    invaders.Remove(deadInvaders.ElementAt(c));
                    score++;
                }
            }
        }

        private void CheckForPlayerCollisions()
        {
            for (int i = 0; i < invaderShots.Count; i++)
            {
                if (playerShip.Area.Contains(invaderShots[i].Location))
                {
                    playerShip.Alive = false;
                    if (livesLeft > 0)
                    {
                        livesLeft--;
                        playerShip.Alive = true;
                        playerShip = new PlayerShip();
                    }
                    else
                        OnGameOver();
                }
                else
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
