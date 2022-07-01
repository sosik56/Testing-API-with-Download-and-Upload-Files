using Aquality.Selenium.Browsers;
using NUnit.Framework;

namespace examTaskVariant2.BaseClasses
{
    public abstract class BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            var browser = AqualityServices.Browser;
            browser.Maximize();           
        }

        [TearDown]
        public void TearDown()
        {
            AqualityServices.Browser.Quit();
        }
    }
}
