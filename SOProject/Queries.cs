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

                if (PlayerGame.Checked)
                {
                    string message = "3/";

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);

                    //Answer from the server.
                    byte[] msg2 = new byte[512];
                    server.Receive(msg2);

                    string answer= Encoding.ASCII.GetString(msg2);
                    MessageBox.Show(answer);
                }

                if (winner.Checked)
                {
                    string message = "4/" + gameid.Text;
                    //We send the game ID
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    server.Send(msg);

                    //We get the server answer
                    byte[] msg2 = new byte[512];
                    server.Receive(msg2);

                    string answer = Encoding.ASCII.GetString(msg2);
                    MessageBox.Show(answer);

                }

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }
    }
}
