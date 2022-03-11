using System;
using System.Data;
using System.Reflection;

namespace Claro.Data
{
    public static class IEnumerable
    {
        public static void Entity(this object source, IDataReader reader)
        {
            DbColumnAttribute dbColumnAttribute;
            Type typeItem = source.GetType();
            PropertyInfo[] properties;
            PropertyInfo property;
            int ordinal;
            object value;

            properties = typeItem.GetProperties();

            if (properties != null && properties.Length > 0)
            {
                for (int i = 0, j = properties.Length; i < j; i++)
                {
                    property = properties[i];

                    dbColumnAttribute = property.GetCustomAttribute<DbColumnAttribute>(true);

                    if (dbColumnAttribute != null)
                    {
                        ordinal = reader.GetOrdinal(dbColumnAttribute.Name);

                        if (ordinal >= 0)
                        {
                            value = reader[ordinal];

                            if (!reader.IsDBNull(ordinal))
                            {
                                property.SetValue(source, Convert.ChangeType(value, property.PropertyType));
                            }
                        }
                    }
                }


            }
        }

        public static void Add1(this System.Collections.IEnumerable source, IDataReader reader)
        {
            Type typeItem = source.GetType().GetGenericArguments()[0];

            object item;

            System.Collections.IList ilist = source as System.Collections.IList;

            do
            {
                while (reader.Read())
                {
                    item = Activator.CreateInstance(typeItem);

                    item.Entity(reader);

                    ilist.Add(item);
                }
            } while (reader.NextResult());
        }
    }
}
