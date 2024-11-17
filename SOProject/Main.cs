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
        Thread atender;

        delegate void DelegadoParaPonerTexto(string texto);

        List<Queries> formularios = new List<Queries>();
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
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[1024];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                string codigo =(trozos[0]);
                string mensaje;
                int nform;

                switch (codigo) 
                {
                    case "1": //query 1
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].query1(mensaje);
                        break;

                    case "2":
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].query2(mensaje);
                        break;
                    case "3":
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].query3(mensaje);
                        break;

                    case "4":
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].ConnectedList(mensaje);
                        break;


                    case "6":
                        mensaje = trozos[1];
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
                                ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
                                Thread T = new Thread(ts);
                                T.Start();
                                break;
                        }
                        break;

                }
            }
        }

        private void PonerEnMarchaFormulario()
        {
            int cont=formularios.Count;
            Queries q = new Queries(cont,server);
            q.ShowDialog();
            formularios.Add(q);
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }
    }
}

