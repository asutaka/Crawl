using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawl.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlRealtimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (var driver = new ChromeDriver(options))
            {
                var lstLink = new List<string>();
                try
                {
                    driver.Navigate().GoToUrl("https://www.tratencongty.com/?page=1");
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
                catch(Exception ex)
                {
                    NLogLogger.PublishException(ex, $"CrawlRealtimeJob.Execute|EXCEPTION| {ex.Message}");
                }

                foreach (var item in lstLink)
                {
                    driver.Navigate().GoToUrl(item);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(driver.PageSource);
                    var nodeTenCongTy = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/h4/a/span");
                    var nodeLoaiHinhHoatDong = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[1]");
                    var nodeMaSoThue = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/a");
                    var nodeDiaChi = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[3]");
                    var nodeDaiDienPhapLuat = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[4]");
                    var nodeNgayCap = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[5]");
                    var nodeNgayHoatDong = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[6]");
                    var nodeDienThoai = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/img");
                    var nodeTrangThai = doc.DocumentNode.SelectSingleNode($"/html/body/div/div[1]/div[3]/text()[9]");
                }
                

                var tmp = 1;
            }
            ////using (var client = new WebClient())
            ////{
            ////    string htmlCode = client.DownloadString($"https://www.tratencongty.com/?page=1");
            ////    var tmp = 1;
            ////}
        }
    }
}
