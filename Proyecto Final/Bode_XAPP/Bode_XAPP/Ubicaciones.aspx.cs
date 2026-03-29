using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BodeX.DAL;

namespace BodeX
{
    public partial class Ubicaciones : Page
    {
        private DatabaseHelper db = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBodegas();
                LoadUbicaciones();
            }
        }

        #region Cargar Datos

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

                // Agregar opción por defecto
                ddlBodega.Items.Insert(0, new ListItem("-- Seleccione una bodega --", "0"));
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar bodegas: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Carga todas las ubicaciones en el GridView
        /// </summary>
        private void LoadUbicaciones()
        {
            try
            {
                DataTable dt = db.GetAllUbicaciones();
                gvUbicaciones.DataSource = dt;
                gvUbicaciones.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar ubicaciones: " + ex.Message, "danger");
            }
        }

        #endregion

        #region Eventos de Botones

        /// <summary>
        /// Evento al hacer clic en Guardar (Crear o Actualizar)
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int ubicacionID = Convert.ToInt32(hfUbicacionID.Value);
                int bodegaID = Convert.ToInt32(ddlBodega.SelectedValue);
                string pasillo = txtPasillo.Text.Trim();
                string estante = txtEstante.Text.Trim();
                string nivel = txtNivel.Text.Trim();
                int capacidad = Convert.ToInt32(txtCapacidad.Text.Trim());

                string result;

                if (ubicacionID == 0)
                {
                    // CREAR nueva ubicación
                    result = db.InsertUbicacion(bodegaID, pasillo, estante, nivel, capacidad);

                    if (result.StartsWith("SUCCESS"))
                    {
                        ShowMessage("Ubicación creada exitosamente", "success");
                        ClearForm();
                    }
                    else
                    {
                        ShowMessage(result, "danger");
                    }
                }
                else
                {
                    // ACTUALIZAR ubicación existente
                    result = db.UpdateUbicacion(ubicacionID, bodegaID, pasillo, estante, nivel, capacidad);

                    if (result == "SUCCESS")
                    {
                        ShowMessage("Ubicación actualizada exitosamente", "success");
                        ClearForm();
                    }
                    else
                    {
                        ShowMessage(result, "danger");
                    }
                }

                LoadUbicaciones();
            }
            catch (Exception ex)
            {
                ShowMessage("❌ Error: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Evento al hacer clic en Cancelar
        /// </summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        #endregion

        #region Eventos del GridView

        /// <summary>
        /// Maneja los comandos del GridView (Editar, Eliminar)
        /// </summary>
        protected void gvUbicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ubicacionID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                LoadUbicacionForEdit(ubicacionID);
            }
            else if (e.CommandName == "Eliminar")
            {
                DeleteUbicacion(ubicacionID);
            }
        }

        /// <summary>
        /// Carga los datos de una ubicación para editarla
        /// </summary>
        private void LoadUbicacionForEdit(int ubicacionID)
        {
            try
            {
                DataTable dt = db.GetUbicacionByID(ubicacionID);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    hfUbicacionID.Value = ubicacionID.ToString();
                    ddlBodega.SelectedValue = row["BodegaID"].ToString();
                    txtPasillo.Text = row["Pasillo"].ToString();
                    txtEstante.Text = row["Estante"].ToString();
                    txtNivel.Text = row["Nivel"].ToString();
                    txtCapacidad.Text = row["Capacidad"].ToString();

                    lblFormTitle.Text = "Editar Ubicación";

                    // Scroll hacia el formulario
                    ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToForm",
                        "window.scrollTo(0, 0);", true);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error al cargar ubicación: " + ex.Message, "danger");
            }
        }

        /// <summary>
        /// Elimina una ubicación
        /// </summary>
        private void DeleteUbicacion(int ubicacionID)
        {
            try
            {
                string result = db.DeleteUbicacion(ubicacionID);

                if (result == "SUCCESS")
                {
                    ShowMessage("✅ Ubicación eliminada exitosamente", "success");
                    LoadUbicaciones();
                }
                else
                {
                    ShowMessage("❌ " + result, "danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("❌ Error al eliminar: " + ex.Message, "danger");
            }
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Limpia el formulario y lo resetea para nueva ubicación
        /// </summary>
        private void ClearForm()
        {
            hfUbicacionID.Value = "0";
            ddlBodega.SelectedIndex = 0;
            txtPasillo.Text = string.Empty;
            txtEstante.Text = string.Empty;
            txtNivel.Text = string.Empty;
            txtCapacidad.Text = string.Empty;
            lblFormTitle.Text = "Nueva Ubicación";
        }

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
                "setTimeout(function(){ document.getElementById('" + pnlMessage.ClientID + "').style.display='none'; }, 5000);", true);
        }

        #endregion
    }
}