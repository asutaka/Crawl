using Crawl.Model;
using Crawl.TraTenCongTy.ChildModel;
using System.Collections.Generic;
using Utils.ScheduleJob;

namespace Crawl
{
    public static class StaticVal
    {
        public static ConfigModel _config = new ConfigModel();

        public static string TinhThanh = string.Empty;
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
        public static List<TinhThanhDTO> _lstCmb = new TinhThanhModel().lData;
    }
}
