using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Form1 : Form
    {

        string connectionString =
            @"Server=.\SQLEXPRESS;Database=HistorialDeMedidas;TrustServerCertificate=True;Integrated Security=SSPI;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnYardas_Click(object sender, EventArgs e)
        {
            try
            {
                double m = double.Parse(txtMetros1.Text);
                double y = m * 39.370;
                txtYardas1.Text = y.ToString("0.####");

                
                GuardarConversion(m.ToString(), y.ToString("0.####"));
            }
            catch
            {
                MessageBox.Show("Por favor, ingrese un número válido en metros.");
            }
        }

        private void btnMetros_Click(object sender, EventArgs e)
        {
            try
            {
                double y = double.Parse(txtYardas2.Text);
                double m = y / 39.370;
                txtMetros2.Text = m.ToString("0.####");

                // aqui uso el metodo de guardado
                GuardarConversion(y.ToString(), m.ToString("0.####"));
            }
            catch
            {
                MessageBox.Show("Por favor, ingrese un número válido en yardas.");
            }
        }

        // Aqui el metodo donde guardo en la base de datos
        private void GuardarConversion(string valorAnterior, string conversion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Respuestas_Guardadas (Valor_Anterior, Conversion) VALUES (@valorAnterior, @conversion)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@valorAnterior", valorAnterior);
                    cmd.Parameters.AddWithValue("@conversion", conversion);

                    cmd.ExecuteNonQuery();
                }
            }
            catch 
            {
                MessageBox.Show("Error al guardar en la base de datos: ");
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Respuestas_Guardadas";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);


                    Historial.Items.Clear();

                    if (tabla.Rows.Count == 0)
                    {
                        Historial.Items.Add("No hay datos guardados.");
                    }
                    else
                    {
                        foreach (DataRow fila in tabla.Rows)
                        {
                            Historial.Items.Add($"Valor anterior: {fila["Valor_Anterior"]}  Conversión: {fila["Conversion"]}\n");
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Error al mostrar los datos: ");
            }
        }


        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtMetros1.Text = txtMetros2.Text = txtYardas1.Text = txtYardas2.Text = string.Empty;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
