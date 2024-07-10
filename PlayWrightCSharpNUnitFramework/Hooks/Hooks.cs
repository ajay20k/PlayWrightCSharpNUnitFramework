using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using PlayWrightCSharpNUnitFramework.Config;
using PlayWrightCSharpNUnitFramework.Driver;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace PlayWrightCSharpNUnitFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;
        private Task<IPage> _page;
        private static ExtentReports? _extentReports;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private ExtentTest _scenario;

        private static ExtentTest _feature;
        private static readonly Dictionary<string, ExtentTest> _featureTests = new();

        // For API
        private readonly AsyncTask<IAPIRequestContext> apiRequestContext;

        public Hooks(IPlaywrightDriver playwrightDriver, TestSettings testSettings, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _page = playwrightDriver.Page;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeExtentReports()
        {
            _extentReports = new ExtentReports();
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../TestResults/TestReports.html");
            var spark = new ExtentSparkReporter(reportPath);
            spark.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            _extentReports.AttachReporter(spark);
        }

        [BeforeScenario("@WEB")]
        public async Task BeforeScenario()
        {
            await (await _page).GotoAsync(_testSettings.ApplicationUrl);

            var featureTitle = _featureContext.FeatureInfo.Title;
            if (!_featureTests.ContainsKey(featureTitle))
            {
                _feature = _extentReports.CreateTest<Feature>(featureTitle);
                _featureTests.Add(featureTitle, _feature);
            }
            else
            {
                _feature = _featureTests[featureTitle];
            }

            _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep("@WEB")]
        public async Task AfterStep()
        {
            var fileName = $"{_featureContext.FeatureInfo.Title.Trim()}_{Regex.Replace(_scenarioContext.ScenarioInfo.Title, @"\s", string.Empty)}";

            if (_scenarioContext.TestError == null)
            {
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                var screenshotPath = await _playwrightDriver.TakeScreenshotAsPathAsync(fileName);
                var errorMessage = _scenarioContext.TestError.Message;
                //var screenCapture = new ScreenCapture { Title = "Error Screenshot", Path = screenshotPath };
                var screenCapture = new ScreenCapture { Path = screenshotPath };

                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(errorMessage, screenCapture);
                        break;
                    case StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(errorMessage, screenCapture);
                        break;
                    case StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(errorMessage, screenCapture);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [AfterTestRun]
        public static void TearDownReport() => _extentReports?.Flush();
    }
}
