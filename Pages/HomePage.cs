using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowFramework.Pages
{
    public class HomePage : SuperPage
    {

        public HomePage(IWebDriver driver) : base(driver) { }
        public IWebElement Checkbox_1 => Driver.FindElement(By.CssSelector("input[type=\"checkbox\"]"));
        public IWebElement CheckboxLink => Driver.FindElement(By.CssSelector("a[href=\"/checkboxes\"]"));
        public IWebElement Inputs => Driver.FindElement(By.CssSelector("a[href=\"/inputs\"]"));
        public IWebElement InputField => Driver.FindElement(By.CssSelector("input"));


        public void GoToCheckboxes()
        {
            CheckboxLink.Click();
        }
        public void GoToInputs()
        {
            Inputs.Click();
        }

        public void FillInput(string input)
        {
            InputField.SendKeys(input);
        }
        public void CheckTheCheckbox()
        {
            Checkbox_1.Click();
            if (!Checkbox_1.Selected)
            {
                Checkbox_1.Click();
            }


        }

        public Boolean IsCheckboxChecked()
        {
            return Checkbox_1.Selected;
        }
    }
}
