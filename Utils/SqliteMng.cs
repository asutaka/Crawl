using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Utils
{
    public class SqliteMng
    {
        private static SQLiteConnection _conn;
        private static SQLiteConnection GetConnection()
        {
            try
            {
                if (_conn != null)
                {
                    if (_conn.State != System.Data.ConnectionState.Open)
                    {
                        _conn.Open();
                    }
                    return _conn;
                }
                var conStr = $@"Data Source={Directory.GetCurrentDirectory()}\{Helper.GetDatabase()};";
                _conn = new SQLiteConnection(conStr);
                _conn.Open();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetConnection|EXCEPTION| {ex.Message}");
            }
            return _conn;
        }

        public static void Insert(string query)
        {
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = query;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetConnection|EXCEPTION|INPUT: {query}| {ex.Message}");
            }
        }

        public static void Update(string query)
        {
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = query;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.UpdateData|EXCEPTION|INPUT: {query}| {ex.Message}");
            }
        }

        public static List<T> Get<T>(string query)
        {
            var lstResult = new List<T>();
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = query;
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var newObject = (T)Activator.CreateInstance(typeof(T));
                            dataReader.MapDataToObject(newObject);
                            lstResult.Add(newObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetData|EXCEPTION| {ex.Message}");
            }
            return lstResult;
        }

        public static bool Exist(string query)
        {
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = query;
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.CheckExist|EXCEPTION|INPUT: {query}| {ex.Message}");
            }
            return false;
        }
    }
}
