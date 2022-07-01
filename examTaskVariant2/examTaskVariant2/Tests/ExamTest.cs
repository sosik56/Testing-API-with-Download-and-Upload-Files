using System;
using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using examTaskVariant2.BaseClasses;
using examTaskVariant2.Models;
using examTaskVariant2.PageObjects;
using examTaskVariant2.UtilityClasses;
using NUnit.Framework;


namespace examTaskVariant2
{
    public class ExamTest:BaseTest
    {
        private Aquality.Selenium.Core.Logging.Logger _loger = Aquality.Selenium.Core.Logging.Logger.Instance;
        private ProjectsPage _projectsPage = new ProjectsPage();       
        private AddProjectPage _addProjectPage = new AddProjectPage();        
        private ProjectPageByName _nexageProjectPage = new ProjectPageByName(UtilityClass.ReturnValue("existProjectName", "testdata"));
        private ProjectPageByName _newProjectPage = new ProjectPageByName(UtilityClass.ReturnValue("nameOfCreatingProject", "testdata"));
        private string _SID = DateTime.Now.ToString();

        [Test]
        public void Test1()
        {   
            _loger.Info("Getting Token");
            string token = TestDbApi.GetTokenByNumVariant(UtilityClass.ReturnValue("variant", "testdata"));
            Assert.True(token != null, "Token can't be null");


            _loger.Info("Authtorization");
            AuthUtils.AuthtorizationByAlert(UtilityClass.ReturnValue("login","config"),
                                               UtilityClass.ReturnValue("password", "config"),
                                               UtilityClass.ReturnValue("urlWithoutHttp", "config"));
            Assert.IsTrue(_projectsPage.State.IsExist, "ProjectsPage is not open");


            _loger.Info("Send cookie token");
            CookiesManager.SetCookieByNameAndValue(UtilityClass.ReturnValue("tokenName", "testdata"), token);
            AqualityServices.Browser.Refresh();
            Assert.IsTrue(_projectsPage.GetNumberOfVariantFromFooter() == Convert.ToInt32(UtilityClass.ReturnValue("variant", "testdata")),
                "Variant incorect");


            _loger.Info("Go to Nexage Project And Get Nexage List Of test");            
            string nexageId = _projectsPage.ReturnProjectIdByName(UtilityClass.ReturnValue("existProjectName", "testdata"));
            _projectsPage.ClickProjectButtonByName(UtilityClass.ReturnValue("existProjectName", "testdata"));
            string nexageTestsJsonString = TestDbApi.GetJsonListOfTestByProjectId(nexageId); 
            List<TestModel> testFromJson =  DeSerializeClass.DeserializeObject<List<TestModel>>(nexageTestsJsonString);
            List<TestModel> testsOnFirstPage = 
                _nexageProjectPage.GetListTestOnPage(Convert.ToInt32(UtilityClass.ReturnValue("testsNumber", "testdata"))); 
            Assert.IsTrue(TestModel.IsSortedByDecendingByFildName(testsOnFirstPage, "StartTime"), "Not ordered by time");            
            Assert.IsTrue(TestModel.MathFromTwoLists(testFromJson, testsOnFirstPage) == testsOnFirstPage.Count, "Not all tests match");


            _loger.Info("Create New Project");
            AqualityServices.Browser.GoBack();
            _projectsPage.ClickAddButton();
            AqualityServices.Browser.Tabs().SwitchToLastTab();
            _addProjectPage.EnterProjectName(UtilityClass.ReturnValue("nameOfCreatingProject", "testdata"));
            _addProjectPage.ClickSaveProjectButton();
            Assert.IsTrue(_addProjectPage.IsThereTextAboutSaveProject(UtilityClass.ReturnValue("nameOfCreatingProject", "testdata")),
                "Project wasn't save");
            JsManager.ClosePopUp();
            Assert.IsTrue(!_addProjectPage.State.IsDisplayed,"Add project page still here");
            AqualityServices.Browser.Refresh();
            Assert.IsTrue(_projectsPage.IsProjectButtonHere(UtilityClass.ReturnValue("nameOfCreatingProject", "testdata")),
                "There is no such project");


            _loger.Info("Create New Test");
            _projectsPage.ClickProjectButtonByName(UtilityClass.ReturnValue("nameOfCreatingProject", "testdata"));
            var screen =  AqualityServices.Browser.GetScreenshot();
            string screenBase64 = Convert.ToBase64String(screen);            
            var testId =  TestDbApi.CreateTestWithImageAndLog(_SID, UtilityClass.ReturnValue("nameOfCreatingProject", "testdata"),
                                                                    UtilityClass.ReturnValue("testName", "testdata"),
                                                                    UtilityClass.ReturnValue("methodName", "testdata"),
                                                                    UtilityClass.ReturnValue("env", "testdata"),
                                                                    screenBase64, 
                                                                    UtilityClass.ReturnLogString());

            Assert.IsTrue(_newProjectPage.IsTestLableHere(UtilityClass.ReturnValue("testName", "testdata"), 
                UtilityClass.ReturnValue("methodName", "testdata")),"There is no such test");
        } 
    }
}