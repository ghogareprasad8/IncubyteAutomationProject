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
public class MyAccountPage : Reusable
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
    public MyAccountPage(IWebDriver driver, ScenarioContext scenarioContext)
        : base(driver, scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }

    #region Elements

    private By _successfulAlertMessage = By.XPath("(//div[@role='alert']//div)[2]");
    private By _loggedInUserName = By.XPath("(//span[@class='logged-in' and contains(@data-bind,'Welcome')])[1]");
    private By _welcomeUserDropdownIcon = By.XPath("//button[@class='action switch']");

    private By customerMenuDropdown(string menuOption)
    {
        return By.XPath($"(//div[@class='customer-menu']//a[normalize-space(text())='{menuOption}'])[1]");
    }
    

    #endregion

    #region Navigation Methods
    public void ValidateThatTheSuccessfulAlertMessageIsDisplayed(string expectedMessage)
    {
        var actualMessage = this.GetElementText(_successfulAlertMessage);
        Assert.That(this.ValidateActualAndExpectedValues(actualMessage,expectedMessage), $"Expected message '{expectedMessage}' but got '{actualMessage}'");
    }

    public void ValidateTheloggedInUserName(string expectedUserName)
    {
        var actualMessage = this.GetElementText(_loggedInUserName);
        Assert.That(this.ValidateActualAndExpectedValues(actualMessage, expectedUserName), $"Expected message '{expectedUserName}' but got '{actualMessage}'");
    }

    public void ClickOnTheWelcomeUserIcon()
    {
        this.ElementOperations("click",_welcomeUserDropdownIcon,"", "Welcome User Dropdown Icon is clicked");
    }

    public void ClickOnTheCustomerMenuOption(string menuOption)
    {
        var menuItem = this.customerMenuDropdown(menuOption);
        this.ElementOperations("click", menuItem, "", $"{menuOption} is clicked from the customer menu");
    }

    #endregion
}