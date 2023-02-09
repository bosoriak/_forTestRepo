using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uiTests.Common;

namespace uiTests.PageObject
{
    public class PageElements
    {
        private SessionBase session;

        public PageElements(SessionBase session) 
        {
            this.session = session;
        }

        public WindowsElement Button => Utilities.TryFindElement(() => session.Driver.FindElementByAccessibilityId("ButtonId"));
        public WindowsElement TextBox => Utilities.TryFindElement(() => session.Driver.FindElementByAccessibilityId("TextBoxId"));
    }
}
