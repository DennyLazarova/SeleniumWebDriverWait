using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriverWait
{
    public class ExplicitWaitTests
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

            // Set the implicit wait to 0 before using explicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                // Create WebDriverWait object with timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait to identify the Buy Now link using the LinkText property
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));



                // Set the implicit wait back to 10 seconds
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

                 Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page.");

            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }


        [Test]
        public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            // Set the implicit wait to 0 before using explicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                // Create WebDriverWait object with timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait to identify the Buy Now link using the LinkText property
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                // If found, fail the test as it should not exist
                buyNowLink.Click();
                Assert.Fail("The 'Buy Now' link was found for a non-existing product.");
            }
            catch (WebDriverTimeoutException)
            {
                // Expected exception for non-existing product
                Assert.Pass("Expected WebDriverTimeoutException was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
            finally
            {
                // Reset the implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}