using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiTests.Common
{
    public class Utilities
    {
        /*
        * This method takes an Action delegate (a delegate that does not return a value), a condition delegate (a delegate that returns a bool), 
        * a timeout (the maximum amount of time in milliseconds to wait for the function to succeed), 
        * and a step (the amount of time in milliseconds to wait between each retry). 
        * The method will keep executing the function and checking the condition until 
        * either the condition returns true or the timeout has been reached.
        */

        public static T TryExecuteWithTimeout<T>(int timeout, int step, Func<T> function, Func<bool> condition)
        {
            int elapsedTime = 0;
            while (elapsedTime < timeout && !condition())
            {
                try
                {
                    T result = function();
                    if (condition())
                    {
                        return result;
                    }
                }
                catch (Exception)
                {
                    elapsedTime += step;
                    Thread.Sleep(step);
                }
            }
            return function();
        }

        public static T? TryFindElement<T>(Func<T> func, Func<T, bool> condition = null, long? timeout = null)
        {
            var elementFound = TryExecuteWithTimeout(5000, 500, func, () => func() != null);
            if (elementFound == null || condition is null)
            {
                return elementFound;
            }

            return TryExecuteWithTimeout(5000, 500, () => condition(elementFound), () => condition(elementFound)) ? elementFound : default(T);
        }

        /*
         * TO BE INVESTIGATED LATER
         */
        public static void TryExecuteWithTimeoutStartSession(int timeout, int step, Action function, Func<bool> condition)
        {
            int elapsedTime = 0;
            while (elapsedTime < timeout && !condition())
            {
                try
                {
                    function();
                    if (condition())
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    elapsedTime += step;
                    Thread.Sleep(step);
                }
            }
        }
     public static void TakeScreenshot(WindowsDriver<WindowsElement> driver, string folderPathToScreensthot)
        {            
            var screenshot = driver.GetScreenshot();
            string folderPath = folderPathToScreensthot;
            string fileName = $"{TestContext.CurrentContext.Test.FullName}.png";

            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            var filePath = folderPath + timeStamp + fileName;
            
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }



    }
}
