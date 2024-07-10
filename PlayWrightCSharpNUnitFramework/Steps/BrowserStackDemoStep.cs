using PlayWrightCSharpNUnitFramework.Pages;
using TechTalk.SpecFlow;

namespace PlayWrightCSharpNUnitFramework.Steps
{
    [Binding]
    public sealed class BrowserStackDemoStep
    {
        private readonly IBrowserStackDemoPage _browserStackDemoPage;
        public BrowserStackDemoStep(IBrowserStackDemoPage browserStackDemoPage)
        {
            _browserStackDemoPage = browserStackDemoPage;
        }

        [Given(@"I am on BrowserStack Demo Home page")]
        public async Task GivenIAmOnBrowserStackDemoHomePage()
        {
            Assert.That(await _browserStackDemoPage.VerifyOfferLabel());
        }

        [When(@"I click on Sign In button on Home page")]
        public async Task WhenIClickOnSignInButtonOnHomePage()
        {
            await _browserStackDemoPage.ClickOnSignInButton();
        }

        [When(@"I add UserName '([^']*)' on Sign in page")]
        public async Task WhenIAddUserNameOnSignInPage(string userName)
        {
            await _browserStackDemoPage.SetUserName(userName);
        }

        [When(@"I add Password '([^']*)' on Sign in page")]
        public async Task WhenIAddPasswordOnSignInPage(string password)
        {
            await _browserStackDemoPage.SetPassword(password);
        }

        [When(@"I click on Log in button on Sign in page")]
        public async Task WhenIClickOnLogInButtonOnSignInPage()
        {
            await _browserStackDemoPage.ClickOnLogInButton();
        }

        [Then(@"I verify I am on BrowserStack Dashboard page")]
        public async Task ThenIVerifyIAmOnBrowserStackDashboardPage()
        {
            Assert.That(await _browserStackDemoPage.VerifyBrowserStackDashboard());
        }
    }
}
