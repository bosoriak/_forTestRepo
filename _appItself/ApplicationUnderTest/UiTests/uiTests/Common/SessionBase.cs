using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiTests.Common
{
    public class SessionBase
    {
        protected readonly CommonOptions? commonOptions;
        public WindowsDriver<WindowsElement>? Driver { get; set; }

        public WindowsDriver<WindowsElement> StartSession()
        {
            var commonOptions = new CommonOptions();
            var desktopAppiumOptions = commonOptions.appiumOptions;

            Thread.Sleep(10000);

            return Driver = new WindowsDriver<WindowsElement>(new Uri(commonOptions.WindowsApplicationDriverUrl), desktopAppiumOptions);
        }

        public void TearDown()
        {
            Driver.Dispose();
            Driver.Close();
            Driver.Quit();
        }
    }
}
