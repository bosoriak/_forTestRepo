using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using System.Drawing.Imaging;
using uiTests.Common;
using uiTests.PageObject;

namespace uiTests
{
    
    public class Tests
    {
        private SessionBase session;
        private PageElements pageElements;

        [OneTimeSetUp]
 
        public void Setup()
        { /**
           * starts the session with the given parameters 
           * initializes the PageElements class
           **/

            session = new SessionBase();
            session.StartSession();
            pageElements = new PageElements(session);
        }
       

        [SetUp]
        public void CheckIfTestShouldRun()
        {
            if (session.failureOnStart)
            {
                Assert.Fail();
            }
        }
        
        [Test, Description("TestCase1: Click on the Button and get non-null value in the Textbox")]
        [NonParallelizable]
        [Category("Smoke")]
        [Order(1)]
        public void ClickWithElements()
        {
            pageElements.Button.Click();
            string text = pageElements.TextBox.GetAttribute("Value.Value");
            // to simulate failure on start and fail of other tests, use:
            // string text = pageElements.TextBox.GetAttribute("Text");
            Assert.That(text, Is.Not.Null);
  
        }

        [Test, Description("TestCase2: 15 clicks on Button gives 4 or less distinct values, slow")]
        [NonParallelizable]
        [Category("Performance")]
        [Order(2)]
        /**
         * to compare the duration of the TestCase2 and TestCase3
         * trace.listener commented out but left for future use 
         **/
        public void ClickWithElements15TimesSlow()
        {
            //Trace.Listeners.Add(new ConsoleTraceListener());
            List<string> textsFromTextbox = new List<string>();

            for (int i = 15; i> 0; i--)
            {
                pageElements.Button.Click();
                string text = pageElements.TextBox.GetAttribute("Value.Value");
                //Assert.That(text, Is.Not.Null);
                //Trace.WriteLine(text);
                textsFromTextbox.Add(text);
            }
            int count = textsFromTextbox.Distinct().Count();
            Assert.That(count, Is.LessThanOrEqualTo(4));
            //Trace.Flush();
        }

        [Test, Description("TestCase3: 15 clicks on Button gives 4 or less distinct values")]
        [NonParallelizable]
        [Category("Performance")]
        [Category("Racing")]
        [Order(3)]
        public void ClickMultipleTimesAssertDistincCount()
        {
            int expectedCount = 4;
            int count = PageActions.CountDistinctResult(pageElements.Button, pageElements.TextBox, 15);
            Assert.That(count, Is.LessThanOrEqualTo(expectedCount));
        }

        [TearDown]
        public void ScreenshotIfFail()
        {
            var result = TestContext.CurrentContext.Result.Outcome;
            if(result != ResultState.Success)
            {
                Utilities.TakeScreenshot(session.Driver, @"C:\ScreenshotTestFails\");
            }
        }

        [TearDown]
        
         // Set boolean start on failure to true, if the first test fails to make the result visible faster.
         // in a bigger repo, this teardown would be valid for only testcases of Smoke category
         
        public void SetFailureOnStart()
        {
            var result = TestContext.CurrentContext.Result.Outcome;
            if (result != ResultState.Success)
            {
               session.failureOnStart = true;
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            session.TearDown();
        }
    }
}