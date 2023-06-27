using Crawl.Model;
using HtmlAgilityPack;
using PuppeteerSharp;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawl.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlRealtimeJobFake : IJob
    {
        private bool IsFirst = true;
        public void Execute(IJobExecutionContext context)
        {
            //Handle("https://www.tratencongty.com/?page=1", 0);

            Handle("https://www.tratencongty.com/?page=1").GetAwaiter().GetResult();
            if(IsFirst)
            {
                IsFirst = false;
                Handle("https://www.tratencongty.com/?page=2").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=3").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=4").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=5").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=6").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=7").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=8").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=9").GetAwaiter().GetResult();
                Handle("https://www.tratencongty.com/?page=10").GetAwaiter().GetResult();
            }
        }

        public async static Task Handle(string url)
        {
            await new BrowserFetcher().DownloadAsync();
            IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Timeout = 0,
                Args = new[] { "--no-sandbox" }
            });

            try
            {
                IPage _page = await browser.NewPageAsync();
                await _page.SetViewportAsync(ViewPortOptions.Default);
                _page.DefaultTimeout = 0;

                await _page.GoToAsync(url, WaitUntilNavigation.Networkidle2);
                var html = await _page.GetContentAsync();

                var lstLink = new List<string>();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var indexMain = 1;
                var emptyData = 0;
                do
                {
                    var node = htmlDoc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/div[{indexMain}]/a");
                    if (node != null)
                    {
                        string hrefValue = node.Attributes["href"].Value.Trim();
                        if (!string.IsNullOrWhiteSpace(hrefValue))
                        {
                            lstLink.Add(hrefValue);
                        }
                        emptyData = 1;
                    }
                    else
                    {
                        emptyData++;
                    }

                    indexMain++;
                    if (emptyData >= 3)
                    {
                        indexMain = -1;
                    }
                }
                while (indexMain > 0);

                //                #region fix Data
                //                var lstLink = new List<string> {
                //                    "https://www.tratencongty.com/company/3fb72f49-cong-ty-tnhh-truyen-thong-va-giai-tri-mau/"
                //,"https://www.tratencongty.com/company/10ff27037-cong-ty-tnhh-san-xuat-thuong-mai-dich-vu-thai-cuong/"
                //,"https://www.tratencongty.com/company/1574b8124-cong-ty-tnhh-xuat-nhap-khau-hao-dong/"
                //,"https://www.tratencongty.com/company/a4878545-cong-ty-tnhh-xay-dung-dv-thuong-mai-minh-khoi/"
                //,"https://www.tratencongty.com/company/9e99bd8f-cong-ty-tnhh-bao-anh-kg/"
                //,"https://www.tratencongty.com/company/9e99bd88-cong-ty-co-phan-thuong-mai-dich-vu-xay-dung-phat-an-khang/"
                //,"https://www.tratencongty.com/company/10ff27009-cong-ty-tnhh-tmdv-van-tai-huong-quynh/"
                //,"https://www.tratencongty.com/company/98a24182-cong-ty-tnhh-quoc-trung-vinh-xuong/"
                //,"https://www.tratencongty.com/company/9e99bd81-cong-ty-tnhh-thuong-mai-dich-vu-va-trang-tri-noi-that-hoang-tu/"
                //,"https://www.tratencongty.com/company/10ff26eef-cong-ty-tnhh-mot-thanh-vien-san-xuat-thanh-long-phat/"
                //,"https://www.tratencongty.com/company/9e99bd6f-cong-ty-tnhh-be-thui-cau-mong-quang-nam-19/"
                //,"https://www.tratencongty.com/company/8cb6c623-cong-ty-tnhh-vat-lieu-xay-dung-bao-kim/"
                //,"https://www.tratencongty.com/company/10ff27030-cong-ty-tnhh-dien-tu-kuzumi-viet-nam/"
                //,"https://www.tratencongty.com/company/121acc1e3-cong-ty-tnhh-mtv-an-vinh-hieu/"
                //,"https://www.tratencongty.com/company/14b69fef6-cong-ty-tnhh-phuong-hang-thai-nguyen/"
                //,"https://www.tratencongty.com/company/14b69ff47-cong-ty-tnhh-quang-mien-home-decor/"
                //,"https://www.tratencongty.com/company/121acc1f5-cong-ty-tnhh-mtv-ac-international/"
                //,"https://www.tratencongty.com/company/4c14e590-cong-ty-tnhh-dau-tu-xay-dung-thuong-mai-hai-dang/"
                //,"https://www.tratencongty.com/company/1810057b7-cong-ty-co-phan-thuong-mai-tri-duc-son-la/"
                //,"https://www.tratencongty.com/company/9e99bd61-dntn-tran-diem-am/"
                //,"https://www.tratencongty.com/company/1751716d3-cong-ty-tnhh-mot-thanh-vien-truong-phat-68/"
                //,"https://www.tratencongty.com/company/c83edc2f-cong-ty-tnhh-dich-vu-thuong-mai-va-san-xuat-nhua-ntt/"
                //,"https://www.tratencongty.com/company/c24d4704-cong-ty-tnhh-thuong-mai-va-san-xuat-tohana/"
                //,"https://www.tratencongty.com/company/c24d4735-cong-ty-tnhh-san-xuat-va-thuong-mai-tong-hop-hai-phat/"
                //,"https://www.tratencongty.com/company/4c14e4ce-cong-ty-tnhh-tmdv-lam-ha/"
                //,"https://www.tratencongty.com/company/8cb6c61c-cong-ty-tnhh-thuong-mai-dich-vu-xuat-nhap-khau-tinh-que/"
                //,"https://www.tratencongty.com/company/c24d4712-cong-ty-tnhh-dich-vu-thuong-mai-vinh-hung-thuan/"
                //,"https://www.tratencongty.com/company/98a2417b-cong-ty-tnhh-san-xuat-thuong-mai-viet-an-tram/"
                //,"https://www.tratencongty.com/company/b658f2b9-dntn-tin-thanh-tv/"
                //,"https://www.tratencongty.com/company/103ddf148-cong-ty-tnhh-muoi-travel-la-ca-dao-phu-quy/"
                //,"https://www.tratencongty.com/company/115dc4efb-cong-ty-tnhh-mtv-thanh-vinh/"
                //,"https://www.tratencongty.com/company/115dc4f0d-cong-ty-tnhh-san-xuat-vy-nhan-vi/"
                //,"https://www.tratencongty.com/company/115dc4f1d-cong-ty-tnhh-fude/"
                //,"https://www.tratencongty.com/company/4c14e395-cong-ty-tnhh-van-tai-va-xep-do-huu-cong/"
                //,"https://www.tratencongty.com/company/d42c9939-cong-ty-tnhh-co-khi-che-tao-hoang-anh/"
                //,"https://www.tratencongty.com/company/13f769ec8-cong-ty-tnhh-dich-vu-dia-chat-va-xay-dung-cong-trinh-khanh-lam/"
                //,"https://www.tratencongty.com/company/4c14e19a-cong-ty-tnhh-vlxd-phat-trien-pham-tuong-8/"
                //,"https://www.tratencongty.com/company/929cb0c9-cong-ty-tnhh-dau-tu-phat-trien-an-phat-t-l/"
                //,"https://www.tratencongty.com/company/4c14e024-cong-ty-tnhh-dau-tu-phat-trien-cong-nghe-sen-vang/"
                //,"https://www.tratencongty.com/company/4c14e26c-cong-ty-tnhh-mtv-dich-vu-tu-van-tan-tam/"
                //,"https://www.tratencongty.com/company/4c14e347-cong-ty-tnhh-thuong-mai-dich-vu-co-khi-xay-dung-quynh-an/"
                //,"https://www.tratencongty.com/company/4c14e23b-cong-ty-tnhh-kinh-doanh-kim-thanh-tan/"
                //,"https://www.tratencongty.com/company/14b69ff24-cong-ty-tnhh-thai-ha-thai-nguyen/"
                //,"https://www.tratencongty.com/company/19ede9c48-cong-ty-tnhh-tm-dv-nhu-ngoc-bmt/"
                //,"https://www.tratencongty.com/company/4c14de92-cong-ty-tnhh-dau-tu-va-phat-trien-thanh-hoang/"
                //,"https://www.tratencongty.com/company/5cf96dd4-cong-ty-tnhh-lo-c-pha-t-68/"
                //,"https://www.tratencongty.com/company/fdef5d3d-dntn-hieu-vang-bac-hoang-thien/"
                //,"https://www.tratencongty.com/company/4c14deb2-cong-ty-tnhh-co-khi-can-dien-tu-tan-viet-my/"
                //,"https://www.tratencongty.com/company/fdef5d4f-cong-ty-tnhh-thuong-mai-vat-lieu-xay-dung-nhu-giang/"
                //,"https://www.tratencongty.com/company/e0368754-cong-ty-tnhh-thuong-mai-crditway/"
                //,"https://www.tratencongty.com/company/ec1605e0-cong-ty-tnhh-hung-nghiep-binh-minh/"
                //,"https://www.tratencongty.com/company/1b6a238d8-cong-ty-tnhh-mtv-xay-dung-nguyen-hao/"
                //,"https://www.tratencongty.com/company/115dc4f44-cong-ty-tnhh-go-duc-thinh-phat/"
                //,"https://www.tratencongty.com/company/ec1605f9-cong-ty-tnhh-mtv-gia-huy-phong/"
                //,"https://www.tratencongty.com/company/c24d45f1-cong-ty-tnhh-van-chuyen-thuan-thoi-dat-ssd/"
                //,"https://www.tratencongty.com/company/4530694b-cong-ty-tnhh-xay-dung-va-dich-vu-du-lich-duc-phuc-travel/"
                //,"https://www.tratencongty.com/company/4c14dffa-cong-ty-tnhh-san-xuat-rang-gia-nha-khoa-kim-chau/"
                //,"https://www.tratencongty.com/company/10ff26ff0-cong-ty-tnhh-xay-dung-trung-hoa-thang/"
                //,"https://www.tratencongty.com/company/115dc4e7c-cong-ty-tnhh-thuong-mai-thiet-ke-xay-dung-phuc-gia-khang/"
                //,"https://www.tratencongty.com/company/98a24162-cong-ty-tnhh-thl-an-giang/"
                //,"https://www.tratencongty.com/company/103ddf128-cong-ty-tnhh-mtv-xay-dung-va-thuong-mai-trinh-ha-gia/"
                //,"https://www.tratencongty.com/company/10ff26fe6-cong-ty-tnhh-xay-dung-va-co-khi-an-huy-phat/"
                //,"https://www.tratencongty.com/company/1397d6db9-cong-ty-tnhh-sx-tm-dv-dai-thang/"
                //,"https://www.tratencongty.com/company/7ad2bb7f-cong-ty-tnhh-thuong-mai-dich-vu-dai-hong-phat-star/"
                //,"https://www.tratencongty.com/company/9e99bd30-cong-ty-tnhh-xay-lap-dien-tuyet-giang/"
                //,"https://www.tratencongty.com/company/bc50b577-cong-ty-tnhh-mot-thanh-vien-tran-an/"
                //,"https://www.tratencongty.com/company/109e6fb15-cong-ty-tnhh-dau-tu-gdp-gnh/"
                //,"https://www.tratencongty.com/company/1397d6dc3-cong-ty-tnhh-uyen-home-spa-beauty-center/"
                //,"https://www.tratencongty.com/company/1810057a6-cong-ty-tnhh-thuy-san-huy-hung/"
                //,"https://www.tratencongty.com/company/4c14e04b-dntn-y-hoc-co-truyen-ky-linh/"
                //,"https://www.tratencongty.com/company/7ad2bba6-cong-ty-tnhh-san-xuat-va-thuong-mai-thien-duc/"
                //,"https://www.tratencongty.com/company/7ad2bbbf-cong-ty-tnhh-dau-tu-xay-dung-hga/"
                //,"https://www.tratencongty.com/company/7ad2bbc6-cong-ty-tnhh-san-xuat-kinh-doanh-nong-nghiep-tong-hop-long-an/"
                //,"https://www.tratencongty.com/company/7ad2bbe1-cong-ty-tnhh-thuong-mai-dich-vu-le-binh-long-an/"
                //,"https://www.tratencongty.com/company/5cf96dc6-cong-ty-tnhh-thuong-mai-dau-tu-va-xay-dung-moi-truong-xanh/"
                //,"https://www.tratencongty.com/company/16336b371-cong-ty-tnhh-mot-thanh-vien-future-center/"
                //,"https://www.tratencongty.com/company/16336b383-cong-ty-co-phan-thuy-dien-phu-binh/"
                //,"https://www.tratencongty.com/company/9e99bd37-cong-ty-tnhh-can-xuan-phat/"
                //,"https://www.tratencongty.com/company/10ff26fc6-cong-ty-tnhh-vi-dieu-phap-hanh-thien-dong-nai/"
                //,"https://www.tratencongty.com/company/115dc4e44-cong-ty-tnhh-thuong-mai-va-san-xuat-my-nghe-nhat-minh/"
                //,"https://www.tratencongty.com/company/115dc4ef4-cong-ty-tnhh-pvb-logistics/"
                //,"https://www.tratencongty.com/company/115dc4ed4-cong-ty-tnhh-mtv-xay-dung-thuong-mai-ngan-long/"
                //,"https://www.tratencongty.com/company/4c14e173-cong-ty-tnhh-tu-van-dau-tu-bat-dong-san-van-mai/"
                //,"https://www.tratencongty.com/company/115dc4ecd-cong-ty-tnhh-mtv-co-khi-xay-dung-minh-anh/"
                //,"https://www.tratencongty.com/company/4c14de11-cong-ty-tnhh-kinh-doanh-thuong-mai-thuy-san-son-ha/"
                //,"https://www.tratencongty.com/company/127a19347-cong-ty-tnhh-thuong-mai-npp-huy-le/"
                //,"https://www.tratencongty.com/company/127a1934e-cong-ty-tnhh-mai-quynh-tien/"
                //,"https://www.tratencongty.com/company/1751716c1-cong-ty-tnhh-thuong-mai-xuat-nhap-khau-thanh-lam/"
                //,"https://www.tratencongty.com/company/18d02c18e-cong-ty-tnhh-mot-thanh-vien-minh-tri-hl-68/"
                //,"https://www.tratencongty.com/company/18d02c195-cong-ty-tnhh-1tv-van-tai-va-thuong-mai-chi-cong/"
                //,"https://www.tratencongty.com/company/16f2313f5-cong-ty-tnhh-san-xuat-nam-an-nam-duoc-lieu-mu-cang-chai/"
                //,"https://www.tratencongty.com/company/121acc1d5-cong-ty-tnhh-bds-nguyen-hoang-anh/"
                //,"https://www.tratencongty.com/company/4c14de21-cong-ty-tnhh-dau-tu-va-phat-trien-thuong-mai-dich-vu-thanh-cong/"
                //,"https://www.tratencongty.com/company/4c14e01d-cong-ty-tnhh-thiet-bi-dien-dien-lanh-phuoc-khanh/"
                //,"https://www.tratencongty.com/company/c83edc40-cong-ty-tnhh-sx-tmth-van-dong/"
                //,"https://www.tratencongty.com/company/1b6a238d1-cong-ty-tnhh-xnk-hoang-sang/"
                //,"https://www.tratencongty.com/company/3fb6f0e2-cong-ty-tnhh-thuong-mai-va-du-lich-an-tien-phat/"
                //,"https://www.tratencongty.com/company/115dc4e5c-cong-ty-tnhh-mtv-thu-mua-phe-lieu-hong-duc-phat/"
                //,"https://www.tratencongty.com/company/4c14db81-cong-ty-tnhh-quang-cao-truyen-thong-hong-phat/"
                //,"https://www.tratencongty.com/company/4c14dc78-cong-ty-tnhh-san-xuat-thuong-mai-nhua-diem-mi/"
                //,"https://www.tratencongty.com/company/115dc4e9c-cong-ty-tnhh-thuong-mai-dien-tu-duy-dai-phat/"
                //,"https://www.tratencongty.com/company/4c14db68-cong-ty-tnhh-thuong-mai-quang-cao-va-truyen-thong-thu-viet/"
                //,"https://www.tratencongty.com/company/4c14ddd1-cong-ty-tnhh-dau-tu-thuong-mai-va-dich-vu-thuan-thien-long/"
                //,"https://www.tratencongty.com/company/115dc4eaa-cong-ty-tnhh-tm-dv-xd-khang-minh-tuan/"
                //,"https://www.tratencongty.com/company/4c14ddc3-cong-ty-tnhh-thuong-mai-dich-vu-an-uong-nhu-y/"
                //,"https://www.tratencongty.com/company/115dc4eb4-cong-ty-tnhh-mot-thanh-vien-mua-ban-vat-lieu-xay-dung-an-binh-phat/"
                //,"https://www.tratencongty.com/company/4c14dba8-cong-ty-tnhh-thuong-mai-dau-tu-dich-vu-gia-han/"
                //,"https://www.tratencongty.com/company/3fb6f0b1-cong-ty-tnhh-ky-thuat-jiu-jiu/"
                //,"https://www.tratencongty.com/company/115dc4e75-cong-ty-tnhh-mtv-thanh-trien-viet-nam/"
                //,"https://www.tratencongty.com/company/115dc4ea3-cong-ty-tnhh-mtv-dich-vu-an-kim/"
                //,"https://www.tratencongty.com/company/f1fa77f5-cong-ty-tnhh-tmdv-tong-hop-phuoc-an/"
                //,"https://www.tratencongty.com/company/4c14de0a-cong-ty-tnhh-giai-phap-xe-hoi-vn/"
                //,"https://www.tratencongty.com/company/11bb61a34-cong-ty-tnhh-dau-tu-phat-trien-bat-dong-san-thanh-phat/"
                //,"https://www.tratencongty.com/company/14b69fefd-cong-ty-tnhh-quan-minh-song-cong/"
                //,"https://www.tratencongty.com/company/f1fa77ee-cong-ty-tnhh-nuoi-trong-thuy-san-va-xay-dung-thanh-minh/"
                //,"https://www.tratencongty.com/company/6ed961d3-cong-ty-co-phan-tu-van-dau-tu-xay-dung-hai-hung-viet-nam/"
                //,"https://www.tratencongty.com/company/3fb6eff6-cong-ty-tnhh-xin-chao/"
                //,"https://www.tratencongty.com/company/3fb6f040-cong-ty-tnhh-oanh-van/"
                //,"https://www.tratencongty.com/company/511c0c98-cong-ty-tnhh-dau-tu-dia-oc-hoang-thinh-land/"
                //,"https://www.tratencongty.com/company/98a24158-dntn-tien-hung-cho-moi/"
                //,"https://www.tratencongty.com/company/16336b36a-cong-ty-tnhh-xay-dung-va-thuong-mai-ngoc-phong/"
                //,"https://www.tratencongty.com/company/511c0c2e-cong-ty-tnhh-thuong-mai-va-truyen-thong-dc-digital/"
                //,"https://www.tratencongty.com/company/511c0c7f-cong-ty-tnhh-dau-tu-tmdv-dien-lanh-an-phat/"
                //,"https://www.tratencongty.com/company/511c0c20-cong-ty-tnhh-mtv-the-box/"
                //,"https://www.tratencongty.com/company/4c14da75-cong-ty-tnhh-dau-tu-kinh-doanh-phat-trien-thuong-mai-nam-long/"
                //,"https://www.tratencongty.com/company/4c14df2a-cong-ty-tnhh-kinh-doanh-vat-lieu-xay-dung-va-thi-cong-cfc/"
                //,"https://www.tratencongty.com/company/4c14df31-cong-ty-tnhh-massage-goi-dau-mai-lan/"
                //,"https://www.tratencongty.com/company/aa6d48c0-cong-ty-tnhh-thien-an-hotel/"
                //,"https://www.tratencongty.com/company/e61e99ab-cong-ty-co-phan-tu-van-thiet-ke-va-san-xuat-thuong-mai-p-t/"
                //,"https://www.tratencongty.com/company/e61e99b5-cong-ty-tnhh-kien-truc-va-noi-that-quoc-oai-home/"
                //,"https://www.tratencongty.com/company/10ff26fbf-cong-ty-tnhh-tmdv-dau-tu-phat-trien-bich-phuong/"
                //,"https://www.tratencongty.com/company/18d02c187-cong-ty-co-phan-dich-vu-thuong-mai-va-che-bien-thuc-pham-hung-thinh/"
                //,"https://www.tratencongty.com/company/80c32b08-cong-ty-tnhh-xang-dau-bac-a/"
                //,"https://www.tratencongty.com/company/4c14da8e-cong-ty-tnhh-bao-hiem-thien-khoi/"
                //,"https://www.tratencongty.com/company/1751716a9-cong-ty-tnhh-s-home-corporation/"
                //,"https://www.tratencongty.com/company/4c14dee0-cong-ty-tnhh-xay-dung-cong-trinh-ha-tang-thanh-pho/"
                //,"https://www.tratencongty.com/company/c83edbd6-cong-ty-tnhh-thuong-mai-dich-vu-va-xay-dung-minh-tri-vu/"
                //,"https://www.tratencongty.com/company/c83edbe8-cong-ty-tnhh-cnc-bg/"
                //,"https://www.tratencongty.com/company/103ddf121-cong-ty-tnhh-khoang-san-nhu-y-2/"
                //,"https://www.tratencongty.com/company/1a4cd6c62-cong-ty-tnhh-thuong-mai-va-dich-vu-du-tonkin-mang-den/"
                //,"https://www.tratencongty.com/company/1514bd126-cong-ty-tnhh-thuong-mai-du-lich-thanh-vinh/"
                //,"https://www.tratencongty.com/company/4c14da4e-cong-ty-tnhh-thiet-ke-co-khi-xay-dung-truong-khanh/"
                //,"https://www.tratencongty.com/company/80c32b0f-cong-ty-tnhh-health-nara/"
                //,"https://www.tratencongty.com/company/c24d4673-cong-ty-tnhh-tu-van-dau-tu-va-xay-dung-ibh-viet-nam/"
                //,"https://www.tratencongty.com/company/198e03f55-cong-ty-tnhh-mot-thanh-vien-dong-thuy-gia-lai/"
                //,"https://www.tratencongty.com/company/4c14dc58-cong-ty-tnhh-thuong-mai-dich-vu-nong-san-thuan-thien/"
                //,"https://www.tratencongty.com/company/192eefab3-cong-ty-tnhh-thuong-mai-va-dich-vu-asanco-hp/"
                //,"https://www.tratencongty.com/company/192eefaa1-cong-ty-tnhh-mtv-sx-kd-m/"
                //,"https://www.tratencongty.com/company/127a1931d-cong-ty-tnhh-tra-hoang-gia-tam-ky/"
                //,"https://www.tratencongty.com/company/e0368726-cong-ty-tnhh-thuc-pham-thanh-cong-th/"
                //,"https://www.tratencongty.com/company/c83edbcf-cong-ty-tnhh-van-bam-phuc-loi/"
                //,"https://www.tratencongty.com/company/133983066-cong-ty-tnhh-thuong-mai-xay-dung-ky-thuat-dien-kt-mien-trung/"
                //,"https://www.tratencongty.com/company/4c14dbe0-cong-ty-tnhh-thuong-mai-dich-vu-dau-tu-phu-an/"
                //,"https://www.tratencongty.com/company/4c14deea-cong-ty-tnhh-thuong-mai-dich-vu-huy-ton/"
                //,"https://www.tratencongty.com/company/ec1605c0-cong-ty-tnhh-co-khi-xay-du-ng-va-thuong-ma-i-huy-hoa-ng-68/"
                //,"https://www.tratencongty.com/company/18d02c167-cong-ty-co-phan-thuong-mai-co-khi-thc/"
                //,"https://www.tratencongty.com/company/fdef5d25-cong-ty-tnhh-dau-tu-xay-dung-phat-trien-bat-dong-san-eco-land/"
                //,"https://www.tratencongty.com/company/4c14daed-cong-ty-tnhh-thuong-mai-vat-lieu-xay-dung-song-nam/"
                //,"https://www.tratencongty.com/company/da205ae5-cong-ty-tnhh-van-tai-thuy-phuong-hanh/"
                //,"https://www.tratencongty.com/company/f7ea725a-cong-ty-tnhh-mtv-van-tai-xuyen-viet-duy-long/"
                //,"https://www.tratencongty.com/company/10ff26f67-cong-ty-tnhh-co-khi-xay-dung-thanh-thai/"
                //,"https://www.tratencongty.com/company/109e6fac7-cong-ty-tnhh-hoa-tuoi-van-hien/"
                //,"https://www.tratencongty.com/company/6ed96149-cong-ty-co-phan-xay-dung-thuong-mai-va-dich-vu-89/"
                //,"https://www.tratencongty.com/company/109e6faee-cong-ty-tnhh-mtv-nha-khoa-hoa-su/"
                //,"https://www.tratencongty.com/company/12d9cf71f-cong-ty-tnhh-trang-thiet-bi-vat-tu-y-te-an-thinh/"
                //,"https://www.tratencongty.com/company/13f769eb0-cong-ty-tnhh-thuong-mai-va-dich-vu-bds-huynh-tran/"
                //,"https://www.tratencongty.com/company/18d02c16e-cong-ty-tnhh-dich-vu-du-lich-hoang-anh-qn/"
                //,"https://www.tratencongty.com/company/fdef5d2f-dntn-hoang-hai-vinh-phat/"
                //,"https://www.tratencongty.com/company/19ede9bf7-cong-ty-tnhh-an-nam-ea-sup/"
                //,"https://www.tratencongty.com/company/109e6faf5-cong-ty-tnhh-giai-tri-dien-tu-tai-nguyen/"
                //,"https://www.tratencongty.com/company/11bb61a45-cong-ty-tnhh-thuan-detailing-93/"
                //,"https://www.tratencongty.com/company/14b69feef-cong-ty-tnhh-dich-vu-va-thuong-mai-duong-phuong/"
                //,"https://www.tratencongty.com/company/68e76e33-cong-ty-tnhh-san-xuat-thuong-mai-gia-phuc-hd/"
                //,"https://www.tratencongty.com/company/4c14d795-cong-ty-tnhh-qa-deco/"
                //,"https://www.tratencongty.com/company/19ede9c10-cong-ty-co-phan-hoa-nam-contrucsion/"
                //,"https://www.tratencongty.com/company/68e76dcd-cong-ty-tnhh-thiet-ke-va-thi-cong-noi-ngoai-that-tan-hung-thinh/"
                //,"https://www.tratencongty.com/company/11bb61a4c-cong-ty-tnhh-mo-t-tha-nh-vien-thu-c-pha-m-ong-kha-i-s/"
                //,"https://www.tratencongty.com/company/1397d6d99-cong-ty-tnhh-tm-dv-du-lich-hai-dang-traval/"
                //,"https://www.tratencongty.com/company/f7ea7253-cong-ty-tnhh-xay-dung-tong-hop-va-thuong-mai-minh-tam/"
                //,"https://www.tratencongty.com/company/9e99bcf7-cong-ty-tnhh-du-lich-sinh-thai-tran-chung/"
                //,"https://www.tratencongty.com/company/10ff26f98-cong-ty-tnhh-mtv-xay-dung-bat-dac/"
                //,"https://www.tratencongty.com/company/11bb61a14-cong-ty-tnhh-thuong-mai-dich-vu-kieu-anh-phat/"
                //,"https://www.tratencongty.com/company/133983046-cong-ty-tnhh-duxi-media/"
                //,"https://www.tratencongty.com/company/68e76db4-cong-ty-tnhh-thuong-mai-thien-uy/"
                //,"https://www.tratencongty.com/company/4c14db30-cong-ty-tnhh-may-mac-mai-thien/"
                //,"https://www.tratencongty.com/company/4c14d88c-cong-ty-tnhh-san-xuat-thuong-mai-dich-vu-rong-a-chau/"
                //,"https://www.tratencongty.com/company/11bb61a3b-cong-ty-tnhh-mtv-thuong-mai-vinh-gia-phat/"
                //,"https://www.tratencongty.com/company/14565b7d5-cong-ty-tnhh-giong-thuy-san-cong-nghe-xanh/"
                //,"https://www.tratencongty.com/company/192eefa7a-cong-ty-tnhh-hai-thanh-vien-pony-land/"
                //,"https://www.tratencongty.com/company/109e6face-cong-ty-tnhh-thuong-mai-dich-vu-lam-sang-phat/"
                //,"https://www.tratencongty.com/company/3fb6eb56-cong-ty-tnhh-hae-san-korea/"
                //,"https://www.tratencongty.com/company/109e6fad5-cong-ty-tnhh-dt-tm-nhut-nam/"
                //,"https://www.tratencongty.com/company/a487849c-cong-ty-tnhh-dien-nuoc-chd/"
                //,"https://www.tratencongty.com/company/a48784ed-cong-ty-tnhh-phat-trien-giao-duc-tu-van-va-tri-lieu-tam-ly-milestones/"
                //,"https://www.tratencongty.com/company/15d40c8a2-cong-ty-tnhh-hong-phat-vq/"
                //,"https://www.tratencongty.com/company/a48784c3-cong-ty-tnhh-thuong-mai-dich-vu-xay-dung-tong-hop-le-thanh-dat/"
                //,"https://www.tratencongty.com/company/a48784bc-cong-ty-tnhh-protect-farm/"
                //,"https://www.tratencongty.com/company/a48784d5-cong-ty-tnhh-thuong-mai-dau-tu-tong-hop-thanh-binh/"
                //,"https://www.tratencongty.com/company/a48784dc-cong-ty-tnhh-dich-vu-boc-xep-tan-khai/"
                //,"https://www.tratencongty.com/company/a48784e6-cong-ty-tnhh-thuong-mai-dich-vu-da-hoa-cuong-lam-kinh/"
                //,"https://www.tratencongty.com/company/a48784b5-cong-ty-tnhh-nong-nghiep-tay-ban-nha/"
                //,"https://www.tratencongty.com/company/9e99bcc6-cong-ty-tnhh-mot-thanh-vien-tm-dv-tuan-kiet-kg/"
                //,"https://www.tratencongty.com/company/9e99bcf0-cong-ty-tnhh-tien-the/"
                //,"https://www.tratencongty.com/company/9e99bd09-cong-ty-tnhh-nong-nghiep-hung-long/"
                //,"https://www.tratencongty.com/company/ce30bde7-cong-ty-tnhh-thuong-mai-va-dich-vu-tong-hop-pd/"
                //,"https://www.tratencongty.com/company/10ff26f40-cong-ty-tnhh-thuong-mai-dich-vu-xay-dung-thai-chuong/"
                //,"https://www.tratencongty.com/company/10ff26f6e-cong-ty-tnhh-thuong-mai-dich-vu-quang-cao-lmt/"
                //,"https://www.tratencongty.com/company/14b69fedd-cong-ty-tnhh-thuong-mai-van-tai-duc-dang/"
                //,"https://www.tratencongty.com/company/ce30bdee-cong-ty-co-phan-shindo-viet-nam/"
                //,"https://www.tratencongty.com/company/ce30bdce-cong-ty-co-phan-kien-truc-thi-cong-biet-thu-lau-dai-viet-classic/"
                //,"https://www.tratencongty.com/company/4c14d62b-cong-ty-tnhh-da-c-sa-n-bi-nh-di-nh-ba-ngoa-i-ka/"
                //,"https://www.tratencongty.com/company/ce30bd9d-cong-ty-tnhh-xay-dung-thuong-mai-va-dich-vu-dien-cong-nghiep-anh-nguyet/"
                //,"https://www.tratencongty.com/company/10ff26f47-cong-ty-tnhh-tm-dv-trung-gia-cam-thanh-khuong-tra/"
                //,"https://www.tratencongty.com/company/e61e9964-cong-ty-tnhh-tu-van-va-xay-dung-minh-hong/"
                //,"https://www.tratencongty.com/company/4c14d2c1-cong-ty-co-phan-kinh-doanh-bat-dong-san-dai-quang/"
                //,"https://www.tratencongty.com/company/9e99bce6-cong-ty-tnhh-chuyen-phat-nhanh-fs-duong-dong-express/"
                //,"https://www.tratencongty.com/company/e61e9952-cong-ty-tnhh-hnk-nghe-an/"
                //,"https://www.tratencongty.com/company/4c14d2e0-cong-ty-tnhh-cao-tien-production/"
                //,"https://www.tratencongty.com/company/62e988a6-cong-ty-co-phan-tu-van-thiet-ke-va-thi-cong-tron-goi-quang-dat/"
                //,"https://www.tratencongty.com/company/b06d8a10-cong-ty-tnhh-khanh-sport/"
                //,"https://www.tratencongty.com/company/10ff26f1d-cong-ty-tnhh-seoul-premium/"
                //,"https://www.tratencongty.com/company/13f769ea9-cong-ty-tnhh-tu-van-giao-duc-nang-moi/"
                //,"https://www.tratencongty.com/company/192eefa50-cong-ty-tnhh-npp-phu-nhuan/"
                //,"https://www.tratencongty.com/company/192eefa49-cong-ty-tnhh-tino-dance/"
                //,"https://www.tratencongty.com/company/4c14d95c-cong-ty-tnhh-thiet-bi-vat-tu-co-khi-va-xay-dung-thien-nam/"
                //,"https://www.tratencongty.com/company/10ff26f27-cong-ty-tnhh-suat-an-cong-nghiep-quang-minh-phat/"
                //,"https://www.tratencongty.com/company/c83edb66-cong-ty-tnhh-xay-dung-va-noi-that-at-home/"
                //,"https://www.tratencongty.com/company/4c14d955-cong-ty-tnhh-tm-dv-hieu-minh/"
                //,"https://www.tratencongty.com/company/4c14d8af-cong-ty-tnhh-dich-vu-spa-massage-linh-ha/"
                //,"https://www.tratencongty.com/company/4c14d904-cong-ty-tnhh-tm-dv-xuat-nhap-khau-hoang-long-khang/"
                //,"https://www.tratencongty.com/company/ec1605b9-cong-ty-tnhh-xay-dung-thuong-mai-phu-son-ha-tinh/"
                //,"https://www.tratencongty.com/company/511c0c55-cong-ty-tnhh-minh-hung-corp/"
                //,"https://www.tratencongty.com/company/511c0bb6-cong-ty-tnhh-dich-vu-lu-hanh-orchard-hill-travel/"
                //,"https://www.tratencongty.com/company/4c14d927-cong-ty-tnhh-vat-lieu-xay-dung-duc-tung/"
                //,"https://www.tratencongty.com/company/511c0c4e-cong-ty-tnhh-thuong-mai-va-dich-vu-vi-dat-trung/"
                //,"https://www.tratencongty.com/company/4c14d8d6-cong-ty-tnhh-thuong-mai-dinh-tran/"
                //,"https://www.tratencongty.com/company/4c14d8cf-cong-ty-tnhh-viet-lux-auto/"
                //,"https://www.tratencongty.com/company/4c14d1e7-cong-ty-tnhh-nha-hang-coc-moi-bia/"
                //,"https://www.tratencongty.com/company/12d9cf706-cong-ty-tnhh-thuong-mai-va-dich-vu-thuy-luc-thanh-chung/"
                //,"https://www.tratencongty.com/company/4c14d885-cong-ty-co-phan-dau-tu-xay-dung-vn-group/"
                //,"https://www.tratencongty.com/company/4c14d8b6-cong-ty-tnhh-tm-dv-boi-phat/"
                //,"https://www.tratencongty.com/company/4c14d7bc-cong-ty-tnhh-thuong-mai-dich-vu-tan-anh-sao/"
                //,"https://www.tratencongty.com/company/10ff26f0f-cong-ty-tnhh-kttm-thuan-phat/"
                //,"https://www.tratencongty.com/company/121acc1ab-cong-ty-tnhh-san-xuat-thuong-mai-le-ngoc-vy/"
                //,"https://www.tratencongty.com/company/4c14d78e-cong-ty-tnhh-dich-vu-phat-trien-giao-duc-duc-tri/"
                //,"https://www.tratencongty.com/company/4c14d865-cong-ty-tnhh-dau-tu-xay-dung-va-noi-that-htb/"
                //,"https://www.tratencongty.com/company/7ad2bb6e-cong-ty-tnhh-mtv-viet-hoa-can-duoc/"
                //,"https://www.tratencongty.com/company/10ff26f16-cong-ty-tnhh-moc-vuong-11/"
                //,"https://www.tratencongty.com/company/4c14d76b-cong-ty-tnhh-thuong-mai-dich-vu-hoang-thien-quan/"
                //,"https://www.tratencongty.com/company/4c14d7f4-cong-ty-co-phan-bat-dong-san-nhat-quang-huy/"
                //,"https://www.tratencongty.com/company/4c14d82d-cong-ty-tnhh-kinh-doanh-dau-tu-thuong-mai-dich-vu-toan-thang/"
                //,"https://www.tratencongty.com/company/4c14d047-cong-ty-tnhh-mot-thanh-vien-duc-thanh-phat/"
                //,"https://www.tratencongty.com/company/4c14d1b9-cong-ty-tnhh-to-chuc-su-kien-tqc/"
                //,"https://www.tratencongty.com/company/4c14d078-cong-ty-tnhh-be-boi-khang-thinh/"
                //,"https://www.tratencongty.com/company/4c14d04e-cong-ty-tnhh-thiet-ke-xay-dung-pham-gia-viet-nam/"
                //,"https://www.tratencongty.com/company/4c14d3a9-cong-ty-tnhh-tu-van-dich-vu-thuong-mai-gia-dinh/"
                //,"https://www.tratencongty.com/company/4c14d5ba-cong-ty-tnhh-tiep-van-dong-a/"
                //,"https://www.tratencongty.com/company/4c14d744-cong-ty-tnhh-ky-thuat-kien-phat/"
                //,"https://www.tratencongty.com/company/14565b7bc-cong-ty-tnhh-thiet-bi-pccc-va-cnch-ninh-thuan/"
                //,"https://www.tratencongty.com/company/19ede9be9-dntn-giai-tri-hung-phat/"
                //,"https://www.tratencongty.com/company/4c14d040-cong-ty-tnhh-focus-media/"
                //,"https://www.tratencongty.com/company/4c14d7c3-cong-ty-tnhh-thuong-mai-dich-vu-cong-nghe-g-p-t/"
                //,"https://www.tratencongty.com/company/e03686bc-cong-ty-tnhh-tu-van-thiet-ke-linh-anh/"
                //,"https://www.tratencongty.com/company/4c14d1a0-cong-ty-tnhh-tm-dv-long-thinh-phat/"
                //,"https://www.tratencongty.com/company/4c14d0f0-cong-ty-tnhh-cong-nghiep-cat-tuong-group/"
                //,"https://www.tratencongty.com/company/4c14d73d-cong-ty-tnhh-thuong-mai-dich-vu-xay-dung-tam-duc/"
                //,"https://www.tratencongty.com/company/e61e9942-cong-ty-co-phan-y-te-bac-nghe/"
                //,"https://www.tratencongty.com/company/453068d3-cong-ty-tnhh-tm-thien-thanh-viet-nam/"
                //,"https://www.tratencongty.com/company/4c14d231-cong-ty-co-phan-dau-tu-xuat-nhap-khau-thuong-mai-dich-vu-hachi-solar/"
                //,"https://www.tratencongty.com/company/7ad2baaf-cong-ty-tnhh-mtv-van-tai-thuy-noi-dia-thanh-dat/"
                //,"https://www.tratencongty.com/company/7ad2baf6-cong-ty-tnhh-thuong-mai-va-san-xuat-thuc-pham-a-ly-son/"
                //,"https://www.tratencongty.com/company/aa6d48a0-cong-ty-tnhh-mot-thanh-vien-thuy-san-le-van-loi/"
                //,"https://www.tratencongty.com/company/e03686fc-cong-ty-tnhh-tm-dv-th-auto/"
                //,"https://www.tratencongty.com/company/e0368703-cong-ty-tnhh-thuong-mai-va-dich-vu-giai-tri-the-new/"
                //,"https://www.tratencongty.com/company/e61e9914-cong-ty-tnhh-hn-duc-do/"
                //,"https://www.tratencongty.com/company/7ad2baa5-cong-ty-tnhh-tm-sx-xnk-to-yen-phuc-tri/"
                //,"https://www.tratencongty.com/company/103ddf0de-cong-ty-tnhh-qua-cua-bien/"
                //,"https://www.tratencongty.com/company/fdef5cfe-cong-ty-tnhh-dau-tu-xay-dung-va-thuong-mai-hong-dai-phat/"
                //,"https://www.tratencongty.com/company/e036870d-cong-ty-tnhh-thuong-mai-dv-anh-phat/"
                //,"https://www.tratencongty.com/company/7ad2bab6-cong-ty-tnhh-tmdv-tong-hop-minh-ha/"
                //,"https://www.tratencongty.com/company/127a192f6-cong-ty-tnhh-thuong-mai-va-dich-vu-chong-tham-an-hai/"
                //,"https://www.tratencongty.com/company/511c0c04-cong-ty-tnhh-mot-thanh-vien-hoang-nhat-tien/"
                //,"https://www.tratencongty.com/company/7ad2bafd-cong-ty-tnhh-mtv-dich-vu-thuong-mai-xnk-cong-hieu/"
                //,"https://www.tratencongty.com/company/7ad2bb2e-cong-ty-tnhh-tm-dau-tu-xay-dung-long-giang/"
                //,"https://www.tratencongty.com/company/133983026-cong-ty-tnhh-phuc-thien-nt/"
                //,"https://www.tratencongty.com/company/19ede9bf0-cong-ty-tnhh-thuong-mai-nong-san-quyet-thang/"
                //,"https://www.tratencongty.com/company/aa6d4899-cong-ty-tnhh-tu-van-xay-dung-hoa-mai-hp-94/"
                //,"https://www.tratencongty.com/company/3fb6eacd-cong-ty-co-phan-xuat-nhap-khau-va-xay-dung-thanh-cong/"
                //,"https://www.tratencongty.com/company/12d9cf6df-cong-ty-tnhh-dich-vu-tong-hop-ngoi-sao-xanh/"
                //,"https://www.tratencongty.com/company/c83edb7e-cong-ty-tnhh-tu-van-va-dich-vu-doanh-nghiep-tri-viet/"
                //,"https://www.tratencongty.com/company/7ad2bb0f-cong-ty-tnhh-thiet-bi-nha-khoa-big-dental/"
                //,"https://www.tratencongty.com/company/12d9cf6d8-cong-ty-tnhh-dich-vu-thuong-mai-ngoc-thao/"
                //,"https://www.tratencongty.com/company/13398301f-cong-ty-tnhh-co-khi-hoa-chat-nam-long/"
                //,"https://www.tratencongty.com/company/1397d6d92-cong-ty-tnhh-thiet-ke-xay-dung-zoom-house/"
                //,"https://www.tratencongty.com/company/115dc4e1d-cong-ty-tnhh-bao-bi-giay-duy-phuong/"
                //,"https://www.tratencongty.com/company/133983015-cong-ty-tnhh-heritage-na/"
                //,"https://www.tratencongty.com/company/c83edb97-cong-ty-tnhh-htv-phuc-anh/"
                //,"https://www.tratencongty.com/company/74d12b8a-cong-ty-tnhh-non-bo-san-vuon-thanh-rang/"
                //,"https://www.tratencongty.com/company/7ad2baef-cong-ty-tnhh-ttnt-quang-cao-hung-thinh/"
                //,"https://www.tratencongty.com/company/98a2414a-cong-ty-tnhh-dong-phat-ag/"
                //,"https://www.tratencongty.com/company/c83edba5-cong-ty-tnhh-tmxd-van-tai-anh-hoa/"
                //,"https://www.tratencongty.com/company/3fb6ea8d-cong-ty-tnhh-truyen-thong-va-dich-vu-vnjoy/"
                //,"https://www.tratencongty.com/company/74d12bb8-cong-ty-tnhh-dich-vu-dau-tu-tong-hop-minh-phat/"
                //,"https://www.tratencongty.com/company/74d12bd1-cong-ty-tnhh-cong-vien-xanh-phat-dat/"
                //,"https://www.tratencongty.com/company/74d12bdb-cong-ty-tnhh-dau-tu-xay-dung-tong-hop-phu-quy/"
                //,"https://www.tratencongty.com/company/7ad2bb19-cong-ty-tnhh-huynh-duc-tan/"
                //,"https://www.tratencongty.com/company/7ad2bb20-cong-ty-tnhh-xd-sx-tm-an-hung-group/"
                //,"https://www.tratencongty.com/company/7ad2bb47-cong-ty-tnhh-xuat-nhap-khau-duy-phuong-vn/"
                //,"https://www.tratencongty.com/company/7ad2bb4e-cong-ty-co-phan-kien-truc-va-dau-tu-mekong-delta/"
                //,"https://www.tratencongty.com/company/7ad2bb67-cong-ty-tnhh-thuong-mai-dich-vu-auto-365-long-an/"
                //,"https://www.tratencongty.com/company/80c32ae1-cong-ty-tnhh-tm-dv-nhat-phu/"
                //,"https://www.tratencongty.com/company/86b0a4a6-cong-ty-tnhh-phat-trien-nguon-nhan-luc-minna/"
                //,"https://www.tratencongty.com/company/10ff26ebe-cong-ty-tnhh-sx-tm-va-dv-noi-that-nhu-y/"
                //,"https://www.tratencongty.com/company/10ff26ec8-cong-ty-tnhh-thuong-mai-va-dich-vu-nana-aerobic/"
                //,"https://www.tratencongty.com/company/13f769e97-cong-ty-tnhh-dau-tu-thuong-mai-dich-vu-tam-dan/"
                //,"https://www.tratencongty.com/company/16f2313ce-cong-ty-tnhh-cong-nghe-va-dich-vu-y-te-an-thinh-phat/"
                //,"https://www.tratencongty.com/company/16f2313e7-cong-ty-co-phan-dau-tu-xay-dung-va-phat-trien-du-an-quang-vinh/"
                //,"https://www.tratencongty.com/company/10ff26ea5-cong-ty-tnhh-mtv-thao-linh-vmn/"
                //,"https://www.tratencongty.com/company/74d12bb1-cong-ty-tnhh-dich-vu-thanh-quang/"
                //,"https://www.tratencongty.com/company/13f769e90-cong-ty-tnhh-giao-duc-va-dao-tao-nhan-tri-viet/"
                //,"https://www.tratencongty.com/company/127a192e4-cong-ty-tnhh-dich-vu-thanh-nien-bao-an/"
                //,"https://www.tratencongty.com/company/7ad2bad6-cong-ty-tnhh-thuong-mai-dich-vu-trung-hieu-thien-phu/"
                //,"https://www.tratencongty.com/company/7ad2bb40-cong-ty-tnhh-khoa-hoc-cong-nghe-phuc-nguyen/"
                //,"https://www.tratencongty.com/company/3fb6e9eb-cong-ty-co-phan-thuong-mai-va-xuat-nhap-khau-minh-anh/"
                //,"https://www.tratencongty.com/company/115dc4dd3-cong-ty-tnhh-xay-dung-trong-luan/"
                //,"https://www.tratencongty.com/company/3fb6e833-cong-ty-tnhh-va-ng-ba-c-nha-t-nam/"
                //,"https://www.tratencongty.com/company/3fb6e6e8-cong-ty-tnhh-nghien-cuu-ung-dung-va-phat-trien-bao-ngan/"
                //,"https://www.tratencongty.com/company/115dc4dac-cong-ty-tnhh-tm-dv-dau-tu-hoang-anh-phat/"
                //,"https://www.tratencongty.com/company/115dc4dcc-cong-ty-tnhh-an-phat-auto-binh-duong/"
                //,"https://www.tratencongty.com/company/115dc4da5-cong-ty-tnhh-thuong-mai-may-mac-duy-khang/"
                //,"https://www.tratencongty.com/company/3fb6e581-cong-ty-tnhh-cthv-media/"
                //,"https://www.tratencongty.com/company/98a24131-cong-ty-tnhh-thuong-mai-dich-vu-phat-trien-phuoc-dien/"
                //,"https://www.tratencongty.com/company/511c0bf6-cong-ty-tnhh-tu-van-thiet-ke-xay-dung-va-thuong-mai-an-tam-home/"
                //,"https://www.tratencongty.com/company/4c14d5d3-cong-ty-tnhh-cherry-house-spa/"
                //,"https://www.tratencongty.com/company/3fb6e89d-cong-ty-tnhh-thuong-mai-va-xay-dung-nguyen-van-dong/"
                //,"https://www.tratencongty.com/company/3fb6e7db-cong-ty-tnhh-dich-vu-va-thuong-mai-minh-tuan-qt/"
                //,"https://www.tratencongty.com/company/e036868b-cong-ty-tnhh-san-xuat-gia-cong-va-thuong-mai-tong-hop-trung-an/"
                //,"https://www.tratencongty.com/company/4c14d492-cong-ty-tnhh-allure-beauty/"
                //,"https://www.tratencongty.com/company/3fb6e530-cong-ty-tnhh-thoi-trang-bao-binh/"
                //,"https://www.tratencongty.com/company/4c14d542-cong-ty-tnhh-mtv-truye-n-thong-zoom1-production/"
                //,"https://www.tratencongty.com/company/109e6fa5d-cong-ty-tnhh-cung-cap-thuc-pham-thanh-tam/"
                //,"https://www.tratencongty.com/company/109e6fa64-cong-ty-tnhh-tmdv-va-dau-tu-hoang-hung-phu/"
                //,"https://www.tratencongty.com/company/e0368684-cong-ty-tnhh-ca-chat/"
                //,"https://www.tratencongty.com/company/4c14d511-cong-ty-tnhh-sx-tm-xd-da-song-an/"
                //,"https://www.tratencongty.com/company/86b0a49f-cong-ty-tnhh-thuong-mai-xay-dung-quoc-thanh/"
                //,"https://www.tratencongty.com/company/da205aa2-cong-ty-tnhh-mtv-chau-hoan/"
                //,"https://www.tratencongty.com/company/4c14cf57-cong-ty-tnhh-du-lich-bk-travel/"
                //,"https://www.tratencongty.com/company/4c14cf85-cong-ty-tnhh-mtv-viet-tin-hung/"
                //,"https://www.tratencongty.com/company/4c14d433-cong-ty-tnhh-van-chuyen-van-hai/"
                //,"https://www.tratencongty.com/company/198e03f32-cong-ty-tnhh-thuong-mai-va-dich-vu-garage-dinh-nguyen/"
                //,"https://www.tratencongty.com/company/4c14c33d-dntn-nguyen-thi-nhung-thuy/"
                //,"https://www.tratencongty.com/company/4c14d503-cong-ty-tnhh-dich-vu-truyen-thong-va-giai-phap-real-marketing/"
                //,"https://www.tratencongty.com/company/4c14d50a-cong-ty-tnhh-tm-dv-sx-noi-that-dev/"
                //,"https://www.tratencongty.com/company/80c32ada-cong-ty-tnhh-xay-du-ng-ba-o-nguye-n/"
                //,"https://www.tratencongty.com/company/4c14c0e3-cong-ty-tnhh-phu-thanh-luxurious-silver-jewellery/"
                //,"https://www.tratencongty.com/company/da205ac9-cong-ty-tnhh-kien-truc-noi-that-35-sm-home/"
                //,"https://www.tratencongty.com/company/4c14d22a-cong-ty-tnhh-ngoai-ngu-a-chau/"
                //,"https://www.tratencongty.com/company/4c14d523-dntn-sx-pham-van-quyet/"
                //,"https://www.tratencongty.com/company/4c14cdb5-cong-ty-tnhh-thuong-mai-san-xuat-dich-vu-may-mac-kim-oanh/"
                //,"https://www.tratencongty.com/company/10ff26e85-cong-ty-tnhh-san-xuat-tm-va-dv-xay-dung-le-gia/"
                //,"https://www.tratencongty.com/company/511c0ba5-cong-ty-co-phan-xnk-thuy-san-hoang-hiep-phat/"
                //,"https://www.tratencongty.com/company/ec1605b2-cong-ty-tnhh-thuong-mai-dich-vu-hoang-nam-ha-tinh/"
                //,"https://www.tratencongty.com/company/e0368656-cong-ty-tnhh-hong-tuyen-t66/"
                //,"https://www.tratencongty.com/company/e0368625-cong-ty-tnhh-truyen-thong-va-giai-tri-luan-trang/"
                //,"https://www.tratencongty.com/company/4c14d288-cong-ty-tnhh-logo-vui/"
                //,"https://www.tratencongty.com/company/127a192d6-cong-ty-tnhh-ha-tang-xay-dung-phuong-nam/"
                //,"https://www.tratencongty.com/company/4c14d281-cong-ty-tnhh-xay-dung-van-tai-hoa-thanh/"
                //,"https://www.tratencongty.com/company/e036867d-cong-ty-tnhh-dich-vu-va-thuong-mai-vat-lieu-xay-dung-thuan-huong/"
                //,"https://www.tratencongty.com/company/f1fa77ce-cong-ty-tnhh-qbshop/"
                //,"https://www.tratencongty.com/company/1a4cd6c5b-cong-ty-co-phan-duoc-lieu-giakon/"
                //,"https://www.tratencongty.com/company/f1fa77d5-cong-ty-tnhh-hai-dang-dinh/"
                //,"https://www.tratencongty.com/company/8cb6c5f2-cong-ty-tnhh-mot-thanh-vien-xay-dung-van-chuyen-tong-hop-co-gioi-xuan-truong-dong-thap/"
                //,"https://www.tratencongty.com/company/8cb6c5f9-cong-ty-tnhh-danh-loi-thap-muoi/"
                //,"https://www.tratencongty.com/company/4c14d06e-cong-ty-tnhh-mtv-san-xuat-tm-dv-fashion-planet/"
                //,"https://www.tratencongty.com/company/4c14cd16-cong-ty-tnhh-mtv-hiep-binh/"
                //,"https://www.tratencongty.com/company/4c14cf5e-cong-ty-tnhh-mot-thanh-vien-thuong-mai-dich-vu-ho-boi-thang-may-duy-khoi/"
                //,"https://www.tratencongty.com/company/4c14cfcf-cong-ty-tnhh-to-chuc-su-kien-nha-hang-khach-san-hong-hieu/"
                //,"https://www.tratencongty.com/company/4c14d0f7-cong-ty-tnhh-xay-dung-van-tai-viet-tin/"
                //,"https://www.tratencongty.com/company/4c14cfd6-cong-ty-tnhh-tm-dv-toan-my/"
                //,"https://www.tratencongty.com/company/68e76d74-cong-ty-tnhh-thuong-mai-va-xay-dung-thanh-dong-vn/"
                //,"https://www.tratencongty.com/company/12d9cf6f8-cong-ty-tnhh-san-xuat-va-thuong-mai-hoai-phuong/"
                //,"https://www.tratencongty.com/company/12d9cf6f1-cong-ty-tnhh-dau-tu-thuong-mai-tan-dat-phat/"
                //,"https://www.tratencongty.com/company/ec160599-cong-ty-tnhh-vat-lieu-xay-dung-dinh-chung/"
                //,"https://www.tratencongty.com/company/4c14d188-cong-ty-tnhh-dau-tu-san-xuat-thuong-mai-dich-vu-hieu-anh/"
                //,"https://www.tratencongty.com/company/6ed96102-cong-ty-tnhh-tu-van-va-xay-lap-long-an/"
                //,"https://www.tratencongty.com/company/4c14d168-cong-ty-tnhh-thuong-mai-dich-vu-tuoi-le/"
                //,"https://www.tratencongty.com/company/74d12b59-cong-ty-tnhh-thuong-mai-va-dich-vu-minh-cuong/"
                //,"https://www.tratencongty.com/company/115dc4d11-cong-ty-tnhh-asia-star/"
                //,"https://www.tratencongty.com/company/133982fe7-cong-ty-tnhh-dau-tu-xay-dung-va-bao-tri-cong-trinh-khanh-hoa/"
                //,"https://www.tratencongty.com/company/10ff26e26-cong-ty-tnhh-mtv-thuong-ma-i-xang-da-u-hoa-ng-vu-55555/"
                //,"https://www.tratencongty.com/company/1b0aaee48-cong-ty-tnhh-dau-tu-nong-nghiep-nam-tien/"
                //,"https://www.tratencongty.com/company/5cf96dad-cong-ty-tnhh-dich-vu-bao-ve-chuyen-nghiep-ha-linh/"
                //,"https://www.tratencongty.com/company/127a192b3-cong-ty-tnhh-dv-dl-tm-tan-kien/"
                //,"https://www.tratencongty.com/company/127a192a5-cong-ty-tnhh-tu-van-ky-thuat-xay-dung-minh-an-phat/"
                //,"https://www.tratencongty.com/company/3fb6e4b1-cong-ty-tnhh-hoajju-viet-nam/"
                //,"https://www.tratencongty.com/company/127a1928c-cong-ty-tnhh-eden-hung-thinh/"
                //,"https://www.tratencongty.com/company/127a192c5-cong-ty-tnhh-do-choi-ngoc-linh/"
                //,"https://www.tratencongty.com/company/453067e3-cong-ty-tnhh-do-viet-anh/"
                //,"https://www.tratencongty.com/company/45306811-cong-ty-tnhh-kinh-doanh-van-tai-tuan-tu/"
                //,"https://www.tratencongty.com/company/4c14cc4d-cong-ty-tnhh-tm-van-tai-va-kinh-doanh-vlxd-hieu-vuong/"
                //,"https://www.tratencongty.com/company/4c14cc6d-cong-ty-tnhh-san-xuat-thuong-mai-dich-vu-hoang-sa/"
                //,"https://www.tratencongty.com/company/175171689-cong-ty-tnhh-du-lich-doan-17/"
                //,"https://www.tratencongty.com/company/68e76d63-cong-ty-tnhh-san-xuat-va-thuong-mai-dang-kien/"
                //,"https://www.tratencongty.com/company/e03685db-cong-ty-tnhh-mot-thanh-vien-ld-truong-phat/"
                //,"https://www.tratencongty.com/company/4c14c485-cong-ty-co-phan-be-tong-thahome/"
                //,"https://www.tratencongty.com/company/4c14c4fd-cong-ty-tnhh-thuong-mai-dv-thanh-tam/"
                //,"https://www.tratencongty.com/company/4c14c66f-cong-ty-tnhh-dau-tu-thuong-mai-dich-vu-song-lam/"
                //,"https://www.tratencongty.com/company/4c14abcb-cong-ty-tnhh-xay-dung-van-tai-dai-tan-hung/"
                //,"https://www.tratencongty.com/company/e61e9751-cong-ty-tnhh-xay-dung-tm-va-dv-toan-cau/"
                //,"https://www.tratencongty.com/company/127a1915d-cong-ty-tnhh-long-phung-restaurant/"
                //,"https://www.tratencongty.com/company/e61e95e9-cong-ty-tnhh-hoang-duc-nghe-an/"
                //,"https://www.tratencongty.com/company/4c141ff3-cong-ty-hop-danh-quan-tai-vien-lam-phat-tai/"


                //                };
                //                #endregion
                //
                foreach (var item in lstLink)
                {
                    try
                    {
                        var model = new CongTyDTO
                        {
                            LinkWeb = item
                        };
                        await _page.GoToAsync(item, WaitUntilNavigation.Networkidle2);
                        html = await _page.GetContentAsync();
                        var doc = new HtmlDocument();
                        doc.LoadHtml(html);
                        var nodeTenCongTy = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/h4/a/span");
                        var nodeMaSoThue = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/a");
                        var nodeDienThoai = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/img");
                        model.TenCongTy = nodeTenCongTy?.InnerText.Replace("&amp;", "&").Trim();
                        model.MaSoThue = nodeMaSoThue?.InnerText.Trim();
                        model.DienThoaiTruSo = nodeDienThoai?.Attributes["src"]?.Value.Trim();

                        var nodeText = doc.DocumentNode.SelectSingleNode("//*[text()[contains(., 'Đại diện pháp luật')]]");
                        if (nodeText == null)
                        {
                            nodeText = doc.DocumentNode.SelectSingleNode("//*[text()[contains(., 'Tên giao dịch')]]");
                            if (nodeText == null)
                            {
                                nodeText = doc.DocumentNode.SelectSingleNode("//*[text()[contains(., 'Loại hình hoạt động')]]");
                            }
                        }
                        if (nodeText != null)
                        {
                            var innerText = nodeText.InnerText;
                            string[] separatingStrings = { "   " };
                            var strSplit = innerText.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var itemSplit in strSplit)
                            {
                                if (itemSplit.Contains("Tên giao dịch:"))
                                {
                                    model.TenGiaoDich = itemSplit.Replace("Tên giao dịch:", string.Empty).Replace("&amp;", "&").Trim();
                                }
                                else if (itemSplit.Contains("Loại hình hoạt động:"))
                                {
                                    model.LoaiHinhHoatDong = itemSplit.Replace("Loại hình hoạt động:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Mã số thuế:") && string.IsNullOrWhiteSpace(model.MaSoThue))
                                {
                                    model.MaSoThue = itemSplit.Replace("Mã số thuế:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Địa chỉ:"))
                                {
                                    model.DiaChi = itemSplit.Replace("Địa chỉ:", string.Empty).Trim();
                                    if (!string.IsNullOrWhiteSpace(model.DiaChi))
                                    {
                                        var strSplitDiaChi = model.DiaChi.Split(',');
                                        var length = strSplitDiaChi.Length;
                                        if (length >= 4)
                                        {
                                            model.TinhThanh = strSplitDiaChi.Last().Trim();
                                            model.QuanHuyen = strSplitDiaChi[length - 2].Trim();
                                            model.PhuongXa = strSplitDiaChi[length - 3].Trim();
                                        }
                                        else if (length >= 3)
                                        {
                                            model.TinhThanh = strSplitDiaChi.Last().Trim();
                                            model.QuanHuyen = strSplitDiaChi[length - 2].Trim();
                                        }
                                    }
                                }
                                else if (itemSplit.Contains("Đại diện pháp luật:"))
                                {
                                    model.DaiDienPhapLuat = itemSplit.Replace("Đại diện pháp luật:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Ngày cấp giấy phép:"))
                                {
                                    model.NgayCapGiayPhep = itemSplit.Replace("Ngày cấp giấy phép:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Ngày hoạt động:"))
                                {
                                    model.NgayHoatDong = itemSplit.Replace("Ngày hoạt động:", string.Empty).Trim();
                                    if (model.NgayHoatDong.Contains("("))
                                    {
                                        var index = model.NgayHoatDong.IndexOf("(");
                                        model.NgayHoatDong = model.NgayHoatDong.Substring(0, index - 1);
                                    }
                                }
                                else if (itemSplit.Contains("Trạng thái:"))
                                {
                                    model.TrangThai = itemSplit.Replace("Trạng thái:", string.Empty).Trim();
                                }
                            }
                        }
                        //Insert Sqlite
                        if (!string.IsNullOrWhiteSpace(model.MaSoThue) 
                            && !SqliteMng.CheckExist(model.MaSoThue))
                        {
                            SqliteMng.InsertData(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"CrawlRealtimeJob.Execute|EXCEPTION(Detail)| {ex.Message}");
                    }
                }
                await _page.CloseAsync();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"CrawlRealtimeJob.Execute|EXCEPTION(Main)| {ex.Message}");
            }
            finally
            {
                browser.Dispose();
            }
        }  
        
        public static void Handle(string url, int mode)
        {
            
            using (var driver = StaticVal.GetChrome(mode))
            {
                var lstLink = new List<string>();
                try
                {
                    driver.Navigate().GoToUrl(url);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(driver.PageSource);
                    var index = 1;
                    var emptyData = 0;
                    do
                    {
                        var node = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/div[{index}]/a");
                        if (node != null)
                        {
                            string hrefValue = node.Attributes["href"].Value.Trim();
                            if (!string.IsNullOrWhiteSpace(hrefValue))
                            {
                                lstLink.Add(hrefValue);
                            }
                            emptyData = 1;
                        }
                        else
                        {
                            emptyData++;
                        }

                        index++;
                        if (emptyData >= 3)
                        {
                            index = -1;
                        }
                    }
                    while (index > 0);
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex, $"CrawlRealtimeJob.Execute|EXCEPTION| {ex.Message}");
                }

                foreach (var item in lstLink)
                {
                    try
                    {
                        var model = new CongTyDTO
                        {
                            LinkWeb = item
                        };
                        driver.Navigate().GoToUrl(item);
                        var doc = new HtmlDocument();
                        doc.LoadHtml(driver.PageSource);
                        var nodeTenCongTy = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/h4/a/span");
                        var nodeMaSoThue = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/a");
                        var nodeDienThoai = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/img");
                        model.TenCongTy = nodeTenCongTy?.InnerText.Replace("&amp;", "&").Trim();
                        model.MaSoThue = nodeMaSoThue?.InnerText.Trim();
                        model.DienThoaiTruSo = nodeDienThoai?.Attributes["src"]?.Value.Trim();

                        var nodeText = doc.DocumentNode.SelectSingleNode("//*[text()[contains(., 'Tên giao dịch')]]");
                        if (nodeText == null)
                        {
                            nodeText = doc.DocumentNode.SelectSingleNode("//*[text()[contains(., 'Loại hình hoạt động')]]");
                        }
                        if (nodeText != null)
                        {
                            var innerText = nodeText.InnerText;
                            string[] separatingStrings = { "   " };
                            var strSplit = innerText.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var itemSplit in strSplit)
                            {
                                if (itemSplit.Contains("Tên giao dịch:"))
                                {
                                    model.TenGiaoDich = itemSplit.Replace("Tên giao dịch:", string.Empty).Replace("&amp;", "&").Trim();
                                }
                                else if (itemSplit.Contains("Loại hình hoạt động:"))
                                {
                                    model.LoaiHinhHoatDong = itemSplit.Replace("Loại hình hoạt động:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Mã số thuế:") && string.IsNullOrWhiteSpace(model.MaSoThue))
                                {
                                    model.MaSoThue = itemSplit.Replace("Mã số thuế:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Địa chỉ:"))
                                {
                                    model.DiaChi = itemSplit.Replace("Địa chỉ:", string.Empty).Trim();
                                    if (!string.IsNullOrWhiteSpace(model.DiaChi))
                                    {
                                        var strSplitDiaChi = model.DiaChi.Split(',');
                                        var length = strSplitDiaChi.Length;
                                        if (length >= 4)
                                        {
                                            model.TinhThanh = strSplitDiaChi.Last().Trim();
                                            model.QuanHuyen = strSplitDiaChi[length - 2].Trim();
                                            model.PhuongXa = strSplitDiaChi[length - 3].Trim();
                                        }
                                        else if (length >= 3)
                                        {
                                            model.TinhThanh = strSplitDiaChi.Last().Trim();
                                            model.QuanHuyen = strSplitDiaChi[length - 2].Trim();
                                        }
                                    }
                                }
                                else if (itemSplit.Contains("Đại diện pháp luật:"))
                                {
                                    model.DaiDienPhapLuat = itemSplit.Replace("Đại diện pháp luật:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Ngày cấp giấy phép:"))
                                {
                                    model.NgayCapGiayPhep = itemSplit.Replace("Ngày cấp giấy phép:", string.Empty).Trim();
                                }
                                else if (itemSplit.Contains("Ngày hoạt động:"))
                                {
                                    model.NgayHoatDong = itemSplit.Replace("Ngày hoạt động:", string.Empty).Trim();
                                    if (model.NgayHoatDong.Contains("("))
                                    {
                                        var index = model.NgayHoatDong.IndexOf("(");
                                        model.NgayHoatDong = model.NgayHoatDong.Substring(0, index - 1);
                                    }
                                }
                                else if (itemSplit.Contains("Trạng thái:"))
                                {
                                    model.TrangThai = itemSplit.Replace("Trạng thái:", string.Empty).Trim();
                                }
                            }
                        }
                        //Insert Sqlite
                        if (!SqliteMng.CheckExist(model.MaSoThue))
                        {
                            SqliteMng.InsertData(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"CrawlRealtimeJob.Execute|EXCEPTION(Detail)| {ex.Message}");
                    }
                }
            }
        }
    }
}
