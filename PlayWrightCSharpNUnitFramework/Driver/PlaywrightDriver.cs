using Microsoft.Playwright;
using PlayWrightCSharpNUnitFramework.Config;
using System.Reflection;

namespace PlayWrightCSharpNUnitFramework.Driver
{
    public class PlaywrightDriver : IDisposable, IPlaywrightDriver
    {
        private readonly AsyncTask<IBrowser> browser; // This will help to use the Browser globally
        private readonly AsyncTask<IBrowserContext> browserContext; // This will help to use the BrowserContext globally
        private readonly TestSettings testSettings;
        private readonly IPlaywrightDriverInitializer playwrightDriverInitializer;
        private readonly AsyncTask<IPage> page;
        private bool isDisposed;

        public PlaywrightDriver(TestSettings _testSettings, IPlaywrightDriverInitializer _playwrightDriverInitializer)
        {
            testSettings = _testSettings;
            playwrightDriverInitializer = _playwrightDriverInitializer;
            browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
            browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
            page = new AsyncTask<IPage>(CreatPageAsync);
        }

        public Task<IPage> Page => page.Value;

        public Task<IBrowser> Browser => browser.Value;

        public Task<IBrowserContext> BrowserContext => browserContext.Value;

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return testSettings.DriverType switch
            {
                DriverType.Chromium => await playwrightDriverInitializer.GetChromiumDriverAsync(testSettings),
                DriverType.Chrome => await playwrightDriverInitializer.GetChromeDriverAsync(testSettings),
                DriverType.Firefox => await playwrightDriverInitializer.GetFirefoxDriverAsync(testSettings),
                DriverType.Edge => await playwrightDriverInitializer.GetEdgeDriverAsync(testSettings),
                _ => await playwrightDriverInitializer.GetChromiumDriverAsync(testSettings)
            };
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            // Without recording and Maximize Window
             //return await (await browser).NewContextAsync();

            // Maximize the window Size 
            return await (await browser).NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport, // For Maximize Window
            });

            // With Maximized Window and Video Recording
            /*return await (await browser).NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport, // For Maximize Window
                RecordVideoDir = "../../../TestResults/Videos/" // for Video Recording
                
            });*/

            // For recording videos without Maximize Window
            /*return await (await browser).NewContextAsync(new()
            {
                RecordVideoDir = "../../../TestResults/Videos/"
            });*/
        }

        private async Task<IPage> CreatPageAsync()
        {
            return await (await browserContext).NewPageAsync();
        }

        public async Task<string> TakeScreenshotAsPathAsync(string fileName)
        {
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";
            await Page.Result.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path
            });
            return path;
        }

        public void Dispose()
        {
            if (!isDisposed) return;
            {
                if (browser.IsValueCreated)
                {
                    Task.Run(async () =>
                    {
                        await (await browser).CloseAsync();
                        await (await browser).DisposeAsync();
                    });
                }
                isDisposed = true;
            }
        }
    }
}
