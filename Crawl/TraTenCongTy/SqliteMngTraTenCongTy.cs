using Crawl.Model;
using Crawl.TraTenCongTy.ChildModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Crawl.TraTenCongTy
{
    public class SqliteMngTraTenCongTy
    {
        public static void InsertData(CongTyDTO param)
        {
            var command = "INSERT INTO CongTy(ID, TenCongTy, TenGiaoDich, LoaiHinhHoatDong, MaSoThue, DiaChi, DaiDienPhapLuat, NgayCapGiayPhep, NgayHoatDong, DienThoaiTruSo, TrangThai, TinhThanh, QuanHuyen, PhuongXa, LinkWeb, CreatedDate) " +
                    $"VALUES('{Guid.NewGuid()}', '{param.TenCongTy.CheckNull().Replace("'", "")}', '{param.TenGiaoDich.CheckNull().Replace("'", "")}', '{param.LoaiHinhHoatDong.CheckNull()}', '{param.MaSoThue.CheckNull()}', '{param.DiaChi.CheckNull().Replace("'", "")}', '{param.DaiDienPhapLuat.CheckNull().Replace("'", "")}', '{param.NgayCapGiayPhep.FormatDate()}', '{param.NgayHoatDong.FormatDate()}', '{param.DienThoaiTruSo.CheckNull()}', '{param.TrangThai.CheckNull()}', '{param.TinhThanh.CheckNull().Replace("'", "")}', '{param.QuanHuyen.CheckNull().Replace("'", "")}', '{param.PhuongXa.CheckNull().Replace("'", "")}', '{param.LinkWeb.CheckNull()}', '{DateTime.Now.ToString("yyyy-MM-dd")}'); ";
            SqliteMng.Insert(command);
        }

        public static void UpdateData(string id, string des)
        {
            var command = $"update CongTy set MoTa = '{des}' where ID = '{id}'";
            SqliteMng.Update(command);
        }

        public static List<CongTyDTO> GetData(string clause)
        {
            var command  = $"SELECT * FROM CongTy WHERE 1 = 1 {clause} ORDER BY ngayhoatdong DESC";
            return SqliteMng.Get<CongTyDTO>(command);
        }

        public static int TotalRow(string clause)
        {
            var command = $"select COUNT(1) as PageNum from CongTy WHERE 1 = 1 {clause}";
            return SqliteMng.Get<PageDTO>(command)?.FirstOrDefault()?.PageNum ?? 0;
        }

        public static bool CheckExist(string MaSoThue)
        {
            var command = $"SELECT * FROM CongTy WHERE MaSoThue = '{MaSoThue}'";
            return SqliteMng.Exist(command);
        }
    }
}
