// <copyright file="NavHeader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Magneto_Softwaretesting.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using IncubyteAutomation.Core;
using TechTalk.SpecFlow;

/// <summary>
/// NavHeader class.
/// </summary>
public class HomePage : Reusable
{
    /// <summary>
    /// Gets or sets test context object.
    /// </summary>
    public TestContext TestContext { get; set; }

    /// <summary>
    /// Scenario Context object.
    /// </summary>
    public ScenarioContext _scenarioContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Reusable"/> class.
    /// </summary>
    /// <param name="driver">Driver.</param>
    /// <param name="scenarioContext">Scenario Context.</param>
    public HomePage(IWebDriver driver, ScenarioContext scenarioContext)
        : base(driver, scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }

    #region Elements

    #endregion

    #region Navigation Methods

    public bool VerifyUserRedirectedToHomePage(int waitInSeconds, string expectedUrl)
    {
        return WaitForRedirectionToUrl(expectedUrl, waitInSeconds);
    }
    #endregion
}