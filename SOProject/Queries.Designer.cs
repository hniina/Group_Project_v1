namespace SOProject
{
    partial class Queries
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
            this.gameid = new System.Windows.Forms.TextBox();
            this.PlayerGame = new System.Windows.Forms.RadioButton();
            this.winner = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.query = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameid
            // 
            this.gameid.Location = new System.Drawing.Point(285, 86);
            this.gameid.Name = "gameid";
            this.gameid.Size = new System.Drawing.Size(167, 22);
            this.gameid.TabIndex = 0;
            this.gameid.Text = "Game ID";
            // 
            // PlayerGame
            // 
            this.PlayerGame.AutoSize = true;
            this.PlayerGame.Location = new System.Drawing.Point(285, 161);
            this.PlayerGame.Name = "PlayerGame";
            this.PlayerGame.Size = new System.Drawing.Size(158, 20);
            this.PlayerGame.TabIndex = 1;
            this.PlayerGame.TabStop = true;
            this.PlayerGame.Text = "Players in each game";
            this.PlayerGame.UseVisualStyleBackColor = true;
            this.PlayerGame.CheckedChanged += new System.EventHandler(this.PlayerGame_CheckedChanged);
            // 
            // winner
            // 
            this.winner.AutoSize = true;
            this.winner.Location = new System.Drawing.Point(285, 220);
            this.winner.Name = "winner";
            this.winner.Size = new System.Drawing.Size(294, 20);
            this.winner.TabIndex = 2;
            this.winner.TabStop = true;
            this.winner.Text = "Winner of a specific game (Provide game ID)";
            this.winner.UseVisualStyleBackColor = true;
            this.winner.CheckedChanged += new System.EventHandler(this.winner_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(285, 280);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(103, 20);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(260, 337);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(207, 31);
            this.query.TabIndex = 4;
            this.query.Text = "Send request";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // Queries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.query);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.winner);
            this.Controls.Add(this.PlayerGame);
            this.Controls.Add(this.gameid);
            this.Name = "Queries";
            this.Text = "Queries";
            this.Load += new System.EventHandler(this.Queries_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox gameid;
        private System.Windows.Forms.RadioButton PlayerGame;
        private System.Windows.Forms.RadioButton winner;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button query;
    }
}