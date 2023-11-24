using OpenQA.Selenium.Chrome;

namespace Utils
{
    public static class Helper
    {
        private static ChromeDriver _chrome = null, _chome1 = null;
        private static string _db;
        public static void SetDatabase(string val)
        {
            _db = $"{val}.db";
        }
        public static string GetDatabase()
        {
            return _db;
        }
        public static ChromeDriver GetChrome(int mode = 0)
        {
            if (mode == 0)
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
