// <copyright file="Hooks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using PwC.SDK.AutomatedTest.GIS.BaseClass;

namespace PwC.SDK.AutomatedTest.GIS.BaseClass
{
    using System;
    using System.IO;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Gherkin.Model;
    using AventStack.ExtentReports.Reporter;
    using BoDi;
    using IncubyteAutomation.Core;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Hooks class.
    /// </summary>
    [Binding]
    public class Hooks
    {
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        [ThreadStatic]
        private static ExtentReports extent;
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly WebDriverContext _webDriverContext;
        private static ExtentHtmlReporter htmlReporter;
        private static string reportPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">Object Container.</param>
        /// <param name="featureContext">Feature Context.</param>
        /// <param name="scenarioContext">Scenario Context.</param>
        /// <param name="webDriverContext">Web Driver Context.</param>
        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext, WebDriverContext webDriverContext)
        {
            this._objectContainer = objectContainer;
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._webDriverContext = webDriverContext;
        }

        /// <summary>
        /// Before Test run method.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Go three directories up from /bin/Debug/net6.0
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            // Desired report path
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string reportFolder = Path.Combine(projectRoot, $@"magneto.Softwaretesting\Results\TestResults\Run_{timestamp}");

            if (!Directory.Exists(reportFolder))
            {
                Directory.CreateDirectory(reportFolder);
            }

            reportPath = Path.Combine(reportFolder, "extentReport.html");

            htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.DocumentTitle = "IncubyteAutomation Report";
            htmlReporter.Config.ReportName = "IncubyteAutomation";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            Console.WriteLine("Initialized Extent Report at: " + reportPath);
        }


        /// <summary>
        /// Statements to be executed before Feature.
        /// </summary>
        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("Before feature run");
        }

        /// <summary>
        /// Statements to be executed after feature.
        /// </summary>
        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("After feature run");
        }

        /// <summary>
        /// Statements to be executed after Test run.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
            TestContext.AddTestAttachment(reportPath);
            Console.WriteLine("Final report flushed to: " + reportPath);
        }
        /// Before Scenario.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            this._objectContainer.RegisterInstanceAs(this._webDriverContext.driver, typeof(IWebDriver));
            Reusable.TestCaseName = this._scenarioContext.ScenarioInfo.Title;

            featureName = extent.CreateTest<Feature>(this._featureContext.FeatureInfo.Title);
            scenario = featureName.CreateNode<Scenario>(this._scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// Insert Specflow Reporting Steps.
        /// </summary>
        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = this._scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepInfo = this._scenarioContext.StepContext.StepInfo.Text;
            var resultOfImplementation = this._scenarioContext.ScenarioExecutionStatus.ToString();
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var screenshotMediaEntity = this._webDriverContext.CaptureScreenshotAndReturn(this._scenarioContext.StepContext.StepInfo.Text.Trim());

            if (this._scenarioContext.TestError == null && resultOfImplementation == "OK")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Pass("Step Passed", screenshotMediaEntity);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Pass("Step Passed", screenshotMediaEntity);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Pass("Step Passed", screenshotMediaEntity);
                else if (stepType == "And" || stepType == "But")
                    scenario.CreateNode<And>(stepInfo).Pass("Step Passed", screenshotMediaEntity);
            }
            else if (this._scenarioContext.TestError != null || status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string testError = this._scenarioContext.TestError.Message;
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                else if (stepType == "And" || stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
            }
            else if (resultOfImplementation == "StepDefinitionPending")
            {
                string errorMessage = "Step Definition is not implemented!";
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Skip(errorMessage);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Skip(errorMessage);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Skip(errorMessage);
                else if (stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Skip(errorMessage);
            }
        }

        /// <summary>
        /// Statements to be executed after scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            this._webDriverContext.driver.Dispose();
            Console.WriteLine("Scenario complete: " + this._scenarioContext.ScenarioInfo.Title);
        }

    }
}
