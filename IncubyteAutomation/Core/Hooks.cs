// <copyright file="Hooks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Core
{
    using BoDi;
    using System;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Hooks class.
    /// </summary>
    [Binding]
    public class Hooks
    {
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
            Console.WriteLine("Before Scenario");

        }

        /// <summary>
        /// Insert Specflow Reporting Steps.
        /// </summary>
        [AfterStep]
        public void InsertReportingSteps()
        {
            Console.WriteLine("After each Scenario step");
        }

        /// <summary>
        /// Statements to be executed after scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            this._webDriverContext.Dispose();
            Console.WriteLine("After scenraio run");
        }
    }
}
