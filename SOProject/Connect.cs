using System;
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
        Socket server;
        Thread atender;
        delegate void DelegadoParaPonerTexto(string texto);
        List<Main> formularios = new List<Main>();
        List<Queries> forms = new List<Queries>();
        public Connect()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50179);
            

            //We create the socket
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                server.Connect(ipep);
                MessageBox.Show("Connected");
                CheckForIllegalCrossThreadCalls = false;
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

                ThreadStart t1 = delegate { PonerEnMarchaFormulario(); };
                Thread T = new Thread(t1);
                T.Start();

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

        private void AtenderServidor() //receive ALL the messages from the server!!
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[512];
                int receivedbytes = server.Receive(msg2);
                if (receivedbytes == 0)
                {
                    Console.WriteLine("No data received."); //Making sure the message is not empty;
                    return;
                }
                string message = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = message.Split('/');
                string codigo = trozos[0];
                string mensaje;
                Queries q = new Queries(server);

                switch (codigo)
                {
                    case "1": //query 1
                        mensaje = trozos[2].Split('\0')[0];
                        q.query1(mensaje);
                        break;

                    case "2": //query 2
                        mensaje = trozos[2].Split('\0')[0];
                        q.query2(mensaje);
                        break;
                    case "3": //query 3
                        mensaje = trozos[1].Split('\0')[0];
                        q.query3(mensaje);
                        break;

                    case "4": //Connected List (not working yet)
                        mensaje = trozos[1].Split('\0')[0];
                        break;


                    case "6": //Login
                        Main m = new Main(server);
                        m.LogIn(trozos[1]);
                        break;
                }

            }
        }
        

        private void PonerEnMarchaFormulario()
        {
            Main m = new Main (server);
            m.ShowDialog();
            formularios.Add(m);
        }

    }
}

