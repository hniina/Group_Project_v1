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
            this.gamesPlayed = new System.Windows.Forms.RadioButton();
            this.query = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.TextBox();
            this.newgame = new System.Windows.Forms.Button();
            this.label_users_connected = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameid
            // 
            this.gameid.Location = new System.Drawing.Point(366, 90);
            this.gameid.Name = "gameid";
            this.gameid.Size = new System.Drawing.Size(122, 22);
            this.gameid.TabIndex = 0;
            this.gameid.Text = "Game ID ";
            this.gameid.TextChanged += new System.EventHandler(this.gameid_TextChanged);
            // 
            // PlayerGame
            // 
            this.PlayerGame.AutoSize = true;
            this.PlayerGame.Location = new System.Drawing.Point(494, 45);
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
            this.winner.Location = new System.Drawing.Point(494, 92);
            this.winner.Name = "winner";
            this.winner.Size = new System.Drawing.Size(294, 20);
            this.winner.TabIndex = 2;
            this.winner.TabStop = true;
            this.winner.Text = "Winner of a specific game (Provide game ID)";
            this.winner.UseVisualStyleBackColor = true;
            this.winner.CheckedChanged += new System.EventHandler(this.winner_CheckedChanged);
            // 
            // gamesPlayed
            // 
            this.gamesPlayed.AutoSize = true;
            this.gamesPlayed.Location = new System.Drawing.Point(494, 136);
            this.gamesPlayed.Name = "gamesPlayed";
            this.gamesPlayed.Size = new System.Drawing.Size(189, 20);
            this.gamesPlayed.TabIndex = 3;
            this.gamesPlayed.TabStop = true;
            this.gamesPlayed.Text = "Games Played by a Player";
            this.gamesPlayed.UseVisualStyleBackColor = true;
            this.gamesPlayed.CheckedChanged += new System.EventHandler(this.gamesPlayed_CheckedChanged);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(608, 178);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(134, 31);
            this.query.TabIndex = 4;
            this.query.Text = "Send request";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Queries:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(240, 240);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Connected:";
            // 
            // playerName
            // 
            this.playerName.Location = new System.Drawing.Point(366, 136);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(122, 22);
            this.playerName.TabIndex = 9;
            this.playerName.Text = "Enter player Name";
            this.playerName.TextChanged += new System.EventHandler(this.playerName_TextChanged);
            // 
            // newgame
            // 
            this.newgame.Location = new System.Drawing.Point(608, 379);
            this.newgame.Name = "newgame";
            this.newgame.Size = new System.Drawing.Size(155, 42);
            this.newgame.TabIndex = 10;
            this.newgame.Text = "New Game";
            this.newgame.UseVisualStyleBackColor = true;
            this.newgame.Click += new System.EventHandler(this.newgame_Click);
            // 
            // label_users_connected
            // 
            this.label_users_connected.AutoSize = true;
            this.label_users_connected.Location = new System.Drawing.Point(176, 9);
            this.label_users_connected.Name = "label_users_connected";
            this.label_users_connected.Size = new System.Drawing.Size(14, 16);
            this.label_users_connected.TabIndex = 11;
            this.label_users_connected.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(57, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "Connected List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Queries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_users_connected);
            this.Controls.Add(this.newgame);
            this.Controls.Add(this.playerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.query);
            this.Controls.Add(this.gamesPlayed);
            this.Controls.Add(this.winner);
            this.Controls.Add(this.PlayerGame);
            this.Controls.Add(this.gameid);
            this.Name = "Queries";
            this.Text = "Queries";
            this.Load += new System.EventHandler(this.Queries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox gameid;
        private System.Windows.Forms.RadioButton PlayerGame;
        private System.Windows.Forms.RadioButton winner;
        private System.Windows.Forms.RadioButton gamesPlayed;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.Button newgame;
        private System.Windows.Forms.Label label_users_connected;
        private System.Windows.Forms.Button button1;
    }
}