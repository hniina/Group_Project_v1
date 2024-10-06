namespace SOProject
{
    partial class SignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUp));
            this.email = new System.Windows.Forms.TextBox();
            this.Username_TextBox = new System.Windows.Forms.TextBox();
            this.password_TextBox = new System.Windows.Forms.TextBox();
            this.ConfirmPassword_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.acceptbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(149, 138);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(423, 22);
            this.email.TabIndex = 0;
            this.email.Text = "Email";
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // Username_TextBox
            // 
            this.Username_TextBox.Location = new System.Drawing.Point(149, 197);
            this.Username_TextBox.Name = "Username_TextBox";
            this.Username_TextBox.Size = new System.Drawing.Size(423, 22);
            this.Username_TextBox.TabIndex = 1;
            this.Username_TextBox.Text = "Username";
            this.Username_TextBox.TextChanged += new System.EventHandler(this.Username_TextBox_TextChanged);
            // 
            // password_TextBox
            // 
            this.password_TextBox.Location = new System.Drawing.Point(149, 254);
            this.password_TextBox.Name = "password_TextBox";
            this.password_TextBox.Size = new System.Drawing.Size(423, 22);
            this.password_TextBox.TabIndex = 2;
            this.password_TextBox.Text = "Password";
            this.password_TextBox.TextChanged += new System.EventHandler(this.password_TextBox_TextChanged);
            // 
            // ConfirmPassword_TextBox
            // 
            this.ConfirmPassword_TextBox.Location = new System.Drawing.Point(149, 312);
            this.ConfirmPassword_TextBox.Name = "ConfirmPassword_TextBox";
            this.ConfirmPassword_TextBox.Size = new System.Drawing.Size(423, 22);
            this.ConfirmPassword_TextBox.TabIndex = 3;
            this.ConfirmPassword_TextBox.Text = "Confirm Password";
            this.ConfirmPassword_TextBox.TextChanged += new System.EventHandler(this.ConfirmPassword_TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "SIGN UP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // acceptbutton
            // 
            this.acceptbutton.Location = new System.Drawing.Point(259, 371);
            this.acceptbutton.Name = "acceptbutton";
            this.acceptbutton.Size = new System.Drawing.Size(180, 35);
            this.acceptbutton.TabIndex = 5;
            this.acceptbutton.Text = "Accept";
            this.acceptbutton.UseVisualStyleBackColor = true;
            this.acceptbutton.Click += new System.EventHandler(this.acceptbutton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-158, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1110, 452);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.acceptbutton);
            this.Controls.Add(this.ConfirmPassword_TextBox);
            this.Controls.Add(this.password_TextBox);
            this.Controls.Add(this.Username_TextBox);
            this.Controls.Add(this.email);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "SignUp";
            this.Text = "SignUp";
            this.Load += new System.EventHandler(this.SignUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox Username_TextBox;
        private System.Windows.Forms.TextBox password_TextBox;
        private System.Windows.Forms.TextBox ConfirmPassword_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptbutton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}