using System;
using System.Collections;


namespace Claro.SIACU.AssetsAndHelpers.Constants
{
    public class ConstantesSIACU
    {
        public ConstantesSIACU()
        { }


        public static String RECIBO8 = "20070628";
        public static String RECIBO9 = "20130111";

        public static string DETALLE_LLAMADAS = "DETALLE_LLAMADAS";
        public static string DETALLE_SMS = "DETALLE_SMS";
        public static string DETALLE_LLAMADAS_CONSUMO = "DETALLE_LLAMADAS_CONSUMO";





        public struct DETALLE_CARGO_FIJO
        {
            public static int consulta_descuento_cargo_fijo = 0;
            public static int consulta_cargo_fijo_timpro = 1;
            public static int consulta_cargo_fijo_timpro_1 = 3;
            public static int consulta_cargo_fijo_timpro_2 = 4;
            public static int consulta_cargo_fijo_timmax = 2;
            public static int consulta_cargo_fijo_timmax_2 = 5;
            public static int consulta_cargo_fijo_timpro_bolsa = 1;
            public static int consulta_cargo_fijo_timpro_bolsa_1 = 2;
            public static int consulta_cargo_fijo_timpro_bolsa_2 = 3;
            public static int consulta_cargo_fijo_timpro_bolsa_3 = 4;

        }


        public struct DETALLE_TRAFICO_LOCAL_GENERAL
        {
            public static int CONSULTA_TRAFICO_LOCAL_ADICIONAL_TIMpro = 1;
            public static int CONSULTA_TRAFICO_LOCAL_ADICIONAL_TIMmax = 2;
            public static int CONSULTA_TRAFICO_LOCAL_A_CONSUMO_TIMpro = 3;
            public static int CONSULTA_TRAFICO_LOCAL_A_CONSUMO_TIMmax = 4;
        }


        public struct DETALLE_LARGA_DISTANCIA
        {
            public static int CONSULTA_LARGA_DISTANCIA_NACIONAL = 20;
            public static int CONSULTA_LARGA_DISTANCIA_INTERNACIONAL = 30;
        }


        public struct DETALLE_OTROS_CARGOS
        {
            public static int CONSULTA_OTROS_CARGOS_Y_ABONOS = 1;
            public static int CONSULTA_OTROS_CARGOS_NO_IGV = 2;
            public static int CONSULTA_COBRANZAS_DIFERIDAS = 3;
        }


        public struct DETALLE_LLAMADAS_INTERNET
        {
            public static int CONSULTA_LLAMADAS_INTERNET = 11;
        }



        public struct DETALLE_TRAFICO_LOCAL
        {
            public static String RTP_OnNet = "RPTOnNet".ToUpper();
            public static String RTP = "RTP".ToUpper();
            public static String OnNet = "OnNet".ToUpper();
            public static String OffNetFijo = "OffNetFijo".ToUpper();
            public static String OffNetCelular = "OffNetCelular".ToUpper();
            public static String OffNet = "OffNet".ToUpper();
            public static String RTP_OnNet_OffNet = "RTP_OnNet_OffNet".ToUpper();
            public static String OnNet_OffNet = "OnNet_OffNet".ToUpper();
            public static String COMPLETO = "TODO".ToUpper();
        }

        public struct CARGO_FIJO_TOTAL_BUSCAR
        {
            public static int SERVICIOS_BASICOS = 1;
            public static int SERVICIOS_ADICIONALES = 2;
        }

    }
}
