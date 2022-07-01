using Aquality.Selenium.Browsers;

namespace examTaskVariant2.UtilityClasses
{
    public static class JsManager
    {
        public static void ClosePopUp()
        {
            AqualityServices.Browser.ExecuteScript<string>("window.close()");
            AqualityServices.Browser.Tabs().SwitchToLastTab();
        }
    }
}
