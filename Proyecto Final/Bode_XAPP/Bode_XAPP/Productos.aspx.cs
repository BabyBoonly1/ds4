using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using BodeX.DAL;

namespace BodeX
{
    public partial class Productos : Page
    {
        private DatabaseHelper db = new DatabaseHelper();
        private DataTable dtProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                LoadProductos();
                CalculateSummary();
            }
        }

        #region Cargar Datos

        /// <summary>
        /// Carga los filtros de búsqueda
        /// </summary>
        private void LoadFilters()
        {
            try
            {
                // Cargar categorías
                LoadCategorias();

                // Cargar bodegas
                LoadBodegas();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar filtros: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga las categorías en el DropDownList
        /// </summary>
        private void LoadCategorias()
        {
            try
            {
                string query = "SELECT DISTINCT CategoriaID, Categoria FROM (" +
                              "SELECT c.CategoriaID, c.Nombre AS Categoria " +
                              "FROM Categorias c WHERE c.Activo = 1" +
                              ") AS Categorias ORDER BY Categoria";

                DataTable dt = new DataTable();
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["BodeXConnection"].ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                    }
                }

                ddlCategoria.DataSource = dt;
                ddlCategoria.DataTextField = "Categoria";
                ddlCategoria.DataValueField = "CategoriaID";
                ddlCategoria.DataBind();

                ddlCategoria.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las categorías --", "0"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar categorías: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga las bodegas en el DropDownList
        /// </summary>
        private void LoadBodegas()
        {
            try
            {
                DataTable dt = db.GetAllBodegas();

                ddlBodega.DataSource = dt;
                ddlBodega.DataTextField = "Nombre";
                ddlBodega.DataValueField = "BodegaID";
                ddlBodega.DataBind();

                ddlBodega.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las bodegas --", "0"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar bodegas: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga todos los productos con sus ubicaciones
        /// </summary>
        private void LoadProductos()
        {
            try
            {
                dtProductos = db.GetProductosWithUbicaciones();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar productos: " + ex.Message);
            }
        }

        #endregion

        #region Filtrado

        /// <summary>
        /// Aplica los filtros seleccionados al DataTable
        /// </summary>
        private void ApplyFilters()
        {
            try
            {
                if (dtProductos == null)
                    return;

                DataView dv = new DataView(dtProductos);
                string filter = "1=1"; // Sin filtro de activo

                // Filtro por búsqueda de texto
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string searchText = txtBuscar.Text.Trim().Replace("'", "''");
                    filter += $" AND (NombreProducto LIKE '%{searchText}%' OR Codigo LIKE '%{searchText}%')";
                }

                // Filtro por categoría
                if (ddlCategoria.SelectedValue != "0")
                {
                    filter += $" AND Categoria = '{ddlCategoria.SelectedItem.Text.Replace("'", "''")}'";
                }

                // Filtro por bodega
                if (ddlBodega.SelectedValue != "0")
                {
                    filter += $" AND NombreBodega = '{ddlBodega.SelectedItem.Text.Replace("'", "''")}'";
                }

                dv.RowFilter = filter;
                gvProductos.DataSource = dv;
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al aplicar filtros: " + ex.Message);
                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();
            }
        }

        /// <summary>
        /// Evento al hacer clic en el botón Filtrar
        /// </summary>
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            LoadProductos();
            CalculateSummary();
        }

        #endregion

        #region Eventos del GridView

        /// <summary>
        /// Maneja los comandos del GridView
        /// </summary>
        protected void gvProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarAsignacion")
            {
                int inventarioID = Convert.ToInt32(e.CommandArgument);
                EliminarAsignacion(inventarioID);
            }
        }

        /// <summary>
        /// Elimina una asignación de producto a ubicación
        /// </summary>
        private void EliminarAsignacion(int inventarioID)
        {
            try
            {
                string result = db.EliminarAsignacion(inventarioID);

                if (result == "SUCCESS")
                {
                    LoadProductos();
                    CalculateSummary();
                    System.Diagnostics.Debug.WriteLine("Asignación eliminada exitosamente");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Error al eliminar asignación: " + result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al eliminar asignación: " + ex.Message);
            }
        }

        #endregion

        #region Resumen

        /// <summary>
        /// Calcula y muestra el resumen de productos
        /// </summary>
        private void CalculateSummary()
        {
            try
            {
                if (dtProductos == null || dtProductos.Rows.Count == 0)
                {
                    lblTotalProductos.Text = "0";
                    lblProductosConUbicacion.Text = "0";
                    lblUnidadesTotales.Text = "0";
                    return;
                }

                // Total de productos únicos
                DataView dvUnique = new DataView(dtProductos);
                DataTable dtUnique = dvUnique.ToTable(true, "ProductoID");
                lblTotalProductos.Text = dtUnique.Rows.Count.ToString();

                // Productos con ubicación asignada
                int productosConUbicacion = dtProductos.AsEnumerable()
                    .Where(row => !string.IsNullOrEmpty(row["CodigoUbicacion"].ToString()))
                    .Select(row => row["ProductoID"])
                    .Distinct()
                    .Count();
                lblProductosConUbicacion.Text = productosConUbicacion.ToString();

                // Total de unidades en inventario
                int unidadesTotales = dtProductos.AsEnumerable()
                    .Where(row => row["Cantidad"] != DBNull.Value)
                    .Sum(row => Convert.ToInt32(row["Cantidad"]));
                lblUnidadesTotales.Text = unidadesTotales.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al calcular resumen: " + ex.Message);
                lblTotalProductos.Text = "Error";
                lblProductosConUbicacion.Text = "Error";
                lblUnidadesTotales.Text = "Error";
            }
        }

        #endregion
    }
}