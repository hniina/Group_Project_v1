using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SOProject
{
    public partial class Queries : Form
    {
        Socket server;
        public Queries(Socket s)
        {
            InitializeComponent();

            this.server = s;
        }

        private void Queries_Load(object sender, EventArgs e)
        {

        }

        private void PlayerGame_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void winner_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                string message = ""; // Prepare the message to send to the server

                if (PlayerGame.Checked)
                {
                    message = "3/"; // The code for fetching players in each game
                }
                else if (winner.Checked)
                {
                    message = "4/" + gameid.Text; // The code for fetching the winner by game ID
                }

                if (!string.IsNullOrEmpty(message))
                {
                    // Send the message to the server
                    byte[] msg = Encoding.ASCII.GetBytes(message);
                    server.Send(msg);

                    // the answer from the server
                    StringBuilder responseBuilder = new StringBuilder();
                    byte[] response = new byte[512];
                    int receivedBytes = 0;

                    do
                    {
                        receivedBytes = server.Receive(response);
                        responseBuilder.Append(Encoding.ASCII.GetString(response, 0, receivedBytes));
                    }
                    while (receivedBytes == response.Length);

                    string fullResponse = responseBuilder.ToString().Trim();

                    MessageBox.Show(fullResponse);
                }
            }
            catch (SocketException ex)
            {
                // If there's a socket exception, show an error message
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gameid_TextChanged(object sender, EventArgs e)
        {

        }

        private void connected_Click(object sender, EventArgs e)
        {
            string message = "5/";
            //We send the game ID
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
            server.Send(msg);

            //We get the server answer
            byte[] msg2 = new byte[1024];
            int receivedBytes = server.Receive(msg2);
            string fullResponse = Encoding.ASCII.GetString(msg2, 0, receivedBytes);

            // Process the server response and split it into players
            string[] players = fullResponse.Split('/');
            int numberOfPlayers = Convert.ToInt32(players[0]);

            // Clear the DataGridView before populating it with new data
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear(); // Clear columns first
            dataGridView1.Columns.Add("PlayerName", "Connected Players");

            // Add rows for each player
            for (int i = 1; i <= numberOfPlayers; i++)  // Start from 1, since 0 is the number of players
            {
                dataGridView1.Rows.Add(players[i]);  // Add player name to the row
            }
        }
    }
}