using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using SGOUtil;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;



namespace SGODataAccess
{
    public class DACliente
    {
        private String _strError;
        public DACliente() { _strError = String.Empty; }

        public String strError { 
            get { return _strError; }
        }

        #region INSERT

        
        public List<BEClienteContacto> usp_LisClienteContacto(string idCliente, string estado)
        {
            List<BEClienteContacto> lst = new List<BEClienteContacto>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisClienteContacto", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                        cmd.Parameters.Add("@IdEstado", SqlDbType.VarChar, 1).Value = estado;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEClienteContacto();
                                    oBe.IdContactoCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTACTOCLIENTE", "int"));
                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));
                                    oBe.Nombre = Util.Validar_Parametros(oReader, "NOMBRE", "string").ToString();
                                    oBe.Apellido = Util.Validar_Parametros(oReader, "APELLIDO", "string").ToString();
                                    oBe.Telefono = Util.Validar_Parametros(oReader, "TELEFONO", "string").ToString();
                                    oBe.Correo = Util.Validar_Parametros(oReader, "CORREO", "string").ToString();

                                    oBe.IdArea = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDAREA", "int"));
                                    oBe.DescArea = Util.Validar_Parametros(oReader, "DESCAREA", "string").ToString();
                                    oBe.IdCargo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCARGO", "int"));
                                    oBe.DescCargo = Util.Validar_Parametros(oReader, "DESCCARGO", "string").ToString();

                                    oBe.DireccionFactura = Util.Validar_Parametros(oReader, "DIRECCIONFACTURA", "string").ToString();
                                    oBe.ReferenciaFactura = Util.Validar_Parametros(oReader, "DIRECCIONREFERENCIA", "string").ToString();

                                    oBe.IdEstado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));
                                    oBe.UsuarioRegistro = Util.Validar_Parametros(oReader, "VUSUARIO", "string").ToString();
                                    oBe.FechaRegistro = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "date").ToString();

                                    lst.Add(oBe);
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
            return lst;
        } //usp_LisClienteContacto

      
        #endregion

    }
}
