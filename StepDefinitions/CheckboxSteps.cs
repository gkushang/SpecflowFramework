using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowFramework.Pages;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace SpecFlowFramework.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {

        private Navigation navigation;
        private HomePage homePage;

        public CalculatorStepDefinitions(IWebDriver driver)
        {
            navigation = new Navigation(driver);
            homePage = new HomePage(driver);
        }



        [When("I check the checkbox")]
        public void IChecktheCheckbox()
        {
            navigation.NavigateTo(Environment.GetEnvironmentVariable("APPLICATION_HOST_URL"));
            homePage.GoToCheckboxes();
            homePage.CheckTheCheckbox();
            Thread.Sleep(100);

        }

        [Then("the checkbox should be checked")]
        public void ThenTheCheckboxShouldBeChecked()
        {
            homePage.IsCheckboxChecked().Should().BeTrue();
        }

        [When(@"I fill Input field ""(.*)""")]
        public void WhenIFillInputField(string p0)
        {
            navigation.NavigateTo(Environment.GetEnvironmentVariable("APPLICATION_HOST_URL"));
            homePage.GoToInputs();
            homePage.FillInput(p0);
        }

        [Then(@"I wait for (.*) Seconds")]
        public void ThenIWaitForSeconds(int p0)
        {

            Thread.Sleep(p0 * 1000);
        }

    }
}