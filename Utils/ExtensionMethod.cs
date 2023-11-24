﻿using FastMember;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Utils
{
    public static class ExtensionMethod
    {
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
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.MapDataToObject|EXCEPTION| {ex.Message}");
            }
            return string.Empty;
        }
        public static string GetDisplayName(this Enum enumValue)
        {
            try
            {
                return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
            }
            catch
            {
                return string.Empty;
            }
        }
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
                        if (obj != null)
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
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.MapDataToObject|EXCEPTION| {ex.Message}");
            }
        }
        public static Image GetImage(this string url)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(url.Replace("data:image/png;base64,", string.Empty));
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                return image;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetImage|EXCEPTION| {ex.Message}");
                return null;
            }
        }
        public static T LoadJsonFile<T>(this string name)
        {
            try
            {
                using (StreamReader r = new StreamReader(name))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.LoadJsonFile|EXCEPTION| {ex.Message}");
            }
            return default(T);
        }

        public static void UpdateJsonFile<T>( this T obj, string name)
        {
            try
            {
                File.WriteAllText(name, JsonConvert.SerializeObject(obj));
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.UpdateJsonFile|EXCEPTION| {ex.Message}");
            }
        }
    }
}