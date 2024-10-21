namespace FLAPPY_BİRDS
{
    partial class anaEkran
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
            this.ground = new System.Windows.Forms.PictureBox();
            this.üst_boru = new System.Windows.Forms.PictureBox();
            this.alt_boru = new System.Windows.Forms.PictureBox();
            this.bird = new System.Windows.Forms.PictureBox();
            this.Skor_text = new System.Windows.Forms.Label();
            this.gametimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.üst_boru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alt_boru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bird)).BeginInit();
            this.SuspendLayout();
            // 
            // ground
            // 
            this.ground.Image = global::FLAPPY_BİRDS.Properties.Resources.ground;
            this.ground.Location = new System.Drawing.Point(-12, 464);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(795, 108);
            this.ground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground.TabIndex = 0;
            this.ground.TabStop = false;
            this.ground.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // üst_boru
            // 
            this.üst_boru.Image = global::FLAPPY_BİRDS.Properties.Resources.pipedown;
            this.üst_boru.Location = new System.Drawing.Point(497, -32);
            this.üst_boru.Name = "üst_boru";
            this.üst_boru.Size = new System.Drawing.Size(93, 129);
            this.üst_boru.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.üst_boru.TabIndex = 0;
            this.üst_boru.TabStop = false;
            // 
            // alt_boru
            // 
            this.alt_boru.Image = global::FLAPPY_BİRDS.Properties.Resources.pipe;
            this.alt_boru.Location = new System.Drawing.Point(497, 330);
            this.alt_boru.Name = "alt_boru";
            this.alt_boru.Size = new System.Drawing.Size(93, 152);
            this.alt_boru.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.alt_boru.TabIndex = 0;
            this.alt_boru.TabStop = false;
            this.alt_boru.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // bird
            // 
            this.bird.Image = global::FLAPPY_BİRDS.Properties.Resources.bird;
            this.bird.Location = new System.Drawing.Point(175, 154);
            this.bird.Name = "bird";
            this.bird.Size = new System.Drawing.Size(92, 67);
            this.bird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bird.TabIndex = 0;
            this.bird.TabStop = false;
            // 
            // Skor_text
            // 
            this.Skor_text.AutoSize = true;
            this.Skor_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Skor_text.Location = new System.Drawing.Point(189, 65);
            this.Skor_text.Name = "Skor_text";
            this.Skor_text.Size = new System.Drawing.Size(119, 32);
            this.Skor_text.TabIndex = 1;
            this.Skor_text.Text = "Skor =0";
            // 
            // gametimer
            // 
            this.gametimer.Enabled = true;
            this.gametimer.Interval = 20;
            this.gametimer.Tick += new System.EventHandler(this.gametimerEvent);
            // 
            // anaEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(784, 570);
            this.Controls.Add(this.Skor_text);
            this.Controls.Add(this.ground);
            this.Controls.Add(this.üst_boru);
            this.Controls.Add(this.alt_boru);
            this.Controls.Add(this.bird);
            this.Name = "anaEkran";
            this.Text = "flappy birds";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gamekeyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gamekeyisup);
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.üst_boru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alt_boru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bird)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox bird;
        private System.Windows.Forms.PictureBox alt_boru;
        private System.Windows.Forms.PictureBox üst_boru;
        private System.Windows.Forms.PictureBox ground;
        private System.Windows.Forms.Label Skor_text;
        private System.Windows.Forms.Timer gametimer;
    }
}

