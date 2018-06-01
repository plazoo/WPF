using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGOEntities;
using SGOUtil;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SGODataAccess
{
    public class DAUsuario
    {
        private String _strError;

        public DAUsuario()
        {
            _strError = String.Empty;
        }

        public String strError
        {
            get
            {
                return _strError;
            }
        }

        //Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public string getMd5Hash(string input)
        {
            // Create a new instance of the MD5 object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /***********************************************************************************************/


        public BEUsuario Validar_Usuario(BEUsuario oBE)
        {
            BEUsuario objBE = null;

            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGU)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_SelUsuarioLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string _passencript = getMd5Hash(oBE.vPassword);

                        cmd.Parameters.Add("@vUsuario", SqlDbType.VarChar).Value = oBE.vUsuario;                        
                        cmd.Parameters.Add("@vPassword", SqlDbType.VarChar).Value = _passencript;
                        cmd.Parameters.Add("@nIdSistema", SqlDbType.Int).Value = oBE.IdSistema;

                        using (SqlDataReader oReader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    objBE = new BEUsuario();
                                    objBE.IdUsuario = oReader.GetInt32(oReader.GetOrdinal("IdUsuario"));
                                    objBE.vNombre = oReader.GetString(oReader.GetOrdinal("vNombre"));
                                    objBE.vApePat = oReader.GetString(oReader.GetOrdinal("vApePat"));
                                    objBE.vApeMat = oReader.GetString(oReader.GetOrdinal("vApeMat"));
                                    objBE.vCorreo = oReader.GetString(oReader.GetOrdinal("vCorreo"));
                                    objBE.vUsuario = oReader.GetString(oReader.GetOrdinal("vUsuario"));
                                    objBE.vPassword = oReader.GetString(oReader.GetOrdinal("vPassword"));
                                    objBE.bCambioPassword = oReader.GetBoolean(oReader.GetOrdinal("bCambioPassword"));
                                    objBE.nEstado = oReader.GetInt32(oReader.GetOrdinal("nEstado"));
                                    objBE.IdRol = oReader.GetInt32(oReader.GetOrdinal("IdRol"));
                                    objBE.IdSistema = oReader.GetInt32(oReader.GetOrdinal("IdSistema"));
                                    objBE.vDescripcionRol = oReader.GetString(oReader.GetOrdinal("vDescripcion"));
                                    objBE.vIdJefatura = oReader.GetString(oReader.GetOrdinal("vIdJefatura"));
                                    objBE.vDescripcionJefatura = oReader.GetString(oReader.GetOrdinal("vDescripcionJefatura"));
                                    objBE.vCodTrabajador = Util.Validar_Parametros(oReader, "vCodTrabajador", "string").ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBE;
        }


    }
}
