using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BodeX.DAL
{
    /// <summary>
    /// Clase helper para manejar todas las operaciones de base de datos
    /// </summary>
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            // Obtener la cadena de conexión del Web.config
            connectionString = ConfigurationManager.ConnectionStrings["BodeXConnection"].ConnectionString;
        }

        #region Métodos Genéricos

        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna un DataTable
        /// </summary>
        public DataTable ExecuteDataTable(string storedProcedure, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros si existen
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento: " + storedProcedure, ex);
            }

            return dt;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado sin retornar datos (INSERT, UPDATE, DELETE)
        /// </summary>
        public string ExecuteNonQuery(string storedProcedure, SqlParameter[] parameters = null)
        {
            string result = "SUCCESS";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                result = "ERROR: " + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna un valor escalar
        /// </summary>
        public object ExecuteScalar(string storedProcedure, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento: " + storedProcedure, ex);
            }

            return result;
        }

        #endregion

        #region Métodos para Ubicaciones

        /// <summary>
        /// Obtiene todas las ubicaciones activas
        /// </summary>
        public DataTable GetAllUbicaciones()
        {
            return ExecuteDataTable("sp_Ubicaciones_GetAll");
        }

        /// <summary>
        /// Obtiene una ubicación por su ID
        /// </summary>
        public DataTable GetUbicacionByID(int ubicacionID)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@UbicacionID", ubicacionID)
            };

            return ExecuteDataTable("sp_Ubicaciones_GetByID", parameters);
        }

        /// <summary>
        /// Inserta una nueva ubicación
        /// </summary>
        public string InsertUbicacion(int bodegaID, string pasillo, string estante, string nivel, int capacidad)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@BodegaID", bodegaID),
                new SqlParameter("@Pasillo", pasillo),
                new SqlParameter("@Estante", estante),
                new SqlParameter("@Nivel", nivel),
                new SqlParameter("@Capacidad", capacidad),
                new SqlParameter("@UbicacionID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Ubicaciones_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int newID = Convert.ToInt32(cmd.Parameters["@UbicacionID"].Value);
                        return "SUCCESS|" + newID;
                    }
                }
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        /// <summary>
        /// Actualiza una ubicación existente
        /// </summary>
        public string UpdateUbicacion(int ubicacionID, int bodegaID, string pasillo, string estante,
                                      string nivel, int capacidad)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@UbicacionID", ubicacionID),
                new SqlParameter("@BodegaID", bodegaID),
                new SqlParameter("@Pasillo", pasillo),
                new SqlParameter("@Estante", estante),
                new SqlParameter("@Nivel", nivel),
                new SqlParameter("@Capacidad", capacidad)
            };

            return ExecuteNonQuery("sp_Ubicaciones_Update", parameters);
        }

        /// <summary>
        /// Elimina (lógicamente) una ubicación
        /// </summary>
        public string DeleteUbicacion(int ubicacionID)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@UbicacionID", ubicacionID)
            };

            try
            {
                DataTable dt = ExecuteDataTable("sp_Ubicaciones_Delete", parameters);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Result"].ToString();
                }
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Métodos para Productos

        /// <summary>
        /// Obtiene todos los productos con sus ubicaciones
        /// </summary>
        public DataTable GetProductosWithUbicaciones()
        {
            return ExecuteDataTable("sp_Productos_GetWithUbicaciones");
        }

        /// <summary>
        /// Asigna un producto a una ubicación
        /// </summary>
        public string AsignarProductoUbicacion(int productoID, int ubicacionID, int cantidad)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ProductoID", productoID),
                new SqlParameter("@UbicacionID", ubicacionID),
                new SqlParameter("@Cantidad", cantidad)
            };

            try
            {
                DataTable dt = ExecuteDataTable("sp_Inventario_AsignarProducto", parameters);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Result"].ToString();
                }
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        /// <summary>
        /// Elimina una asignación de producto a ubicación
        /// </summary>
        public string EliminarAsignacion(int inventarioID)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@InventarioID", inventarioID)
            };

            try
            {
                DataTable dt = ExecuteDataTable("sp_Inventario_EliminarAsignacion", parameters);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Result"].ToString();
                }
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        #endregion

        #region Métodos para Bodegas

        /// <summary>
        /// Obtiene todas las bodegas
        /// </summary>
        public DataTable GetAllBodegas()
        {
            string query = "SELECT BodegaID, Nombre FROM Bodegas ORDER BY Nombre";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        #endregion

        #region Método de Prueba de Conexión

        /// <summary>
        /// Verifica si la conexión a la base de datos funciona
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}