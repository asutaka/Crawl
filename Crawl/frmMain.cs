using Crawl.Jobs;
using Crawl.Model;
using Crawl.ScheduleJob;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Crawl
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker _bkgr = new BackgroundWorker();
        private List<CongTyDTO> _lstData = new List<CongTyDTO>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJob>(), "0/5 * * * * ?", nameof(CrawlRealtimeJob)).Start();
            //StaticVal.scheduleMng.AddSchedule(new ScheduleMember(StaticVal.scheduleMng.GetScheduler(), JobBuilder.Create<CrawlJobPrev>(), "0 0/5 * * * ?", nameof(CrawlJobPrev)));
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            _bkgr.DoWork += bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            _lstData = SqliteMng.GetData();
        }

        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grid.BeginUpdate();
            grid.DataSource = _lstData;
            grid.EndUpdate();
        }
    }
}