using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MySeleniumTestsnew
{
    public class SampleTest : IDisposable
    {
        private readonly IWebDriver driver;

        public SampleTest()
        {
            // Set Chrome options to run in headless mode
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact]
        public void GoogleSearchTest()
        {
            try
            {
                // Navigate to Google
                driver.Navigate().GoToUrl("https://www.google.com");

                // Find the search box
                IWebElement searchBox = driver.FindElement(By.Name("q"));

                // Type search query
                searchBox.SendKeys("Selenium");

                // Submit the form
                searchBox.Submit();

                // Assert the title contains "Selenium"
                Assert.Contains("Selenium", driver.Title);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Test failed: {ex.Message}");
            }
        }

        public void Dispose()
        {
            // Ensure the browser is closed after the test
            driver.Quit();
        }
    }
}
