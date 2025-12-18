using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BodeX.DAL;

namespace BodeX
{
    public partial class AsignarProducto : Page
    {
        private DatabaseHelper db = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductos();
                LoadUbicaciones();
            }
        }

        #region Cargar Datos

        /// <summary>
        /// Carga todos los productos en el DropDownList
        /// </summary>
        private void LoadProductos()
        {
            try
            {
                string query = "SELECT ProductoID, Codigo, Nombre FROM Productos ORDER BY Nombre";

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

                ddlProducto.DataSource = dt;
                ddlProducto.DataTextField = "Nombre";
                ddlProducto.DataValueField = "ProductoID";
                ddlProducto.DataBind();

                ddlProducto.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar productos: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Carga todas las ubicaciones en el DropDownList
        /// </summary>
        private void LoadUbicaciones()
        {
            try
            {
                DataTable dt = db.GetAllUbicaciones();

                ddlUbicacion.Items.Clear();
                ddlUbicacion.Items.Add(new ListItem("-- Seleccione una ubicación --", "0"));

                foreach (DataRow row in dt.Rows)
                {
                    string codigo = row["CodigoUbicacion"].ToString();
                    string bodega = row["NombreBodega"].ToString();
                    int disponible = Convert.ToInt32(row["Disponible"]);

                    string text = $"{codigo} - {bodega} (Disponible: {disponible})";
                    string value = row["UbicacionID"].ToString();

                    ddlUbicacion.Items.Add(new ListItem(text, value));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar ubicaciones: " + ex.Message, "danger");
            }
        }

        #endregion

        #region Eventos DropDownList

        /// <summary>
        /// Muestra información del producto seleccionado
        /// </summary>
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProducto.SelectedValue != "0")
            {
                LoadProductoInfo(Convert.ToInt32(ddlProducto.SelectedValue));
            }
            else
            {
                pnlProductoInfo.Visible = false;
            }
        }

        /// <summary>
        /// Muestra información de la ubicación seleccionada
        /// </summary>
        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUbicacion.SelectedValue != "0")
            {
                LoadUbicacionInfo(Convert.ToInt32(ddlUbicacion.SelectedValue));
            }
            else
            {
                pnlUbicacionInfo.Visible = false;
            }
        }

        #endregion

        #region Cargar Información Detallada

        /// <summary>
        /// Carga y muestra la información detallada del producto
        /// </summary>
        private void LoadProductoInfo(int productoID)
        {
            try
            {
                string query = @"SELECT p.Codigo, p.Nombre, c.Nombre AS Categoria, 
                                p.PrecioUnitario, p.UnidadMedida
                                FROM Productos p
                                LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID
                                WHERE p.ProductoID = @ProductoID";

                DataTable dt = new DataTable();
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["BodeXConnection"].ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblProductoCodigo.Text = row["Codigo"].ToString();
                    lblProductoNombre.Text = row["Nombre"].ToString();
                    lblProductoCategoria.Text = string.IsNullOrEmpty(row["Categoria"].ToString())
                        ? "Sin categoría"
                        : row["Categoria"].ToString();

                    if (row["PrecioUnitario"] != DBNull.Value)
                        lblProductoPrecio.Text = String.Format("${0:N2}", row["PrecioUnitario"]);
                    else
                        lblProductoPrecio.Text = "No definido";

                    pnlProductoInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar información del producto: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Carga y muestra la información detallada de la ubicación
        /// </summary>
        private void LoadUbicacionInfo(int ubicacionID)
        {
            try
            {
                DataTable dt = db.GetUbicacionByID(ubicacionID);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblUbicacionCodigo.Text = row["CodigoUbicacion"].ToString();
                    lblUbicacionBodega.Text = row["NombreBodega"].ToString();

                    int capacidad = Convert.ToInt32(row["Capacidad"]);
                    int ocupado = Convert.ToInt32(row["Ocupado"]);
                    int disponible = capacidad - ocupado;

                    lblUbicacionCapacidad.Text = capacidad.ToString();
                    lblUbicacionOcupado.Text = ocupado.ToString();
                    lblUbicacionDisponible.Text = disponible.ToString();

                    // Calcular porcentaje
                    double porcentaje = capacidad > 0 ? (ocupado * 100.0 / capacidad) : 0;
                    lblPorcentajeOcupacion.Text = $"{porcentaje:F1}%";

                    // Establecer el ancho de la barra
                    capacityFill.Style["width"] = $"{porcentaje}%";

                    // Cambiar color según ocupación
                    if (porcentaje >= 80)
                        capacityFill.Style["background"] = "linear-gradient(135deg, #dc3545 0%, #c82333 100%)";
                    else if (porcentaje >= 50)
                        capacityFill.Style["background"] = "linear-gradient(135deg, #ffc107 0%, #e0a800 100%)";
                    else
                        capacityFill.Style["background"] = "linear-gradient(135deg, #28a745 0%, #218838 100%)";

                    pnlUbicacionInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar información de la ubicación: " + ex.Message, "danger");
            }
        }

        #endregion

        #region Eventos de Botones

        /// <summary>
        /// Asigna el producto a la ubicación
        /// </summary>
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int productoID = Convert.ToInt32(ddlProducto.SelectedValue);
                int ubicacionID = Convert.ToInt32(ddlUbicacion.SelectedValue);
                int cantidad = Convert.ToInt32(txtCantidad.Text);

                string result = db.AsignarProductoUbicacion(productoID, ubicacionID, cantidad);

                if (result == "SUCCESS")
                {
                    ShowMessage("Producto asignado exitosamente a la ubicación", "success");

                    // Recargar información
                    LoadUbicaciones();
                    if (ddlUbicacion.SelectedValue != "0")
                    {
                        LoadUbicacionInfo(ubicacionID);
                    }
                }
                else
                {
                    ShowMessage(result, "danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Limpia el formulario
        /// </summary>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlProducto.SelectedIndex = 0;
            ddlUbicacion.SelectedIndex = 0;
            txtCantidad.Text = "1";
            pnlProductoInfo.Visible = false;
            pnlUbicacionInfo.Visible = false;
            pnlMessage.Visible = false;
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Muestra un mensaje de alerta
        /// </summary>
        private void ShowMessage(string message, string type)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;

            if (type == "success")
            {
                divMessage.Attributes["class"] = "alert alert-success";
            }
            else if (type == "danger")
            {
                divMessage.Attributes["class"] = "alert alert-danger";
            }

            // Ocultar el mensaje después de 5 segundos
            ScriptManager.RegisterStartupScript(this, GetType(), "HideMessage",
                $"setTimeout(function(){{ document.getElementById('{pnlMessage.ClientID}').style.display='none'; }}, 5000);", true);
        }

        #endregion
    }
}