using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Race
{
    public partial class Form1 : Form
    {
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Pen pen = new Pen(Color.Black, 2);

        Player player1;
        Player player2;
        List<Asteroid> leftToRightAsteroids;
        List<Asteroid> rightToLeftAsteroids;
        Timer gameTimer;
        int player1Score = 0;
        int player2Score = 0;
        bool gameRunning = true;

        bool wKeyDown, sKeyDown, upKeyDown, downKeyDown, aKeyDown, dKeyDown, rightKeyDown, leftKeyDown;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) wKeyDown = false;
            if (e.KeyCode == Keys.S) sKeyDown = false;
            if (e.KeyCode == Keys.Up) upKeyDown = false;
            if (e.KeyCode == Keys.Down) downKeyDown = false;
            if (e.KeyCode == Keys.A) aKeyDown = false;
            if (e.KeyCode == Keys.D) dKeyDown = false;
            if (e.KeyCode == Keys.Right) rightKeyDown = false;
            if (e.KeyCode == Keys.Left) leftKeyDown = false;
        }

        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);

            player1 = new Player(50, ClientSize.Height - 60, 0, ClientSize.Width - 80, ClientSize.Height - 60);
            player2 = new Player(ClientSize.Width - 80, ClientSize.Height - 60, 0, ClientSize.Width - 80, ClientSize.Height - 60);

            Random rand = new Random();
            leftToRightAsteroids = new List<Asteroid>();
            rightToLeftAsteroids = new List<Asteroid>();
            for (int i = 0; i < 15; i++) // asteroid number
            {
                int size = rand.Next(10, 30);
                int speed = rand.Next(2, 6);

                // Ensure asteroids do not start near the rockets' initial positions
                int yPosition;
                do
                {
                    yPosition = rand.Next(0, ClientSize.Height - size);
                } while (Math.Abs(yPosition - (ClientSize.Height - 5)) < 100); // Adjust the range as needed

                leftToRightAsteroids.Add(new Asteroid(0, yPosition, speed, size));
                rightToLeftAsteroids.Add(new Asteroid(ClientSize.Width, yPosition, -speed, size)); // Negative speed for right to left
            }

            gameTimer = new Timer();
            gameTimer.Interval = 20; // 50 FPS
            gameTimer.Tick += new EventHandler(GameLoop);
            gameTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawRocket(g, player1.X, player1.Y, blueBrush);
            DrawRocket(g, player2.X, player2.Y, redBrush);

            foreach (var asteroid in leftToRightAsteroids)
            {
                g.FillEllipse(whiteBrush, asteroid.X, asteroid.Y, asteroid.Size, asteroid.Size);
            }

            foreach (var asteroid in rightToLeftAsteroids)
            {
                g.FillEllipse(whiteBrush, asteroid.X, asteroid.Y, asteroid.Size, asteroid.Size);
            }

            // Draw middle separator
            Rectangle separator = new Rectangle(ClientSize.Width / 2 - 2, 0, 4, ClientSize.Height);
            g.FillRectangle(whiteBrush, separator);

            g.DrawString("Player 1 Score: " + player1Score, this.Font, whiteBrush, 10, 10);
            g.DrawString("Player 2 Score: " + player2Score, this.Font, whiteBrush, ClientSize.Width - 150, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameRunning)
            {
                if (e.KeyCode == Keys.W) wKeyDown = true;
                if (e.KeyCode == Keys.S) sKeyDown = true;
                if (e.KeyCode == Keys.Up) upKeyDown = true;
                if (e.KeyCode == Keys.Down) downKeyDown = true;
                if (e.KeyCode == Keys.A) aKeyDown = true;
                if (e.KeyCode == Keys.D) dKeyDown = true;
                if (e.KeyCode == Keys.Right) rightKeyDown = true;
                if (e.KeyCode == Keys.Left) leftKeyDown = true;
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!gameRunning) return;

            if (wKeyDown) player1.MoveUp();
            if (sKeyDown) player1.MoveDown();
            if (upKeyDown) player2.MoveUp();
            if (downKeyDown) player2.MoveDown();
            if (aKeyDown) player1.MoveLeft();
            if (dKeyDown) player1.MoveRight();
            if (leftKeyDown) player2.MoveLeft();
            if (rightKeyDown) player2.MoveRight();

            foreach (var asteroid in leftToRightAsteroids)
            {
                asteroid.Move(ClientSize.Width, ClientSize.Height);
                if (CheckCollision(player1, asteroid)) ResetPlayer(player1);
                if (CheckCollision(player2, asteroid)) ResetPlayer(player2);
            }

            foreach (var asteroid in rightToLeftAsteroids)
            {
                asteroid.Move(ClientSize.Width, ClientSize.Height);
                if (CheckCollision(player1, asteroid)) ResetPlayer(player1);
                if (CheckCollision(player2, asteroid)) ResetPlayer(player2);
            }

            if (player1.Y <= 0) { player1Score++; ResetPlayer(player1); }
            if (player2.Y <= 0) { player2Score++; ResetPlayer(player2); }

            if (player1Score == 3 || player2Score == 3)
            {
                gameRunning = false;
                gameTimer.Stop();
                MessageBox.Show(player1Score == 3 ? "Player 1 Wins!" : "Player 2 Wins!");
            }

            Invalidate();
        }

        private void DrawRocket(Graphics g, int x, int y, Brush bodyBrush)
        {
            // Adjusted dimensions for smaller rocket
            int bodyWidth = 15;
            int bodyHeight = 25;
            int noseHeight = 15;
            int finHeight = 15;
            int flameHeight = 20;

            // Rocket body
            Rectangle body = new Rectangle(x, y, bodyWidth, bodyHeight);
            g.FillRectangle(bodyBrush, body);
            g.DrawRectangle(pen, body);

            // Draw nose cone
            Point[] noseCone =
            {
                new Point(x, y),
                new Point(x + bodyWidth, y),
                new Point(x + bodyWidth / 2, y - noseHeight)
            };
            g.FillPolygon(whiteBrush, noseCone);
            g.DrawPolygon(pen, noseCone);

            // Draw fins
            Point[] leftFin =
            {
                new Point(x, y + bodyHeight),
                new Point(x - 10, y + bodyHeight + finHeight),
                new Point(x, y + bodyHeight + finHeight)
            };
            g.FillPolygon(redBrush, leftFin);
            g.DrawPolygon(pen, leftFin);

            Point[] rightFin =
            {
                new Point(x + bodyWidth, y + bodyHeight),
                new Point(x + bodyWidth + 10, y + bodyHeight + finHeight),
                new Point(x + bodyWidth, y + bodyHeight + finHeight)
            };
            g.FillPolygon(redBrush, rightFin);
            g.DrawPolygon(pen, rightFin);

            // Draw flame
            Point[] flame =
            {
                new Point(x + bodyWidth / 4, y + bodyHeight + finHeight),
                new Point(x + 3 * bodyWidth / 4, y + bodyHeight + finHeight),
                new Point(x + bodyWidth / 2, y + bodyHeight + finHeight + flameHeight)
            };
            g.FillPolygon(yellowBrush, flame);
            g.DrawPolygon(pen, flame);
        }

        private bool CheckCollision(Player player, Asteroid asteroid)
        {
            // Cover entire rocket area including fins and flames
            int bodyWidth = 15;
            int bodyHeight = 25;
            int noseHeight = 15;
            int finHeight = 15;
            int flameHeight = 20;

            Rectangle playerRect = new Rectangle(player.X - 10, player.Y - noseHeight, bodyWidth + 20, bodyHeight + noseHeight + finHeight + flameHeight);
            Rectangle asteroidRect = new Rectangle(asteroid.X, asteroid.Y, asteroid.Size, asteroid.Size);
            return playerRect.IntersectsWith(asteroidRect);
        }

        private void ResetPlayer(Player player)
        {
            player.Y = ClientSize.Height - 60; // Adjusted starting position for smaller rocket
        }
    }
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int speed = 5;
        private int minY; // Minimum Y-coordinate to prevent rocket from going above the white line
        private int maxX; // Maximum X-coordinate to prevent rocket from going out of the right boundary
        private int maxY; // Maximum Y-coordinate to prevent rocket from going out of the bottom boundary

        public Player(int x, int y, int minY, int maxX, int maxY)
        {
            X = x;
            Y = y;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public void MoveUp()
        {
            if (Y > minY) // Check if rocket is above the minimum Y-coordinate
                Y -= speed;
        }
        public void MoveDown()
        {
            if (Y < maxY)
                Y += speed;
        }
        public void MoveLeft()
        {
            if (X > 0) // Check if rocket is within left boundary
                X -= speed;
        }
        public void MoveRight()
        {
            if (X < maxX)
                X += speed;
        }
    }

    public class Asteroid
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Speed { get; }
        public int Size { get; }

        public Asteroid(int x, int y, int speed, int size)
        {
            X = x;
            Y = y;
            Speed = speed;
            Size = size;
        }

        public void Move(int screenWidth, int screenHeight)
        {
            X += Speed;

            // Wrap around the screen
            if (X > screenWidth)
            {
                X = -Size;
            }
            else if (X < -Size)
            {
                X = screenWidth;
            }
        }

    }
}

