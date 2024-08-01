using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SeleniumWebDriverWait
{
    public class WindowTesting
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-search-engine-choice-screen");
            options.AddArgument("--no-first-run");
            options.AddArgument("--no-default-browser-check");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-default-apps");

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows ");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }


        [Test]
        public void Handle_NoSuch_Windows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> handles = driver.WindowHandles;

            Assert.That(handles.Count, Is.EqualTo(2), "The number of open tabs should be 2");

            driver.SwitchTo().Window(handles[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;

            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window content");

            driver.Close();

            try
            {
                driver.SwitchTo().Window(handles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                Assert.Pass("Expected error");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected errror" + ex.Message);
            }
        }


            [Test]
        public void Handle_Multiple_Windows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

           ReadOnlyCollection<string>  handles = driver.WindowHandles;

            Assert.That(handles.Count, Is.EqualTo(2), "The number of open tabs should be 2");

            driver.SwitchTo().Window(handles[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;

            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window content");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "content.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "current handle: " + driver.CurrentWindowHandle);
            File.AppendAllText(path, "page content: " + driver.PageSource);

            driver.SwitchTo().Window(handles[0]);

            string originalWindowContent = driver.FindElement(By.TagName("h3")).Text;

            Assert.That(originalWindowContent, Is.EqualTo("Opening a new window"), "Did not find new window content");

            File.AppendAllText(path, "current handle: " + driver.CurrentWindowHandle);
            File.AppendAllText(path, "page content: " + driver.PageSource);
        }
    }
}
