using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverWait
{
    public class ImplicitWaitTests
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

            driver.Navigate().GoToUrl("http://practice.bpbonline.com");

            //ImplicitWait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();

        }



        [Test]
        public void Search_Keyboard_Test()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The keyboard is not added to cart.");
            }
            catch (Exception ex)
            {

                Assert.Fail("Unexpected Exception" + ex.Message);

            }
        }


        [Test]
        public void Search_Junk_Test()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }

            catch (NoSuchElementException ex)
            {
                Assert.Pass("NoSuchElementException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected Exception" + ex.Message);
            }
        }
    }
}
