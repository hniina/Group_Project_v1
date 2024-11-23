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

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "5/";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);   
        }

        //(public) funcion para poner datos en el datagrid desde el otro form y llamarla


        public void query1(string message)
        {
            bool messageShown = false;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(query1), message);
            }
            else
            {
                if (!messageShown) // Si no se ha mostrado el mensaje antes
                {
                    string formattedMessage = message.Replace(",", "\n");
                    MessageBox.Show(formattedMessage);
                    messageShown = true; // Marcar que ya se ha mostrado el mensaje
                }
            }
        }

        public void query2(string message)
        {
            bool messageShown = false;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(query2), message);
            }
            else
            {
                if (!messageShown) // Si no se ha mostrado el mensaje antes
                {
                    MessageBox.Show(message);
                    messageShown = true; // Marcar que ya se ha mostrado el mensaje
                }
            }
        }

        public void query3(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(query3), message);
            }
            else
            {
                MessageBox.Show(message);
            }
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
                    dataGridView1.Columns.Clear(); // Clear columns first
                    dataGridView1.Columns.Add("PlayerName", "Connected Players");
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
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }

        private void label_users_connected_Click(object sender, EventArgs e)
        {

        }
    }
    
}
