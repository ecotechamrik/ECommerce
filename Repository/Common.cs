using System;
using System.Data.Common;

namespace Repository
{
    public class Common
    {
        public static string SafeGetString(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetString(reader.GetOrdinal(column));
            return string.Empty;
        }

        public static int SafeGetInt(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetInt32(reader.GetOrdinal(column));
            return 0;
        }

        public static double SafeGetDouble(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetDouble(reader.GetOrdinal(column));
            return 0;
        }
        public static DateTime SafeGetDate(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetDateTime(reader.GetOrdinal(column));
            return DateTime.Now;
        }

        public static Boolean SafeGetBoolean(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetBoolean(reader.GetOrdinal(column));
            return false;
        }
    }
}
