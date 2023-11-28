using Crawl.InfoCom.ChildModel;
using Crawl.Model;
using HtmlAgilityPack;
using PuppeteerSharp;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Crawl.InfoCom.Jobs
{
    [DisallowConcurrentExecution]
    public class InfoCom_CrawlRealTimeJob : IJob
    {
        private bool IsFirst = true;
        private static List<string> _lClause = new List<string>();
        private readonly string _url = "https://infocom.vn/?page=";
        public void Execute(IJobExecutionContext context)
        {
            _lClause = BuildClause();
            Handle($"{_url}1").GetAwaiter().GetResult();
            if (IsFirst)
            {
                IsFirst = false;
                Handle($"{_url}2").GetAwaiter().GetResult();
                Handle($"{_url}3").GetAwaiter().GetResult();
                Handle($"{_url}4").GetAwaiter().GetResult();
                Handle($"{_url}5").GetAwaiter().GetResult();
                Handle($"{_url}6").GetAwaiter().GetResult();
                Handle($"{_url}7").GetAwaiter().GetResult();
                Handle($"{_url}8").GetAwaiter().GetResult();
                Handle($"{_url}9").GetAwaiter().GetResult();
                Handle($"{_url}10").GetAwaiter().GetResult();
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
                var indexMain = 2;
                var emptyData = 0;
                do
                {
                    var node = htmlDoc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[{indexMain}]/div[1]/div[1]/h3[1]/a[1]");
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
                        var nodeTenCongTy = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/h1[1]");
                        var nodeMaSoThue = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[3]/td[2]");
                        var nodeDiaChi = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div/div[1]/table/tbody[1]/tr[4]/td[2]");
                        var nodeDienThoai = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[5]/td[2]/a[1]");
                        var nodeNguoiDaiDien = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[7]/td[2]/strong[1]");
                        if(nodeNguoiDaiDien == null)
                        {
                            nodeNguoiDaiDien = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[6]/td[2]/strong[1]");
                        }

                        var ngayThanhLap = string.Empty;
                        for (int i = 8; i < 12; i++)
                        {
                            var nodeNgayThanhLap = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[{i}]/td[2]");
                            ngayThanhLap = nodeNgayThanhLap?.InnerText.Trim().CleanDate().FormatDate("yyyy-MM-dd");
                            if (!string.IsNullOrWhiteSpace(ngayThanhLap))
                                break;
                        }

                        var strNganhNghe = string.Empty;
                        for (int i = 0; i < 3; i++)
                        {
                            var nodeNganhNghe = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[{i + 2}]/table[1]/tbody[1]");
                            if(nodeNganhNghe != null)
                            {
                                var pathNganhNghe = $"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[{i + 2}]/table[1]/tbody[1]/";
                                for (int j = 0; j < 4; j++)
                                {
                                    var NodeCheckNganhNghe = doc.DocumentNode.SelectSingleNode($"{pathNganhNghe}/tr[{j + 2}]/td[1]");
                                    if (NodeCheckNganhNghe != null && !string.IsNullOrWhiteSpace(NodeCheckNganhNghe.InnerText))
                                    {
                                        var NodeDataNganhNghe1 = doc.DocumentNode.SelectSingleNode($"{pathNganhNghe}/tr[{j + 2}]/td[2]");
                                        strNganhNghe += $"{NodeDataNganhNghe1?.InnerText},";
                                    }
                                }
                                break;
                            }    
                        }

                        if(string.IsNullOrWhiteSpace(strNganhNghe))
                        {
                            var nodeBusinessMain = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[2]");
                            if (nodeBusinessMain != null)
                                strNganhNghe = nodeBusinessMain.InnerText.Replace("Ngành nghề hoạt động chính:", "").Trim();
                        }    

                        model.TenCongTy = nodeTenCongTy?.InnerText.Trim();
                        model.MaSoThue = nodeMaSoThue?.InnerText.Trim();
                        model.DiaChi = nodeDiaChi?.InnerText.Trim();
                        model.DienThoaiTruSo = nodeDienThoai?.InnerText.Trim();
                        if (string.IsNullOrWhiteSpace(model.DienThoaiTruSo))
                            continue;
                        model.DaiDienPhapLuat = nodeNguoiDaiDien?.InnerText.Trim();
                        model.NgayHoatDong = ngayThanhLap;
                        model.LoaiHinhHoatDong = strNganhNghe.Trim().MaxLengthText(100);

                        if (!string.IsNullOrWhiteSpace(model.DiaChi))
                        {
                            var strSplitDiaChi = model.DiaChi.Split(',');
                            var length = strSplitDiaChi.Length;
                            if (length >= 5)
                            {
                                model.TinhThanh = strSplitDiaChi[length - 2].Trim();
                                model.QuanHuyen = strSplitDiaChi[length - 3].Trim();
                                model.PhuongXa = strSplitDiaChi[length - 4].Trim();
                            }
                            else if (length >= 4)
                            {
                                model.TinhThanh = strSplitDiaChi[length - 2].Trim();
                                model.QuanHuyen = strSplitDiaChi[length - 3].Trim();
                            }
                        }
                        //Insert Sqlite
                        if (!string.IsNullOrWhiteSpace(model.MaSoThue) && !SqliteMngInfoCom.CheckExist(model.MaSoThue) && !string.IsNullOrWhiteSpace(model.TinhThanh))
                        {
                            if (!_lClause.Any())
                            {
                                SqliteMngInfoCom.InsertData(model);
                            }
                            else if (_lClause.Any(x => model.TinhThanh.Contains(x)))
                            {
                                SqliteMngInfoCom.InsertData(model);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.PublishException(ex, $"InfoCom_CrawlRealTimeJob.Execute|EXCEPTION(Detail)| {ex.Message}");
                    }
                }
                await _page.CloseAsync();
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"InfoCom_CrawlRealTimeJob.Execute|EXCEPTION(Main)| {ex.Message}");
            }
            finally
            {
                browser.Dispose();
            }
        }

        private List<string> BuildClause()
        {
            var val = StaticVal._config.TinhThanhInfoCom;
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
