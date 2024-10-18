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

        private void NewGame_Load(object sender, EventArgs e)
        {
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
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InicializarTablero(DataGridView tablero)
        {
            tablero.ColumnCount = 9;
            tablero.RowCount = 9;

            // Adjust cells
            foreach (DataGridViewColumn col in tablero.Columns)
            {
                col.Width = 30; //Columns
            }

            foreach (DataGridViewRow row in tablero.Rows)
            {
                row.Height = 30; //Rows
            }

            // Fill the gaps with water 
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    tablero[j, i].Value = "~"; // Water
                }
            }

        }
        private void lblEstadoJuego_Click(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            InicializarTablero(dataGridView1);
            InicializarTablero(dataGridView2);
            lblEstadoJuego.Text = "Game started!";
  
        }
    }
}
