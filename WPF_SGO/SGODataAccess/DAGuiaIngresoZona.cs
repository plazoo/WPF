using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using SGOUtil;
using SGOEntities;
using System.Data;

namespace SGODataAccess
{
    public class DAGuiaIngresoZona
    {

        private String _strError;
        public DAGuiaIngresoZona() { _strError = String.Empty; }
        public String strError {
            get { return _strError; }
        }

        #region INSERT

        public int usp_InsGuiaIngresoCabecera(BEGuiaIngresoZona oBe, List<BEGuiaIngresoZona> lstDetalle, BECertificadoVSP oCertificado)
        {
            int returnValue = 0;
            int IdNewResult = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                    {
                        cnn.Open();
                        using (var cmd = new SqlCommand("usp_InsGuiaIngresoCabecera", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@TipoOperacion", SqlDbType.Int).Value = oBe.TipoOperacion;
                            cmd.Parameters.Add("@IdGuia", SqlDbType.Int).Value = oBe.IdGuia;
                            cmd.Parameters.Add("@IdLocal", SqlDbType.Char, 2).Value = oBe.IdLocal;

                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = oBe.IdCliente;
                            cmd.Parameters.Add("@IdContactoCliente", SqlDbType.Int).Value = oBe.IdContactoCliente;
                            cmd.Parameters.Add("@RumaDestino", SqlDbType.VarChar, 20).Value = oBe.RumaDestino;

                            cmd.Parameters.Add("@TotalSaco", SqlDbType.Int).Value = oBe.TotalSaco;
                            cmd.Parameters.Add("@TotalKgBruto", SqlDbType.Decimal, 18).Value = oBe.TotalKgBruto;
                            cmd.Parameters.Add("@TotalDsctoAgua", SqlDbType.Decimal, 18).Value = oBe.TotalDsctoAgua;

                            cmd.Parameters.Add("@TotalTara", SqlDbType.Decimal, 18).Value = oBe.TotalTara;
                            cmd.Parameters.Add("@TotalKgNeto", SqlDbType.Decimal, 18).Value = oBe.TotalKgNeto;
                            cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oBe.IdEstado;

                            cmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = oBe.IdProceso;
                            cmd.Parameters.Add("@IdDivision", SqlDbType.Int).Value = oBe.DivisionGuia;
                            cmd.Parameters.Add("@UsuarioRegistro", SqlDbType.VarChar, 6).Value = oBe.UsuarioRegistro;

                            cmd.Parameters.Add("@IdCertificadoVSP", SqlDbType.VarChar, 250).Value = oBe.IdCertificadoVSP;
                            cmd.Parameters.Add("@DescCertificacion", SqlDbType.VarChar, 250).Value = oBe.DescCertificacion;
                            cmd.Parameters.Add("@ModalidadIngreso", SqlDbType.VarChar, 10).Value = oBe.ModalidadIngreso;

                            cmd.Parameters.Add("@IdIngresoPRP", SqlDbType.Int).Value = oBe.IdIngresoPRP;
                            cmd.Parameters.Add("@IdOficinaOrigen", SqlDbType.Int).Value = oBe.IdOficinaOrigen;
							cmd.Parameters.Add("@IdTipoCafe", SqlDbType.Int).Value = Convert.ToInt32(oBe.IdTipoCafe);

                            cmd.Parameters.Add("@IdTraslado", SqlDbType.Int).Value = oBe.IdTraslado;
                            cmd.Parameters.Add("@GuiaRemisionExterna", SqlDbType.VarChar, 200).Value = oBe.GuiaRemisionExterna;
                            cmd.Parameters.Add("@IdClienteTrazabilidad", SqlDbType.Int).Value = oBe.DetalleIdCliente;

                            cmd.Parameters.Add("@IdTrasladoFila", SqlDbType.Int).Value = oBe.DetalleIdResultado;
                            cmd.Parameters.Add("@Observacion",SqlDbType.VarChar,800).Value=oBe.Observacion;
                            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = oBe.IdContrato;
                            cmd.Parameters.Add("@cosecha", SqlDbType.VarChar).Value = oBe.vcCosecha;

                            cmd.Parameters.Add("@IdGuiaIngresoResult", SqlDbType.Int).Direction = ParameterDirection.Output;

                            returnValue = cmd.ExecuteNonQuery();
                            IdNewResult = Convert.ToInt32(cmd.Parameters["@IdGuiaIngresoResult"].Value);

                        }

                        //**************************************************************************************************************
                    if (oBe.TipoOperacion == 1) {
						if (lstDetalle.Count > 0)
						{
                                try
                                {
                                    returnValue = 0;
                                    foreach (BEGuiaIngresoZona item in lstDetalle)
                                    {
                                        using (var cmd = new SqlCommand("usp_InsGuiaIngresoDetalle", cnn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Add("@IdGuia", SqlDbType.Int).Value = IdNewResult;
                                            cmd.Parameters.Add("@IdTicketPesada", SqlDbType.Int).Value = item.IdTicketPesada;
                                            cmd.Parameters.Add("@IdOrdenServicio", SqlDbType.Int).Value = item.IdOrdenServicio;

                                            cmd.Parameters.Add("@IdResultado", SqlDbType.Int).Value = item.IdResultado;
                                            cmd.Parameters.Add("@NroSaco", SqlDbType.Int).Value = item.NroSaco;
                                            cmd.Parameters.Add("@KgBruto", SqlDbType.Decimal, 18).Value = item.KgBruto;

                                            cmd.Parameters.Add("@Tara", SqlDbType.Decimal, 18).Value = item.Tara;
                                            cmd.Parameters.Add("@KgNeto", SqlDbType.Decimal, 18).Value = item.KgNeto;

                                            returnValue = cmd.ExecuteNonQuery();
                                        }

                                        using (var cmd = new SqlCommand("usp_InsGuiaIngresoSaldo", cnn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Add("@IdGuia", SqlDbType.Int).Value = IdNewResult;
                                            cmd.Parameters.Add("@IdTicketPesada", SqlDbType.Int).Value = item.IdTicketPesada;
                                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = oBe.IdCliente;
                                            cmd.Parameters.Add("@Cosecha", SqlDbType.Int).Value = oBe.Cosecha;

                                            cmd.Parameters.Add("@Saco", SqlDbType.Int).Value = item.NroSaco;
                                            cmd.Parameters.Add("@KgBruto", SqlDbType.Decimal, 18).Value = item.KgBruto;
                                            cmd.Parameters.Add("@Tara", SqlDbType.Decimal, 18).Value = item.Tara;
                                            cmd.Parameters.Add("@KgNeto", SqlDbType.Decimal, 18).Value = item.KgNeto;

                                            cmd.Parameters.Add("@SacoOperacion", SqlDbType.Int).Value = item.SacoOperacion;
                                            cmd.Parameters.Add("@KgBrutoOperacion", SqlDbType.Decimal, 18).Value = item.KgBrutoOperacion;
                                            cmd.Parameters.Add("@TaraOperacion", SqlDbType.Decimal, 18).Value = item.TaraOperacion;
                                            cmd.Parameters.Add("@KgNetoOperacion", SqlDbType.Decimal, 18).Value = item.KgNetoOperacion;

                                            cmd.Parameters.Add("@SacoSaldo", SqlDbType.Int).Value = item.SacoSaldo;
                                            cmd.Parameters.Add("@KgBrutoSaldo", SqlDbType.Decimal, 18).Value = item.KgBrutoSaldo;
                                            cmd.Parameters.Add("@TaraSaldo", SqlDbType.Decimal, 18).Value = item.TaraSaldo;
                                            cmd.Parameters.Add("@KgNetoSaldo", SqlDbType.Decimal, 18).Value = item.KgNetoSaldo;

                                            cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oBe.IdEstado;
                                            cmd.Parameters.Add("@UsuarioRegistro", SqlDbType.VarChar, 6).Value = oBe.UsuarioRegistro;

                                            returnValue = cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                catch (Exception)
                                {
                                    throw;
                                }

                                //FIN usp_InsGuiaIngresoDetalle
                                //******************************

                            } // FIN IF

                            if (!(oCertificado.Sello.Equals(String.Empty)))
                            {
                                try
                                {
                                    if ((oBe.ModalidadIngreso == "CON" || oBe.ModalidadIngreso == "CER") && oBe.IdProceso == 0 )
                                    {
                                        returnValue = 0;
                                        using (var cmd = new SqlCommand("usp_InsSaldoCertificado", cnn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            cmd.Parameters.Add("@IDGUIA", SqlDbType.VarChar, 200).Value = IdNewResult;
                                            cmd.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar, 200).Value = oCertificado.IdCliente;
                                            cmd.Parameters.Add("@COSECHA", SqlDbType.VarChar, 200).Value = oCertificado.Cosecha;
                                            cmd.Parameters.Add("@OPERACION", SqlDbType.VarChar, 200).Value = oCertificado.Operacion;

                                            cmd.Parameters.Add("@SELLO", SqlDbType.VarChar, 200).Value = oCertificado.Sello;
                                            cmd.Parameters.Add("@TIPOOPERACION", SqlDbType.VarChar, 2).Value = oCertificado.TipoOperacion;

                                            returnValue = cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                //FIN usp_InsSaldoCertificado
                            } // FIN IF
	                    } // FIN IF TIPO OPERACION 1

                        //************************************************************************************************

                    } // FIN PRIMER USING
                }

                catch (Exception ex) {
                    returnValue = 0;
                }

                if (returnValue >= 1 || returnValue == -1)
                    scope.Complete();

            } // FIN SCOPE

            return IdNewResult;
        } // FIN usp_InsGuiaIngresoCabecera

      
        public int usp_UpdGuiaIngresoRendHumZona(string idGuiaIngreso)
        {
            Int32 returnValue = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                    {
                        cnn.Open();
                        using (var cmd = new SqlCommand("usp_UpdGuiaIngresoRendHumZona", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@GUIAINGRESO", SqlDbType.Int).Value = idGuiaIngreso;
                            returnValue = cmd.ExecuteNonQuery();
                        }
                    }
                    if (returnValue >= 1 || returnValue == -1)
                        scope.Complete();
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;

                }
            }
            return returnValue;
        } //fin usp_UpdGuiaIngresoRendHumZona
        
        public List<BEGuiaIngresoZona> usp_LisGuiaIngresoZona(string idlocal, string estado, string filtro, string fechaInicio, string fechaFin)
        {
            var lst = new List<BEGuiaIngresoZona>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisGuiaIngresoZona", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = estado;
                        cmd.Parameters.Add("@FILTRO", SqlDbType.VarChar, 200).Value = filtro;
                        cmd.Parameters.Add("@IDLOCAL", SqlDbType.VarChar, 2).Value = idlocal;
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = Convert.ToDateTime(fechaFin).AddDays(1).ToString(); ;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaIngresoZona();

                                    oBe.IdGuia = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDGUIA", "int"));
                                    oBe.GuiaIngreso = Util.Validar_Parametros(oReader, "GUIAINGRESO", "string").ToString();
                                    oBe.IdLocal = Util.Validar_Parametros(oReader, "IDLOCAL", "string").ToString();
                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));

                                    oBe.DescCliente = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();
                                    oBe.IdContactoCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTACTOCLIENTE", "int"));
                                    oBe.ContactoCliente = Util.Validar_Parametros(oReader, "CONTACTOCLIENTE", "string").ToString();
                                    oBe.RumaDestino = Util.Validar_Parametros(oReader, "RUMADESTINO", "string").ToString();

                                    oBe.PromedioRendimiento = Convert.ToDecimal(Util.Validar_Parametros(oReader, "PROMEDIORENDIMIENTO", "decimal"));
                                    oBe.PromedioHumedad = Convert.ToDecimal(Util.Validar_Parametros(oReader, "PROMEDIOHUMEDAD", "decimal"));
                                    oBe.PromedioTaza = Convert.ToDecimal(Util.Validar_Parametros(oReader, "PROMEDIOTAZA", "decimal"));
                                    oBe.TotalSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "TOTALSACO", "int"));

                                    oBe.TotalKgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALKGBRUTO", "decimal"));
                                    oBe.TotalTara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALTARA", "decimal"));
                                    oBe.TotalDsctoAgua = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALDSCTOAGUA", "decimal"));
                                    oBe.TotalKgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALKGNETO", "decimal"));

                                    oBe.IdProceso = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDPROCESO", "int"));
                                    oBe.ProcesoDescripcion = Util.Validar_Parametros(oReader, "ProcesoDescripcion", "string").ToString();
                                    oBe.DescEstado = Util.Validar_Parametros(oReader, "ESTADO", "string").ToString();
                                    oBe.DescPropiedad = Util.Validar_Parametros(oReader, "PROPIEDAD", "string").ToString();

                                    oBe.UsuarioRegistro = Util.Validar_Parametros(oReader, "DESCUSUARIOREGISTRO", "string").ToString();
                                    oBe.FechaRegistro = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "string").ToString();
                                    oBe.UsuarioModifica = Util.Validar_Parametros(oReader, "DESCUSUARIOMODIFICA", "string").ToString();
                                    oBe.FechaModifica = Util.Validar_Parametros(oReader, "FECHAMODIFICA", "string").ToString();

                                    oBe.Impresion = Convert.ToInt32(Util.Validar_Parametros(oReader, "IMPRESION", "int"));

                                    oBe.Ruma = Util.Validar_Parametros(oReader, "RUMA", "string").ToString();
                                    oBe.ModalidadIngreso = Util.Validar_Parametros(oReader, "MODALIDADINGRESO", "string").ToString();
                                    oBe.IdTipoCafe = Util.Validar_Parametros(oReader, "IdTipoCafe", "int").ToString();
                                    oBe.DescTipoCafe = Util.Validar_Parametros(oReader, "TipoCafe", "string").ToString();

                                    oBe.DescOficinaOrigen = Util.Validar_Parametros(oReader, "ORIGEN", "string").ToString();
                                    oBe.GuiaRemision = Util.Validar_Parametros(oReader, "TRASLADO", "string").ToString();
                                    oBe.GuiaRemisionExterna = Util.Validar_Parametros(oReader, "GUIAREMISIONEXTERNA", "string").ToString();
                                    oBe.Observacion = Util.Validar_Parametros(oReader, "OBSERVACION", "string").ToString();

                                    oBe.Liquidacion = Util.Validar_Parametros(oReader, "LIQUIDACION", "string").ToString();
                                    oBe.ContratoRef = Util.Validar_Parametros(oReader, "CONTRATOCOMPRA", "string").ToString();
                                    oBe.DescCertificacion = Util.Validar_Parametros(oReader, "DESCCERTIFICACION", "string").ToString();
                                    oBe.IdCertificadoVSP = Util.Validar_Parametros(oReader, "IdCertificadoVSP", "string").ToString();
                                    oBe.Servicio = Util.Validar_Parametros(oReader, "SS", "string").ToString();

                                    oBe.IdContrato = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTRATO", "int"));
                                    oBe.DIF_NETOYAGUA = Convert.ToDecimal(Util.Validar_Parametros(oReader, "DIF_NETOYAGUA", "decimal"));
                                    oBe.Calidad = Util.Validar_Parametros(oReader, "CALIDAD", "string").ToString();

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

        //      //******************************************************************************************************

        public List<BEGuiaIngresoZona> usp_LisDatoGuiaIngresoZona(string idGuiaIngreso)
        {
            List<BEGuiaIngresoZona> lst = new List<BEGuiaIngresoZona>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisDatoGuiaIngresoZona", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGuiaIngreso", SqlDbType.VarChar, 20).Value = idGuiaIngreso;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaIngresoZona();
                                    oBe.TipoDocumento = Util.Validar_Parametros(oReader, "DOCUMENTO", "string").ToString();
                                    oBe.NumeroDocumento = Util.Validar_Parametros(oReader, "NUMERO", "string").ToString();
                                    oBe.Calidad = Util.Validar_Parametros(oReader, "CALIDAD", "string").ToString();
                                    oBe.NroSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "NROSACO", "int"));
                                    oBe.KgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGBRUTO", "decimal"));
                                    oBe.Tara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TARA", "decimal"));
                                    oBe.KgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KGNETO", "decimal"));
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
        } // FIN usp_LisDatoGuiaIngresoZona

        public DataTable usp_ReporteGuiaIngreso(string idGuiaIngreso)
        {
            DataTable oBeTabla = new DataTable();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    using (var cmd = new SqlCommand("usp_ReporteGuiaIngreso", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDGUIAINGRESO", SqlDbType.Int).Value = idGuiaIngreso;
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

        } // usp_ReporteGuiaIngreso

       

        public BEGuiaIngresoZona usp_LisGuiaIngresoEditar(int idGuia)
        {
            var lst = new BEGuiaIngresoZona();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisGuiaIngresoEditar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDGUIA", SqlDbType.Int).Value = idGuia;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaIngresoZona();
                                    oBe.IdGuia = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDGUIA", "int"));
                                    oBe.GuiaIngreso = Util.Validar_Parametros(oReader, "IDGUIAINGRESO", "string").ToString();
                                    oBe.IdIngresoPRP = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDINGRESOPRP", "int"));
                                    oBe.IdLocal = Util.Validar_Parametros(oReader, "IDLOCAL", "string").ToString();

                                    oBe.Cosecha = Convert.ToInt32(Util.Validar_Parametros(oReader, "COSECHA", "int"));
                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));
                                    oBe.IdContactoCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTACTOCLIENTE", "int"));
                                    oBe.ContactoCliente = Util.Validar_Parametros(oReader, "CONTACTO", "string").ToString();

                                    oBe.IdTipoCafe = Util.Validar_Parametros(oReader, "IDTIPOCAFE", "int").ToString();
                                    oBe.RumaDestino = Util.Validar_Parametros(oReader, "RUMADESTINO", "string").ToString();
                                    oBe.ModalidadIngreso = Util.Validar_Parametros(oReader, "MODALIDADINGRESO", "string").ToString();
                                    oBe.IdProceso = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDPROCESO", "int"));

                                    oBe.IdCertificado = Util.Validar_Parametros(oReader, "IDCERTIFICADOVSP", "string").ToString();
                                    oBe.DescCertificacion = Util.Validar_Parametros(oReader, "DESCCERTIFICACION", "string").ToString();
                                    oBe.IdOficinaOrigen = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDOFICINAORIGEN", "int"));
                                    oBe.IdTraslado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTRASLADO", "int"));

                                    oBe.GuiaRemisionExterna = Util.Validar_Parametros(oReader, "GUIAREMISIONEXTERNA", "string").ToString();
                                    oBe.TotalSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "TOTALSACO", "int"));
                                    oBe.TotalKgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALKGBRUTO", "decimal"));
                                    oBe.TotalTara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALTARA", "decimal"));

                                    oBe.TotalDsctoAgua = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALDSCTOAGUA", "decimal"));
                                    oBe.TotalKgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "TOTALKGNETO", "decimal"));
                                    oBe.IdEstado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));

                                    oBe.DescCliente = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();
                                    oBe.Observacion = Util.Validar_Parametros(oReader, "OBSERVACION", "string").ToString();
                                    oBe.IdContrato = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTRATO", "int"));
                                    oBe.DivisionGuia = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdDivision", "int")); /*ADD PLAZO 20180406 */
                                    lst = oBe;
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
        } // FIN usp_LisGuiaIngresoEditar

        //      //******************************************************************************************************

        public List<BEGuiaIngresoZona> usp_ListadoDetalleRemision(int idTraslado)
        {
            List<BEGuiaIngresoZona> lst = new List<BEGuiaIngresoZona>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_ListadoDetalleRemision", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdTraslado", SqlDbType.Int, 20).Value = idTraslado;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaIngresoZona();
                                    oBe.IdResultado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDRESULTADO", "int"));
                                    oBe.IdCliente = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTE", "int"));
                                    oBe.DescCliente = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();
                                    oBe.Saco = Convert.ToInt32(Util.Validar_Parametros(oReader, "GRSACO", "int"));
                                    oBe.KgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRKGBRUTO", "decimal"));
                                    oBe.Tara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRTARA", "decimal"));
                                    oBe.KgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "GRKGNETO", "decimal"));
                                    oBe.DescCertificacion = Util.Validar_Parametros(oReader, "CERTIFICADO", "string").ToString();
                                    oBe.Certificado = Util.Validar_Parametros(oReader, "IDCERTIFICADO", "string").ToString();
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
        } // FIN usp_LisDatoGuiaIngresoZona

        //      //******************************************************************************************************
        public List<BEGuiaIngresoZona> usp_LisGuiaIngresoDetalle(int idGuia)
        {
            List<BEGuiaIngresoZona> lst = new List<BEGuiaIngresoZona>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisGuiaIngresoDetalle", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDGUIA", SqlDbType.Int, 20).Value = idGuia;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BEGuiaIngresoZona();
                                    oBe.IdGuiaIngresoDetalle = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdGuiaIngresoDetalle", "int"));
                                    oBe.IdGuia = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdGuia", "int"));
                                    oBe.IdTicketPesada = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdTicketPesada", "int"));
                                    oBe.IdOrdenServicio = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdOrdenServicio", "int"));
                                    oBe.IdResultado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdResultado", "int"));
                                    oBe.NroSaco = Convert.ToInt32(Util.Validar_Parametros(oReader, "NroSaco", "int"));
                                    oBe.KgBruto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KgBruto", "decimal"));
                                    oBe.Tara = Convert.ToDecimal(Util.Validar_Parametros(oReader, "Tara", "decimal"));
                                    oBe.KgNeto = Convert.ToDecimal(Util.Validar_Parametros(oReader, "KgNeto", "decimal"));
                                    oBe.IdEstado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdEstado", "int"));
                                    //oBe.IdSSAsignado = Util.Validar_Parametros(oReader, "IdSSAsignado", "string").ToString();
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
        } // FIN usp_LisDatoGuiaIngresoZona

      

        #endregion


    } // FIN PUBLIC CLASS
} // FIN TODO
