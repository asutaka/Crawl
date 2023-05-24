using Crawl.Model;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

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
            var sqlite_cmd = GetConnection().CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO CongTy(ID, TenCongTy, TenGiaoDich, LoaiHinhHoatDong, MaSoThue, DiaChi, DaiDienPhapLuat, NgayCapGiayPhep, NgayHoatDong, DienThoaiTruSo, TrangThai, TinhThanh, QuanHuyen, PhuongXa, CreatedDate) " +
                $"VALUES('{Guid.NewGuid()}', '{param.TenCongTy}', '{param.TenGiaoDich}', '{param.LoaiHinhHoatDong}', '{param.MaSoThue}', '{param.DiaChi}', '{param.DaiDienPhapLuat}', '{param.NgayCapGiayPhep}', '{param.NgayHoatDong}', '{param.DienThoaiTruSo}', '{param.TrangThai}', '{param.TinhThanh}', '{param.QuanHuyen}', '{param.PhuongXa}', '{DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")}'); ";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static List<CongTyDTO> GetData()
        {
            var lstResult = new List<CongTyDTO>();
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM CongTy ORDER BY ngayhoatdong DESC";
                using (var dataReader = sqlite_cmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var newObject = new CongTyDTO();
                            dataReader.MapDataToObject(newObject);
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
        }

        public static bool CheckExist(string MaSoThue)
        {
            try
            {
                var sqlite_cmd = GetConnection().CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM CongTy WHERE MaSoThue = '{MaSoThue}'";
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
                NLogLogger.PublishException(ex, $"SqliteMng.CheckExist|EXCEPTION| {ex.Message}");
            }
            return false;
        }
    }
}
