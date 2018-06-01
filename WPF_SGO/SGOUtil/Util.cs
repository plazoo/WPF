using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Web;
using Microsoft.Office.Interop;
using Exception = System.Exception;
using System.Net.Mail;
//using System.Web.Script.Serialization;
using Microsoft.Office.Interop.Outlook;

//using Exception = System.Exception;

namespace SGOUtil
{
    public class Util
    {
        //20180222
        public enum CnnType
        {
            CnnSGU = 7,
            CnnSGO = 24,
            CnnRRHH = 25,
            CnnSCG = 26,
            CnnPRPE=27
        }

        public enum TipoOperacion { 
            Insertar = 1,
            Modificar = 2,
            Select = 3,
            Seguridad = 4
        }

        public enum EstadoVisita { 
            Espera = 1,
            Cancelado =	2,
            Ingreso = 3,
            Retiro = 4        
        }

        public enum Estado { 
            Activo = 1,
            Anulado = 0
        }

        public enum EstadoTicketPesada { 
            No_requerido = 0,
            Pendiente = 1,
            En_proceso = 2,
            Terminado = 3
        }

        public enum EstadoServicioPRP {
            Anulado = 0,
            En_proceso = 1,
            Terminado = 2
        }

        public enum CodigoLocal { 
            Pichanaqui = 24,
            VillaRica = 31,
            Pangoa = 32,
            Moyobamba = 29,
            SanIgnacio = 30,
            Mazamari = 25,
            JaenAgromar = 26,
            JaenComcafe = 33

        }
        public enum OCuentaContable
        {
            Facturacion=1,
            CajaChica=2,
            HCajaChica=3
        }
        public string[] arrStrJefesZona = { "1416", "1455", "1398", "1402", "1403", "1415", "567","1487", "1385", "1431","1477" }; //PLAZO 20171003 
        public static string GetIdEmpresa()
        {
            return System.Configuration.ConfigurationManager.AppSettings["IdEmpresa"].ToString();
        }
      
        public static String GetStringConnection(CnnType typeConnection)
        {
            string name = Enum.GetName(typeof(CnnType), typeConnection);
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static Object Validar_Parametros(SqlDataReader oReader, String campo, String tipo)
        {
            Object result = DBNull.Value;
            if (tipo == "string")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetString(oReader.GetOrdinal(campo)).Trim();
                else
                    result = "";
            }
            else if (tipo == "int")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetInt32(oReader.GetOrdinal(campo));
                else
                    result = 0;
            }
            else if (tipo == "date")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDateTime(oReader.GetOrdinal(campo));
                else
                    result = Convert.ToDateTime("01/01/1900");
            }
            else if (tipo == "bool")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetBoolean(oReader.GetOrdinal(campo));
                else
                    result = false;
            }
            else if (tipo == "decimal")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDecimal(oReader.GetOrdinal(campo));
                else
                    result = (Decimal)0;
            }
            else if (tipo == "float")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetFloat(oReader.GetOrdinal(campo));
                else
                    result = (Double)0;
            }
            return result;
        }

        public static Object Asignar_Parametros(Object valor, String tipo, Boolean nulo)
        {
            Object result = DBNull.Value;
            if (tipo == "string")
            {
                String Temporal = (String)valor;
                if (!String.IsNullOrEmpty(Temporal)) result = Temporal;
                else if (!nulo) result = "";
            }
            else if (tipo == "date")
            {
                String Temporal = ((DateTime)valor).ToString("dd/MM/yyyy");
                if (Temporal == "01/01/0001")
                {
                    if (!nulo) result = "";
                }
                else
                {
                    result = (DateTime)valor;
                }
            }
            if (tipo == "int")
            {
                Int32 Temporal = (Int32)valor;
                if (Temporal != 0) result = Temporal;
            }
            return result;
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static String Left(String str, Int32 length) {
            return str.Substring(0, Math.Min(str.Length, length));
        }

        public static String Right(String str, Int32 length)
        {
            return str.Substring(str.Length - length, length);
        }

        //************************************************************************************************

        public static Boolean ToEnviarCorreo_Outlook(string strCorreo, string strAsunto, string strCuerpo)
        {
            Boolean resultado = false;

            try
            {
                var oApp = new Microsoft.Office.Interop.Outlook.Application();

                Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                var f = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.Subject = strAsunto;
                mailItem.HTMLBody = strCuerpo;
                mailItem.To = strCorreo;
                mailItem.Send();
                resultado = true;

            }
            catch (Exception ex)
            {
                resultado = false;
            }
            
            //try
            //{
            //    var oApp = new Microsoft.Office.Interop.Outlook.Application();
            //    MailItem oMsg = default(MailItem);
            //    oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
            //    oMsg.Subject = strAsunto;
            //    oMsg.HTMLBody = strCuerpo;
            //    oMsg.To = strCorreo.Trim();

            //    oMsg.Send();
            //    oApp = null;
            //    oMsg = null;
            //    resultado = true;
            //    //*****************************************************************************
            //}
            //catch (Exception ex)
            //{
            //    resultado = false;

            //}

            return resultado;
        }

        //************************************************************************************************

		public static response toSendEmail(string strCorreo, string strAsunto, string strCuerpo, string path)
		{
			var resultado = new response();
			try
			{
				var oApp = new Microsoft.Office.Interop.Outlook.Application();
				MailItem oMsg = default(MailItem);
				oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
				oMsg.Subject = strAsunto;
				oMsg.HTMLBody = strCuerpo;
				oMsg.To = strCorreo.Trim();
				if (!string.IsNullOrEmpty(path)) oMsg.Attachments.Add(path);
				//oMsg.CC = Trim(StrCorreoOp)

				oMsg.Send();
				oApp = null;
				oMsg = null;
				resultado.Exito = true;
				resultado.MensajeError = "Se ha enviado correctamente el correo.";
				//*****************************************************************************
			}
			catch (Exception ex)
			{
				resultado.Exito = false;
				resultado.MensajeError = ex.Message;
			}
			return resultado;
		}

		//**********************************

        public static Boolean ToEnviarCorreo_SMTP(string strCorreo, string strAsunto, string strCuerpo)
        {
            Boolean resultado = false;
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(strCorreo));
            email.From = new MailAddress("SGO@prodelsur.com");
            email.Subject = strAsunto;
            email.Body = strCuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "10.125.0.15";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("email_from@example.com", "contraseña");

            string output = null;
            try{
                smtp.Send(email);
                email.Dispose();
                resultado = true;
            }
            catch (Exception ex){
                resultado = false;
            }
            return resultado;
        }

        //************************************************************************************************

        #region EXCEL

        public static string NumeroALetras(string moneda, string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            string mon = "";

            switch (moneda)
            {
                case "S":
                    mon = "SOLES";
                    break;
                case "D":
                    mon = "DOLARES AMERICANOS";
                    break;
            }
            dec = " CON " + decimales.ToString() + "/100 " + mon;
            res = "SON " + toText(Convert.ToDouble(entero)) + dec;
            return res;

        }

        //********************************************************************************************

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }

        //********************************************************************************************

        //public static object DataTableToJSON(DataTable table)
        //{
        //    var list = new List<Dictionary<string, object>>();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        var dict = new Dictionary<string, object>();

        //        foreach (DataColumn col in table.Columns)
        //        {
        //            dict[col.ColumnName] = (Convert.ToString(row[col]));
        //        }
        //        list.Add(dict);
        //    }
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();

        //    return serializer.Serialize(list);
        //}


        #endregion

		/// <summary>
		/// Agregado por: Slater Abanto
		/// Fecha: 12/09/2016
		/// </summary>
		public const decimal IGV = 0.18m;
		public const decimal ISC = 0.10m;
		public const decimal Detraccion = 0.10m;
		public enum TipoDoc
		{
			Factura = 01,
			BoleteVenta = 03,
			NotaCredito = 07,
			NotaDebito = 08,
			ComprobanteRetencion = 20
		}

      
    } //FIN PUBLIC CLASS
} // FIN NAMESPACE
