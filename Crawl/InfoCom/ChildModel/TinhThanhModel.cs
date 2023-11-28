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
    }

    public class TinhThanhModel
    {
        public List<TinhThanhDTO> lData { get; set; }

        public TinhThanhModel()
        {
            lData = new List<TinhThanhDTO>();
            lData.Add(new TinhThanhDTO { TenTinhThanh = "An Giang", MaMap = "tinh-an-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bà Rịa - Vũng Tàu", MaMap = "tinh-ba-ria-vung-tau" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Giang", MaMap = "tinh-bac-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Kạn", MaMap = "tinh-bac-kan" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bạc Liêu", MaMap = "tinh-bac-lieu" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bắc Ninh", MaMap = "tinh-bac-ninh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bến Tre", MaMap = "tinh-ben-tre" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Định", MaMap = "tinh-binh-dinh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Dương", MaMap = "tinh-binh-duong" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Phước", MaMap = "tinh-binh-phuoc" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Bình Thuận", MaMap = "tinh-binh-thuan" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cà Mau", MaMap = "tinh-ca-mau" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cần Thơ", MaMap = "thanh-pho-can-tho" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Cao Bằng", MaMap = "tinh-cao-bang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đà Nẵng", MaMap = "thanh-pho-da-nang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Lắk", MaMap = "tinh-dak-lak" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đắk Nông", MaMap = "tinh-dak-nong" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Điện Biên", MaMap = "tinh-dien-bien" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Nai", MaMap = "tinh-dong-nai" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Đồng Tháp", MaMap = "tinh-dong-thap" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Gia Lai", MaMap = "tinh-gia-lai" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Giang", MaMap = "tinh-ha-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nam", MaMap = "tinh-ha-nam" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Nội", MaMap = "thanh-pho-ha-noi" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hà Tĩnh", MaMap = "tinh-ha-tinh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Dương", MaMap = "tinh-hai-duong" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hải Phòng", MaMap = "thanh-pho-hai-phong" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hậu Giang", MaMap = "tinh-hau-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hòa Bình", MaMap = "tinh-hoa-binh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hồ Chí Minh", MaMap = "thanh-pho-ho-chi-minh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Hưng Yên", MaMap = "tinh-hung-yen" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Khánh Hòa", MaMap = "tinh-khanh-hoa" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kiên Giang", MaMap = "tinh-kien-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Kon Tum", MaMap = "tinh-kon-tum" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lai Châu", MaMap = "tinh-lai-chau" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lâm Đồng", MaMap = "tinh-lam-dong" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lạng Sơn", MaMap = "tinh-lang-son" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Lào Cai", MaMap = "tinh-lao-cai" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Long An", MaMap = "tinh-long-an" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nam Định", MaMap = "tinh-nam-dinh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Nghệ An", MaMap = "tinh-nghe-an" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Bình", MaMap = "tinh-ninh-binh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Ninh Thuận", MaMap = "tinh-ninh-thuan" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Thọ", MaMap = "tinh-phu-tho" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Phú Yên", MaMap = "tinh-phu-yen" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Bình", MaMap = "tinh-quang-binh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Nam", MaMap = "tinh-quang-nam" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ngãi", MaMap = "tinh-quang-ngai" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Ninh", MaMap = "tinh-quang-ninh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Quảng Trị", MaMap = "tinh-quang-tri" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sóc Trăng", MaMap = "tinh-soc-trang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Sơn La", MaMap = "tinh-son-la" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tây Ninh", MaMap = "tinh-tay-ninh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Bình", MaMap = "tinh-thai-binh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thái Nguyên", MaMap = "tinh-thai-nguyen" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thanh Hóa", MaMap = "tinh-thanh-hoa" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Thừa Thiên - Huế", MaMap = "tinh-thua-thien-hue" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tiền Giang", MaMap = "tinh-tien-giang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Trà Vinh", MaMap = "tinh-tra-vinh" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Tuyên Quang", MaMap = "tinh-tuyen-quang" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Long", MaMap = "tinh-vinh-long" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Vĩnh Phúc", MaMap = "tinh-vinh-phuc" });
            lData.Add(new TinhThanhDTO { TenTinhThanh = "Yên Bái", MaMap = "tinh-yen-bai" });
        }
    }
}
