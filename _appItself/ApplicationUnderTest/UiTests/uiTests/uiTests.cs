using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
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

            for (int i = 15; i > 0; i--)
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

        [Test, Description("TestCase4: compare")]
        [NonParallelizable]
        [Order(4)]

        public void CompareCorrectResults()
        {
            /*
             * Click on Button
             * write the value of the textbox into a csv file with order Id to each line
             * write the value of the textbox into a csv file with order Id to each line
             * compare these values with the list of strings that are approved
             * 
             */


            string FilesPath = @"C:\test\";
            // this is a fixed values file for validation
            string CsvFileNameCore = "texts.csv";
            // this file will be created by the test
            string CsvFileNameFromTest = "testResultsTexts.csv";

            //Clear the content of the csv if it already exists
            if (File.Exists(FilesPath + CsvFileNameFromTest))
            {
                FileStream fileStream = File.Open(FilesPath + CsvFileNameFromTest, FileMode.Open);
                fileStream.SetLength(0);
                fileStream.Close();
            }

            // perform 2 times the button click
            // write the text from TextBox to the file

            using (var w = new StreamWriter(FilesPath + CsvFileNameFromTest))
            {
                // create the header
                w.WriteLine(string.Format("Order", "Value"));

                // write the 2 clicks and their result
                // TODO: create a method to click button and take the result

                for (int i = 0; i < 2; i++)
                {
                    pageElements.Button.Click();
                    string valueToWrite = pageElements.TextBox.GetAttribute("Value.Value");
                    string IdToWrite = i.ToString();
                    var line = string.Format($"{IdToWrite},{valueToWrite}");
                    w.WriteLine(line);
                    w.Flush();
                }
            }

            // read both files
            string[] csvTextLinesFromCoreFile = File.ReadAllLines(FilesPath + CsvFileNameCore);
            string[] csvTextLinesFromTestFile = File.ReadAllLines(FilesPath + CsvFileNameFromTest);

            // compare if the text from textbox is valid value from CsvFileNameCore 
            // TODO: create a method for checking the values

            foreach (string csvTextLine in csvTextLinesFromTestFile.Skip(1))
            {
                string testValue = csvTextLine.Split(',')[1];
                bool testResult = false;
                foreach (string csvLine in csvTextLinesFromCoreFile.Skip(1))
                {
                    string coreValue = csvLine.Split(",")[1];
                    
                    if (testValue == coreValue) { testResult = true; break; }
                }
                if (!testResult) { Assert.Fail(testValue); }
            }
            Assert.Pass();
        }


            [TearDown]
            public void ScreenshotIfFail()
            {
                var result = TestContext.CurrentContext.Result.Outcome;
                if (result != ResultState.Success)
                {
                    Utilities.TakeScreenshot(session.Driver, @"C:\ScreenshotTestFails\");
                }
            }

            [TearDown]

            // Set boolean start on failure to true, if the first test fails to make the result visible faster.

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