namespace PlayWrightCSharpNUnitFramework.Config
{
    public class TestSettings
    {
        public bool Headless { get; set; }
        public bool DevTools { get; set; }
        public int SlowMo { get; set; } 
        public string[] Args { get; set; }
        public float TimeOut { get; set; }
        public DriverType DriverType { get; set; }
        public string? ApplicationUrl { get; set; }
    }

    public enum DriverType
    {
        Chromium,
        Firefox,
        Edge,
        Chrome
    }
}
