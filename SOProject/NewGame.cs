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

        //Punto de referencia para arrastrar los barcos sobre el tablero local
        private Point firstPoint = new Point();
        private Point lastPoint = new Point();

        public NewGame(Socket s)
        {
            InitializeComponent();

            this.server = s;
        }

        private void NewGame_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            InitializeComponent();

            // Configura el PictureBox para ser arrastrado
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);

            // Configura el Panel para aceptar el Drop
            panel1.DragEnter += new DragEventHandler(panel1_DragEnter);
            panel1.DragDrop += new DragEventHandler(panel1_DragDrop);

            // Habilitar el panel para que acepte drop
            panel1.AllowDrop = true;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop(ship.Image, DragDropEffects.Move);
            }
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

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            // Mover el barco mientras mantienes presionado el botón izquierdo
            pictureBox1.MouseMove += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    // Calcular el nuevo lugar para el PictureBox
                    pictureBox1.Location = new Point(
                        pictureBox1.Location.X + (temp.X - firstPoint.X),
                        pictureBox1.Location.Y + (temp.Y - firstPoint.Y));
                    firstPoint = temp; // Actualiza la posición inicial
                }
            };
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            // Verifica que el objeto arrastrado es un PictureBox
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                // Permite la operación de soltar (mover)
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                // Si no es el objeto correcto, no permite la operación
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)e.Data.GetData(typeof(PictureBox));

            // Obtener la posición donde fue soltado dentro del panel
            Point dropLocation = panel1.PointToClient(new Point(e.X, e.Y));

            // Tamaño de la celda del tablero
            int cellSize = 40;

            // Ajustar la posición a la celda más cercana (alinear)
            int newX = (dropLocation.X / cellSize) * cellSize;
            int newY = (dropLocation.Y / cellSize) * cellSize;

            // Mover el PictureBox a la nueva ubicación ajustada
            pictureBox.Location = new Point(newX, newY);

            // Añadir el PictureBox al panel si no está ya dentro
            if (!panel1.Controls.Contains(pictureBox))
            {
                panel1.Controls.Add(pictureBox);
            }
        }
    }
}
