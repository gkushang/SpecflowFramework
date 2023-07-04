using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Mail;
using System.Drawing;

namespace SpecFlowFramework.Drivers
{


    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        private IWebDriver _driver;


        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set the desired wait time

            _container.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void CleanupWebDriver(ScenarioContext scenarioContext)
        {
            try
            {
                //if (scenarioContext.TestError != null)
                //{

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotFileName = $"screenshot_{timestamp}.png";


                long totalHeight = (long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.scrollHeight");


                _driver.Manage().Window.Size = new Size(1920, (int)totalHeight);


                Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();


                string screenshotFilePath = Path.Combine(Directory.GetCurrentDirectory(), screenshotFileName);
                screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);


                var scenarioName = scenarioContext.ScenarioInfo.Title;
                var attachment = new Attachment(screenshotFilePath, "Scenario Failure Screenshot");
                scenarioContext.Add("screenshot", screenshot.AsBase64EncodedString);
                scenarioContext.ScenarioContainer.RegisterInstanceAs(attachment);

                //}
            }
            finally
            {

                _driver?.Quit();
            }
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            TestEnvironments.Environments.Load();
        }

    }
}
