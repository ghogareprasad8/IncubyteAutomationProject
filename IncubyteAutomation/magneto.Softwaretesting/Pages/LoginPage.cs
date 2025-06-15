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
public class LoginPage : Reusable
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
    /// Initializes a new instance of the <see cref="NavHeader"/> class.
    /// </summary>
    /// <param name="driver">Driver.</param>
    /// <param name="scenarioContext">Scenario Context.</param>
    public LoginPage(IWebDriver driver, ScenarioContext scenarioContext)
        : base(driver, scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }

    #region Elements

    private By _homePageText = By.XPath("//span[@data-ui-id='page-title-wrapper']");
   

    private By AllSurveysValueSelectionlink(string value)
    {
        return By.XPath($"//div[@aria-expanded='true']//a[contains(text(),'{value}')]");
    }

    private By SelectTheEngagement(string engagementName)
    {
        return By.XPath($"(//h3[a[text()='{engagementName}']]/a)[1]");
    }

    private By TxtDataSelection(string value)
    {
        return By.XPath($"//a[contains(text(),'{value}')]");
    }

    private By ExportableReportsValueSelectionlink(string value)
    {
        return By.XPath($"//div[@aria-expanded='true']//a[contains(text(),'{value}')]");
    }

    #endregion

    #region Navigation Methods

    /// <summary>
    /// To Log into Luma application.
    /// </summary>
    public void NavigateToLumaApplication()
    {
        this.LaunchURL(TestContext.Parameters["loginURL"]);
        this.ElementAvailability(this._homePageText);
        this.ElementOperations("verify", _homePageText, "Home Page", $"{_homePageText} is displayed");

    }


    /// <summary>
    /// To Add loggedIn user name and record.
    /// </summary>
    public void AddLoginUserName()
    {
        //this._scenarioContext.Add("loginUserName", this.ReturnAttributeValue(this._loginUserName, "title"));
    }

    /// <summary>
    /// To Get loggedIn user name and record.
    /// </summary>
    /// <returns>Returns Logged in User name.</returns>
    public string GetLoginUserName()
    {
        return this._scenarioContext.Get<string>("loginUserName");
    }

    #endregion
}