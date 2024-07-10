using Microsoft.Playwright;
using PlayWrightCSharpNUnitFramework.Driver;

namespace PlayWrightCSharpNUnitFramework.Pages
{
    public interface IBrowserStackDemoPage
    {
        Task ClickOnSignInButton();
        Task SetPassword(string password);
        Task SetUserName(string userName);
        Task<bool> VerifyOfferLabel();
        Task ClickOnLogInButton();
        Task<bool> VerifyBrowserStackDashboard();
    }

    public class BrowserStackDemoPage : IBrowserStackDemoPage
    {
        private readonly IPage page;

        public BrowserStackDemoPage(IPlaywrightDriver playwrightDriver)
        {
            page = playwrightDriver.Page.Result;
        }

        private ILocator lblOffers => page.Locator("//strong[text()='Offers']");
        private ILocator btnSignIn => page.Locator("//a[@id='signin']");
        private ILocator imgLogo => page.Locator("//div[starts-with(@class,'flex justify')]");
        private ILocator btnLogiIn => page.GetByText("Log In");
        private ILocator pnlShelfContainer => page.Locator("//div[@class='shelf-container']");

        public async Task<bool> VerifyOfferLabel()
        {
            return await lblOffers.IsVisibleAsync();
        }

        public async Task ClickOnSignInButton()
        {
            await btnSignIn.ClickAsync();
        }

        public async Task SetUserName(string userName)
        {
            await page.Locator("(//div[@class=' css-tlfecz-indicatorContainer'])[1]").ClickAsync();
            await page.Locator("//*[contains(text(),'" + userName + "')]").ClickAsync();
        }

        public async Task SetPassword(string password)
        {
            await imgLogo.ClickAsync();
            await page.Locator("(//div[@class=' css-tlfecz-indicatorContainer'])[2]").ClickAsync();
            await page.Locator("//*[contains(text(),'" + password + "')]").ClickAsync();
        }

        public async Task ClickOnLogInButton()
        {
            await btnLogiIn.ClickAsync();
        }

        public async Task<bool> VerifyBrowserStackDashboard()
        {
            return await pnlShelfContainer.IsVisibleAsync();
        }
    }
}
