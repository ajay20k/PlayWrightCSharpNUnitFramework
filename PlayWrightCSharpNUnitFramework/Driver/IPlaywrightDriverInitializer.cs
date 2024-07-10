using Microsoft.Playwright;
using PlayWrightCSharpNUnitFramework.Config;

namespace PlayWrightCSharpNUnitFramework.Driver
{
    public interface IPlaywrightDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(TestSettings testsettings);
        Task<IBrowser> GetChromiumDriverAsync(TestSettings testsettings);
        Task<IBrowser> GetFirefoxDriverAsync(TestSettings testsettings);
        Task<IBrowser> GetEdgeDriverAsync(TestSettings testsettings);
    }
}