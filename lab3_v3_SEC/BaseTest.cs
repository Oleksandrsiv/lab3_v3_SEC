using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace lab3_v3_SEC
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly IWebDriver driver;

        protected BaseTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        
        protected void OpenPage(string path)
        {
            driver.Navigate().GoToUrl($"https://the-internet.herokuapp.com/{path}");
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}