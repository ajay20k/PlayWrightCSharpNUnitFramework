using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlayWrightCSharpNUnitFramework.Config
{
    public static class ConfigReader
    {
        public static TestSettings ReadConfig()
        {
            var configFile = File.ReadAllText("../../../appSettings.json");
            var jsonSerializerSettings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            jsonSerializerSettings.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializerSettings);
        }

        public static string getAppConfig(string parameter)
        {
            return JObject.Parse(File.ReadAllText("../../../AppConfig.json").ToString()).GetValue(parameter)!.ToString();
        }
    }
}
