using Crawl.Jobs;
using Crawl.Model;
using Crawl.ScheduleJob;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Crawl
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker _bkgr = new BackgroundWorker();
        private List<CongTyDTO> _lstData = new List<CongTyDTO>();
        private int _totalRow = 0;
        private ScheduleMember _RealTimeJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJob>(), "0 * * * * ?", nameof(CrawlRealtimeJob));
        private ScheduleMember _PrevJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlPrevJob>(), "30 * * * * ?", nameof(CrawlPrevJob));
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ReloadData();
            //new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJob>(), "0/10 * * * * ?", nameof(CrawlRealtimeJob)).Start();
            _RealTimeJob.Start();
            _PrevJob.Start();
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            _totalRow = SqliteMng.TotalRow();
            _lstData = SqliteMng.GetData();
        }

        private void bkgrConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grid.BeginUpdate();
            grid.DataSource = _lstData;
            grid.EndUpdate();
            lblTotalRow.Text = _totalRow.ToString("#,##0");

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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = gridView1.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                var cellValue = gridView1.GetRowCellValue(info.RowHandle, "LinkWeb").ToString();
                if(!string.IsNullOrWhiteSpace(cellValue))
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo($"{cellValue}");
                    Process.Start(sInfo);
                }    
            }
        }

        private void frmMain_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            try
            {
                ScheduleMng.Instance().StopAllJob();
                foreach (var process in Process.GetProcessesByName("chromedriver"))
                {
                    process.Kill();
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.frmMain_FormClosed|EXCEPTION| {ex.Message}");
            }
        }
    }
}