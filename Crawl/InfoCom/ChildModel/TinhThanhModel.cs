using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawl.InfoCom.ChildModel
{
    public class TinhThanhDTO
    {
        public string TenTinhThanh { get; set; }
        public string MaMap { get; set; }
        public int Page { get; set; }
    }

    public class TinhThanhModel
    {
        public List<TinhThanhDTO> lData { get; set; }

        public TinhThanhModel()
        {
            lData = new List<TinhThanhDTO>();
            lData.Add(new TinhThanhDTO { TenTinhThanh = "An Giang", MaMap = "tinh-an-giang", Page = 60 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bà Rịa - Vũng Tàu", MaMap = "tinh-ba-ria-vung-tau", Page = 143 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Giang", MaMap = "tinh-bac-giang", Page = 127 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Kạn", MaMap = "tinh-bac-kan", Page = 9 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bạc Liêu", MaMap = "tinh-bac-lieu", Page = 22 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Ninh", MaMap = "tinh-bac-ninh", Page = 219 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bến Tre", MaMap = "tinh-ben-tre", Page = 39 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Định", MaMap = "tinh-binh-dinh", Page = 75 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Dương", MaMap = "tinh-binh-duong", Page = 534 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Phước", MaMap = "tinh-binh-phuoc", Page = 85 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Thuận", MaMap = "tinh-binh-thuan", Page = 58 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cà Mau", MaMap = "tinh-ca-mau", Page = 44 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cần Thơ", MaMap = "thanh-pho-can-tho", Page = 128 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cao Bằng", MaMap = "tinh-cao-bang", Page = 12 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đà Nẵng", MaMap = "thanh-pho-da-nang", Page = 275 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Lắk", MaMap = "tinh-dak-lak", Page = 84 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Nông", MaMap = "tinh-dak-nong", Page = 29 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Điện Biên", MaMap = "tinh-dien-bien", Page = 12 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Nai", MaMap = "tinh-dong-nai", Page = 304 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Tháp", MaMap = "tinh-dong-thap", Page = 49 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Gia Lai", MaMap = "tinh-gia-lai", Page = 60 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Giang", MaMap = "tinh-ha-giang", Page = 15 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nam", MaMap = "tinh-ha-nam", Page = 56 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nội", MaMap = "thanh-pho-ha-noi", Page = 2343 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Tĩnh", MaMap = "tinh-ha-tinh", Page = 52 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Dương", MaMap = "tinh-hai-duong", Page = 118 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Phòng", MaMap = "thanh-pho-hai-phong", Page = 241 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hậu Giang", MaMap = "tinh-hau-giang", Page = 39 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hòa Bình", MaMap = "tinh-hoa-binh", Page = 34 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hồ Chí Minh", MaMap = "thanh-pho-ho-chi-minh", Page = 3741 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hưng Yên", MaMap = "tinh-hung-yen", Page = 100 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Khánh Hòa", MaMap = "tinh-khanh-hoa", Page = 128 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kiên Giang", MaMap = "tinh-kien-giang", Page = 103 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kon Tum", MaMap = "tinh-kon-tum", Page = 21 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lai Châu", MaMap = "tinh-lai-chau", Page = 13 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lâm Đồng", MaMap = "tinh-lam-dong", Page = 93 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lạng Sơn", MaMap = "tinh-lang-son", Page = 40 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lào Cai", MaMap = "tinh-lao-cai", Page = 43 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Long An", MaMap = "tinh-long-an", Page = 160 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nam Định", MaMap = "tinh-nam-dinh", Page = 74 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nghệ An", MaMap = "tinh-nghe-an", Page = 134 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Bình", MaMap = "tinh-ninh-binh", Page = 54 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Thuận", MaMap = "tinh-ninh-thuan", Page = 34 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Thọ", MaMap = "tinh-phu-tho", Page = 66 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Yên", MaMap = "tinh-phu-yen", Page = 35 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Bình", MaMap = "tinh-quang-binh", Page = 41 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Nam", MaMap = "tinh-quang-nam", Page = 82 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ngãi", MaMap = "tinh-quang-ngai", Page = 51 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ninh", MaMap = "tinh-quang-ninh", Page = 111 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Trị", MaMap = "tinh-quang-tri", Page = 31 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sóc Trăng", MaMap = "tinh-soc-trang", Page = 29 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sơn La", MaMap = "tinh-son-la", Page = 21 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tây Ninh", MaMap = "tinh-tay-ninh", Page = 60 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Bình", MaMap = "tinh-thai-binh", Page = 73 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Nguyên", MaMap = "tinh-thai-nguyen", Page = 72 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thanh Hóa", MaMap = "tinh-thanh-hoa", Page = 209 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thừa Thiên - Huế", MaMap = "tinh-thua-thien-hue", Page = 52 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tiền Giang", MaMap = "tinh-tien-giang", Page = 65 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Trà Vinh", MaMap = "tinh-tra-vinh", Page = 34 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tuyên Quang", MaMap = "tinh-tuyen-quang", Page = 21 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Long", MaMap = "tinh-vinh-long", Page = 30 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Phúc", MaMap = "tinh-vinh-phuc", Page = 99 });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Yên Bái", MaMap = "tinh-yen-bai", Page = 25 });
        }
    }
}
