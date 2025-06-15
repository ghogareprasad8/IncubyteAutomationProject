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
        /// Captures a screenshot and saves it locally.
        /// </summary>
        /// <param name="scenarioName">The name used to save the screenshot file.</param>
        /// <returns>The full path of the saved screenshot.</returns>
        // WebDriverContext.cs
        public string CaptureScreenshot(string scenarioName, int stepNumber)
        {
            try
            {
                // Get clean scenario name for folder
                string safeScenarioName = string.Join("_", scenarioName.Split(Path.GetInvalidFileNameChars()));

                // Folder path relative to base directory
                string baseFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\magneto.Softwaretesting\Results\Screenshots", safeScenarioName));

                if (!Directory.Exists(baseFolder))
                {
                    Directory.CreateDirectory(baseFolder);
                }

                // Define filename as Step1, Step2...
                string fileName = $"Step{stepNumber}.png";
                string fullPath = Path.Combine(baseFolder, fileName);

                var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot();
                File.WriteAllBytes(fullPath, screenshot.AsByteArray);

                Console.WriteLine($"[Screenshot] Saved to: {fullPath}");
                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Screenshot] Failed: {ex.Message}");
                return null;
            }
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
