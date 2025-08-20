using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace BDD.Bussines.Pages;

public class ServicesPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By Title = By.XPath("//span[@class='museo-sans-500 gradient-text']");
    private readonly By RelatedExpertiseSection = By.XPath("//span[@class='museo-sans-light' and contains(text(), 'Our Related Expertise')]");

    public ServicesPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public string GetTitle()
    {
        IWebElement element = wait.Until(d => d.FindElement(Title));
        return element.Text.Trim();
    }

    public void ScrollToRelatedExpertiseSection()
    {
        IWebElement element = wait.Until(d => d.FindElement(RelatedExpertiseSection));

        Actions actions = new Actions(driver);
        actions.MoveToElement(element).Perform();
    }

    public bool IsRelatedExpertiseSectionVisible()
    {
        try
        {
            IWebElement element = wait.Until(d => d.FindElement(RelatedExpertiseSection));
            return element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}