using FastMember;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Utils
{
    public static class ExtensionMethod
    {
        private static RestClient _client = new RestClient(new RestClientOptions()
        {
            Proxy = new WebProxy()
            {
                Address = new Uri("http://proxy.zenrows.com:8001"),
                Credentials = new NetworkCredential("2e71a878b4001dab17981e09f064683dbd1519c1", "")
            },
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        });

        public static string CheckNull(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            return val.Trim();
        }
        public static string FormatDate(this string val, string format = "dd/MM/yyyy")
        {
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            try
            {
                var dt = DateTime.ParseExact(val, format, CultureInfo.InvariantCulture);
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

        public static string CleanDate(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            return val.Replace("00:00:00","").Trim();
        }
        public static string MaxLengthText(this string str, int length = 50, bool cutEnd = true)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            if (str.Length > length)
            {
                if (cutEnd)
                    return $"{str.Substring(0, length)}...";

                return $"...{str.Substring(str.Length - length)}";
            }
            return str;
        }
        public static string GetHtml(this string url)
        {
            try
            {
                var response = _client.Get(new RestRequest(url));
                return response.Content;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
            }
            return string.Empty;
        }
    }
}
