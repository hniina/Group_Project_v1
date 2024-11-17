﻿using System;
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
        int nForm;
        Socket server;
        public Thread atender;
        public Queries(int nForm,Socket s)
        {
            InitializeComponent();
            this.nForm = nForm;
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
                    message = "3/" + nForm+ "/";               
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);

                }
                else if (winner.Checked)
                {
                    message = "4/"+ nForm +"/" + gameid.Text; // The code for fetching the winner by game ID
                                                  // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);
                }
                else if (gamesPlayed.Checked)  // New case for Games Played by a Player
                {
                    message = "6/" + nForm + "/" + playerName.Text; // "6" represents the new query
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

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "5/";
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);
        }

        //(public) funcion para poner datos en el datagrid desde el otro form y llamarla

        public void query1 (string message)
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

        private void Queries_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }


    }
    
}
