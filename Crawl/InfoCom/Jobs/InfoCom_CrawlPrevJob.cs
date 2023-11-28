using Crawl.Model;
using Quartz;
using System.Linq;
using Utils;

namespace Crawl.InfoCom.Jobs
{
    [DisallowConcurrentExecution]
    public class InfoCom_CrawlPrevJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var query = "SELECT * FROM PageTbl";
            var entityPage = SqliteMng.Get<PageDTO>(query)?.FirstOrDefault();
            var pageIndex = entityPage.PageNum;
            InfoCom_CrawlRealTimeJob.Handle($"https://infocom.vn/thanh-pho-ha-noi?page={pageIndex}").GetAwaiter().GetResult();
            SqliteMng.Update($"update PageTbl set pagenum = {entityPage.PageNum + 1}");
        }
    }
}
