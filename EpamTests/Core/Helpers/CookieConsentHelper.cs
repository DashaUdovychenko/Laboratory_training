using EpamTests.Core.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Core.Helpers;

public static class CookieConsentHelper
{
    public static void AcceptCookies(IWebDriver driver)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            Logger.Info("Waiting for the cookie consent button to become clickable.");
            IWebElement cookieBtn = wait.Until(d =>
            {
                IWebElement el = d.FindElement(By.Id("onetrust-accept-btn-handler"));
                return (el.Displayed && el.Enabled) ? el : null;
            });

            Logger.Info("Clicking the cookie consent button.");
            cookieBtn.Click();
        }
        catch (WebDriverTimeoutException)
        {
            Logger.Error("Cookie consent button not clickable within the timeout period.");
        }
        catch (NoSuchElementException)
        {
            Logger.Error("Cookie consent button not found on the page.");
        }
    }
}