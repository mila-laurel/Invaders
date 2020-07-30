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
        private List<Shot> playerShots;
        private List<Shot> invaderShots;
        private Stars stars;
        internal void FireShot()
        {
            throw new NotImplementedException();
        }

        internal void Go()
        {
            throw new NotImplementedException();
        }

        internal void MovePlayer(Direction left)
        {
            throw new NotImplementedException();
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
            foreach(Invader invader in invaders)
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
