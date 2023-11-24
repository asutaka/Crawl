using Crawl.Model;
using Crawl.TraTenCongTy.ChildModel;
using Crawl.TraTenCongTy.Jobs;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Utils;
using Utils.ScheduleJob;

namespace Crawl.TraTenCongTy
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker _bkgr = new BackgroundWorker();
        private List<CongTyDTO> _lstData = new List<CongTyDTO>();
        private int _totalRow = 0;
        private bool _changeSelect = false;
        private bool _modeSelect = false;
        private ScheduleMember _RealTimeJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlRealtimeJobFake>(), "0/5 * * * * ?", nameof(CrawlRealtimeJobFake));
        private ScheduleMember _PrevJob = new ScheduleMember(ScheduleMng.Instance().GetScheduler(), JobBuilder.Create<CrawlPrevJob>(), "30 * * * * ?", nameof(CrawlPrevJob));
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadJsonFile();
            LoadComboBox();
            ReloadData();
            ScheduleMng.Instance().AddSchedule(_RealTimeJob);
            ScheduleMng.Instance().AddSchedule(_PrevJob);
            ScheduleMng.Instance().StartAllJob();
        }

        private string BuildClause()
        {
            var val = cmbCheck.EditValue.ToString();
            if (string.IsNullOrWhiteSpace(val))
                return string.Empty;
            var arrSplit = val.Split(',');
            var lClause = new List<string>();
            foreach (var item in arrSplit)
            {
                var entityTinhThanh = StaticVal._lstCmb.FirstOrDefault(x => x.MaMap.Equals(item.Trim()));
                if (entityTinhThanh == null)
                    continue;

                lClause.Add($"tinhthanh LIKE '%{ entityTinhThanh.TenTinhThanh }%'");
            }

            if (!lClause.Any())
                return string.Empty;

            return $"AND ({string.Join(" OR ", lClause.ToArray())})";
        }

        private void bkgrConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            var strClause = BuildClause();
            _totalRow = SqliteMngTraTenCongTy.TotalRow(strClause); 
            _lstData = SqliteMngTraTenCongTy.GetData(strClause);
            foreach (var item in _lstData)
            {
                item.DienThoaiTruSoImg = item.DienThoaiTruSo.GetImage();
            }
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
            if(_changeSelect)
            {
                UpdateJsonFile();
            }
            ReloadData();
        }

        private void ReloadData()
        {
            _bkgr.DoWork += bkgrConfig_DoWork;
            _bkgr.RunWorkerCompleted += bkgrConfig_RunWorkerCompleted;
            _bkgr.RunWorkerAsync();
        }

        private void LoadComboBox()
        {
            cmbCheck.Properties.ValueMember = "MaMap"; // IDNo = bigint  
            cmbCheck.Properties.DisplayMember = "TenTinhThanh"; // Name = nvarchar(256)  
            cmbCheck.Properties.DataSource = StaticVal._lstCmb;
            if(StaticVal._config != null)
            {
                cmbCheck.SetEditValue(StaticVal._config.TinhThanhTraTenCongTy);
            }
            _modeSelect = true;
        }

        private void UpdateJsonFile()
        {
            try
            {
                StaticVal._config = new ConfigModel
                {
                    TinhThanhTraTenCongTy = cmbCheck.EditValue?.ToString()
                };
                File.WriteAllText("config.json", JsonConvert.SerializeObject(StaticVal._config));
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.UpdateJsonFile|EXCEPTION| {ex.Message}");
            }
        }

        private void LoadJsonFile()
        {
            try
            {
                using (StreamReader r = new StreamReader("config.json"))
                {
                    string json = r.ReadToEnd();
                    StaticVal._config = JsonConvert.DeserializeObject<ConfigModel>(json);
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"frmMain.LoadJsonFile|EXCEPTION| {ex.Message}");
            }
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
                    SqliteMngTraTenCongTy.UpdateData(item.Item2, item.Item1);
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

        private void cmbCheck_EditValueChanged(object sender, EventArgs e)
        {
            if (!_modeSelect)
                return;
            _changeSelect = true;
        }
    }
}