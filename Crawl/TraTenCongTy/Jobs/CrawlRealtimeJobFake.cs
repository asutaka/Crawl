using Crawl.Model;
using Crawl.TraTenCongTy.ChildModel;
using HtmlAgilityPack;
using PuppeteerSharp;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Crawl.TraTenCongTy.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlRealtimeJobFake : IJob
    {
        private bool IsFirst = true;
        private static List<string> _lClause = new List<string>();
        public void Execute(IJobExecutionContext context)
        {
            _lClause = BuildClause();
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
                        if (!string.IsNullOrWhiteSpace(model.MaSoThue) && !SqliteMngTraTenCongTy.CheckExist(model.MaSoThue) && !string.IsNullOrWhiteSpace(model.TinhThanh))
                        {
                            if(!_lClause.Any())
                            {
                                SqliteMngTraTenCongTy.InsertData(model);
                            }
                            else if (_lClause.Any(x => model.TinhThanh.Contains(x)))
                            {
                                SqliteMngTraTenCongTy.InsertData(model);
                            }
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

        private List<string> BuildClause()
        {
            var val = StaticVal._config.TinhThanhTraTenCongTy;
            if (string.IsNullOrWhiteSpace(val))
                return new List<string>();
            var arrSplit = val.Split(',');
            var lClause = new List<string>();
            foreach (var item in arrSplit)
            {
                var entityTinhThanh = new TinhThanhModel().lData.FirstOrDefault(x => x.MaMap.Equals(item.Trim()));
                if (entityTinhThanh == null)
                    continue;

                lClause.Add(entityTinhThanh.TenTinhThanh);
            }
            return lClause;
        }
    }
}
