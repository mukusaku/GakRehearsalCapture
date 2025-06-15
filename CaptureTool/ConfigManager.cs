using Newtonsoft.Json.Linq;

namespace GakRehearsalCapture
{
    public static class ConfigManager
    {
        private static string configPath = "config.json";

        public static JObject LoadConfig()
        {
            if (File.Exists(configPath))
            {
                return JObject.Parse(File.ReadAllText(configPath));
            }
            return new JObject();
        }

        public static void SaveCaptureArea(Rectangle selectedArea)
        {
            JObject config = LoadConfig();

            config["captureArea"] = new JObject
            {
                ["X"] = selectedArea.X,
                ["Y"] = selectedArea.Y,
                ["Width"] = selectedArea.Width,
                ["Height"] = selectedArea.Height
            };

            File.WriteAllText(configPath, config.ToString());
        }
        public static Rectangle? GetCaptureArea()
        {
            JObject config = LoadConfig();
            if (config["captureArea"] == null)
            {
                return null;
            }

            var area = config["captureArea"];
            return new Rectangle(
                (int)area["X"],
                (int)area["Y"],
                (int)area["Width"],
                (int)area["Height"]
            );
        }
    }
}