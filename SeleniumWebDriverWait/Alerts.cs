using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverWait
{
    public class Alerts
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

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
           
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Handle_Basic_Alerts_Test()
        {          
            driver.FindElement(By.XPath("//button[@onclick='jsAlert()' and text()='Click for JS Alert']")).Click();

            //Switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //Verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert did not open");

            //Accept the alert
            alert.Accept();

            //Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected.");

        }

        [Test]
        public void Confirm_Basic_Alerts_Test()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()' and text()='Click for JS Confirm']")).Click();

            //Switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //Verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Confirm did not open");

            //Accept the alert
            alert.Accept();

            //Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"));

        }

        [Test]
        public void Cancel_Basic_Alerts_Test()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()' and text()='Click for JS Confirm']")).Click();

            //Switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //Verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Confirm did not open");

            //Accept the alert
            alert.Dismiss();

            //Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"));

        }

        [Test]
        public void Handle_Prompt_Alerts_Test()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()' and text()='Click for JS Prompt']")).Click();

            //Switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //Verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Prompt did not open");

            string inputText = "populated";
            alert.SendKeys(inputText);

            //Accept the alert
            alert.Accept();

            //Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputText));

        }
    }
}
