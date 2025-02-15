﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOProject;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace SOProject
{
    public partial class Connect : Form
    {
        public Socket server;
        public Connect()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("10.4.119.5");
            IPEndPoint ipep = new IPEndPoint(direc, 50016);
            

            //We create the socket
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
                MessageBox.Show("Connected");
                this.Hide();
                Main m = new Main(server);
                m.ShowDialog();

            }
            catch (SocketException)
            {
                MessageBox.Show("Error connecting to server", "Error");
                this.Close();

            }
        }

        private void Connect_Load(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

