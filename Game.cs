﻿using System;
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
        private List<Shot> playerShots;
        private List<Shot> invaderShots;
        private Stars stars;

        public Game(Random random, Rectangle playArea)
        {
            boundaries = playArea;
            this.random = random;
            NextWave();
            playerShip = new PlayerShip(boundaries);
            playerShots = new List<Shot>();
            invaderShots = new List<Shot>();
            stars = new Stars(boundaries, random);
        }
        public void FireShot()
        {
            if (playerShots.Count < 2)
                playerShots.Add(new Shot(new Point(playerShip.Location.X + playerShip.Area.Width / 2, playerShip.Location.Y), Direction.Up, boundaries));
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
            Bitmap image = new Bitmap(Properties.Resources.bug1);
            invaders = new List<Invader>(30);
            for (int i = 0; i < 6; i++)
            {
                invaders.Add(new Invader(ShipType.Satellite, new Point(boundaries.Width - (image.Size.Width * 2 * (i + 1)), image.Size.Height), 50));
            }
            for (int i = 6; i < 12; i++)
            {
                invaders.Add(new Invader(ShipType.Bug, new Point(boundaries.Width - (image.Size.Width * 2 * (i + 1)), image.Size.Height * 3), 40));
            }
            for (int i = 12; i < 18; i++)
            {
                invaders.Add(new Invader(ShipType.Saucer, new Point(boundaries.Width - (image.Size.Width * 2 * (i + 1)), image.Size.Height * 5), 30));
            }
            for (int i = 18; i < 24; i++)
            {
                invaders.Add(new Invader(ShipType.Spaceship, new Point(boundaries.Width - (image.Size.Width * 2 * (i + 1)), invaders[0].Area.Height * 7), 20));
            }
            for (int i = 24; i < 30; i++)
            {
                invaders.Add(new Invader(ShipType.Star, new Point(boundaries.Width - (image.Size.Width * 2 * (i + 1)), image.Size.Height * 9), 10));
            }
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
                invaderShots.Add(new Shot(new Point(shootingInvader.Area.X + shootingInvader.Area.Width / 2, shootingInvader.Area.Y), Direction.Down, boundaries));
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
                    {
                        playerShots.Remove(playerShots[i]);
                        score += deadInvaders.ElementAt(c).Score;
                        invaders.Remove(deadInvaders.ElementAt(c));
                    }
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
                        playerShip = new PlayerShip(boundaries);
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
            using (Font font = new Font("Arail", 16))
            {
                g.DrawString(score.ToString(), font, Brushes.AliceBlue, new Point(8, 8));
            }
            for (int i = 0; i < livesLeft; i++)
            {
                g.DrawImage(Properties.Resources.player, new Point(boundaries.Width - (playerShip.Area.Width*livesLeft + 5), 3));
            }
            foreach (Invader invader in invaders)
                invader.Draw(g, animationCell);
            playerShip.Draw(g);
            foreach (Shot shot in playerShots)
                shot.Draw(g);
            foreach (Shot shot in invaderShots)
                shot.Draw(g);            
        }

        public void Twinkle()
        {
            stars.Twinkle();
        }
    }
}
