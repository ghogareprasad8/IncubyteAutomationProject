// <copyright file="Reusable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using AventStack.ExtentReports.Model;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using TechTalk.SpecFlow;
    using Actions = OpenQA.Selenium.Interactions.Actions;
    using SeleniumExtras.WaitHelpers;

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

        protected void LaunchURL(string url)
        {
            try
            {
                this.driver.Navigate().GoToUrl(url);
                Console.WriteLine("  url-->" + url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message is " + e.Message);
            }
        }
      
    }
}