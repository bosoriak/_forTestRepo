using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiTests.PageObject
{
    public class PageActions
    {
        public static int CountDistinctResult(WindowsElement button, WindowsElement textBoxToGetValueFrom, int repetitionCount)
        {
            List<string> textsFromTextbox = new List<string>();

            for (int i = repetitionCount; i > 0; i--)
            {
                button.Click();
                string text = textBoxToGetValueFrom.GetAttribute("Value.Value");
                textsFromTextbox.Add(text);
            }

            return textsFromTextbox.Distinct().Count();
            
       }
    }
}
