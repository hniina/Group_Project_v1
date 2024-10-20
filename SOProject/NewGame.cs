using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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

            pictureBox1.AllowDrop = true;
            pictureBox2.AllowDrop = true;
            pictureBox3.AllowDrop = true;
            pictureBox4.AllowDrop = true;
            pictureBox5.AllowDrop = true;
            pictureBox6.AllowDrop = true;

            dataGridView1.ColumnCount = 9;
            dataGridView1.RowCount = 9;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            dataGridView1[0, 1].Value = "A";
            dataGridView1[0, 2].Value = "B";
            dataGridView1[0, 3].Value = "C";
            dataGridView1[0, 4].Value = "D";
            dataGridView1[0, 5].Value = "E";
            dataGridView1[0, 6].Value = "F";
            dataGridView1[0, 7].Value = "G";
            dataGridView1[0, 8].Value = "H";

            dataGridView1[1, 0].Value = "1";
            dataGridView1[2, 0].Value = "2";
            dataGridView1[3, 0].Value = "3";
            dataGridView1[4, 0].Value = "4";
            dataGridView1[5, 0].Value = "5";
            dataGridView1[6, 0].Value = "6";
            dataGridView1[7, 0].Value = "7";
            dataGridView1[8, 0].Value = "8";
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 50;
            }

            dataGridView1.RowTemplate.Height = 50;

            dataGridView2.ColumnCount = 9;
            dataGridView2.RowCount = 9;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;

            dataGridView2[0, 1].Value = "A";
            dataGridView2[0, 2].Value = "B";
            dataGridView2[0, 3].Value = "C";
            dataGridView2[0, 4].Value = "D";
            dataGridView2[0, 5].Value = "E";
            dataGridView2[0, 6].Value = "F";
            dataGridView2[0, 7].Value = "G";
            dataGridView2[0, 8].Value = "H";

            dataGridView2[1, 0].Value = "1";
            dataGridView2[2, 0].Value = "2";
            dataGridView2[3, 0].Value = "3";
            dataGridView2[4, 0].Value = "4";
            dataGridView2[5, 0].Value = "5";
            dataGridView2[6, 0].Value = "6";
            dataGridView2[7, 0].Value = "7";
            dataGridView2[8, 0].Value = "8";

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.Width = 50;
            }

            dataGridView2.RowTemplate.Height = 50;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.BackColor = Color.White;
            dataGridView2.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView2.BackColor = Color.White;
            SetShips(dataGridView1);

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

        private void SetShips(DataGridView dg)
        {
            // Rotar el barco de 90 grados al hacer clic derecho
            pictureBox1.MouseDown += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    firstPoint = Control.MousePosition;
                }
                else if (ee.Button == MouseButtons.Right)
                {
                    // Gira la imagen del barco
                    Image img = (Image)pictureBox1.Image.Clone(); // Clonar para evitar modificar el original
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pictureBox1.Image = img;
                }
            };

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

            // Evento para colocar el barco en el DataGridView
            pictureBox1.MouseUp += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    // Definir cuántas celdas ocupa el PictureBox
                    int columnasOcupadas = 3; // Cambia esto según el tamaño del barco
                    int filasOcupadas = 1; // Cambia esto según el tamaño del barco

                    // Calcular la posición final
                    Point posicionFinal = pictureBox1.Location;

                    // Obtener la celda más cercana
                    int columnaObjetivo = posicionFinal.X / dg.Columns[0].Width;
                    int filaObjetivo = posicionFinal.Y / dg.Rows[0].Height;

                    // Asegurarse de que el PictureBox no salga de los límites del DataGridView
                    if (columnaObjetivo + columnasOcupadas <= dg.ColumnCount && filaObjetivo + filasOcupadas <= dg.RowCount)
                    {
                        // Calcular la posición donde se debe colocar el PictureBox
                        pictureBox1.Location = new Point(columnaObjetivo * dg.Columns[0].Width, filaObjetivo * dg.Rows[0].Height);

                        // Ajustar el tamaño del PictureBox para que ocupe varias celdas
                        pictureBox1.Size = new Size(columnasOcupadas * dg.Columns[0].Width, filasOcupadas * dg.Rows[0].Height);
                    }
                    else
                    {
                        // Si no se puede colocar, vuelve a la última posición válida (opcional)
                        // pictureBox1.Location = lastPoint; 
                    }
                }
            };

            AddDragDropEvents(pictureBox2, dg, 2, 1);
            AddDragDropEvents(pictureBox3, dg, 4, 1);
            AddDragDropEvents(pictureBox4, dg, 3, 1);
            AddDragDropEvents(pictureBox5, dg, 5, 1);
            AddDragDropEvents(pictureBox6, dg, 1, 1);
        }

        private void AddDragDropEvents(PictureBox ship, DataGridView dg, int columnasOcupadas, int filasOcupadas)
        {
            ship.MouseDown += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left)
                {
                    firstPoint = Control.MousePosition;
                }
                else if (ee.Button == MouseButtons.Right)
                {
                    Image img = (Image)ship.Image.Clone();
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    ship.Image = img;
                }
            };

            ship.MouseMove += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    ship.Location = new Point(
                        ship.Location.X + (temp.X - firstPoint.X),
                        ship.Location.Y + (temp.Y - firstPoint.Y));
                    firstPoint = temp;
                }
            };

            ship.MouseUp += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left)
                {
                    Point posicionFinal = ship.Location;
                    int columnaObjetivo = posicionFinal.X / dg.Columns[0].Width;
                    int filaObjetivo = posicionFinal.Y / dg.Rows[0].Height;

                    if (columnaObjetivo + columnasOcupadas <= dg.ColumnCount && filaObjetivo + filasOcupadas <= dg.RowCount)
                    {
                        ship.Location = new Point(columnaObjetivo * dg.Columns[0].Width, filaObjetivo * dg.Rows[0].Height);
                        ship.Size = new Size(columnasOcupadas * dg.Columns[0].Width, filasOcupadas * dg.Rows[0].Height);
                    }
                }
            };
        }
    }
}
