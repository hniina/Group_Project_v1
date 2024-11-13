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
                StringBuilder responseBuilder = new StringBuilder();
                byte[] response = new byte[1024];
                int receivedBytes = 0;

                switch (Encoding.ASCII.GetString(response).Split('/')[0])
                {
                    case "1":

                    case "2":

                    case "3":
                        
                        do
                        {
                            receivedBytes = server.Receive(response);
                            responseBuilder.Append(Encoding.ASCII.GetString(response, 0, receivedBytes));
                        }
                        while (receivedBytes == response.Length);

                        // Paso 2: Convertir el mensaje completo a string
                        string fullResponse = responseBuilder.ToString();

                        // Paso 3: Separar el prefijo y el contenido del mensaje con Split('/')
                        string[] partes = fullResponse.Split(new char[] { '/' }, 2); // Divide en máximo 2 partes

                        if (partes.Length > 1)
                        {
                            string comando = partes[0];  // "1" o cualquier otro número
                            string mensaje = partes[1];  // El contenido que queremos después de "1/"

                            string fullResp = responseBuilder.ToString().Trim();

                            MessageBox.Show(fullResp);
                        }


                        break;

                    case "4":
                       
                            int receivedB = server.Receive(response);
                            string fResponse = Encoding.ASCII.GetString(response, 0, receivedBytes);

                            string[] parts = fResponse.Split(new char[] { '/' }, 2); // Dividimos en dos partes: "5/" y el mensaje

                            // Comprobar si la división ha sido exitosa y que el prefijo es el esperado ("5")
                            if (parts.Length > 1 && parts[0] == "4")  // Aquí suponemos que el servidor devuelve "5/" como prefijo
                            {
                                string playersData = parts[1];  // Contenido después del "5/", que contiene los jugadores

                                // Dividir la cadena de jugadores por el separador "/"
                                string[] players = playersData.Split('/');

                                // Clear the DataGridView before populating it with new data
                                dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear(); // Clear columns first
                            dataGridView1.Columns.Add("PlayerName", "Connected Players");

                            for (int i = 1; i < players.Length; i++)
                            {
                                dataGridView1.Rows.Add(players[i]);  // Add each player
                            }
                           }
                        break;
                            
                    }
            }
        }
    }
    
}
