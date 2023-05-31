using Crawl.ScheduleJob;
using OpenQA.Selenium.Chrome;

namespace Crawl
{
    public static class StaticVal
    {
        public static ScheduleMng scheduleMng = ScheduleMng.Instance();
        private static ChromeDriver _chrome = null, _chome1 = null;
        public static ChromeDriver GetChrome(int mode)
        {
            if(mode == 0)
            {
                if (_chrome == null)
                {
                    var options = new ChromeOptions();
                    options.AddArguments("headless", "--blink-settings=imagesEnabled=false");
                    _chrome = new ChromeDriver(options);
                }
                return _chrome;
            }
            else
            {
                if (_chome1 == null)
                {
                    var options = new ChromeOptions();
                    options.AddArguments("headless", "--blink-settings=imagesEnabled=false");
                    _chome1 = new ChromeDriver(options);
                }
                return _chome1;
            }
        }
    }
}
