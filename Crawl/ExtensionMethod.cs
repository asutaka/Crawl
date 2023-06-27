using FastMember;
using System;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;

namespace Crawl
{
    public static class ExtensionMethod
    {
        public static void MapDataToObject<T>(this SQLiteDataReader dataReader, T newObject)
        {
            try
            {
                if (newObject == null) throw new ArgumentNullException(nameof(newObject));

                // Fast Member Usage
                var objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
                var propertiesHashSet =
                        objectMemberAccessor
                        .GetMembers()
                        .Select(mp => mp.Name)
                        .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    var name = propertiesHashSet.FirstOrDefault(a => a.Equals(dataReader.GetName(i), StringComparison.InvariantCultureIgnoreCase));
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var obj = objectMemberAccessor[newObject, name];
                        if(obj != null)
                        {
                            var type = obj.GetType();
                            if ("int32".Equals(type.Name, StringComparison.InvariantCultureIgnoreCase))
                            {
                                objectMemberAccessor[newObject, name] = dataReader.IsDBNull(i) ? 0 : int.Parse(dataReader.GetValue(i).ToString());
                            }
                            else
                            {
                                objectMemberAccessor[newObject, name] = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                            }
                        }
                        else
                        {
                            objectMemberAccessor[newObject, name] = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.MapDataToObject|EXCEPTION| {ex.Message}");
            }
        }

        public static string CheckNull(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            return val.Trim();
        }

        public static string FormatDate(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            try
            {
                var dt = DateTime.ParseExact(val, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return dt.ToString("yyyy-MM-dd");
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.MapDataToObject|EXCEPTION| {ex.Message}");
            }
            return string.Empty;
        }
    }
}
