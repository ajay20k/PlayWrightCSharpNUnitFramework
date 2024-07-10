using Microsoft.Extensions.DependencyInjection;
using PlayWrightCSharpNUnitFramework.Config;
using PlayWrightCSharpNUnitFramework.Driver;
using PlayWrightCSharpNUnitFramework.Pages;
using SolidToken.SpecFlow.DependencyInjection;

namespace PlayWrightCSharpNUnitFramework
{
    public class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
                .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
                .AddScoped<IBrowserStackDemoPage, BrowserStackDemoPage>();
            return services;
        }
    }
}
