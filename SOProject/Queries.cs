using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

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
    }
}

