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
                ["x"] = selectedArea.X,
                ["y"] = selectedArea.Y,
                ["width"] = selectedArea.Width,
                ["height"] = selectedArea.Height
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
                (int)area["x"],
                (int)area["y"],
                (int)area["width"],
                (int)area["height"]
            );
        }
    }
}