namespace MeuAlelo
{
    partial class Relogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_captcha = new System.Windows.Forms.TextBox();
            this.pictureBox_captcha = new System.Windows.Forms.PictureBox();
            this.textBox_cartao = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_captcha)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Salvar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_captcha
            // 
            this.textBox_captcha.Location = new System.Drawing.Point(192, 114);
            this.textBox_captcha.Name = "textBox_captcha";
            this.textBox_captcha.Size = new System.Drawing.Size(152, 20);
            this.textBox_captcha.TabIndex = 6;
            // 
            // pictureBox_captcha
            // 
            this.pictureBox_captcha.Location = new System.Drawing.Point(23, 71);
            this.pictureBox_captcha.Name = "pictureBox_captcha";
            this.pictureBox_captcha.Size = new System.Drawing.Size(146, 64);
            this.pictureBox_captcha.TabIndex = 5;
            this.pictureBox_captcha.TabStop = false;
            this.pictureBox_captcha.Click += new System.EventHandler(this.pictureBox_captcha_Click);
            // 
            // textBox_cartao
            // 
            this.textBox_cartao.Location = new System.Drawing.Point(23, 33);
            this.textBox_cartao.Name = "textBox_cartao";
            this.textBox_cartao.Size = new System.Drawing.Size(321, 20);
            this.textBox_cartao.TabIndex = 4;
            // 
            // Relogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 221);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_captcha);
            this.Controls.Add(this.pictureBox_captcha);
            this.Controls.Add(this.textBox_cartao);
            this.Name = "Relogin";
            this.Text = "Relogin";
            this.Load += new System.EventHandler(this.Relogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_captcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_captcha;
        private System.Windows.Forms.PictureBox pictureBox_captcha;
        private System.Windows.Forms.TextBox textBox_cartao;
    }
}