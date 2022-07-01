using Aquality.Selenium.Browsers;

namespace examTaskVariant2.UtilityClasses
{
    public static class CookiesManager
    {
        public static void SetCookieByNameAndValue(string cookieName, string cookieValue)
        {
            OpenQA.Selenium.Cookie cookie1 = new OpenQA.Selenium.Cookie(cookieName, cookieValue);
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(cookie1);
        }
    }
}
