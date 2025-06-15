// <copyright file="Hooks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using System;
    using System.IO;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Gherkin.Model;
    using AventStack.ExtentReports.Reporter;
    using BoDi;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using IncubyteAutomation.Core;
    using TechTalk.SpecFlow;
    using AventStack.ExtentReports.Reporter.Configuration;

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
            Console.WriteLine("Before test run");
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
            Console.WriteLine("After test run");
        }

        // Update the BeforeScenario method
        [BeforeScenario]
        public void BeforeScenario()
        {
            string downloadPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Downloads";
            this._objectContainer.RegisterInstanceAs(this._webDriverContext.driver, typeof(IWebDriver));

            // Ensure Reusable is defined and accessible
            Reusable.TestCaseName = this._scenarioContext.ScenarioInfo.Title; // Ensure Reusable is defined

            string folderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\TestResults\{this._scenarioContext.ScenarioInfo.Title}\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            string testReportPath = folderPath + "index.html";
            this._scenarioContext.Add("extentRepoPath", testReportPath);
            var htmlReporter = new ExtentHtmlReporter(this._scenarioContext.Get<string>("extentRepoPath"));
            extent = new ExtentReports();
            htmlReporter.Configuration().DocumentTitle = " Automation Report";
            htmlReporter.Configuration().ReportName = " Automation Report";
            htmlReporter.Configuration().Theme = Theme.Dark; // Use Theme directly if Configuration is not found
            extent.AttachReporter(htmlReporter);
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
            if (this._scenarioContext.TestError == null && resultOfImplementation == "OK")
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepInfo).Pass(resultOfImplementation);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(stepInfo).Pass(resultOfImplementation);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(stepInfo).Pass(resultOfImplementation);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(stepInfo).Pass(resultOfImplementation);
                }
                else if (stepType == "But")
                {
                    scenario.CreateNode<And>(stepInfo).Pass(resultOfImplementation);
                }
            }
            else if (this._scenarioContext.TestError != null || status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string testError = this._scenarioContext.TestError.Message;
                var screenshotMediaEntity = this._webDriverContext.CaptureScreenshotAndReturn(this._scenarioContext.ScenarioInfo.Title.Trim());

                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<Then>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                }
                else if (stepType == "But")
                {
                    scenario.CreateNode<Then>(stepInfo).Fail(testError + stackTrace, screenshotMediaEntity);
                }
            }
            else if (resultOfImplementation == "StepDefinitionPending")
            {
                string errorMessage = "Step Definition is not implemented!";
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepInfo).Skip(errorMessage);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(stepInfo).Skip(errorMessage);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(stepInfo).Skip(errorMessage);
                }
                else if (stepType == "But")
                {
                    scenario.CreateNode<Then>(stepInfo).Skip(errorMessage);
                }
            }
        }

        /// <summary>
        /// Statements to be executed after scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            this._webDriverContext.Dispose();
            //extent.Flush();
            //TestContext.AddTestAttachment(this._scenarioContext.Get<string>("extentRepoPath"));
            Console.WriteLine("After scenraio run");
        }
    }
}
