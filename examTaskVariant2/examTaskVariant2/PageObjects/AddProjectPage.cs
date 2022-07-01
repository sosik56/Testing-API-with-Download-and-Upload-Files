using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace examTaskVariant2.PageObjects
{
    public class AddProjectPage:Form
    {       

        private ILabel _textAboutProjectSave(string projectName) => ElementFactory
             .GetLabel(By.XPath($"//div[contains(text(),'Project {projectName} saved')]"), "Info text about save project");       

        private ITextBox _projectNameTextBox = ElementFactory
            .GetTextBox(By.Id("projectName"), "Project name Text Box");

        private IButton _saveProjectButton = ElementFactory
            .GetButton(By.XPath("//button[@type='submit']"),"Save project button");

        public AddProjectPage() : base(By.Id("projectName"), "Add Projects Page") { }

        public void EnterProjectName(string name)
        {
            _projectNameTextBox.ClearAndType(name);
        }

        public void ClickSaveProjectButton()
        {
            _saveProjectButton.Click();
        }       

        public bool IsThereTextAboutSaveProject(string projectName)
        {
            return _textAboutProjectSave(projectName).State.IsExist;
        }
    }
}
