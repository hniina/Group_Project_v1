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
using System.Security.Cryptography.X509Certificates;

namespace SOProject
{
    public partial class Main : Form
    {
        Socket server;
        Thread atender;


        delegate void DelegadoParaPonerTexto(string texto);

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

            MessageBox.Show("Disconnected");
            this.Close();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string soutput = "2/" + usernameText.Text + "/" + passwordText.Text;
            byte[] output = System.Text.Encoding.ASCII.GetBytes("2/" + usernameText.Text + "/" + passwordText.Text);

            MessageBox.Show("Enviando mensaje: " + soutput);  // Debugging line
            server.Send(output);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }

        public void LogIn(string message)
        {
            switch (message)
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
                    ThreadStart ts = delegate { PonerEnMarchaForm(); };
                    Thread T = new Thread(ts);
                    T.Start();
                    break;
            }
        }

        public void PonerEnMarchaForm()
        {
                Queries q = new Queries(server);
                q.ShowDialog();
        }
    }
}


