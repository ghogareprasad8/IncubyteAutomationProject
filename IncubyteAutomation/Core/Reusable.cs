// <copyright file="Reusable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;
    using Actions = OpenQA.Selenium.Interactions.Actions;

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

        private int zoomValue = 100;
        private int zoomIncrement = 30;

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
        protected void ScrollToElement(IWebElement element)
        {
            try
            {
                Actions actions = new Actions(this.driver);
                actions.MoveToElement(element);
                actions.Perform();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
            }
        }
        protected bool ElementUnavailability(By locator, string report)
        {
            try
            {

                IList<IWebElement> element = this.driver.FindElements(locator);

                if (element.Count == 0)
                {
                    Console.WriteLine(report + " is not Available");

                    return true;
                }
                else
                {
                    Console.WriteLine(report + " is Available");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
                return false;
            }
        }
        protected void CleanUp()
        {
            try
            {
                if (this.driver != null)
                {
                    this.driver.Quit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
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
        protected void WaitUntilElementVisible(By locator)
        {
            try
            {
                Task.Delay(3000).Wait();
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
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
        protected string ReturnAttributeValue(By locater, string attr)
        {
            try
            {
                this.JavaScrollToElement(locater);
                var attributeValue = this.driver.FindElement(locater).GetAttribute(attr).Trim();
                Console.WriteLine($"value of attribute {attr} for locator {locater} is equal to {attributeValue}");
                return attributeValue;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
                return null;
            }
        }
        protected IList<IWebElement> ReturnListOfWebElements(By locater)
        {
            IList<IWebElement> webElement = this.driver.FindElements(locater);
            return webElement;
        }
        protected void UpdateJson(string section, string key, string newValue)
        {
            try
            {
                string path = $@"{AppDomain.CurrentDomain.BaseDirectory}AppSettings.json";
                JObject jObject = JObject.Parse(File.ReadAllText(path));
                jObject[section][key] = newValue;
                File.WriteAllText(path, jObject.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
            }
        }
        protected IWebElement WaitAndFindWebElement(By by)
        {
            var webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
            return webDriverWait.Until(ExpectedConditions.ElementExists(by));
        }
        protected IWebElement FluentWaitAndFindWebElement(By by)
        {
            WebDriverWait fluentWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["FluentWait"])))
            {
                PollingInterval = TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["PollingInterval"])),
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(x => x.FindElement(by));
        }
        protected void WaitForAjax()
        {
            var webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
            var js = (IJavaScriptExecutor)this.driver;
            webDriverWait.Until(wd => js.ExecuteScript("return !!window.jQuery && window.jQuery.active == 0"));
        }
        protected void Refresh()
        {
            this.driver.Navigate().Refresh();
            this.WaitForAjax();
        }
        protected void WaitUntilPageLoadsCompletely()
        {
            var webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"])));
            var js = (IJavaScriptExecutor)this.driver;
            webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        protected int CountElements(By by)
        {
            Task.Delay(1000).Wait();
            IList<IWebElement> elements = this.driver.FindElements(by);
            return elements.Count;
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


    }
}