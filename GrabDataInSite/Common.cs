using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GrabDataInSite
{
    public class Common
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        public int defaultTimeout = 10;
        public void StartChrome()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
            driver.Manage().Window.Maximize();
        }

        public void OpenSite(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForPageLoad(driver, defaultTimeout);
        }

        public void WaitForPageLoad(IWebDriver driver, int timeoutSec)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }


        public void WaitForElementPresent(string xp)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xp)));
        }


        public void WaitForElementClickable(string xp)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xp)));
        }

        public IWebElement element(string xp)
        {
            WaitForElementPresent(xp);
            return driver.FindElement(By.XPath(xp));
        }
    }
}
