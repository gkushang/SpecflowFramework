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
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports;
using SpecFlowFramework.Utils;

namespace SpecFlowFramework.Drivers
{


    [Binding]
    public class Hooks : ExtentReport
    {
        private readonly IObjectContainer _container;
        private IWebDriver _driver;


        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Running before test run...");
            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
            ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature...");
            TestEnvironments.Environments.Load();
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }




        [BeforeScenario]
        public void InitializeWebDriver(ScenarioContext scenarioContext)
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set the desired wait time

            _container.RegisterInstanceAs(_driver);

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void CleanupWebDriver(ScenarioContext scenarioContext)
        {


            _driver?.Quit();

        }


        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            var driver = _container.Resolve<IWebDriver>();

            //When scenario passed
            //  if (scenarioContext.TestError == null)
            //{
            if (stepType == "Given")
            {
                _scenario.CreateNode<Given>(stepName);
            }
            else if (stepType == "When")
            {
                _scenario.CreateNode<When>(stepName);
            }
            else if (stepType == "Then")
            {
                _scenario.CreateNode<Then>(stepName);
            }
            else if (stepType == "And")
            {
                _scenario.CreateNode<And>(stepName);
            }
            //}

            //When scenario fails
            if (scenarioContext.TestError != null)
            {

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
            }
        }

    }
}
