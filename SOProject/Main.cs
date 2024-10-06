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
    public partial class Main : Form
    {
        Socket server;
        public Main(Socket s)
        {
            InitializeComponent();

            this.server = s;
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
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            byte[] output = System.Text.Encoding.ASCII.GetBytes("2/" + usernameText + '/' + passwordText.Text);
            server.Send(output);

            byte[] input = new byte[1024];
            server.Receive(input);


            switch (Encoding.ASCII.GetString(input, 0, 1))
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
                    this.Hide();
                    q.ShowDialog();
                    break;

            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}

