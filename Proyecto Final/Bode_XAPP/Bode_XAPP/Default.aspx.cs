using System;
using System.Data;
using System.Web.UI;
using BodeX.DAL;

namespace BodeX
{
    public partial class _Default : Page
    {
        private DatabaseHelper db = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
                TestDatabaseConnection();
            }
        }

        /// <summary>
        /// Carga los datos estadísticos del dashboard
        /// </summary>
        private void LoadDashboardData()
        {
            try
            {
                // Obtener estadísticas de ubicaciones
                DataTable dtUbicaciones = db.GetAllUbicaciones();

                if (dtUbicaciones != null && dtUbicaciones.Rows.Count > 0)
                {
                    lblTotalUbicaciones.Text = dtUbicaciones.Rows.Count.ToString();

                    // Calcular capacidad total y ocupación
                    int capacidadTotal = 0;
                    int ocupadoTotal = 0;

                    foreach (DataRow row in dtUbicaciones.Rows)
                    {
                        capacidadTotal += Convert.ToInt32(row["Capacidad"]);
                        ocupadoTotal += Convert.ToInt32(row["Ocupado"]);
                    }

                    lblCapacidadTotal.Text = capacidadTotal.ToString();

                    // Calcular porcentaje de ocupación promedio
                    if (capacidadTotal > 0)
                    {
                        double porcentajeOcupacion = (ocupadoTotal * 100.0) / capacidadTotal;
                        lblOcupacionPromedio.Text = porcentajeOcupacion.ToString("F1") + "%";
                    }
                    else
                    {
                        lblOcupacionPromedio.Text = "0%";
                    }
                }
                else
                {
                    lblTotalUbicaciones.Text = "0";
                    lblCapacidadTotal.Text = "0";
                    lblOcupacionPromedio.Text = "0%";
                }

                // Obtener estadísticas de productos
                DataTable dtProductos = db.GetProductosWithUbicaciones();

                if (dtProductos != null)
                {
                    // Contar productos únicos
                    DataView dv = new DataView(dtProductos);
                    DataTable distinctProducts = dv.ToTable(true, "ProductoID");
                    lblTotalProductos.Text = distinctProducts.Rows.Count.ToString();
                }
                else
                {
                    lblTotalProductos.Text = "0";
                }

                // Mostrar fecha actual
                lblFechaActual.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar valores por defecto
                lblTotalUbicaciones.Text = "Error";
                lblTotalProductos.Text = "Error";
                lblCapacidadTotal.Text = "Error";
                lblOcupacionPromedio.Text = "Error";

                // Opcional: Registrar el error
                System.Diagnostics.Debug.WriteLine("Error al cargar dashboard: " + ex.Message);
            }
        }

        /// <summary>
        /// Prueba la conexión a la base de datos y muestra el resultado
        /// </summary>
        private void TestDatabaseConnection()
        {
            try
            {
                bool connectionOk = db.TestConnection();

                pnlConnectionStatus.Visible = true;

                if (connectionOk)
                {
                    pnlConnectionStatus.CssClass = "connection-status connection-success";
                    lblConnectionStatus.Text = "✅ Conexión a base de datos exitosa";
                }
                else
                {
                    pnlConnectionStatus.CssClass = "connection-status connection-error";
                    lblConnectionStatus.Text = "❌ Error de conexión a la base de datos";
                }
            }
            catch (Exception ex)
            {
                pnlConnectionStatus.Visible = true;
                pnlConnectionStatus.CssClass = "connection-status connection-error";
                lblConnectionStatus.Text = "❌ Error: " + ex.Message;
            }
        }
    }
}