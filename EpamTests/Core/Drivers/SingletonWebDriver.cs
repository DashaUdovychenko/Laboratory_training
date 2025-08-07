using OpenQA.Selenium;
using EpamTests.Core.Configuration;
using EpamTests.Core.Logging;
using System.Threading;

namespace EpamTests.Core.Driver
{
    public static class SingletonWebDriver
    {
        private static ThreadLocal<IWebDriver?> _webDriver = new ();

        public static IWebDriver GetDriver()
        {
            if (!_webDriver.IsValueCreated || _webDriver.Value == null)
            {
                _webDriver.Value = WebDriverFactory.CreateDriverFromConfig();
            }

            return _webDriver.Value!;
        }

        public static void Close()
        {
            if (_webDriver.IsValueCreated && _webDriver.Value != null)
            {
                Logger.Info("[Singleton] Closing WebDriver instance");
                _webDriver.Value.Quit();
                _webDriver.Value.Dispose();
                _webDriver.Value = null;
            }
        }
    }
}