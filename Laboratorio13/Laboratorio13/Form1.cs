using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorio13
{
    public partial class Form1 : Form
    {
        // Cadena de conexión
        string connectionString =
            @"Server=.\SQLEXPRESS;Database=Northwind;TrustServerCertificate=True;Integrated Security=SSPI;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            // Limpiar el ListBox antes de cargar los datos
            listDBO.Items.Clear();

            // Crear la conexión
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Crear el comando SQL
                    string query = "SELECT ProductName FROM [dbo].[Products]";
                    SqlCommand comando = new SqlCommand(query, conexion);

                    // Ejecutar el comando y leer los datos
                    SqlDataReader lector = comando.ExecuteReader();

                    // Recorrer los resultados
                    while (lector.Read())
                    {
                        // Agregar el nombre del producto al ListBox
                        listDBO.Items.Add(lector["ProductName"].ToString());
                    }

                    lector.Close();
                    MessageBox.Show("Productos cargados correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void listDBO_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

