﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SOProject
{
    public partial class ChatRoom : Form
    {
        Socket server;
        string myname;
        public ChatRoom(Socket s, string myname)
        {
            InitializeComponent();
            this.server = s;
            this.myname = myname;
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string soutput;           
            soutput = "10/" + myname + "/" + message.Text;
            byte[] output = System.Text.Encoding.ASCII.GetBytes(soutput);
            server.Send(output);
        }

        public void UpdateChat(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateChat(message)));
            }
            else
            {
                if (listBox1 != null)
                {
                    // Agregar el mensaje recibido al ListBox
                    listBox1.Items.Add(message);

                    // Autoscroll para mostrar siempre el último mensaje
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                }
            }
        }
    }
}
