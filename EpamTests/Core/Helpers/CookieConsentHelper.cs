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

            IWebElement cookieBtn = wait.Until(d =>
            {
                IWebElement el = d.FindElement(By.Id("onetrust-accept-btn-handler"));
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