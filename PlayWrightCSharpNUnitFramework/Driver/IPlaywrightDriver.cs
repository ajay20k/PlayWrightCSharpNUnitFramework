using Microsoft.Playwright;

namespace PlayWrightCSharpNUnitFramework.Driver
{
    public interface IPlaywrightDriver
    {
        Task<IBrowser> Browser { get; }
        Task<IBrowserContext> BrowserContext { get; }
        Task<IPage> Page { get; }
        Task<string> TakeScreenshotAsPathAsync(string fileName);
    }
}