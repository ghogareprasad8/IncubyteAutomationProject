using System;
using System.IO;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace IncubyteAutomation.Core
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly WebDriverContext _webDriverContext;
        private int stepCounter =1;

        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext, WebDriverContext webDriverContext)
        {
            this._objectContainer = objectContainer;
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._webDriverContext = webDriverContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Before test run");
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("Before feature run");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("After feature run");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("After test run");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
            this._objectContainer.RegisterInstanceAs(this._webDriverContext.driver, typeof(IWebDriver));

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            Reusable.TestCaseName = this._scenarioContext.ScenarioInfo.Title;
        }

        [AfterStep]
        [AfterStep]
        public void AfterEachStep()
        {
            this._webDriverContext.CaptureScreenshot(_scenarioContext.ScenarioInfo.Title, stepCounter);
            stepCounter++;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            this._webDriverContext.Dispose();
            Console.WriteLine("After scenario run");
        }
    }
}
