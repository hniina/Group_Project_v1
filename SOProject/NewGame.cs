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
        public NewGame(Socket s)
        {
            InitializeComponent();

            this.server = s;
        }

        private void HabilitarArrastrarBarcos()
        {
            pictureBox1.MouseDown += Ship_MouseDown;
            pictureBox2.MouseDown += Ship_MouseDown;
            pictureBox3.MouseDown += Ship_MouseDown;
            pictureBox4.MouseDown += Ship_MouseDown;
            pictureBox5.MouseDown += Ship_MouseDown;
            pictureBox6.MouseDown += Ship_MouseDown;
        }

        private void Ship_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            ship.DoDragDrop(ship, DragDropEffects.Move);
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // Obtener el PictureBox que se está arrastrando
            PictureBox barco = (PictureBox)e.Data.GetData(typeof(PictureBox));

            // Obtener la posición en el DataGridView
            Point dropPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = dataGridView1.HitTest(dropPoint.X, dropPoint.Y);

            if (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
            {
                // Colocar la imagen del barco en la celda correspondiente
                dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Value = barco.Image;
            }
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
            dataGridView1.RowCount=9;
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int barcosColocados = 0; 
            int totalBarcos = 5;

            if (barcosColocados < totalBarcos)
            {
                // Verificar si la celda ya tiene un barco ("B") para evitar colocar otro en el mismo lugar
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == null || dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString() != "B")
                {
                    // Colocar barco en la celda seleccionada
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "B"; // "B" representa un barco
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray; // Color para indicar el barco

                    // Incrementar la cantidad de barcos colocados
                    barcosColocados++;

                    // Verificar si se ha alcanzado el número máximo de barcos
                    if (barcosColocados == totalBarcos)
                    {
                        MessageBox.Show("Jugador 1 ha colocado todos sus barcos.");
                    }
                }
                else
                {
                    MessageBox.Show("Ya hay un barco en esta posición. Selecciona otra celda.");
                }
            }
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
            HabilitarArrastrarBarcos();


        }

        private void dataGridView1_DragDrop1(object sender, DragEventArgs e)
        {
            PictureBox barco = (PictureBox)e.Data.GetData(typeof(PictureBox));

            // Obtener la posición de la celda en la que se soltó el barco
            Point dropPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = dataGridView1.HitTest(dropPoint.X, dropPoint.Y);

            if (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
            {
                // Colocar la imagen del barco en la celda correspondiente
                dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Value = barco.Image;
            }
        }

       private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void pictureBox2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }
       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox ship = sender as PictureBox;
            {
                ship.DoDragDrop (ship.Image, DragDropEffects.Move); 
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
    }
}
