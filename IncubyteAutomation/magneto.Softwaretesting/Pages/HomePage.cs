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
    /// Initializes a new instance of the <see cref="NavHeader"/> class.
    /// </summary>
    /// <param name="driver">Driver.</param>
    /// <param name="scenarioContext">Scenario Context.</param>
    public HomePage(IWebDriver driver, ScenarioContext scenarioContext)
        : base(driver, scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }

    #region Elements

    private By _emailInputTextBox = By.XPath("//input[@id='initEmail']");
    private By _rememberMeCheckBox = By.XPath("//input[@name='remember-me-checkbox']");
    private By _nextButton = By.XPath("//button[text()='Next']");
    private By _dataAndConfigurationButton = By.XPath("//a[contains(@aria-label,'Data & Configuration')]");
    private By _dataMenuItem = By.XPath("//div[contains(text(),'Data') and @class='dropdown-item']");
    private By _surveyManagementlink = By.XPath("//a[contains(@aria-label,'Survey Management')]");
    private By _eulaCheckbox = By.XPath("//*[@id='acceptEula']/input[@id='chkAcceptEula']");
    private By _eulaAcceptButton = By.XPath("//*[@id='eulaForm']/button[contains(text(),'I Accept')]");
    private By _reportsMenuItem = By.XPath("//a[@aria-label='Reports']");
    private By _passwordTextBox = By.XPath("//input[@type='password']");
    private By _submitButton = By.XPath("//button[@type='submit']");
    private By _welcomeMessageText = By.XPath("//*[contains(text(),' Welcome, you have successfully logged in.')]");
    private By _notAuthorizedMessageText = By.XPath("//div[@class='container-fluid']//h1[text()='You are currently not authorized to access this page.']");
    private By _homeTab = By.XPath("//a[@aria-label='Home']");
    private By _rolesIcon = By.XPath("//img[@id='changeRole']");
    private By _rolePwCAdministratorMax = By.XPath("//*[text()='PwC Administrator Max']");
    private By _loginUserName = By.CssSelector(".sdk-avatar.sdk-avatar-sm.bg-primary >abbr");
    private By _dashboardMenuItem = By.CssSelector("a[aria-label='Dashboards'] >i");
    private By _engagementDashboard = By.CssSelector("a.dropdown-item[aria-label='Engagement Dashboard']");

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

    public bool VerifyUserRedirectedToHomePage(int waitInSeconds, string expectedUrl)
    {
        return WaitForRedirectionToUrl(expectedUrl, waitInSeconds);
    }
    #endregion
}