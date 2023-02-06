using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;

namespace uiTests.Common
{
    public class CommonOptions
    {
        public readonly AppiumOptions appiumOptions;
        public readonly string AppProcessPath = @"C:\MyRepos\_forTest\_appItself\ApplicationUnderTest\bin\Debug\ApplicationUnderTest.exe";
        public readonly string WindowsApplicationDriverUrl;

        public CommonOptions()
        {
            var port = TestContext.Parameters["Common.Port"] ?? "4723";
            WindowsApplicationDriverUrl = $"http://127.0.0.1:{port}/";

            appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", AppProcessPath);
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appiumOptions.AddAdditionalCapability("platformName", "Windows");
            appiumOptions.AddAdditionalCapability("automationName", "Windows");

        }
    }
}
