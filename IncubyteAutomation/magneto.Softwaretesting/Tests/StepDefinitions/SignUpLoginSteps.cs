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

        [Given(@"the user is on the registration page")]
        public void GivenTheUserIsOnTheRegistrationPage()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters valid details and submits the form")]
        public void WhenTheUserEntersValidDetailsAndSubmitsTheForm()
        {
            throw new PendingStepException();
        }

        [Then(@"the user should be redirected to the My Account page")]
        public void ThenTheUserShouldBeRedirectedToTheMyAccountPage()
        {
            throw new PendingStepException();
        }

        [Given(@"the user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters valid credentials and clicks Sign In")]
        public void WhenTheUserEntersValidCredentialsAndClicksSignIn()
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see the My Account page")]
        public void ThenTheUserShouldSeeTheMyAccountPage()
        {
            throw new PendingStepException();
        }




    }
}