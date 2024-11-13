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
using System.Threading;

namespace SOProject
{
    public partial class Queries : Form
    {
        Socket server;
        public Thread atender;
        public Queries(Socket s)
        {
            InitializeComponent();

            this.server = s;
            CheckForIllegalCrossThreadCalls = false;
            ThreadStart ts = delegate { AtenderServidor(); };

            atender = new Thread(ts);
            atender.Start();

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
                else if (gamesPlayed.Checked)  // New case for Games Played by a Player
                {
                    message = "6/" + playerName.Text; // "6" represents the new query
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
        private void gamesPlayed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void playerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void connected_Click(object sender, EventArgs e)
        {

        }

        private void newgame_Click(object sender, EventArgs e)
        {
            NewGame  ng = new NewGame(server);
            ng.ShowDialog();
            
        }

        private void GetList()
        {
            try
            {

                //We get the server answer
                byte[] msg2 = new byte[1024];
                int receivedBytes = server.Receive(msg2);
                string fullResponse = Encoding.ASCII.GetString(msg2, 0, receivedBytes);

                // Process the server response and split it into players
                string[] players = fullResponse.Split('/');


                // Clear the DataGridView before populating it with new data
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear(); // Clear columns first
                dataGridView1.Columns.Add("PlayerName", "Connected Players");

                for (int i = 1; i < players.Length; i++)
                {
                    dataGridView1.Rows.Add(players[i]);  // Add each player
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }

        }

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {
                    case 1:
                        StringBuilder responseBuilder = new StringBuilder();
                        do
                        {
                            responseBuilder.Append(Encoding.ASCII.GetString(msg2, 0, server.Receive(msg2)));
                        }
                        while (server.Receive(msg2) == msg2.Length);

                        string fullResponse = responseBuilder.ToString().Trim();

                        MessageBox.Show(fullResponse);
                        break;
                    case 2:
                        StringBuilder responseBuild = new StringBuilder();
                        do
                        {
                            responseBuild.Append(Encoding.ASCII.GetString(msg2, 0, server.Receive(msg2)));
                        }
                        while (server.Receive(msg2) == msg2.Length);

                        string Response = responseBuild.ToString().Trim();

                        MessageBox.Show(Response);
                        break;
                    case 3:
                        StringBuilder responseB = new StringBuilder();
                        do
                        {
                            responseB.Append(Encoding.ASCII.GetString(msg2, 0, server.Receive(msg2)));
                        }
                        while (server.Receive(msg2) == msg2.Length);

                        string fullResp = responseB.ToString().Trim();

                        MessageBox.Show(fullResp);
                        break;
                    case 4:
                        try
                        {
                            string answer = Encoding.ASCII.GetString(msg2, 0, server.Receive(msg2));

                            //Process the server response and split it into players
                            string[] players = answer.Split('/');


                            //Clear the DataGridView before populating it with new data
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear(); // Clear columns first
                            dataGridView1.Columns.Add("PlayerName", "Connected Players");

                            for (int i = 1; i < players.Length; i++)
                            {
                                dataGridView1.Rows.Add(players[i]);  // Add each player
                            }
                        }
                        catch (SocketException ex)
                        {
                            MessageBox.Show("Error connecting to the server: " + ex.Message);
                        }
                        break;
                }
            }
        }
    }
    
}