using ContactoDb.Models;
using System.Data.SqlClient;
using System.Data;
namespace ContactoDb.Datos
{
    public class ContactoDatos
    {
        public List<ContactoModel> ListarContacto()
        {
            List<ContactoModel>lista=new List<ContactoModel>();
            var conexion= new Conexion();
            using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
            {
                conexion1.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarContacto", conexion1);
                cmd.CommandType = CommandType.StoredProcedure;
                using(var dr=cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        lista.Add(new ContactoModel()
                        {
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    } 
                }
            }

                return lista;
        }
        public ContactoModel ObtenerContacto(int IdContacto)
        {
            ContactoModel contacto= new ContactoModel();
            var conexion = new Conexion();
            using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
            {
                conexion1.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion1);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        contacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        contacto.Nombre = dr["Nombre"].ToString();
                        contacto.Telefono = dr["Telefono"].ToString();
                        contacto.Clave = dr["Clave"].ToString();
                        contacto.Correo = dr["Correo"].ToString();
                    }
                }
            }

            return contacto;
        }

        public bool GuardarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using(var conexion= new SqlConnection(cn.CadenaSql())){
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarContacto", conexion);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch(Exception ex)
            {
                string error= ex.Message;
                respuesta= false;
            }
            return respuesta;
        }
        /*hola*/
        public bool EditarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarContacto", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool EliminarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarContacto", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }



    }
}
