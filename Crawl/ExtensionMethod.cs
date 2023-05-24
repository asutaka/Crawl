using FastMember;
using System;
using System.Data.SQLite;
using System.Linq;

namespace Crawl
{
    public static class ExtensionMethod
    {
        public static void MapDataToObject<T>(this SQLiteDataReader dataReader, T newObject)
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
                if (!String.IsNullOrEmpty(name))
                {
                    //Attention! if you are getting errors here, then double check that your model and sql have matching types for the field name.
                    //Check api.log for error message!
                    objectMemberAccessor[newObject, name]
                        = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                }
            }
        }
    }
}
