﻿using Crawl.Jobs;
using Crawl.Model;
using Crawl.ScheduleJob;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Crawl
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker _bkgr = new BackgroundWorker();
        private List<CongTyDTO> _lstData = new List<CongTyDTO>();
        private int _totalRow = 0;
        private ScheduleMember _RealTimeJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJobFake>(), "0 * * * * ?", nameof(CrawlRealtimeJobFake));
        private ScheduleMember _PrevJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlPrevJob>(), "30 * * * * ?", nameof(CrawlPrevJob));
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ReloadData();
            ScheduleMng.Instance().AddSchedule(_RealTimeJob);
            ScheduleMng.Instance().AddSchedule(_PrevJob);
            ScheduleMng.Instance().StartAllJob();
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

            btnReload.Enabled = true;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            btnReload.Enabled = false;
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
                foreach (var process in Process.GetProcessesByName("chromium"))
                {
                    process.Kill();
                }
                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.frmMain_FormClosed|EXCEPTION| {ex.Message}");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            try
            {
                foreach (var item in _lst)
                {
                    SqliteMng.UpdateData(item.Item1, item.Item2);
                }
                _lst.Clear();
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.btnExport_Click|EXCEPTION| {ex.Message}");
            }
            var path = Directory.GetCurrentDirectory();
            grid.ExportToXlsx($"{path}/Company.xlsx");
            btnExport.Enabled = true;
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            btnCrawl.Enabled = false;
            if(_RealTimeJob.IsPaused())
            {
                ScheduleMng.Instance().StartAllJob();
                btnCrawl.Text = "Stop Crawl";
            }
            else
            {
                ScheduleMng.Instance().StopAllJob();
                btnCrawl.Text = "Start Crawl";
            }
            btnCrawl.Enabled = true;
        }

        private List<(string, string)> _lst = new List<(string, string)>();
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            var id = gridView1.GetRowCellValue(e.RowHandle, "ID")?.ToString();
            var des = gridView1.GetRowCellValue(e.RowHandle, "MoTa")?.ToString();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = _lst.FirstOrDefault(x => x.Item1.Equals(id));
                if (entity.Item1 != null)
                {
                    _lst.Remove(entity);
                } 
                
                if(!string.IsNullOrWhiteSpace(des))
                {
                    _lst.Add((id, des));
                }    
            }    
        }
    }
}