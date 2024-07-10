using Microsoft.Playwright;
using PlayWrightCSharpNUnitFramework.Config;

namespace PlayWrightCSharpNUnitFramework.Driver
{
    public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
    {
        public const float DEFAULT_TIMEOUT = 30f;


        // For Chrome Browser
        public async Task<IBrowser> GetChromeDriverAsync(TestSettings testsettings)
        {
            var options = GetParameters(testsettings.Args, testsettings.TimeOut, testsettings.Headless, testsettings.SlowMo);
            options.Channel = "chrome";
            return await GetBrowserAsync(DriverType.Chromium, options);
        }

        // For Firefox Browser
        public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testsettings)
        {
            var options = GetParameters(testsettings.Args, testsettings.TimeOut, testsettings.Headless, testsettings.SlowMo);
            options.Channel = "firefox";
            return await GetBrowserAsync(DriverType.Firefox, options);
        }

        // For Chromium Browser
        public async Task<IBrowser> GetChromiumDriverAsync(TestSettings testsettings)
        {
            var options = GetParameters(testsettings.Args, testsettings.TimeOut, testsettings.Headless, testsettings.SlowMo);
            options.Channel = "chromium";
            return await GetBrowserAsync(DriverType.Chromium, options);
        }

        // For Edge Browser
        public async Task<IBrowser> GetEdgeDriverAsync(TestSettings testsettings)
        {
            var options = GetParameters(testsettings.Args, testsettings.TimeOut, testsettings.Headless, testsettings.SlowMo);
            options.Channel = "msedge";
            return await GetBrowserAsync(DriverType.Chromium, options);
        }

        private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);
        }

        private BrowserTypeLaunchOptions GetParameters(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = false, float? slowmo = null)
        {
            return new BrowserTypeLaunchOptions
            {
                //Args = args,
                Args = new List<string> { "--start-maximized" }, // for maximize the window also made changes in PlaywrightDriver.cs -> in this method CreateBrowserContext 
                Timeout = ToMilliseconds(timeout),
                Headless = headless,
                SlowMo = slowmo
            };
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds = 1000;
        }
    }
}
