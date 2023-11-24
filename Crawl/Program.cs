using Crawl.Model;
using Crawl.TraTenCongTy;
using System;
using System.Windows.Forms;
using Utils;

namespace Crawl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StaticVal._config = "config.json".LoadJsonFile<ConfigModel>();
            if(StaticVal._config.Host.Equals(EWebsite.TraTenCongTy.GetDisplayName()))
            {
                Helper.SetDatabase(EWebsite.TraTenCongTy.ToString());
                Application.Run(new frmMain());
            }
            else
            {

            }
        }

        
    }
}
