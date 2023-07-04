using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SpecFlowFramework.Drivers;

namespace SpecFlowFramework.Pages
{
    [Binding]
    public class Navigation : SuperPage
    {
        public Navigation(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateTo(String url)
        {
            // Navigate to the login page
            Driver.Navigate().GoToUrl(url);
        }

    }
}

