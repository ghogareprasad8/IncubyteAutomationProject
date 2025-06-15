// <copyright file="WebDriverContext.cs" company="Incubyte">
// Copyright (c) Incubyte. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using OpenQA.Selenium;
    using NUnit.Framework;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.IO;
    using AventStack.ExtentReports;

    /// <summary>
    /// Manages WebDriver instance and supports screenshot capture.
    /// </summary>
    public class WebDriverContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the Selenium WebDriver instance.
        /// </summary>
        public IWebDriver driver { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverContext"/> class.
        /// </summary>
        public WebDriverContext()
        {
            string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
            ChromeOptions options = new ChromeOptions();

            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AcceptInsecureCertificates = true;

            this.driver = new ChromeDriver(options);
            this.driver.Manage().Window.Maximize();

            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"]));
            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["PageLoad"]));

            Console.WriteLine("[WebDriverContext] ChromeDriver initialized.");
        }

        /// <summary>
        /// To Capture Screenshot.
        /// </summary>
        /// <param name="name">Screenshot name.</param>
        /// <returns>Returns Sreenshot.</returns>
        public MediaEntityModelProvider CaptureScreenshotAndReturn(string name)
        {
            var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }

        /// <summary>
        /// Quits the WebDriver instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.driver?.Quit();
                Console.WriteLine("[WebDriverContext] ChromeDriver quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WebDriverContext] Error during driver quit: {ex.Message}");
            }
        }
    }
}
