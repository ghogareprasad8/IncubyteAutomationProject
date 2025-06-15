// <copyright file="Reusable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using System;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Reusable Class.
    /// </summary>
    public class Reusable
    {
        /// <summary>
        /// Test case name string object.
        /// </summary>
        public static string TestCaseName = string.Empty;

        /// <summary>
        /// /Environment string object.
        /// </summary>
        public static string Environment = string.Empty;

        /// <summary>
        /// Gets or sets web driver object.
        /// </summary>
        public IWebDriver driver { get; set; }
        public Reusable(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
        }
        protected string RandomNumber(int length)
        {
            try
            {
                var random = new Random();
                string s = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    s = string.Concat(s, random.Next(10).ToString());
                }

                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
                return string.Empty;
            }
        }
        protected void JavaScrollToElement(By locator)
        {
            try
            {
                ((IJavaScriptExecutor)this.driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", this.driver.FindElement(locator));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Scrolling to Particular element, Error message: " + ex.Message);
            }
        }
        protected void LaunchURL(string url)
        {
            try
            {
                this.driver.Navigate().GoToUrl(url);
                Console.WriteLine("Application url-->" + url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
            }
        }
        protected void ElementOperations(string action, By locator, string value, string report)
        {
            try
            {
                Task.Delay(500).Wait();
                this.WaitUntilPageLoadsCompletely();
                IWebElement element = this.driver.FindElement(locator);
                this.JavaScrollToElement(locator);
                switch (action)
                {
                    case "click":
                        try
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)this.driver;
                            executor.ExecuteScript("arguments[0].click();", element);
                        }
                        catch (Exception)
                        {
                            element.Click();
                        }

                        Console.WriteLine(report + " is clicked");
                        break;

                    case "sendKeys":
                        element.Clear();
                        element.SendKeys(value);
                        Console.WriteLine(report + " is send as input");
                        break;

                    case "verify":
                        if (element.Displayed)
                        {
                            Console.WriteLine(element.Text + " is displayed");
                        }
                        else
                        {
                            Console.WriteLine(element.Text + " is not displayed");
                        }

                        break;
                    case "selected":
                        if (element.Selected)
                        {
                            Console.WriteLine(element.Text + " is selected/checked");
                        }
                        else
                        {
                            Console.WriteLine(element.Text + " is not selected/checked");
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine(action + "Failed Due to " + e.Message);
                    throw;
                }
            }
        }
        protected bool ElementAvailability(By locator)
        {
            try
            {
                IWebElement element = this.driver.FindElement(locator);
                if (element.Displayed)
                {
                    Console.WriteLine(element.Text + locator + " is displayed");
                    element = null;
                    return true;
                }
                else
                {
                    Console.WriteLine(element.Text + locator + " is not displayed");
                    element = null;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
                return false;
            }
        }
        protected bool ValidateActualAndExpectedValues(string actual, string expected)
        {
            try
            {
                if (actual.ToLower().Contains(expected.ToLower()))
                {
                    Console.WriteLine(actual + " and " + expected + " value are matched");
                    return true;
                }
                else
                {
                    Console.WriteLine(actual + " and " + expected + " value are NOT matched");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
                return false;
            }
        }
        protected string GetElementText(By locator)
        {
            return this.driver.FindElement(locator).Text;
        }
        protected IWebElement WaitAndFindWebElement(By by)
        {
            var webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
            return webDriverWait.Until(ExpectedConditions.ElementExists(by));
        }  
        protected void WaitUntilPageLoadsCompletely()
        {
            var webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
            var js = (IJavaScriptExecutor)this.driver;
            webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        protected bool WaitForRedirectionToUrl(string expectedUrl, int waitTimeInSeconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(waitTimeInSeconds + 3));
                bool isRedirected = wait.Until(driver =>
                {
                    return driver.Url.Contains(expectedUrl);
                });

                Console.WriteLine("Redirected successfully to: " + this.driver.Url);
                return isRedirected;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"User was not redirected to {expectedUrl} within {waitTimeInSeconds} seconds.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message is " + ex.Message);
                return false;
            }
        }
        protected void VerifyPartialUrl(string expectedPartialUrl)
        {
            try
            {
                string currentUrl = this.driver.Url;
                if (!currentUrl.Contains(expectedPartialUrl))
                {
                    throw new Exception($"Expected URL to contain '{expectedPartialUrl}', but found '{currentUrl}'.");
                }
                Console.WriteLine($"Verified: Current URL contains '{expectedPartialUrl}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while verifying URL: {ex.Message}");
                throw;
            }
        }


    }
}