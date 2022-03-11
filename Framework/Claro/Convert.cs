using System;

namespace Claro
{
    public static class Convert
    {
        public static string ToString(object value)
        {
            string salida;
            if (value == null || value == System.DBNull.Value)
                salida = "";
            else
                salida = value.ToString();
            return salida.Trim();
        }


        public static Int64 ToInt64(object value)
        {
            Int64 salida;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (System.Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = System.Convert.ToInt64(value);
            }
            return salida;
        }

        public static DateTime ToDate(object value)
        {
            DateTime salida;
            if (value == null || value == System.DBNull.Value || String.IsNullOrEmpty(value.ToString()))
                salida = DateTime.Now;
            else
                salida = System.Convert.ToDateTime(value);

            return salida;
        }
        public static DateTime? ToDateNullable(object value)
        {
            DateTime? salida;
            if (value == null || value == System.DBNull.Value || String.IsNullOrEmpty(value.ToString()))
                salida = (DateTime?)null;
            else
                salida = System.Convert.ToDateTime(value);

            return salida;
        }


        public static decimal ToDecimal(object value)
        {
            decimal salida = 0;
            if (value != null && value != System.DBNull.Value)
            {
                decimal.TryParse(value.ToString(), out salida);
            }

            return salida;
        }

        public static int ToInt(object value)
        {
            int salida;

            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (System.Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = System.Convert.ToInt32(value);
            }

            return salida;
        }

        public static double ToDouble(object value)
        {
            double salida;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (System.Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = System.Convert.ToDouble(value);
            }

            return salida;
        }       

    }
}
