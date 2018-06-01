using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using System.Data.SqlClient;
using SGOUtil;
using System.Data;
using System.Transactions;

namespace SGODataAccess
{
    public class DASolicitudServicio
    {

        private String _strError;
        public DASolicitudServicio() { _strError = String.Empty; }
        public String strError {
            get { return _strError; }
        }


        #region SELECT

        
        public List<BETicketPesada> usp_LisTickedPesadaEnProceso(int proceso, string idlocal, int cliente, int estado)
        {
            List<BETicketPesada> lst = new List<BETicketPesada>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisTickedPesadaEnProceso", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = proceso;
                        cmd.Parameters.Add("@IdLocal", SqlDbType.Char, 2).Value = idlocal;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente;
                        cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = estado;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETicketPesada();
                                    oBe.IdTicketPesadaZona = Util.Validar_Parametros(oReader, "CODIGOTICKETPESADAZONA", "string").ToString();
                                    oBe.IdTicketPesada = Util.Validar_Parametros(oReader, "IDTICKETPESADA", "string").ToString();
                                    oBe.FechaTicket = Util.Validar_Parametros(oReader, "FECHATICKET", "string").ToString();

                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));
                                    oBe.IdContactoCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTACTOCLIENTE", "int"));
                                    oBe.ContactoCliente = Util.Validar_Parametros(oReader, "Proveedor", "string").ToString();

                                    oBe.IdLaboratorio = Util.Validar_Parametros(oReader, "IDLABORATORIO", "string").ToString();
                                    oBe.DescLaboratorio = Util.Validar_Parametros(oReader, "LaboratorioZona", "string").ToString();
                                    oBe.AnalisisLaboratorio = Util.Validar_Parametros(oReader, "Analisis", "string").ToString();

                                    oBe.NroSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "NROSACO", "int"));
                                    oBe.PesoSaco = Convert.ToDecimal(Util.Validar_Parametros(oReader, "PESOSACO", "decimal"));
                                    oBe.Tara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TARA", "decimal"));

									oBe.KgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGBRUTO", "decimal"));
									oBe.KgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGNETO", "decimal"));
									oBe.Observacion = Util.Validar_Parametros(oReader, "OBSERVACIONES", "string").ToString();
									oBe.IdTipoCafe = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdTipoCafe", "int"));
									oBe.TipoCafe = Util.Validar_Parametros(oReader, "TipoCafe", "string").ToString();
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
		} // FIN usp_LisTickedPesadaEnProceso

        #endregion




    } // FIN PUBLIC CLASS
}
