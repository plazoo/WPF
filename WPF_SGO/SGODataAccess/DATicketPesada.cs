using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;
using SGOEntities;
using SGOUtil;

namespace SGODataAccess
{
    public class DATicketPesada
    {
        private String _strError;
        public DATicketPesada() { _strError = String.Empty; }
        public String strError
        {
            get { return _strError; }
        }

        //#region INSERT

        public string usp_InsTicketPesada(BETicketPesada oBe)
        {
            int oResultado = 0;
            string strResult = "";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                    {
                        cnn.Open();
                        using (var cmd = new SqlCommand("usp_InsTicketPesada", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = oBe.TipoOperacion;
                            cmd.Parameters.Add("@CODIGO", SqlDbType.Int).Value = oBe.IdTicket;
                            cmd.Parameters.Add("@COSECHA", SqlDbType.Int).Value = oBe.Cosecha;
                            cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = oBe.IdCliente;
                            cmd.Parameters.Add("@IDCONTACTOCLIENTE", SqlDbType.Int).Value = oBe.IdContactoCliente;
                            cmd.Parameters.Add("@IDLOCAL", SqlDbType.Char, 2).Value = oBe.IdLocal;
                            cmd.Parameters.Add("@FECHATICKET", SqlDbType.VarChar, 10).Value = oBe.FechaTicket;
                            cmd.Parameters.Add("@IDLABORATORIO", SqlDbType.Int).Value = oBe.IdLaboratorio;

                            cmd.Parameters.Add("@NROSACO", SqlDbType.Int).Value = oBe.NroSaco;
                            cmd.Parameters.Add("@IDSACO", SqlDbType.Int).Value = oBe.IdTipoSaco;
                            cmd.Parameters.Add("@PESOSACO", SqlDbType.Decimal, 18).Value = oBe.PesoSaco;

                            cmd.Parameters.Add("@TARA", SqlDbType.Decimal, 18).Value = oBe.Tara;
                            cmd.Parameters.Add("@KGBRUTO", SqlDbType.Decimal, 18).Value = oBe.KgBruto;
                            cmd.Parameters.Add("@KGNETO", SqlDbType.Decimal, 18).Value = oBe.KgNeto;

                            cmd.Parameters.Add("@DSCTOAGUA", SqlDbType.Decimal, 18).Value = oBe.DsctoAgua;
                            cmd.Parameters.Add("@KGSECO", SqlDbType.Decimal, 18).Value = oBe.KgSeco;
                            cmd.Parameters.Add("@IDPROCESO", SqlDbType.Int).Value = oBe.IdProceso;
                            cmd.Parameters.Add("@OBSERVACIONES", SqlDbType.Text).Value = oBe.Observacion;

                            cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oBe.IdEstado;
                            cmd.Parameters.Add("@IDTIPOCAFE", SqlDbType.Int).Value = oBe.IdTipoCafe;
                            cmd.Parameters.Add("@IDGUIAREMISION", SqlDbType.VarChar, 100).Value = oBe.IdGuiaRemision;
                            cmd.Parameters.Add("@GUIAREMISION", SqlDbType.VarChar, 200).Value = oBe.GuiaRemision;
                            cmd.Parameters.Add("@UsuarioRegistro", SqlDbType.VarChar, 6).Value = oBe.UsuarioRegistro;

                            Int32 Index = 0;
                            Index = cmd.ExecuteNonQuery();

                            if (Index > 0)
                                oResultado = 1;
                            else
                                oResultado = 0;
                        }
                    } // FIN SQLCONNECTION
                }
                catch (SqlException esql)
                {
                    oResultado = 9;
                    strResult = esql.Message.ToString();

                }
                catch (Exception ex)
                {
                    oResultado = 0;
                }

                if (oResultado == 1)
                {
                    scope.Complete();
                    strResult = oResultado.ToString();
                }
            } // FIN SCOPE 


            return strResult;
        } // FIN usp_InsCliente

        //#endregion

        //#region SELECT

        ////******************************************************************************************************

        public List<BETicketPesada> usp_LisTicketPesada(string estado, string filtro, string idlocal, string fechaIncio, string fechaFin)
        {
            List<BETicketPesada> lst = new List<BETicketPesada>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisTicketPesada", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = estado;
                        cmd.Parameters.Add("@FILTRO", SqlDbType.VarChar, 200).Value = filtro;
                        cmd.Parameters.Add("@IDLOCAL", SqlDbType.VarChar, 2).Value = idlocal;
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = fechaIncio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = Convert.ToDateTime(fechaFin).AddDays(1).ToString();
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETicketPesada();

                                    oBe.Cosecha = Convert.ToInt32(Util.Validar_Parametros(oReader, "COSECHA", "int"));

                                    oBe.IdTicket = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTICKETPESADA", "int"));
                                    oBe.IdTicketPesadaZona = Util.Validar_Parametros(oReader, "CODIGOTICKETPESADA", "string").ToString();
                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));
                                    oBe.IdContactoCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTACTOCLIENTE", "int"));

                                    oBe.IdLocal = Util.Validar_Parametros(oReader, "IDLOCAL", "string").ToString();
                                    oBe.FechaTicket = Util.Validar_Parametros(oReader, "FECHATICKET", "string").ToString();
                                    oBe.DocIdentidadCliente = Util.Validar_Parametros(oReader, "DOCIDENTIDAD", "string").ToString();
                                    oBe.DescCliente = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();

                                    oBe.Departamento = Util.Validar_Parametros(oReader, "DEPARTAMENTO", "string").ToString();
                                    oBe.Provincia = Util.Validar_Parametros(oReader, "PROVINCIA", "string").ToString();
                                    oBe.Distrito = Util.Validar_Parametros(oReader, "DISTRITO", "string").ToString();
                                    oBe.IdLaboratorio = Util.Validar_Parametros(oReader, "IDLABORATORIO", "string").ToString();
                                    oBe.CodigoLaboratorioZona = Util.Validar_Parametros(oReader, "CODIGOLABORATORIO", "string").ToString();
                                    oBe.DescLaboratorio = Util.Validar_Parametros(oReader, "DESCLABORATORIO", "string").ToString();

                                    oBe.Humedad = Util.Validar_Parametros(oReader, "HUMEDADZONA", "decimal").ToString();
                                    oBe.Rendimiento = Util.Validar_Parametros(oReader, "PORCRENDIMIENTO", "decimal").ToString();

                                    oBe.NroSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "NROSACO", "int"));

                                    oBe.IdTipoSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDSACO", "int"));
                                    oBe.PesoSaco = Convert.ToDecimal(Util.Validar_Parametros(oReader, "PESOSACO", "decimal"));

                                    oBe.Tara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TARA", "decimal"));
                                    oBe.KgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGBRUTO", "decimal"));
                                    oBe.KgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGNETO", "decimal"));

                                    oBe.DsctoAgua = Convert.ToDecimal(Util.Validar_Parametros(oReader, "DSCTOAGUA", "decimal"));
                                    oBe.KgSeco = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGSECO", "decimal"));
                                    oBe.IdProceso = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDPROCESO", "int"));
                                    oBe.Observacion = Util.Validar_Parametros(oReader, "OBSERVACIONES", "string").ToString();

                                    oBe.IdTipoCafe = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTIPOCAFE", "int"));
                                    oBe.TipoCafe = Util.Validar_Parametros(oReader, "TIPOCAFE", "string").ToString();
                                    oBe.IdGuiaRemision = Util.Validar_Parametros(oReader, "IDGUIAREMISION", "string").ToString();
                                    oBe.GuiaRemision = Util.Validar_Parametros(oReader, "GUIAREMISION", "string").ToString();

                                    oBe.IdEstado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));
                                    oBe.UsuarioRegistro = Util.Validar_Parametros(oReader, "DESCUSUARIOREGISTRO", "string").ToString();
                                    oBe.FechaRegistro = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "string").ToString();
                                    oBe.UsuarioModifica = Util.Validar_Parametros(oReader, "DESCUSUARIOMODIFICA", "string").ToString();

                                    oBe.FechaModifica = Util.Validar_Parametros(oReader, "FECHAMODIFICA", "string").ToString();

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
        } // FIN usp_LisTicketPesada

        ////******************************************************************************************************

        public List<BEGuiaRemision> usp_LisRecepcionGuiaRemisionZona(string idlocal)
        {
            List<BEGuiaRemision> lst = new List<BEGuiaRemision>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisRecepcionGuiaRemisionZona", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDLOCAL", SqlDbType.VarChar, 3).Value = idlocal;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaRemision();
                                    oBe.IdTraslado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTRASLADO", "int"));
                                    oBe.IdLocal = Util.Validar_Parametros(oReader, "LOCAL", "string").ToString();
                                    oBe.CodigoTraslado = Util.Validar_Parametros(oReader, "TRASLADO", "string").ToString();
                                    oBe.FechaTraslado = Util.Validar_Parametros(oReader, "FECHATRASLADO", "string").ToString();
                                    oBe.GrSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "GRSACO", "int"));
                                    oBe.GrKgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRKGBRUTO", "decimal"));
                                    oBe.GrRendimiento = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRRENDIMIENTO", "decimal"));
                                    oBe.GrHumedad = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRHUMEDAD", "decimal"));

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
        } // FIN usp_LisTicketPesada



        #region REPORTE

        ////******************************************************************************************************

        public DataTable usp_ReporteTicketPesada(string idTicketPesada)
        {
            DataTable oBeTabla = new DataTable();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    using (var cmd = new SqlCommand("usp_ReporteTicketPesada", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDTICKETPESADA", SqlDbType.Int).Value = idTicketPesada;

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(oBeTabla);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
            }
            return oBeTabla;
        }

        ////******************************************************************************************************

        #endregion

    }
}
