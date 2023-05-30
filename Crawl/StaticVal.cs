using Crawl.ScheduleJob;
using OpenQA.Selenium.Chrome;

namespace Crawl
{
    public static class StaticVal
    {
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
        private static ChromeDriver _chrome = null;
        public static ChromeDriver GetChrome()
        {
            if(_chrome == null)
            {
                var options = new ChromeOptions();
                options.AddArguments("headless", "--blink-settings=imagesEnabled=false");
                _chrome = new ChromeDriver(options);
            }
            return _chrome;
        }
    }
}
