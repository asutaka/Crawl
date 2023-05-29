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

        private void frmMain_Load(object sender, EventArgs e)
        {
            ReloadData();
            new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJob>(), "0 * * * * ?", nameof(CrawlRealtimeJob)).Start();
            new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlJobPrev>(), "30 * * * * ?", nameof(CrawlJobPrev)).Start();
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
            _bkgr.DoWork -= bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted -= bkgrConfig_RunWorkerCompleted;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            _bkgr.DoWork += bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }
    }
}