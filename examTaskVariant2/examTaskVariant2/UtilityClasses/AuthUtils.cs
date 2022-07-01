using Aquality.Selenium.Browsers;

namespace examTaskVariant2.UtilityClasses
{
    public static class AuthUtils
    {
        public static void AuthtorizationByAlert(string login, string password, string urlWithoutHttp)
        {
            AqualityServices.Browser.GoTo($"http://{login}:{password}@{urlWithoutHttp}");
        }
    }
}
