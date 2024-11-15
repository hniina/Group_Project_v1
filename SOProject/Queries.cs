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
                    message = "3/";               
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);

                }
                else if (winner.Checked)
                {
                    message = "4/" + gameid.Text; // The code for fetching the winner by game ID
                                                  // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);
                }
                else if (gamesPlayed.Checked)  // New case for Games Played by a Player
                {
                    message = "6/" + playerName.Text; // "6" represents the new query
                                                      // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);
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

        private void newgame_Click(object sender, EventArgs e)
        {
            NewGame  ng = new NewGame(server);
            ng.ShowDialog();

        }

        private void AtenderServidor() //receive ALL the messages from the server!!
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[512];
                int receivedbytes = server.Receive(msg2);
                if (receivedbytes == 0)
                {
                    Console.WriteLine("No data received."); //Making sure the message is not empty;
                    return;
                }
                string message = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = message.Split('/');
                string codigo =(trozos[0]);
                string mensaje = (trozos[1]);

                switch (codigo)
                {
                    case "1":
                        string formattedMessage = mensaje.Replace(",", "\n");
                        MessageBox.Show(formattedMessage);
                        break;

                    case "2":
                        MessageBox.Show(mensaje);
                        break;
                    case "3":
                        MessageBox.Show(mensaje);
                        break;

                    case "4":
                        int m = Convert.ToInt32(mensaje.Split(',')[0]);

                        label_users_connected.Text = "Users connected:";
                        label_users_connected.Text = label_users_connected.Text + " " + Convert.ToString(m);

                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear(); // Clear columns first
                        dataGridView1.Columns.Add("PlayerName", "Connected Players");


                        string name;

                        for (int i = 0; i < m; i++)
                        {
                            name = Convert.ToString(Encoding.ASCII.GetString(msg2).Split(',')[i]);
 

                            dataGridView1.Rows.Add(name);

                        }

                        break;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "5/";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);
        }

        //(public) funcion para poner datos en el datagrid desde el otro form y llamarla

    }
    
}
