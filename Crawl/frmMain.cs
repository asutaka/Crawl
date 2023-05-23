using Crawl.Jobs;
using Crawl.ScheduleJob;
using Quartz;
using System;

namespace Crawl
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJob>(), "0/5 * * * * ?", nameof(CrawlRealtimeJob)).Start();
            //StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<CrawlJobPrev>(), "0 0/5 * * * ?", nameof(CrawlJobPrev)));
        }
    }
}