// <copyright file="NavHeader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IncubyteAutomation.Magneto_Softwaretesting.Pages;

using IncubyteAutomation.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

/// <summary>
/// NavHeader class.
/// </summary>
public class CreateAccountPage : Reusable
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
    public CreateAccountPage(IWebDriver driver, ScenarioContext scenarioContext)
        : base(driver, scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }

    #region Elements

    private By _pageTitle = By.XPath("//span[@data-ui-id='page-title-wrapper']");
    private By _loginUserName = By.CssSelector(".sdk-avatar.sdk-avatar-sm.bg-primary >abbr");
    private By _dashboardMenuItem = By.CssSelector("a[aria-label='Dashboards'] >i");
    private By _engagementDashboard = By.CssSelector("a.dropdown-item[aria-label='Engagement Dashboard']");
    private By _passwordField = By.Id("password"); // Replace with actual locator
    private By inputFeildByTitle(string inputTitle)
    {
        return By.XPath($"//input[@title='{inputTitle}'] | //button[@title='{inputTitle}']");
    }

    private By buttonWithText(string buttonText)
    {
        return By.XPath($"//span[text()='{buttonText}']/parent::button");
    }
    private By headerLinkText(string linkText)
    {
        return By.XPath($"//div[@class='panel header']//a[normalize-space(text())='{linkText}']");
    }
    private By feildValidationMessage(string validationMessageFeildName)
    {
        return By.XPath($"//div[@class='control']//input[@title='{validationMessageFeildName}']//following-sibling::div");
    }

    #endregion

    #region Navigation Methods
    public void ClickOnHeaderLinks(string linkText)
    {
        this.ElementOperations("click", this.headerLinkText(linkText), "", $"{linkText} is clicked");
    }

    /// <summary>
    /// To validate current page title of the application.
    /// </summary>
    /// <param name="pageTitle">Title of currect page</param>
    public void ValidateThatTheUserIsNavigatedToPage(string pageTitle)
    {
        this.ElementOperations("verify", this._pageTitle, pageTitle, $"{pageTitle} is displayed");
       Assert.IsTrue(this.ValidateActualAndExpectedValues(this.GetElementText(_pageTitle),pageTitle));
    }

    public void EnterValueInTheEmailFeild(string feildTitle ,string value)
    {
        this.ElementOperations("sendKeys", this.inputFeildByTitle(feildTitle), value, $"{value} in feild {feildTitle} is entered");
    }

    public void EnterValueInTheFeild(string fieldTitle, string valueKey)
    {
        string inputValue;

        if (valueKey.Equals("uniqueEmail", StringComparison.OrdinalIgnoreCase))
        {
            if (!ScenarioContext.Current.ContainsKey("GeneratedEmail"))
            {
                string uniqueEmail = $"test_{this.RandomNumber(3)}@test.com";
                ScenarioContext.Current["GeneratedEmail"] = uniqueEmail;

                // Optionally store in FeatureContext if you want to share across scenarios
                FeatureContext.Current["CreatedEmail"] = uniqueEmail;
            }

            inputValue = ScenarioContext.Current["GeneratedEmail"].ToString();
        }
        else if (valueKey.Equals("existingEmail", StringComparison.OrdinalIgnoreCase))
        {
            if (!FeatureContext.Current.ContainsKey("CreatedEmail"))
                throw new Exception("No created email found in FeatureContext. Make sure TC001 is run before TC002.");

            inputValue = FeatureContext.Current["CreatedEmail"].ToString();
        }
        else
        {
            inputValue = TestContext.Parameters[valueKey];
        }

        this.ElementOperations("sendKeys", this.inputFeildByTitle(fieldTitle), inputValue,
            $"{inputValue} in field {fieldTitle} is entered");
    }

    public void ClickOnTheButton(string buttonName)
    { 
        this.ElementOperations("click", this.buttonWithText(buttonName),"", $"{buttonName} button is clicked");
    }

    public void VerifyFeildValidationMessage(string feildName, string validationMessage)
    {
        this.ElementOperations("verify", this.feildValidationMessage(feildName), validationMessage, $"{validationMessage} is displayed for {feildName} field");
    }

    public bool IsFieldOrButtonAvailable(string fieldTitle)
    {
        try
        {
            if (ElementAvailability(inputFeildByTitle(fieldTitle)))
                return true;

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error finding field or button '{fieldTitle}': {ex.Message}");
            return false;
        }
    }

    public void VerifyUrlContains(string expectedPartialUrl) {
        this.VerifyPartialUrl(expectedPartialUrl);
       
    }

    public bool IsPasswordFieldMasked()
    {
        return this.WaitAndFindWebElement(_passwordField).GetAttribute("type") == "password";
    }
    #endregion
}