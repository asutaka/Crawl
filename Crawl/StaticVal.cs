using Crawl.InfoCom.ChildModel;
using Crawl.Model;
using System.Collections.Generic;
using Utils.ScheduleJob;

namespace Crawl
{
    public static class StaticVal
    {
        public static ConfigModel _config = new ConfigModel();
        public static List<TinhThanhDTO> _lTinhThanhComplete = new List<TinhThanhDTO>();

        public static string TinhThanh = string.Empty;
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
    }
}
