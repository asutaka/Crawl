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
    public class CrawlJobPrev : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (var driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl("https://www.tratencongty.com/?page=1");
                var doc = new HtmlDocument();
                doc.LoadHtml(driver.PageSource);
                var node = doc.DocumentNode.SelectSingleNode("//head/title");
            }
            //using (var client = new WebClient())
            //{
            //    string htmlCode = client.DownloadString($"https://www.tratencongty.com/?page=1");
            //    var tmp = 1;
            //}
        }
    }
}
