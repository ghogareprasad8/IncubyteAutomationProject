using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using IncubyteAutomation.Magneto_Softwaretesting.Pages;
using IncubyteAutomation.Core;

namespace IncubyteAutomation.StepDefinitions
{
    
    using NUnit.Framework;
    using OpenQA.Selenium;
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

        public SignUpLoginSteps(WebDriverContext webDriverContext, ScenarioContext scenarioContext)
        {
            _webDriverContext = webDriverContext;
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(_webDriverContext.driver, scenarioContext);
            _createAccountPage = new CreateAccountPage(_webDriverContext.driver, scenarioContext);
            _homePage = new HomePage(_webDriverContext.driver, scenarioContext);
            _myAccountPage = new MyAccountPage(_webDriverContext.driver, scenarioContext);
        }

        [Given(@"the user is on the e-commerce website")]
        public void GivenUserIsOnTheE_CommerceWebsite()
        {
            _loginPage.NavigateToLumaApplication();
        }

        [Given(@"the user navigates to the ""([^""]*)"" page")]
        public void GivenTheUserNavigatesToThePage(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"the user enters ""([^""]*)"" as ""([^""]*)""")]
        public void WhenTheUserEntersAs(string p0, string firstName)
        {
            throw new PendingStepException();
        }

        [When(@"the user accepts terms and conditions if present")]
        public void WhenTheUserAcceptsTermsAndConditionsIfPresent()
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the ""([^""]*)"" button")]
        public void WhenTheUserClicksTheButton(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should be navigated to ""([^""]*)"" page")]
        public void ThenTheUserShouldBeNavigatedToPage(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see message ""([^""]*)""")]
        public void ThenTheUserShouldSeeMessage(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should be logged in as ""([^""]*)""")]
        public void ThenTheUserShouldBeLoggedInAs(string firstName)
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks on the Welcome user icon")]
        public void WhenTheUserClicksOnTheWelcomeUserIcon()
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks on ""([^""]*)""")]
        public void WhenTheUserClicksOn(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should be logged out successfully")]
        public void ThenTheUserShouldBeLoggedOutSuccessfully()
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see error message ""([^""]*)""")]
        public void ThenTheUserShouldSeeErrorMessage(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see validation error for password mismatch")]
        public void ThenTheUserShouldSeeValidationErrorForPasswordMismatch()
        {
            throw new PendingStepException();
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
            throw new PendingStepException();
        }

        [Then(@"the page title should be ""([^""]*)""")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the page URL should contain ""([^""]*)""")]
        public void ThenThePageURLShouldContain(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see the following fields on Sign-Up page:")]
        public void ThenTheUserShouldSeeTheFollowingFieldsOnSign_UpPage(Table table)
        {
            throw new PendingStepException();
        }





    }
}