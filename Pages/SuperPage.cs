using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SpecFlowFramework.Pages
{
    public class SuperPage
    {
        protected readonly IWebDriver Driver;

        public SuperPage(IWebDriver driver)
        {
            Driver = driver;
        }
    }

}
