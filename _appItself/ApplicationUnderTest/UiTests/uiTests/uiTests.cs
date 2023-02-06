using OpenQA.Selenium.Appium.Windows;
using uiTests.Common;

namespace uiTests
{
    
    public class Tests
    {
        private SessionBase session;

        [OneTimeSetUp]
        public void Setup()
        {
            session = new();
            session.StartSession();
            Thread.Sleep(5000);
        }

        [Test]
        [NonParallelizable]

        public void Click()
        {
            WindowsElement button = session.Driver.FindElementByAccessibilityId("ButtonId");
            Thread.Sleep(1000);
            button.Click();
            WindowsElement textBox = session.Driver.FindElementByAccessibilityId("TextBoxId");
            Assert.That(textBox.GetAttribute("Value.Value"), Is.Not.Null);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            session.TearDown();
        }
    }
}