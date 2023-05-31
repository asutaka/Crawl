using Crawl.Model;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawl.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlRealtimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Handle("https://www.tratencongty.com/?page=1", 0);
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
