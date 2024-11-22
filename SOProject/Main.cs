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
        Queries q;
        delegate void DelegadoParaPonerTexto(string texto);

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
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Disconnected");
            this.Close();
        }

        private void AtenderServidor() //receive ALL the messages from the server!!
        {
            try
            {
                while (true)
                {
                    //Recibimos mensaje del servidor
                    byte[] msg2 = new byte[1024];
                    server.Receive(msg2);
                    int receivedbytes = server.Receive(msg2);
                    if (receivedbytes == 0)
                    {
                        MessageBox.Show("No data received."); //Making sure the message is not empty;
                        return;
                    }
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    string codigo = (trozos[0]);
                    string mensaje; 

                    switch (codigo)
                    {
                        case "1": //query 1
                            mensaje=Encoding.ASCII.GetString(msg2).Trim('\0');
                            q.query1();
                            break;

                        case "2": //query 2
                            mensaje = trozos[1].Split('\0')[0];
                            q.query2(mensaje);
                            break;
                        case "3": //query 3
                            mensaje = trozos[1].Split('\0')[0];
                            q.query3(mensaje);
                            break;

                        case "4": //Connected List (not working yet)
                            mensaje = trozos[1].Split('\0')[0];
                            break;
                        case "5":
                            SignUp sg = new SignUp(server);
                            mensaje = trozos[1].Split('\0')[0];
                            sg.SignUpFunction(mensaje);
                            break;

                        case "6": //Login
                            mensaje= trozos[1].Split('\0')[0];
                            int code = Convert.ToInt32(mensaje);
                            switch (code)
                            {
                                case 0:
                                    MessageBox.Show("The userame does not exist.");
                                    break;
                                case 1:
                                    MessageBox.Show("The data was not found in the database");
                                    break;

                                case 2:
                                    MessageBox.Show("Login error. Please, try again.");
                                    break;

                                case 3:

                                    MessageBox.Show("Login successful");
                                    q = new Queries(server);
                                    q.ShowDialog();
                                    break;
                                case 4:
                                    MessageBox.Show("The password is not correct.");
                                    break;
                                default:
                                    MessageBox.Show(Convert.ToString(mensaje));
                                    break;
                            }
                            break;


                        default:
                            MessageBox.Show("Something went wrong :(");
                            break;
                    }

                }
            }

            catch (SocketException ex)
            {
                MessageBox.Show($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }
    }
}


