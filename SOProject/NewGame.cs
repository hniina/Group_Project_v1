using System;
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

namespace SOProject
{
    public partial class NewGame : Form
    {

        Socket server;

        private const int CellSize = 25;
        // Margen del tablero (espacio para las letras y números en el borde)
        private const int BoardOffsetX = 25; // Offset para el margen izquierdo
        private const int BoardOffsetY = 25; // Offset para el margen superior
        private const int GridSize = 200;    // Tamaño total de la cuadrícula (8 celdas de 25 px)
        public string PlayerName { get; private set; }
        public string PlayerName2 { get; private set; }

        public NewGame(Socket s, string PlayerName1, string PlayerName2)
        {
            InitializeComponent();
            
            this.server = s;
            this.PlayerName = PlayerName1;
            this.PlayerName2 = PlayerName2;

            // Configurar el panel para permitir operaciones de soltar
            panel1.AllowDrop = true;

            // Conectar eventos para arrastrar y soltar en el panel
            panel1.DragEnter += Panel1_DragEnter;
            panel1.DragDrop += Panel1_DragDrop;

            // Configurar cada PictureBox (barco) para iniciar el arrastre
            pictureBox1.MouseDown += PictureBox_MouseDown;
            pictureBox1.MouseDown += RotatePictureBox;
            pictureBox2.MouseDown += PictureBox_MouseDown;
            pictureBox2.MouseDown += RotatePictureBox;
            pictureBox4.MouseDown += PictureBox_MouseDown;
            pictureBox4.MouseDown += RotatePictureBox;
            pictureBox5.MouseDown += PictureBox_MouseDown;
            pictureBox5.MouseDown += RotatePictureBox;
            pictureBox6.MouseDown += PictureBox_MouseDown;
            pictureBox6.MouseDown += RotatePictureBox;

            // Hacer que cada PictureBox permita arrastre
            pictureBox1.AllowDrop = true;
            pictureBox2.AllowDrop = true;
            pictureBox4.AllowDrop = true;
            pictureBox5.AllowDrop = true;
            pictureBox6.AllowDrop = true;

        }
        
        private void NewGame_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            string message = "9/" + PlayerName + "/start"; // to the server
            byte[] msg = Encoding.ASCII.GetBytes(message);
            server.Send(msg);
            MessageBox.Show("Waiting for the other player to connect...");
        

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;
            Pen mypen1 = new Pen(Color.Black);
            int i = 0;
            while (i<=225)
            {
                graphics.DrawLine(mypen1, Convert.ToSingle(0), Convert.ToSingle(i), Convert.ToSingle(225), Convert.ToSingle(i));
                i=i+25;
            }

            int j= 0;
            while (j <=225)
            {
                graphics.DrawLine(mypen1, Convert.ToSingle(j), Convert.ToSingle(0), Convert.ToSingle(j), Convert.ToSingle(225));
                j = j + 25;
            }
            Font font = new Font("Arial", 12);
            graphics.DrawString("A",font, Brushes.Black,0, 25);
            graphics.DrawString("B", font, Brushes.Black, 0, 50);
            graphics.DrawString("C", font, Brushes.Black, 0, 75);
            graphics.DrawString("D", font, Brushes.Black, 0, 100);
            graphics.DrawString("E", font, Brushes.Black, 0, 125);
            graphics.DrawString("F", font, Brushes.Black, 0, 150);
            graphics.DrawString("G", font, Brushes.Black, 0, 175);
            graphics.DrawString("H", font, Brushes.Black, 0, 200);

            graphics.DrawString("1", font, Brushes.Black, 25,0);
            graphics.DrawString("2", font, Brushes.Black, 50,0);
            graphics.DrawString("3", font, Brushes.Black, 75, 0);
            graphics.DrawString("4", font, Brushes.Black, 100,0);
            graphics.DrawString("5", font, Brushes.Black, 125,0);
            graphics.DrawString("6", font, Brushes.Black, 150,0);
            graphics.DrawString("7", font, Brushes.Black, 175,0);
            graphics.DrawString("8", font, Brushes.Black, 200,0);


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;
            Pen mypen1 = new Pen(Color.Black);
            int i = 0;
            while (i <= 225)
            {
                graphics.DrawLine(mypen1, Convert.ToSingle(0), Convert.ToSingle(i), Convert.ToSingle(225), Convert.ToSingle(i));
                i = i + 25;
            }

            int j = 0;
            while (j <= 225)
            {
                graphics.DrawLine(mypen1, Convert.ToSingle(j), Convert.ToSingle(0), Convert.ToSingle(j), Convert.ToSingle(225));
                j = j + 25;
            }

            Font font = new Font("Arial", 12);
            graphics.DrawString("A", font, Brushes.Black, 0, 25);
            graphics.DrawString("B", font, Brushes.Black, 0, 50);
            graphics.DrawString("C", font, Brushes.Black, 0, 75);
            graphics.DrawString("D", font, Brushes.Black, 0, 100);
            graphics.DrawString("E", font, Brushes.Black, 0, 125);
            graphics.DrawString("F", font, Brushes.Black, 0, 150);
            graphics.DrawString("G", font, Brushes.Black, 0, 175);
            graphics.DrawString("H", font, Brushes.Black, 0, 200);

            graphics.DrawString("1", font, Brushes.Black, 25, 0);
            graphics.DrawString("2", font, Brushes.Black, 50, 0);
            graphics.DrawString("3", font, Brushes.Black, 75, 0);
            graphics.DrawString("4", font, Brushes.Black, 100, 0);
            graphics.DrawString("5", font, Brushes.Black, 125, 0);
            graphics.DrawString("6", font, Brushes.Black, 150, 0);
            graphics.DrawString("7", font, Brushes.Black, 175, 0);
            graphics.DrawString("8", font, Brushes.Black, 200, 0);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Inicia el proceso de arrastre con el PictureBox actual
                PictureBox pictureBox = sender as PictureBox;
                pictureBox.DoDragDrop(pictureBox, DragDropEffects.Move);
            }
        }

        // Evento DragEnter del panel para permitir el arrastre
        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            // Verificar que el objeto arrastrado es un PictureBox
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move; // Permitir el movimiento
            }
            else
            {
                e.Effect = DragDropEffects.None; // No permitir otros tipos de objetos
            }
        }

        // Evento DragDrop del panel para soltar y ajustar el PictureBox en la cuadrícula
        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            // Obtener el PictureBox que fue soltado
            PictureBox pictureBox = (PictureBox)e.Data.GetData(typeof(PictureBox));

            // Convertir la posición del mouse al sistema de coordenadas del panel
            Point dropLocation = panel1.PointToClient(new Point(e.X, e.Y));

            // Ajustar la posición dentro de las celdas de la cuadrícula
            int adjustedX = ((dropLocation.X - BoardOffsetX) / CellSize) * CellSize + BoardOffsetX;
            int adjustedY = ((dropLocation.Y - BoardOffsetY) / CellSize) * CellSize + BoardOffsetY;

            // Chequear que la posición ajustada esté dentro del área del tablero
            if (adjustedX >= BoardOffsetX && adjustedX < BoardOffsetX + GridSize &&
                adjustedY >= BoardOffsetY && adjustedY < BoardOffsetY + GridSize)
            {
                // Centrar el barco en la celda
                int offsetX = (CellSize - pictureBox.Width) / 2;
                int offsetY = (CellSize - pictureBox.Height) / 2;

                // Establecer la posición final
                pictureBox.Location = new Point(adjustedX + offsetX, adjustedY + offsetY);

                // Agregar el PictureBox al panel si no está ya en él
                if (!panel1.Controls.Contains(pictureBox))
                {
                    panel1.Controls.Add(pictureBox);
                }
            }
            else
            {
                // Opción: mensaje si el barco se coloca fuera de los límites
                MessageBox.Show("¡Coloca el barco dentro de la cuadrícula!");
            }
        }

        private void RotatePictureBox(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Verificar si es clic derecho
            {
                PictureBox pictureBox = sender as PictureBox;

                // Intercambiar el ancho y el alto para rotar
                int temp = pictureBox.Width;
                pictureBox.Width = pictureBox.Height;
                pictureBox.Height = temp;

                pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            }
        }

    }

}

