
namespace Crawl
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.TenCongTy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LoaiHinhHoatDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaSoThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhuongXa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.QuanHuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TinhThanh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DaiDienPhapLuat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayCapGiayPhep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayHoatDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DienThoaiTruSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCrawl = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnReload = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.Location = new System.Drawing.Point(0, 83);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(983, 396);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.TenCongTy,
            this.LoaiHinhHoatDong,
            this.MaSoThue,
            this.DiaChi,
            this.PhuongXa,
            this.QuanHuyen,
            this.TinhThanh,
            this.DaiDienPhapLuat,
            this.NgayCapGiayPhep,
            this.NgayHoatDong,
            this.DienThoaiTruSo,
            this.TrangThai});
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.AllowHtmlDrawGroups = false;
            this.gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // TenCongTy
            // 
            this.TenCongTy.AppearanceHeader.Options.UseTextOptions = true;
            this.TenCongTy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenCongTy.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TenCongTy.Caption = "Công Ty";
            this.TenCongTy.FieldName = "TenCongTy";
            this.TenCongTy.Name = "TenCongTy";
            this.TenCongTy.Visible = true;
            this.TenCongTy.VisibleIndex = 0;
            // 
            // LoaiHinhHoatDong
            // 
            this.LoaiHinhHoatDong.AppearanceHeader.Options.UseTextOptions = true;
            this.LoaiHinhHoatDong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LoaiHinhHoatDong.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LoaiHinhHoatDong.Caption = "Loại Hình";
            this.LoaiHinhHoatDong.FieldName = "LoaiHinhHoatDong";
            this.LoaiHinhHoatDong.Name = "LoaiHinhHoatDong";
            this.LoaiHinhHoatDong.Visible = true;
            this.LoaiHinhHoatDong.VisibleIndex = 1;
            // 
            // MaSoThue
            // 
            this.MaSoThue.AppearanceCell.Options.UseTextOptions = true;
            this.MaSoThue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaSoThue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MaSoThue.AppearanceHeader.Options.UseTextOptions = true;
            this.MaSoThue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaSoThue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MaSoThue.Caption = "Mã Số Thuế";
            this.MaSoThue.FieldName = "MaSoThue";
            this.MaSoThue.MaxWidth = 90;
            this.MaSoThue.MinWidth = 90;
            this.MaSoThue.Name = "MaSoThue";
            this.MaSoThue.Visible = true;
            this.MaSoThue.VisibleIndex = 2;
            this.MaSoThue.Width = 90;
            // 
            // DiaChi
            // 
            this.DiaChi.AppearanceHeader.Options.UseTextOptions = true;
            this.DiaChi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DiaChi.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DiaChi.Caption = "Địa Chỉ";
            this.DiaChi.FieldName = "DiaChi";
            this.DiaChi.Name = "DiaChi";
            this.DiaChi.Visible = true;
            this.DiaChi.VisibleIndex = 3;
            // 
            // PhuongXa
            // 
            this.PhuongXa.AppearanceHeader.Options.UseTextOptions = true;
            this.PhuongXa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PhuongXa.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PhuongXa.Caption = "Phường/Xã";
            this.PhuongXa.FieldName = "PhuongXa";
            this.PhuongXa.Name = "PhuongXa";
            this.PhuongXa.Visible = true;
            this.PhuongXa.VisibleIndex = 4;
            // 
            // QuanHuyen
            // 
            this.QuanHuyen.AppearanceHeader.Options.UseTextOptions = true;
            this.QuanHuyen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.QuanHuyen.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.QuanHuyen.Caption = "Quận/Huyện";
            this.QuanHuyen.FieldName = "QuanHuyen";
            this.QuanHuyen.Name = "QuanHuyen";
            this.QuanHuyen.Visible = true;
            this.QuanHuyen.VisibleIndex = 5;
            // 
            // TinhThanh
            // 
            this.TinhThanh.AppearanceHeader.Options.UseTextOptions = true;
            this.TinhThanh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TinhThanh.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TinhThanh.Caption = "Tỉnh/Thành";
            this.TinhThanh.FieldName = "TinhThanh";
            this.TinhThanh.Name = "TinhThanh";
            this.TinhThanh.Visible = true;
            this.TinhThanh.VisibleIndex = 6;
            // 
            // DaiDienPhapLuat
            // 
            this.DaiDienPhapLuat.AppearanceHeader.Options.UseTextOptions = true;
            this.DaiDienPhapLuat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DaiDienPhapLuat.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DaiDienPhapLuat.Caption = "Đại Diện";
            this.DaiDienPhapLuat.FieldName = "DaiDienPhapLuat";
            this.DaiDienPhapLuat.Name = "DaiDienPhapLuat";
            this.DaiDienPhapLuat.Visible = true;
            this.DaiDienPhapLuat.VisibleIndex = 7;
            // 
            // NgayCapGiayPhep
            // 
            this.NgayCapGiayPhep.AppearanceCell.Options.UseTextOptions = true;
            this.NgayCapGiayPhep.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgayCapGiayPhep.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NgayCapGiayPhep.AppearanceHeader.Options.UseTextOptions = true;
            this.NgayCapGiayPhep.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgayCapGiayPhep.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NgayCapGiayPhep.Caption = "Cấp Phép";
            this.NgayCapGiayPhep.FieldName = "NgayCapGiayPhep";
            this.NgayCapGiayPhep.MaxWidth = 80;
            this.NgayCapGiayPhep.MinWidth = 80;
            this.NgayCapGiayPhep.Name = "NgayCapGiayPhep";
            this.NgayCapGiayPhep.Visible = true;
            this.NgayCapGiayPhep.VisibleIndex = 8;
            this.NgayCapGiayPhep.Width = 80;
            // 
            // NgayHoatDong
            // 
            this.NgayHoatDong.AppearanceCell.Options.UseTextOptions = true;
            this.NgayHoatDong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgayHoatDong.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NgayHoatDong.AppearanceHeader.Options.UseTextOptions = true;
            this.NgayHoatDong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgayHoatDong.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NgayHoatDong.Caption = "Hoạt Động";
            this.NgayHoatDong.FieldName = "NgayHoatDong";
            this.NgayHoatDong.MaxWidth = 80;
            this.NgayHoatDong.MinWidth = 80;
            this.NgayHoatDong.Name = "NgayHoatDong";
            this.NgayHoatDong.Visible = true;
            this.NgayHoatDong.VisibleIndex = 9;
            this.NgayHoatDong.Width = 80;
            // 
            // DienThoaiTruSo
            // 
            this.DienThoaiTruSo.AppearanceHeader.Options.UseTextOptions = true;
            this.DienThoaiTruSo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DienThoaiTruSo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DienThoaiTruSo.Caption = "Điện Thoại";
            this.DienThoaiTruSo.FieldName = "DienThoaiTruSo";
            this.DienThoaiTruSo.Name = "DienThoaiTruSo";
            this.DienThoaiTruSo.Visible = true;
            this.DienThoaiTruSo.VisibleIndex = 10;
            // 
            // TrangThai
            // 
            this.TrangThai.AppearanceCell.Options.UseTextOptions = true;
            this.TrangThai.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TrangThai.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TrangThai.AppearanceHeader.Options.UseTextOptions = true;
            this.TrangThai.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TrangThai.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TrangThai.Caption = "Trạng Thái";
            this.TrangThai.FieldName = "TrangThai";
            this.TrangThai.MaxWidth = 90;
            this.TrangThai.MinWidth = 90;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.Visible = true;
            this.TrangThai.VisibleIndex = 11;
            this.TrangThai.Width = 90;
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(8, 36);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(75, 23);
            this.btnCrawl.TabIndex = 1;
            this.btnCrawl.Text = "CRAWL";
            this.btnCrawl.Click += new System.EventHandler(this.btnCrawl_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(89, 36);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnReload);
            this.groupControl1.Controls.Add(this.btnExport);
            this.groupControl1.Controls.Add(this.btnCrawl);
            this.groupControl1.Location = new System.Drawing.Point(0, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(411, 76);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Thao tác";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(170, 36);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 479);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grid);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnCrawl;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnReload;
        private DevExpress.XtraGrid.Columns.GridColumn TenCongTy;
        private DevExpress.XtraGrid.Columns.GridColumn LoaiHinhHoatDong;
        private DevExpress.XtraGrid.Columns.GridColumn MaSoThue;
        private DevExpress.XtraGrid.Columns.GridColumn DiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn PhuongXa;
        private DevExpress.XtraGrid.Columns.GridColumn QuanHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn TinhThanh;
        private DevExpress.XtraGrid.Columns.GridColumn DaiDienPhapLuat;
        private DevExpress.XtraGrid.Columns.GridColumn NgayCapGiayPhep;
        private DevExpress.XtraGrid.Columns.GridColumn NgayHoatDong;
        private DevExpress.XtraGrid.Columns.GridColumn DienThoaiTruSo;
        private DevExpress.XtraGrid.Columns.GridColumn TrangThai;
    }
}