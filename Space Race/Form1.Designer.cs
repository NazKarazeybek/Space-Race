namespace Space_Race
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startingScreen = new System.Windows.Forms.PictureBox();
            this.gameOverScreen = new System.Windows.Forms.PictureBox();
            this.backgroundTimer = new System.Windows.Forms.Timer(this.components);
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.startingScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // startingScreen
            // 
            this.startingScreen.BackColor = System.Drawing.Color.Transparent;
            this.startingScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("startingScreen.BackgroundImage")));
            this.startingScreen.Location = new System.Drawing.Point(-8, -17);
            this.startingScreen.Name = "startingScreen";
            this.startingScreen.Size = new System.Drawing.Size(470, 495);
            this.startingScreen.TabIndex = 0;
            this.startingScreen.TabStop = false;
            // 
            // gameOverScreen
            // 
            this.gameOverScreen.BackColor = System.Drawing.Color.Transparent;
            this.gameOverScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gameOverScreen.BackgroundImage")));
            this.gameOverScreen.Location = new System.Drawing.Point(-8, -1);
            this.gameOverScreen.Name = "gameOverScreen";
            this.gameOverScreen.Size = new System.Drawing.Size(470, 495);
            this.gameOverScreen.TabIndex = 2;
            this.gameOverScreen.TabStop = false;
            this.gameOverScreen.Visible = false;
            // 
            // backgroundTimer
            // 
            this.backgroundTimer.Enabled = true;
            this.backgroundTimer.Interval = 20;
            this.backgroundTimer.Tick += new System.EventHandler(this.backgroundTimer_Tick);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(454, 456);
            this.Controls.Add(this.gameOverScreen);
            this.Controls.Add(this.startingScreen);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.startingScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox startingScreen;
        private System.Windows.Forms.PictureBox gameOverScreen;
        private System.Windows.Forms.Timer backgroundTimer;
        private System.Windows.Forms.Timer gameTimer;
    }
}

