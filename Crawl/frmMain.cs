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
            StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<CheckStatusJob>(), StaticVal.Scron_CheckStatus, nameof(CheckStatusJob)));
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            
            //StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<CrawlJobPrev>(), "0 0/5 * * * ?", nameof(CrawlJobPrev)));
        }
    }
}