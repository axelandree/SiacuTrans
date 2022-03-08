using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections;

namespace Claro.Data.Database
{
    internal static class OraDbTypeTable
    {
        internal static readonly int[] dbTypeToOracleDbTypeMapping = new int[0x88];
        internal static readonly int[] oraTypeToOracleDbTypeMapping = new int[0xfd];
        public static readonly Hashtable s_table = new Hashtable(0x5c);

        static OraDbTypeTable()
        {
            dbTypeToOracleDbTypeMapping[0] = 0x7e;
            dbTypeToOracleDbTypeMapping[0x16] = 0x68;
            dbTypeToOracleDbTypeMapping[1] = 120;
            dbTypeToOracleDbTypeMapping[2] = 0x67;
            dbTypeToOracleDbTypeMapping[5] = 0x6a;
            dbTypeToOracleDbTypeMapping[6] = 0x7b;
            dbTypeToOracleDbTypeMapping[7] = 0x6b;
            dbTypeToOracleDbTypeMapping[8] = 0x6c;
            dbTypeToOracleDbTypeMapping[10] = 0x6f;
            dbTypeToOracleDbTypeMapping[11] = 0x70;
            dbTypeToOracleDbTypeMapping[12] = 0x71;
            dbTypeToOracleDbTypeMapping[15] = 0x7a;
            dbTypeToOracleDbTypeMapping[0x10] = 0x7e;
            dbTypeToOracleDbTypeMapping[0x17] = 0x68;
            dbTypeToOracleDbTypeMapping[0x11] = 0x7b;
            dbTypeToOracleDbTypeMapping[3] = 0x86;
            dbTypeToOracleDbTypeMapping[13] = 0x79;
            dbTypeToOracleDbTypeMapping[0x65] = 13;
            dbTypeToOracleDbTypeMapping[0x66] = 13;
            dbTypeToOracleDbTypeMapping[0x67] = 2;
            dbTypeToOracleDbTypeMapping[0x68] = 0x17;
            dbTypeToOracleDbTypeMapping[0x69] = 13;
            dbTypeToOracleDbTypeMapping[0x6a] = 5;
            dbTypeToOracleDbTypeMapping[0x6b] = 7;
            dbTypeToOracleDbTypeMapping[0x6c] = 8;
            dbTypeToOracleDbTypeMapping[0x84] = 8;
            dbTypeToOracleDbTypeMapping[0x6f] = 10;
            dbTypeToOracleDbTypeMapping[0x70] = 11;
            dbTypeToOracleDbTypeMapping[0x71] = 12;
            dbTypeToOracleDbTypeMapping[0x72] = 13;
            dbTypeToOracleDbTypeMapping[0x73] = 12;
            dbTypeToOracleDbTypeMapping[0x6d] = 0x10;
            dbTypeToOracleDbTypeMapping[110] = 1;
            dbTypeToOracleDbTypeMapping[0x75] = 0x17;
            dbTypeToOracleDbTypeMapping[0x74] = 13;
            dbTypeToOracleDbTypeMapping[0x77] = 0x10;
            dbTypeToOracleDbTypeMapping[120] = 1;
            dbTypeToOracleDbTypeMapping[0x79] = 13;
            dbTypeToOracleDbTypeMapping[0x7a] = 15;
            dbTypeToOracleDbTypeMapping[0x85] = 15;
            dbTypeToOracleDbTypeMapping[0x7b] = 6;
            dbTypeToOracleDbTypeMapping[0x7c] = 6;
            dbTypeToOracleDbTypeMapping[0x7d] = 6;
            dbTypeToOracleDbTypeMapping[0x7e] = 0x10;
            dbTypeToOracleDbTypeMapping[0x7f] = 0x10;
            dbTypeToOracleDbTypeMapping[0x86] = 3;
            dbTypeToOracleDbTypeMapping[0x81] = 13;
            dbTypeToOracleDbTypeMapping[130] = 13;
            dbTypeToOracleDbTypeMapping[0x80] = 13;

            oraTypeToOracleDbTypeMapping[0x60] = 0x68;
            oraTypeToOracleDbTypeMapping[1] = 0x7e;
            oraTypeToOracleDbTypeMapping[12] = 0x6a;
            oraTypeToOracleDbTypeMapping[0xbd] = 0x73;
            oraTypeToOracleDbTypeMapping[190] = 0x72;
            oraTypeToOracleDbTypeMapping[8] = 0x6d;
            oraTypeToOracleDbTypeMapping[0x18] = 110;
            oraTypeToOracleDbTypeMapping[2] = 0x6b;
            oraTypeToOracleDbTypeMapping[0x65] = 0x84;
            oraTypeToOracleDbTypeMapping[100] = 0x85;
            oraTypeToOracleDbTypeMapping[0x16] = 0x84;
            oraTypeToOracleDbTypeMapping[0x15] = 0x85;
            oraTypeToOracleDbTypeMapping[0x72] = 0x65;
            oraTypeToOracleDbTypeMapping[0x71] = 0x66;
            oraTypeToOracleDbTypeMapping[0x70] = 0x69;
            oraTypeToOracleDbTypeMapping[0x68] = 0x7e;
            oraTypeToOracleDbTypeMapping[0x17] = 120;
            oraTypeToOracleDbTypeMapping[0xbb] = 0x7b;
            oraTypeToOracleDbTypeMapping[0xbc] = 0x7d;
            oraTypeToOracleDbTypeMapping[0xe8] = 0x7c;
            oraTypeToOracleDbTypeMapping[0x6c] = 0x7f;
            oraTypeToOracleDbTypeMapping[110] = 130;
            oraTypeToOracleDbTypeMapping[0x7a] = 0x80;
            oraTypeToOracleDbTypeMapping[0xfc] = 0x86;

            InsertTableEntries();
        }

        public static OracleDbType ConvertNumberToOraDbType(int precision, int scale)
        {
            OracleDbType @decimal = OracleDbType.Decimal;
            if ((scale <= 0) && ((precision - scale) < 5))
            {
                return OracleDbType.Int16;
            }
            if ((scale <= 0) && ((precision - scale) < 10))
            {
                return OracleDbType.Int32;
            }
            if ((scale <= 0) && ((precision - scale) < 0x13))
            {
                return OracleDbType.Int64;
            }
            if ((precision < 8) && (((scale <= 0) && ((precision - scale) <= 0x26)) || ((scale > 0) && (scale <= 0x2c))))
            {
                return OracleDbType.Single;
            }
            if (precision < 0x10)
            {
                @decimal = OracleDbType.Double;
            }
            return @decimal;
        }

        internal static void InsertTableEntries()
        {
            s_table.Add(typeof(byte), OracleDbType.Byte);
            s_table.Add(typeof(byte[]), OracleDbType.Raw);
            s_table.Add(typeof(char), OracleDbType.Varchar2);
            s_table.Add(typeof(char[]), OracleDbType.Varchar2);
            s_table.Add(typeof(DateTime), OracleDbType.TimeStamp);
            s_table.Add(typeof(short), OracleDbType.Int16);
            s_table.Add(typeof(int), OracleDbType.Int32);
            s_table.Add(typeof(long), OracleDbType.Int64);
            s_table.Add(typeof(float), OracleDbType.Single);
            s_table.Add(typeof(double), OracleDbType.Double);
            s_table.Add(typeof(decimal), OracleDbType.Decimal);
            s_table.Add(typeof(string), OracleDbType.Varchar2);
            s_table.Add(typeof(TimeSpan), OracleDbType.IntervalDS);
            s_table.Add(typeof(OracleBFile), OracleDbType.BFile);
            s_table.Add(typeof(OracleBinary), OracleDbType.Raw);
            s_table.Add(typeof(OracleBlob), OracleDbType.Blob);
            s_table.Add(typeof(OracleClob), OracleDbType.Clob);
            s_table.Add(typeof(OracleDate), OracleDbType.Date);
            s_table.Add(typeof(OracleDecimal), OracleDbType.Decimal);
            s_table.Add(typeof(OracleIntervalDS), OracleDbType.IntervalDS);
            s_table.Add(typeof(OracleIntervalYM), OracleDbType.IntervalYM);
            s_table.Add(typeof(OracleRefCursor), OracleDbType.RefCursor);
            s_table.Add(typeof(OracleString), OracleDbType.Varchar2);
            s_table.Add(typeof(OracleTimeStamp), OracleDbType.TimeStamp);
            s_table.Add(typeof(OracleTimeStampLTZ), OracleDbType.TimeStampLTZ);
            s_table.Add(typeof(OracleTimeStampTZ), OracleDbType.TimeStampTZ);
            s_table.Add(typeof(OracleXmlType), OracleDbType.XmlType);
            s_table.Add(typeof(OracleRef), OracleDbType.Ref);
            s_table.Add(typeof(bool), OracleDbType.Boolean);
        }
    }
}
