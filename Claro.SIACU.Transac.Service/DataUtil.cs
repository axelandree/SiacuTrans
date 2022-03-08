using System;

namespace Claro.SIACU.Transac.Service
{
    public class DataUtil
    {
        public static T? DbValueToNullable<T>(object dbValue) where T : struct
        {
            T? returnValue = null;

            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }

            return returnValue;
        }

        public static T DbValueToDefault<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value) return default(T);
            return (T)obj;
        }


        
    }
}
