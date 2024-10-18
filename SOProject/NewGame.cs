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
            dataGridView1.ColumnCount = 10;
            dataGridView1.RowCount=10;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            dataGridView2.ColumnCount = 10;
            dataGridView2.RowCount = 10;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;
                    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InicializarTablero(DataGridView tablero)
        {
            tablero.ColumnCount = 10;
            tablero.RowCount = 10;

            // Ajustar tamaño de las celdas
            foreach (DataGridViewColumn col in tablero.Columns)
            {
                col.Width = 30; // Tamaño de las columnas
            }

            foreach (DataGridViewRow row in tablero.Rows)
            {
                row.Height = 30; // Tamaño de las filas
            }

            // Llenar las celdas con un valor predeterminado (agua)
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tablero[j, i].Value = "~"; // Representa agua
                }
            }

        }

        private void dataGridViewOponente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtener las coordenadas de la celda seleccionada
            int fila = e.RowIndex;
            int columna = e.ColumnIndex;

            // Simular un disparo
            Disparar(fila, columna);
        }

        private void Disparar(int fila, int columna)
        {
            // Lógica del juego para manejar un disparo
            if (dataGridView2[columna, fila].Value.ToString() == "~") // Agua
            {
                dataGridView2[columna, fila].Value = "O"; // Fallo
                lblEstadoJuego.Text = "Fallo!";
            }
            else if (dataGridView2[columna, fila].Value.ToString() == "B") // Barco
            {
                dataGridView2[columna, fila].Value = "X"; // Acertado
                lblEstadoJuego.Text = "Acertaste!";
            }
        }

        private void ColocarBarcos()
        {
            Random rand = new Random();
            // Ejemplo de colocación de un barco de 4 casillas
            for (int i = 0; i < 4; i++)
            {
                int fila = rand.Next(0, 10);
                int columna = rand.Next(0, 10);
                dataGridView1[columna, fila].Value = "B"; // "B" para Barco
            }
        }

        private void lblEstadoJuego_Click(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            InicializarTablero(dataGridView1);
            InicializarTablero(dataGridView2);
            ColocarBarcos();
            lblEstadoJuego.Text = "Game started!";
  
        }
    }
}
