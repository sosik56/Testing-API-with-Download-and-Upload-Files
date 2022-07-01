using System;
using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using examTaskVariant2.Models;
using OpenQA.Selenium;

namespace examTaskVariant2.PageObjects
{
    public class ProjectPageByName:Form
    {
        private ILabel _tableCell(string rowNumber, string numberOfCell) => ElementFactory
            .GetLabel(By.XPath($"//table[@id='allTests']//tr[{rowNumber}]//td[{numberOfCell}]"), "Cell of table by row and cell number");

        private ILabel _cellTableHeaderByName(string name) => ElementFactory.
                GetLabel(By.XPath($"//table[@id='allTests']//th[text()='{name}']"), "Cell of table Header");

        private ILabel _testByNameAndMethod(string testName, string methodName) => ElementFactory
            .GetLabel(By.XPath($"//table[@id='allTests']//tr//a[text()='{testName}']//following::td[text()='{methodName}']"),
            "Test Line");

        public ProjectPageByName(string nameOfProject) : base(By.XPath($"//ol//li[text() ='{nameOfProject}']"), $"{nameOfProject} Project Page") { }

        public List<TestModel> GetListTestOnPage(int rows)
        {
            List<TestModel> tests = new List<TestModel>();

            for (int i = 2; i < rows + 2; i++)
            {
                TestModel test = new TestModel() { };
                test.Name = _tableCell(i.ToString(), GetIndexByName("Test name")).Text;
                test.Method = _tableCell(i.ToString(), GetIndexByName("Test method")).Text;
                test.Status = _tableCell(i.ToString(), GetIndexByName("Latest test result")).Text;
                test.StartTime = _tableCell(i.ToString(), GetIndexByName("Latest test start time")).Text;
                test.EndTime = _tableCell(i.ToString(), GetIndexByName("Latest test end time")).Text;
                test.Duration = _tableCell(i.ToString(), GetIndexByName("Latest test duration (H:m:s.S)")).Text;
                tests.Add(test);
            }

            return tests;
        }

        public string GetIndexByName(string columName)
        {
            int index = Convert.ToInt32(_cellTableHeaderByName(columName).GetAttribute("cellIndex")) + 1;
            return index.ToString();
        }

        public bool IsTestLableHere(string testNama, string methodName)
        {
            return _testByNameAndMethod(testNama, methodName).State.WaitForDisplayed();
        }
    }
}
