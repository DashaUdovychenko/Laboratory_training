using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Helpers;

public static class CookieConsentHelper
{
    public static void AcceptCookies(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var cookieBtn = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("onetrust-accept-btn-handler"));
                return (el.Displayed && el.Enabled) ? el : null;
            });

            cookieBtn.Click();
        }
        catch (WebDriverTimeoutException)
        {
        }
        catch (NoSuchElementException)
        {
        }
    }
}