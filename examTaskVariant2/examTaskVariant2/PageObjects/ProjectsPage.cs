using System;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using examTaskVariant2.UtilityClasses;
using OpenQA.Selenium;

namespace examTaskVariant2.PageObjects
{
    public class ProjectsPage:Form
    {       
        private ILabel _footerSpan = ElementFactory
            .GetLabel(By.XPath("//footer//p//span"), "Footer Version Lable");
        
        private IButton _projectButtonByName(string projectName) => ElementFactory
            .GetButton(By.XPath($"//div[@class='list-group']//a[text()='{projectName}']"), $"{projectName} Project Button");

        
        private IButton _projectAddButton = ElementFactory
            .GetButton(By.XPath($"//a[contains(@href,'addProject')]"), "Add Project Button");

        public ProjectsPage() : base(By.Id("addProject"), "Projects Page") { }

        public bool IsProjectButtonHere(string projectName)
        {
            return _projectButtonByName(projectName).State.IsDisplayed;
        }

        public string GetFooterSpanText()
        {
           return _footerSpan.Text;
        }

        public int GetNumberOfVariantFromFooter()
        {
           return Convert.ToInt32(UtilityClass.ReturnDigitFromEndOfString(_footerSpan.Text));
        }

        public void ClickAddButton()
        {
            _projectAddButton.Click();
        }

        public void ClickProjectButtonByName(string projectName)
        {
            _projectButtonByName(projectName).Click();            
        }        

        public string ReturnProjectIdByName(string projectName)
        {            
            return UtilityClass.ReturnDigitFromEndOfString(_projectButtonByName(projectName).GetAttribute("href"));
        }
    }
}
