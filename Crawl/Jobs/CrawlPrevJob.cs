using Quartz;

namespace Crawl.Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlPrevJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var entityPage = SqliteMng.GetPage();
            CrawlRealtimeJob.Handle($"https://www.tratencongty.com/?page={entityPage.PageNum}");
            entityPage.PageNum = entityPage.PageNum + 1;
            SqliteMng.UpdatePage(entityPage);
        }
    }
}
