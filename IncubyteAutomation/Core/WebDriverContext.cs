// <copyright file="WebDriverContext.cs" company="Incubyte">
// Copyright (c) Incubyte. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using AventStack.ExtentReports;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;

    /// <summary>
    /// Web Driver Context class.
    /// </summary>
    public class WebDriverContext : IDisposable
    {
        /// <summary>
        /// Gets or sets web Driver.
        /// </summary>
        public IWebDriver driver { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverContext"/> class.
        /// </summary>
        public WebDriverContext()
        {
            ChromeOptions options = new ChromeOptions();
            string downloadPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Downloads";
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            options.AddArgument("test-type");
            options.AddArgument("-disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("start-maximized");
            options.AddArgument("force-device-scale-factor=1.20");
            options.AcceptInsecureCertificates = true;
            options.LeaveBrowserRunning = true;
            this.driver = new ChromeDriver(options);
            this.driver.Manage().Window.Maximize();
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["ImplicitWait"]));
            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["PageLoad"]));
        }
        /// <summary>
        /// To Capture Screenshot.
        /// </summary>
        /// <param name="name">Screenshot name.</param>
        /// <returns>Returns Sreenshot.</returns>
        public MediaEntityModelProvider CaptureScreenshotAndReturn(string name)
        {
            var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();
        }
        /// <summary>
        /// To close the driver.
        /// </summary>
        public void Dispose()
        {
            this.driver?.Quit();
        }
    }
}
