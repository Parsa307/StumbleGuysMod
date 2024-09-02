namespace StumbleGuysMod
{
    public static class ConfigLoader
    {
        private static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Stumble Guys Mod", "config.txt");

        public static void SaveSettings(bool unlockAllCosmetics)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath) ?? throw new InvalidOperationException());

            var configLines = new[]
            {
                $"UnlockAllCosmetics={unlockAllCosmetics}",
            };

            File.WriteAllLines(ConfigPath, configLines);
        }

        public static ConfigData LoadSettings()
        {
            if (!File.Exists(ConfigPath))
            {
                SaveSettings(false); // Save default settings if the file does not exist
                return new ConfigData();
            }

            var configData = new ConfigData();

            var configLines = File.ReadAllLines(ConfigPath);
            foreach (var line in configLines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    switch (parts[0])
                    {
                        case "UnlockAllCosmetics":
                            configData.UnlockAllCosmetics = bool.Parse(parts[1]);
                            break;
                    }
                }
            }

            return configData;
        }
    }

    public class ConfigData
    {
        public bool UnlockAllCosmetics = false;
    }
}
