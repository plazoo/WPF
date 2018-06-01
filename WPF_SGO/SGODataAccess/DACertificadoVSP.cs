using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOUtil;
using SGOEntities;
using System.Data;
using System.Data.SqlClient;


namespace SGODataAccess
{
    public class DACertificadoVSP
    {
        private String _strError;
        public DACertificadoVSP() { _strError = String.Empty; }
        public String strError {
            get { return _strError; }
        }
        public List<BECertificadoVSP> usp_LisSaldoCertificado(string idCliente, string cosecha)
        {
            List<BECertificadoVSP> lst = new List<BECertificadoVSP>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisSaldoCertificado", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = idCliente;
                        cmd.Parameters.Add("@COSECHA", SqlDbType.VarChar, 8).Value = cosecha;
                        
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BECertificadoVSP();

                                    oBe.IdCertificadoVsp = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCERTIFICADOVSP", "int"));
                                    oBe.Descripcion = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();
                                    oBe.Cosecha = Convert.ToInt32(Util.Validar_Parametros(oReader, "COSECHA", "int"));
                                    oBe.Tope = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOPE", "decimal"));
                                    oBe.Limite = Convert.ToDecimal(Util.Validar_Parametros(oReader, "LIMITE", "decimal"));
                                    oBe.Operacion = Convert.ToDecimal(Util.Validar_Parametros(oReader, "OPERACION", "decimal"));
                                    oBe.Saldo = Convert.ToDecimal(Util.Validar_Parametros(oReader, "SALDO", "decimal"));
                                    oBe.SaldoCalculado = Convert.ToDecimal(Util.Validar_Parametros(oReader, "SALDO_CALCULADO", "decimal"));

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
        } // FIN usp_LisGuiaIngresoZona
        
    } // FIN PUBLIC CLASS
} // FIN NAMESPACE
