using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using IncubyteAutomation.Magneto_Softwaretesting.Pages;
using IncubyteAutomation.Core;

namespace IncubyteAutomation.StepDefinitions
{
    
    using NUnit.Framework;
    using OpenQA.Selenium;
    using System.Security.AccessControl;
    using System.Security.Cryptography.X509Certificates;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Log In Step Definition class.
    /// </summary>
    [Binding]
    public class SignUpLoginSteps
    {
        private readonly WebDriverContext _webDriverContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPage _loginPage;
        private readonly CreateAccountPage _createAccountPage;
        private readonly HomePage _homePage;
        private readonly MyAccountPage _myAccountPage;
        private readonly Reusable _reuable;

        public SignUpLoginSteps(WebDriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(_webDriverContext.driver, scenarioContext);
            _createAccountPage = new CreateAccountPage(_webDriverContext.driver, scenarioContext);
            _homePage = new HomePage(_webDriverContext.driver, scenarioContext);
            _myAccountPage = new MyAccountPage(_webDriverContext.driver, scenarioContext);
            _reuable = new Reusable(_webDriverContext.driver, scenarioContext);
        }

        [Given(@"the user is on the e-commerce website")]
        public void GivenUserIsOnTheE_CommerceWebsite()
        {
            _loginPage.NavigateToLumaApplication();
        }

        [Given(@"the user navigates to the ""([^""]*)"" page")]
        public void GivenTheUserNavigatesToThePage(string p0)
        {
            this._createAccountPage.ClickOnHeaderLinks(p0);
        }

        [When(@"the user enters ""([^""]*)"" as ""([^""]*)""")]
        public void WhenTheUserEntersAs(string feildTitle, string value)
        {
            this._createAccountPage.EnterValueInTheFeild(feildTitle , value);
        }

        [When(@"the user clicks the ""([^""]*)"" button")]
        public void WhenTheUserClicksTheButton(string p0)
        {
            this._createAccountPage.ClickOnTheButton(p0);
        }
        
        [Then(@"the user should be navigated to ""([^""]*)"" page")]
        public void ThenTheUserShouldBeNavigatedToPage(string p0)
        {
            this._createAccountPage.ValidateThatTheUserIsNavigatedToPage(p0);
        }

        [Then(@"the user should see message ""([^""]*)""")]
        public void ThenTheUserShouldSeeMessage(string p0)
        {
            this._myAccountPage.ValidateThatTheSuccessfulAlertMessageIsDisplayed(p0);
        }
        [Then(@"the user should be logged in as ""([^""]*)"" ""([^""]*)""")]
        public void ThenTheUserShouldBeLoggedInAs(string firstName, string lastName)
        {
            this._myAccountPage.ValidateTheloggedInUserName($"Welcome, {TestContext.Parameters[firstName]} {TestContext.Parameters[lastName]}!");
        }

        [When(@"the user clicks on the Welcome user icon")]
        public void WhenTheUserClicksOnTheWelcomeUserIcon()
        {
            this._myAccountPage.ClickOnTheWelcomeUserIcon();
        }

        [When(@"the user clicks on ""([^""]*)""")]
        public void WhenTheUserClicksOn(string p0)
        {
            this._myAccountPage.ClickOnTheCustomerMenuOption(p0);
        }

        [Then(@"the user should be logged out successfully")]
        public void ThenTheUserShouldBeLoggedOutSuccessfully()
        {
            throw new PendingStepException();
        }
        [Then(@"after (.*) seconds, the user should be redirected to the home page")]
        public void ThenAfterSecondsTheUserShouldBeRedirectedToTheHomePage(int p0)
        {
            bool redirected = this._homePage.VerifyUserRedirectedToHomePage(p0, TestContext.Parameters["loginURL"]);
            Assert.IsTrue(redirected, $"Expected to be redirected to {TestContext.Parameters["loginURL"]}, but was not."); ;
        }


        [Then(@"the user should see error message ""([^""]*)""")]
        public void ThenTheUserShouldSeeErrorMessage(string p0)
        {
            throw new PendingStepException();
        }
        [Then(@"the user should see validation error ""([^""]*)"" for ""([^""]*)"" feild")]
        public void ThenTheUserShouldSeeValidationErrorForFeild(string p0, string p1)
        {
            this._createAccountPage.VerifyFeildValidationMessage(p1, p0);
        }

        [When(@"the user enters ""([^""]*)"" in the email feild")]
        public void WhenTheUserEntersInTheEmailFeild(string value)
        {
            this._createAccountPage.EnterValueInTheEmailFeild("Email", value);
        }


        [Then(@"the user should see password strength error message")]
        public void ThenTheUserShouldSeePasswordStrengthErrorMessage()
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see validation errors for all required fields")]
        public void ThenTheUserShouldSeeValidationErrorsForAllRequiredFields()
        {
            throw new PendingStepException();
        }

        [Then(@"the password fields should be masked")]
        public void ThenThePasswordFieldsShouldBeMasked()
        {
            Assert.IsTrue(this._createAccountPage.IsPasswordFieldMasked(), "The password field is not masked.");
        }

        [Then(@"the page title should be ""([^""]*)""")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the page URL should contain ""([^""]*)""")]
        public void ThenThePageURLShouldContain(string expectedPartialUrl)
        {
            this._createAccountPage.VerifyUrlContains(expectedPartialUrl);
        }

        [Then(@"the user should see the following fields on Sign-Up page:")]
        public void ThenTheUserShouldSeeTheFollowingFieldsOnSign_UpPage(Table table)
        {
            foreach (var row in table.Rows)
            {
                string field = row[0];
                Assert.IsTrue(_createAccountPage.IsFieldOrButtonAvailable(field),
                    $"Element '{field}' is not visible on the Sign-Up page.");
            }
        }





    }
}