using System.Collections.Generic;

namespace Crawl.Model
{
    public class TinhThanhDTO
    {
        public string TenTinhThanh { get; set; }
        public string MaMap { get; set; }
    }

    public class TinhThanhModel
    {
        public List<TinhThanhDTO> lData { get; set; }

        public TinhThanhModel()
        {
            lData.Add(new TinhThanhDTO { TenTinhThanh = "An Giang", MaMap= "an-giang-93" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bà Rịa - Vũng Tàu", MaMap= "ba-ria-vung-tau-32" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Giang", MaMap= "bac-giang-72" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Kạn", MaMap= "bac-kan-1127" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bạc Liêu", MaMap= "bac-lieu-197" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Ninh", MaMap= "bac-ninh-170" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bến Tre", MaMap= "ben-tre-185" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Định", MaMap= "binh-dinh-152" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Dương", MaMap= "binh-duong-17" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Phước", MaMap= "binh-phuoc-1" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Thuận", MaMap= "binh-thuan-20" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cà Mau", MaMap= "ca-mau-108" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cần Thơ", MaMap= "can-tho-96" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cao Bằng", MaMap= "cao-bang-1612" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đà Nẵng", MaMap= "da-nang-35" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Lắk", MaMap= "dak-lak-214" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Nông", MaMap= "dak-nong-245" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Điện Biên", MaMap= "dien-bien-1007" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Nai", MaMap= "dong-nai-57" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Tháp", MaMap= "dong-thap-63" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Gia Lai", MaMap= "gia-lai-563" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Giang", MaMap= "ha-giang-529" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nam", MaMap= "ha-nam-162" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nội", MaMap= "ha-noi-7" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Tĩnh", MaMap= "ha-tinh-342" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Dương", MaMap= "hai-duong-147" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Phòng", MaMap= "hai-phong-99" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hậu Giang", MaMap= "hau-giang-190" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hồ Chí Minh", MaMap= "ho-chi-minh-23" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hòa Bình", MaMap= "hoa-binh-786" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hưng Yên", MaMap= "hung-yen-123" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Khánh Hòa", MaMap= "khanh-hoa-26" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kiên Giang", MaMap= "kien-giang-80" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kon Tum", MaMap= "kon-tum-956" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lai Châu", MaMap= "lai-chau-2501" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lâm Đồng", MaMap= "lam-dong-10" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lạng Sơn", MaMap= "lang-son-984" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lào Cai", MaMap= "lao-cai-320" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Long An", MaMap= "long-an-29" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nam Định", MaMap= "nam-dinh-137" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nghệ An", MaMap= "nghe-an-144" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Bình", MaMap= "ninh-binh-75" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Thuận", MaMap= "ninh-thuan-11" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Thọ", MaMap= "phu-tho-134" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Yên", MaMap= "phu-yen-14" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Bình", MaMap= "quang-binh-60" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Nam", MaMap= "quang-nam-49" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ngãi", MaMap= "quang-ngai-301" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ninh", MaMap= "quang-ninh-142" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Trị", MaMap= "quang-tri-69" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sóc Trăng", MaMap= "soc-trang-949" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sơn La", MaMap= "son-la-316" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tây Ninh", MaMap= "tay-ninh-90" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Bình", MaMap= "thai-binh-128" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Nguyên", MaMap= "thai-nguyen-131" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thanh Hóa", MaMap= "thanh-hoa-4" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thừa Thiên Huế", MaMap= "thua-thien-hue-66" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tiền Giang", MaMap= "tien-giang-177" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Trà Vinh", MaMap= "tra-vinh-41" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tuyên Quang", MaMap= "tuyen-quang-1284" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Long", MaMap= "vinh-long-193" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Phúc", MaMap= "vinh-phuc-420" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Yên Bái", MaMap= "yen-bai-724" });
        }
    }
}
