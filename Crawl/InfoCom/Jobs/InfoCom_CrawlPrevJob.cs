using Crawl.InfoCom.ChildModel;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Crawl.InfoCom.Jobs
{
    [DisallowConcurrentExecution]
    public class InfoCom_CrawlPrevJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var lTinhThanhComplete = "complete.json".LoadJsonFile<List<TinhThanhDTO>>();
            if(lTinhThanhComplete is null)
            {
                lTinhThanhComplete = new List<TinhThanhDTO>();
            }

            var entityTinhThanhCrawl = lTinhThanhComplete.FirstOrDefault(x => x.Page > 0);
            if(entityTinhThanhCrawl is null)
            {
                var lCrawl = new TinhThanhModel().lData.Where(x => !lTinhThanhComplete.Any(y => y.TenTinhThanh == x.TenTinhThanh));
                if (!lCrawl.Any())//Đã crawl hết
                {
                    //set tất cả các page của các tỉnh = 5;
                    foreach (var item in lTinhThanhComplete)
                    {
                        item.Page = 5;
                    }
                    lTinhThanhComplete.UpdateJsonFile("complete.json");
                    return;
                }

                var entityTinhThanh = lCrawl.First();
                lTinhThanhComplete.Add(new TinhThanhDTO
                {
                    TenTinhThanh = entityTinhThanh.TenTinhThanh,
                    MaMap = entityTinhThanh.MaMap,
                    Page = entityTinhThanh.Page
                });
                lTinhThanhComplete.UpdateJsonFile("complete.json");

                return;
            }

            lTinhThanhComplete.Remove(entityTinhThanhCrawl);
            lTinhThanhComplete.Add(new TinhThanhDTO { 
                TenTinhThanh = entityTinhThanhCrawl.TenTinhThanh,
                MaMap = entityTinhThanhCrawl.MaMap,
                Page = entityTinhThanhCrawl.Page - 1
            });
            lTinhThanhComplete.UpdateJsonFile("complete.json");
            InfoCom_CrawlRealTimeJob.Handle($"https://infocom.vn/{entityTinhThanhCrawl.MaMap}?page={entityTinhThanhCrawl.Page}");
        }
    }
}
