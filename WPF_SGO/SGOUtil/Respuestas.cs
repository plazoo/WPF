namespace SGOUtil
{
    public static class ErrorGenerico
    {

        public const string Codigo = "10101";
        public const string Mensaje = "Error Genérico detectado";

    }

    public static class ErrorBaseDatos
    {

        public const string Codigo = "10102";
        public const string Mensaje = "Error de Base de Datos";

    }

    public static class ErrorSUNAT
    {

        public const string Codigo = "10103";
        public const string Mensaje = "Error con WebService SUNAT";

    }

    public static class ErrorValidacion
    {
        public const string Codigo = "10104";
        public const string Mensaje = "Error de Validación";
    }

    public static class MensajeExitoso
    {
        public const string Codigo = "10200";
        public const string Mensaje = "Enviado Correctamente";
    }

    public static class Constantes
    {
        public const string FormatoFecha = "yyyy-MM-dd";
        public const string FormatoNumerico = "###0.#0";

        public const string Registrado = "1";
        public const string Activo = "2";
        public const string PendienteRespuesta = "3";
        public const string Aceptado = "4";
        public const string Rechazado = "5";
        public const string Anulado = "6";
        public const string DatosInvalidos = "7";
        public const string urlTest = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
        public const string urlTestRet = "https://www.sunat.gob.pe/ol-ti-itemision-otroscpe-gem/billService";
        public const string urlVerFac = "https://www.sunat.gob.pe/ol-it-wsconsvalidcpe/billValidService";
        public const string urlConsCDR = " https://www.sunat.gob.pe/ol-it-wsconscpegem/billConsultService";
        public const string urlHom = "https://www.sunat.gob.pe/ol-ti-itcpgem-sqa/billService";
        public const string urlProd = "https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService";

    }
    public class response
    {
        public bool Exito { get; set; }
        public string MensajeError { get; set; }
    }


}

