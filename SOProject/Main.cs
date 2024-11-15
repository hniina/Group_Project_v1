using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace SOProject
{
    public partial class Main : Form
    {
        Socket server;
        private Thread atender;
        public Main(Socket s)
        {
            InitializeComponent();

            this.server = s;
            CheckForIllegalCrossThreadCalls = false;
            ThreadStart ts = delegate { AtenderServidor(); };

            atender = new Thread(ts);
            atender.Start();
        }

        private void signupbutton_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp(server);
            this.Hide();
            signUp.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string soutput = "2/" + usernameText.Text + "/" + passwordText.Text;
            byte[] output = System.Text.Encoding.ASCII.GetBytes("2/" + usernameText.Text + "/" + passwordText.Text);
            server.Send(output);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void AtenderServidor() //receive ALL the messages from the server!!
        {
            while (true)
            {
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string message = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = message.Split('/');
                string codigo = (trozos[0]);
                string mensaje = trozos[1];
                switch (codigo) 
                { 
                    
                    case "6":
                        switch (mensaje)
                        {
                            case "0":

                                MessageBox.Show("The userame does not exist.");
                                break;

                            case "1":
                                MessageBox.Show("Login error. Please, try again.");
                                break;

                            case "2":
                                MessageBox.Show("The password is not correct.");
                                break;

                            case "3":

                                MessageBox.Show("Login successful");
                                Queries q = new Queries(server);
                                q.ShowDialog();
                                atender.Abort();
                                break;
                        }
                        break;

                }
            }
        }
    }
}

