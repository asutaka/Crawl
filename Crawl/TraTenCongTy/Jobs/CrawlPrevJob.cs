using Crawl.Model;
using Quartz;
using System.Linq;
using Utils;

namespace Crawl.TraTenCongTy.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlPrevJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var query = "SELECT * FROM PageTbl";
            var entityPage = SqliteMng.Get<PageDTO>(query)?.FirstOrDefault();
            CrawlRealtimeJobFake.Handle($"https://www.tratencongty.com/?page={entityPage.PageNum}").GetAwaiter().GetResult();
            SqliteMng.Update($"update PageTbl set pagenum = {entityPage.PageNum + 1}");
        }
    }
}
