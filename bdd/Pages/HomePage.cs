using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace BDD.Pages;

public class HomePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    private const string url = "https://www.epam.com/";

    private readonly By servicesBtn = By.XPath("//a[contains(@class, 'top-navigation__item-link') and contains(text(), 'Services')]");

    private readonly By serviceCategoryLinks = By.XPath(
       "//div[contains(@class, 'top-navigation__flyout')]//a[contains(@href, '/services')]"
    );

    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateToHomePage()
    {
        driver.Navigate().GoToUrl(url);
    }

    public ServicesPage NavigateToService(string categoryName)
    {
        HoverOverServicesMenu();
        ClickOnServiceCategory(categoryName);
        return new ServicesPage(driver);
    }

    private void HoverOverServicesMenu()
    {
        var servicesMenu = wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(servicesBtn);
                return (el.Displayed && el.Enabled) ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        new Actions(driver).MoveToElement(servicesMenu).Perform();
    }

    private void ClickOnServiceCategory(string categoryName)
    {
        var servicesList = wait.Until(driver =>
        {
            var elements = driver.FindElements(serviceCategoryLinks);
            return elements.Count > 0 ? elements : null;
        });

        var link = servicesList.FirstOrDefault(link =>
            link.Text.Trim().Equals(categoryName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (link == null)
        {
            throw new NoSuchElementException($"Service category '{categoryName}' not found in the dropdown.");
        }

        wait.Until(d =>
        {
            try
            {
                return link.Displayed && link.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        });

        link.Click();
    }
}