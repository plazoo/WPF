using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGOEntities;
using SGOUtil;
using System.Data.SqlClient;
using System.Data;

namespace SGODataAccess
{
    public class DATablaGenerica
    {
        private String _strError;
        public DATablaGenerica() { _strError = String.Empty; }
        public String strError {
            get { return _strError; }
        }

        #region INSERT / UPDATE

       

        public List<BETablaGenerica> usp_SelLocalIdEmpresaUsuario(string IdUsuario)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGU)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_SelLocalIdEmpresaUsuario", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdEmpresa", SqlDbType.VarChar, 2).Value = Util.GetIdEmpresa();
                        cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();

                                    oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdLocal", "string"));
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "vDescripcion", "string").ToString();
                                    oBe.DescripcionDos = Util.Validar_Parametros(oReader, "vDireccion", "string").ToString();
                                    oBe.Estado = Convert.ToInt32(Util.Validar_Parametros(oReader, "nESTADO", "int"));
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

        } //usp_SelLocalIdEmpresaUsuario
        
        ////******************************************************************************************************

        public List<BETablaGenerica> usp_Mant_TIPO_COSECHA(BETablaGenerica oBE)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            int returnValue = 0;
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {

                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_Mant_TIPO_COSECHA", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@BUS", SqlDbType.Int, 1).Value = oBE.Opcion;
                        cmd.Parameters.Add("@IdCosecha", SqlDbType.Int, 2).Value = oBE.Codigo;
                        cmd.Parameters.Add("@vcCosecha", SqlDbType.VarChar, 20).Value = oBE.DescripcionUno;
                        cmd.Parameters.Add("@vcComentario", SqlDbType.VarChar, 100).Value = oBE.Comentario;
                        cmd.Parameters.Add("@IdEstado", SqlDbType.VarChar, 1).Value = oBE.Estado;
                        cmd.Parameters.Add("@UsuarioRegistro", SqlDbType.Int, 5).Value = Convert.ToInt32(oBE.Usuario);
                        cmd.Parameters.Add("@UsuarioModifica", SqlDbType.Int, 5).Value = Convert.ToInt32(oBE.Usuario);

                        var oBe = new BETablaGenerica();
                        if (oBE.Opcion == 1)/*listar cosechas*/
                        {
                            using (SqlDataReader oReader = cmd.ExecuteReader())
                            {
                                if (oReader.HasRows)
                                {
                                    while (oReader.Read())
                                    {

                                        oBe = new BETablaGenerica();
                                        oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdCosecha", "int"));
                                        oBe.DescripcionUno = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();
                                        oBe.Comentario = Util.Validar_Parametros(oReader, "COMENTARIO", "string").ToString();
                                        oBe.Estado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));
                                        oBe.Usuario = Util.Validar_Parametros(oReader, "VUSUARIO", "string").ToString();
                                        oBe.Fecha = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "date").ToString();
                                        lst.Add(oBe);
                                    }
                                }
                            }
                        }
                        else if (oBE.Opcion == 2) /*insertar*/
                        {
                            returnValue = cmd.ExecuteNonQuery();
                            oBe.inResult = returnValue;
                            lst.Add(oBe);
                        }
                        else if (oBE.Opcion == 3) /*Cambiar estado*/
                        {
                            returnValue = cmd.ExecuteNonQuery();
                            oBe.inResult = returnValue;
                            lst.Add(oBe);
                        }
                        else if (oBE.Opcion == 4) /*Editar*/
                        {
                            returnValue = cmd.ExecuteNonQuery();
                            oBe.inResult = returnValue;
                            lst.Add(oBe);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        } // FIN usp_Mant_TIPO_COSECHA

        ////******************************************************************************************************

        public List<BETablaGenerica> usp_Mant_CLIENTE_COSECHA(BETablaGenerica oBE)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            int returnValue = 0;
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {

                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_Mant_CLIENTE_COSECHA", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@BUS", SqlDbType.Int, 1).Value = oBE.BUS;
                        cmd.Parameters.Add("@IdCosecha", SqlDbType.VarChar, 10).Value = oBE.IdCosecha;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.VarChar, 10).Value = oBE.IdCliente;
                        cmd.Parameters.Add("@InEstado", SqlDbType.VarChar, 1).Value = oBE.InEstado;
                        cmd.Parameters.Add("@UsuarioRegistro", SqlDbType.Int, 6).Value = Convert.ToInt32(oBE.Usuario);
                        cmd.Parameters.Add("@UsuarioModifica", SqlDbType.Int, 6).Value = Convert.ToInt32(oBE.Usuario);
                        cmd.Parameters.Add("@InVigente", SqlDbType.Int, 1).Value = oBE.InVigente;
                        var oBe = new BETablaGenerica();
                        if (oBE.BUS == 1)/*listar cosechas*/
                        {
                            using (SqlDataReader oReader = cmd.ExecuteReader())
                            {
                                if (oReader.HasRows)
                                {
                                    while (oReader.Read())
                                    {

                                        oBe = new BETablaGenerica();
                                        oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdClienteCosecha", "int"));
                                        oBe.EnteroUno = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdCosecha", "int")); /*value del combo*/
                                        oBe.DescripcionUno = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();/* texto del combo*/

                                        oBe.EnteroDos = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdCliente", "int"));
                                        oBe.DescripcionDos = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();

                                        oBe.EnteroTres = Convert.ToInt32(Util.Validar_Parametros(oReader, "IdTipoCliente", "int"));
                                        oBe.DescripcionTres = Util.Validar_Parametros(oReader, "TIPO_CLIENTE", "string").ToString();

                                        oBe.Estado = Convert.ToInt32(Util.Validar_Parametros(oReader, "InEstado", "int"));
                                        oBe.Usuario = Util.Validar_Parametros(oReader, "VUSUARIO", "string").ToString();
                                        oBe.Fecha = Util.Validar_Parametros(oReader, "FechaRegistro", "date").ToString();
                                        oBe.InVigente = Convert.ToInt32(Util.Validar_Parametros(oReader, "InVigente", "int"));
                                        lst.Add(oBe);
                                    }
                                }
                            }
                        }
                        else if (oBE.BUS == 2 || oBE.BUS == 4 || oBE.BUS == 3) /*insertar*/
                        {
                            returnValue = cmd.ExecuteNonQuery();
                            oBe.inResult = returnValue;
                            lst.Add(oBe);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        } // FIN usp_Mant_CLIENTE_COSECHA
        
        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisTipoCafe(string estado)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisTipoCafe", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = estado;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();

                                    oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTIPOCAFE", "int"));
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();
                                    oBe.Estado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));
                                    oBe.Usuario = Util.Validar_Parametros(oReader, "VUSUARIO", "string").ToString();
                                    oBe.Fecha = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "date").ToString();
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
        } // FIN usp_LisTipoCafe

      
        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisComboSacoCafe(string estado)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisComboSacoCafe", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = estado;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "CODIGO", "string").ToString();
                                    oBe.DescripcionDos = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();
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
        } // FIN usp_LisComboSacoCafe

        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisLaboratorioDisponibleXCliente(int idCLiente, string idLocal)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisLaboratorioDisponibleXCliente", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCLiente;
                        cmd.Parameters.Add("@idLocal", SqlDbType.VarChar, 2).Value = idLocal;


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "IdLaboratorio", "string").ToString();
                                    oBe.DescripcionDos = Util.Validar_Parametros(oReader, "CodigoLaboratorio", "string").ToString();
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
        } // FIN usp_LisLaboratorioDisponibleXCliente
        
        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisTipoRuma(string idLocal, string idEstado)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisTipoRuma", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDLOCAL", SqlDbType.VarChar, 3).Value = idLocal;
                        cmd.Parameters.Add("@IDESTADO", SqlDbType.VarChar, 1).Value = idEstado;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();

                                    oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDTIPORUMA", "int"));
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "DESCRIPCION", "string").ToString();
                                    oBe.Estado = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDESTADO", "int"));
                                    oBe.Usuario = Util.Validar_Parametros(oReader, "VUSUARIO", "string").ToString();
                                    oBe.Fecha = Util.Validar_Parametros(oReader, "FECHAREGISTRO", "date").ToString();
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
        } // FIN usp_LisTipoRuma
        
        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisBusquedaClienteFiltro(string filtro)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisBusquedaClienteFiltro", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FILTRO", SqlDbType.VarChar, 100).Value = filtro;

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();

                                    oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCLIENTESGO", "int"));
                                    oBe.DescripcionTres = Util.Validar_Parametros(oReader, "IDCLIENTE", "string").ToString();
                                    oBe.DescripcionDos = Util.Validar_Parametros(oReader, "IDEXCEL", "string").ToString();
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "CLIENTE", "string").ToString();

                                    oBe.DescripcionCuatro = Util.Validar_Parametros(oReader, "TIPO_DOCUMENTO", "string").ToString();
                                    oBe.DescripcionCinco = Util.Validar_Parametros(oReader, "DOCIDENTIDAD", "string").ToString();

                                    oBe.DescripcionSeis = Util.Validar_Parametros(oReader, "DEPARTAMENTO", "string").ToString();
                                    oBe.DescripcionSiete = Util.Validar_Parametros(oReader, "PROVINCIA", "string").ToString();
                                    oBe.DescripcionOcho = Util.Validar_Parametros(oReader, "DISTRITO", "string").ToString();

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
        } // FIN usp_LisBusquedaClienteFiltro

        ////******************************************************************************************************

        public List<BETablaGenerica> usp_LisContratoConGI(int idCliente, int cosecha)
        {
            List<BETablaGenerica> lst = new List<BETablaGenerica>();
            try
            {
                using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    cnn.Open();
                    using (var cmd = new SqlCommand("usp_LisContratoConGI", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDCLIENTE", SqlDbType.Int, 4).Value = idCliente;
                        cmd.Parameters.Add("@COSECHA", SqlDbType.Int, 4).Value = cosecha;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    var oBe = new BETablaGenerica();
                                    oBe.Codigo = Convert.ToInt32(Util.Validar_Parametros(oReader, "IDCONTRATO", "int"));
                                    oBe.DescripcionUno = Util.Validar_Parametros(oReader, "COMBO", "string").ToString();
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
        } // FIN usp_LisTipoRuma
        
        ////******************************************************************************************************

        #endregion
    } // FIN PUBLIC CLASS
}
