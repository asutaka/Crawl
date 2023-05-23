using Crawl.ScheduleJob;

namespace Crawl
{
    public static class StaticVal
    {
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
        public static string Scron_CheckStatus = "* * * * * ?";
    }
}
