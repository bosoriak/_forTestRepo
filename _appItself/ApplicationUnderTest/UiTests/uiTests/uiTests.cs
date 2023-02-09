using OpenQA.Selenium.Appium.Windows;
using uiTests.Common;
using uiTests.PageObject;

namespace uiTests
{
    
    public class Tests
    {
        private SessionBase session;
        private PageElements pageElements;

        [OneTimeSetUp]
        //public void Setup()
        //{
        //    session = new();
        //    session.StartSession();
        //    Thread.Sleep(5000);
        //}

        public void Setup()
        {
            session = new SessionBase();
            session.StartSession();
            pageElements = new PageElements(session);
        }

        
        [Test]
        [NonParallelizable]

        public void ClickWithElements()
        {
            pageElements.Button.Click();
            string text = pageElements.TextBox.GetAttribute("Value.Value");
            Assert.That(text, Is.Not.Null);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            session.TearDown();
        }
    }
}