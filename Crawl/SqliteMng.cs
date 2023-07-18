using Crawl.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;

namespace Crawl
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
                var conStr = $@"Data Source={Directory.GetCurrentDirectory()}\sqlite.db;";
                _conn = new SQLiteConnection(conStr);
                _conn.Open();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetConnection|EXCEPTION| {ex.Message}");
            }
            return _conn;
        }

        public static void InsertData(CongTyDTO param)
        {
            var command = "INSERT INTO CongTy(ID, TenCongTy, TenGiaoDich, LoaiHinhHoatDong, MaSoThue, DiaChi, DaiDienPhapLuat, NgayCapGiayPhep, NgayHoatDong, DienThoaiTruSo, TrangThai, TinhThanh, QuanHuyen, PhuongXa, LinkWeb, CreatedDate) " +
                    $"VALUES('{Guid.NewGuid()}', '{param.TenCongTy.CheckNull().Replace("'", "")}', '{param.TenGiaoDich.CheckNull().Replace("'", "")}', '{param.LoaiHinhHoatDong.CheckNull()}', '{param.MaSoThue.CheckNull()}', '{param.DiaChi.CheckNull().Replace("'", "")}', '{param.DaiDienPhapLuat.CheckNull().Replace("'", "")}', '{param.NgayCapGiayPhep.FormatDate()}', '{param.NgayHoatDong.FormatDate()}', '{param.DienThoaiTruSo.CheckNull()}', '{param.TrangThai.CheckNull()}', '{param.TinhThanh.CheckNull().Replace("'", "")}', '{param.QuanHuyen.CheckNull().Replace("'", "")}', '{param.PhuongXa.CheckNull().Replace("'", "")}', '{param.LinkWeb.CheckNull()}', '{DateTime.Now.ToString("yyyy-MM-dd")}'); ";
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = command;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetConnection|EXCEPTION|INPUT: {command}| {ex.Message}");
            }
        }

        public static void UpdateData(string id, string des)
        {
            var command = $"update CongTy set MoTa = '{des}' where ID = '{id}'";
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = command;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.UpdateData|EXCEPTION|INPUT: {command}| {ex.Message}");
            }
        }

        public static List<CongTyDTO> GetData(string clause)
        {
            var lstResult = new List<CongTyDTO>();
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM CongTy WHERE 1 = 1 {clause} ORDER BY ngayhoatdong DESC";
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var newObject = new CongTyDTO();
                            dataReader.MapDataToObject(newObject);
                            newObject.DienThoaiTruSoImg = GetImage(newObject.DienThoaiTruSo);
                            lstResult.Add(newObject);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetData|EXCEPTION| {ex.Message}");
            }
            return lstResult;
            
            Image GetImage(string url)
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
                catch(Exception ex)
                {
                    NLogLogger.PublishException(ex, $"SqliteMng.GetImage|EXCEPTION| {ex.Message}");
                    return null;
                }
            }
        }

        public static int TotalRow(string clause)
        {
            var command = $"select COUNT(1) as PageNum from CongTy WHERE 1 = 1 {clause}";
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = command;
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var newObject = new PageDTO();
                            dataReader.MapDataToObject(newObject);
                            return newObject.PageNum;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.CheckExist|EXCEPTION|INPUT: {command}| {ex.Message}");
            }
            return 0;
        }

        public static bool CheckExist(string MaSoThue)
        {
            var command = $"SELECT * FROM CongTy WHERE MaSoThue = '{MaSoThue}'";
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = command;
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
                NLogLogger.PublishException(ex, $"SqliteMng.CheckExist|EXCEPTION|INPUT: {command}| {ex.Message}");
            }
            return false;
        }

        public static void UpdatePage(PageDTO param)
        {
            var command = $"update PageTbl set pagenum = {param.PageNum}";
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = command;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.UpdatePage|EXCEPTION|INPUT: {command}| {ex.Message}");
            }
        }

        public static PageDTO GetPage()
        {
            var result = new PageDTO();
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM PageTbl";
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var newObject = new PageDTO();
                            dataReader.MapDataToObject(newObject);
                            return newObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"SqliteMng.GetPage|EXCEPTION| {ex.Message}");
            }
            return result;
        }
    }
}
