using Crawl.InfoCom.ChildModel;
using Crawl.Model;
using HtmlAgilityPack;
using PuppeteerSharp;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utils;

namespace Crawl.InfoCom.Jobs
{
    [DisallowConcurrentExecution]
    public class InfoCom_CrawlRealTimeJob : IJob
    {
        private static List<string> _lClause = new List<string>();
        private readonly string _url = "https://infocom.vn";

        public void Execute(IJobExecutionContext context)
        {
            _lClause = BuildClause();
            Handle($"{_url}");
        }

        private static List<string> GetLinkCom(string url)
        {
            try
            {
                var lstLink = new List<string>();
                var html = url.GetHtml();
                if (string.IsNullOrWhiteSpace(html))
                    return null;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var indexMain = 2;
                var emptyData = 0;
                do
                {
                    var node = htmlDoc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[{indexMain}]/div[1]/div[1]/h3[1]/a[1]");
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
                return lstLink;
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex);
            }
            return null;
        }

        public static void Handle(string url)
        {
            try
            {
                var lLink = GetLinkCom(url);
                if (lLink == null || !lLink.Any())
                    return;

                foreach (var item in lLink)
                {
                    try
                    {
                        var model = new CongTyDTO
                        {
                            LinkWeb = item
                        };

                        var doc = new HtmlDocument();
                        var html = item.GetHtml();
                        if (string.IsNullOrWhiteSpace(html))
                            continue;
                        doc.LoadHtml(html);
                        var basePath = "/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/";
                        var nodeTenCongTy = doc.DocumentNode.SelectSingleNode($"{basePath}h1[1]");
                        var nodeMaSoThue = doc.DocumentNode.SelectSingleNode($"{basePath}table[1]/tbody[1]/tr[3]/td[2]");
                        var nodeDiaChi = doc.DocumentNode.SelectSingleNode($"{basePath}table/tbody[1]/tr[4]/td[2]");
                        var nodeDienThoai = doc.DocumentNode.SelectSingleNode($"{basePath}table[1]/tbody[1]/tr[5]/td[2]/a[1]");
                        var nodeNguoiDaiDien = doc.DocumentNode.SelectSingleNode($"{basePath}table[1]/tbody[1]/tr[7]/td[2]/strong[1]");
                        if (nodeNguoiDaiDien == null)
                        {
                            nodeNguoiDaiDien = doc.DocumentNode.SelectSingleNode($"{basePath}table[1]/tbody[1]/tr[6]/td[2]/strong[1]");
                        }

                        var ngayThanhLap = string.Empty;
                        for (int i = 8; i < 12; i++)
                        {
                            var nodeNgayThanhLap = doc.DocumentNode.SelectSingleNode($"{basePath}table[1]/tbody[1]/tr[{i}]/td[2]");
                            ngayThanhLap = nodeNgayThanhLap?.InnerText.Trim().CleanDate().FormatDate("yyyy-MM-dd");
                            if (!string.IsNullOrWhiteSpace(ngayThanhLap))
                                break;
                        }

                        var strNganhNghe = string.Empty;
                        var nodeBusinessMain = doc.DocumentNode.SelectSingleNode($"{basePath}/div[1]");
                        if (nodeBusinessMain != null && nodeBusinessMain.InnerText.Contains("Ngành nghề hoạt động chính:"))
                        {
                            strNganhNghe = nodeBusinessMain.InnerText.Replace("Ngành nghề hoạt động chính:", "").Trim();
                        }

                        if (string.IsNullOrWhiteSpace(strNganhNghe))
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                var pathNganhNghe = $"{basePath}table[2]/tbody[1]";
                                var nodeNganhNghe = doc.DocumentNode.SelectSingleNode(pathNganhNghe);
                                if (nodeNganhNghe != null)
                                {
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
                        }

                        model.TenCongTy = nodeTenCongTy?.InnerText?.Trim() ?? string.Empty;
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
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"InfoCom_CrawlRealTimeJob.Execute|EXCEPTION(Main)| {ex.Message}");
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
