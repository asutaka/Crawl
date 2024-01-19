using Crawl.Model;
using Quartz;
using System.Linq;
using System.Threading;
using Utils;

namespace Crawl.InfoCom.Jobs
{
    [DisallowConcurrentExecution]
    public class InfoCom_CrawlPrevJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var val = StaticVal._config.TinhThanhInfoCom;
            if (string.IsNullOrWhiteSpace(val))
                return;
            var query = "SELECT * FROM PageTbl";
            var entityPage = SqliteMng.Get<PageDTO>(query)?.FirstOrDefault();
            var pageIndex = entityPage.PageNum;

            var arrSplit = val.Split(',');
            foreach (var item in arrSplit)
            {
                InfoCom_CrawlRealTimeJob.Handle($"https://infocom.vn/{item.Trim()}?page={pageIndex}");
                Thread.Sleep(5000);
            }
            SqliteMng.Update($"update PageTbl set pagenum = {entityPage.PageNum + 1}");
        }
    }
}
