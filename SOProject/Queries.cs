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
using System.Net;

namespace SOProject
{
    public partial class Queries : Form
    {
        Socket server;
        Thread atender;
        string conectados;
        string myname;
        delegate void hide_window_delegate();
        int acceptedInvitation = 0;
        public string player1;
        public string player2;
        public int idgame;
        public Queries(Socket s, string conectados, string myname)
        {
            InitializeComponent();
            this.server = s;
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            Console.WriteLine("Hilo iniciado...");
            this.conectados = conectados;
            ConnectedList(conectados);
            this.myname = myname;
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

                }
                else if (winner.Checked)
                {
                    message = "4/" +gameid.Text; // The code for fetching the winner by game ID

                }
                else if (gamesPlayed.Checked)  // New case for Games Played by a Player
                {
                    message = "6/" + playerName.Text; // "6" represents the new query

                }
                byte[] msg = Encoding.ASCII.GetBytes(message);
                server.Send(msg);
                Console.WriteLine($"Mensaje enviado: {message}");
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
            int RowSelection = e.RowIndex;
            if (RowSelection >= 0)
            {
                string name = dataGridView1[0, RowSelection].Value.ToString();
                if (name==myname)
                {
                    MessageBox.Show("You can't invite yourself.");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to invite" + " " + dataGridView1[0, RowSelection].Value.ToString(), "You're about to invite this person", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("You have invited" + " " + dataGridView1[0, RowSelection].Value.ToString());
                        string message = "7/" + myname + "/" + name;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                        server.Send(msg);                        
                    }
                }
            }

            else
            {
                MessageBox.Show("You must select a valid row.");
            }
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
            if (acceptedInvitation == 1)
            {
                ThreadStart ts = delegate { PonerEnMarchaFormulario(player1,player2,idgame); };
                Thread T = new Thread(ts);
                T.Start();
            }

            else
            {
                MessageBox.Show("You must invite someone and they have to accept your invitation to start a new game.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "5/";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);   
        }

        public void query1(string message)
        {
            string formattedMessage = message.Replace(",", "\n");
            MessageBox.Show(formattedMessage);
        }

        public void query2(string message)
        {
            MessageBox.Show(message);
        }

        public void query3(string message)
        {
            MessageBox.Show(message);
        }

        public void ConnectedList(string mensaje)
        {
            Console.WriteLine("ConnectedList ejecutado con: " + mensaje);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ConnectedList), mensaje);
            }
            else
            {
                try
                {
                    string[] trozos = mensaje.Split(',');
                    string connected = trozos[0];
                    label_users_connected.Text = connected;

                    dataGridView1.Rows.Clear();
                    //dataGridView1.Columns.Clear(); // Clear columns first
                    //dataGridView1.Columns.Add("PlayerName", "Connected Players"); Commented in order to prevent format exceptions
                    for (int i = 1; i < trozos.Length; i++)
                    {
                        dataGridView1.Rows.Add(trozos[i]);
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("Error connecting to the server: " + ex.Message);
                }
            }
        }

        private void Queries_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Ensure the server is valid before proceeding
                if (server != null)
                {
                    string mensaje = "0/"; // Message to notify server for player removal
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);

                    // Send the message
                    server.Send(msg);

                    // Gracefully shut down and close the socket
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Socket error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Display the disconnect message, ensuring it always happens
                MessageBox.Show("Disconnected");
            }
        }

        private void label_users_connected_Click(object sender, EventArgs e)
        {

        }

        public void AtenderServidor() //receive ALL the messages from the server!!
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("I am executing this while");
                    //Recibimos mensaje del servidor
                    byte[] msg2 = new byte[1024];
                    Array.Clear(msg2, 0, msg2.Length);
                    server.Receive(msg2);
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    string codigo = (trozos[0]);
                    string mensaje;

                    switch (codigo)
                    {
                        case "1": //query 1
                            string mess = trozos[1].Split('\0')[0];
                            query1(mess);
                            break;

                        case "2": //query 2
                            mensaje = trozos[1].Split('\0')[0];
                            query2(mensaje);
                            break;
                        case "3": //query 3
                            mensaje = trozos[1].Split('\0')[0];
                            query3(mensaje);
                            break;

                        case "4": //Connected List 
                            mensaje = trozos[1].Split('\0')[0];
                            Console.WriteLine("Llamando a ConnectedList...");
                            ConnectedList(mensaje);
                            break;
                        case "7": //invitaion
                            string invites = trozos[1];
                            string message = trozos[2];
                            DialogResult dialogResult = MessageBox.Show(message, "You're about to accept this game", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string accepted = "8/" + invites + "/" + "1";                                
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(accepted);
                                server.Send(msg);
                                acceptedInvitation = 1;
                                MessageBox.Show("You can now press the start button. Enjoy the game!");
                                player2 = myname;

                            }
                            else //not accepted
                            {
                                string accepted = "8/" + invites + "/" + "0";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(accepted);
                                server.Send(msg);
                            }
                            break;
                        case "8":
                            int aceptado = Convert.ToInt32(trozos[1]);
                            string invitation = trozos[2];
                            if (aceptado == 0)
                            {
                                MessageBox.Show(invitation);
                            }
                            else
                            {
                                MessageBox.Show(invitation);
                                acceptedInvitation = 1;
                                player1 = myname;
                            }
                            break;

                        case "9": //get id if you are the onw that invites
                            idgame = Convert.ToInt32(trozos[1]);
                            break;

                        case "10": // start the game
                            player1 = trozos[1];
                            player2 = trozos[2];

                            // Send "GAME START" message to both players
                            string gameStartMessage = "GAME START/" + player1 + "/" + player2;
                            byte[] startMsg = Encoding.ASCII.GetBytes(gameStartMessage);
                            server.Send(startMsg);  // Send start message to both clients

                            Console.WriteLine($"Game started between {player1} and {player2}.");
                            break;
                    }
                }
            }

            catch (SocketException ex)
            {
                MessageBox.Show($"Socket error (Atender): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error (Atender): {ex.Message}");
            }
        }

        private void PonerEnMarchaFormulario(string p1, string p2, int id)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PonerEnMarchaFormulario(p1, p2, id)));
            }
            else
            {
                NewGame newgame = new NewGame(server, p1, p2, id);
                newgame.ShowDialog();
            }
        }
    }
    
}
