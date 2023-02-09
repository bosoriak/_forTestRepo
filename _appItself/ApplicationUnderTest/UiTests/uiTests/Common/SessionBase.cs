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
        private const int MaxMilisecondsToWait = 20000;
        private const int Step = 1000;

        //public WindowsDriver<WindowsElement> StartSession()
        //{
        //    var commonOptions = new CommonOptions();
        //    var desktopAppiumOptions = commonOptions.appiumOptions;

        //    Thread.Sleep(10000);

        //    return Driver = new WindowsDriver<WindowsElement>(new Uri(commonOptions.WindowsApplicationDriverUrl), desktopAppiumOptions);
        //}

        public void StartSession()
        {
            if (Driver != null)
            {
                TearDown();
            }
            var commonOptions = new CommonOptions();
            var desktopAppiumOptions = commonOptions.appiumOptions;

            Utilities.TryExecuteWithTimeout(
                MaxMilisecondsToWait,
                Step, 
                () => { return Driver = new WindowsDriver<WindowsElement>(new Uri(commonOptions.WindowsApplicationDriverUrl), desktopAppiumOptions); }, 
                () => Driver != null
                );
            
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public void TearDown()
        {
            Driver.Dispose();
            Driver.Close();
            Driver.Quit();
        }
    }
}
