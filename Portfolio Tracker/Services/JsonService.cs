using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace Portfolio_Tracker.Services
{
    public static class JsonService
    {
        // Абсолютний шлях до директорії Data поруч із виконуваним файлом
        public static readonly string DataDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Стандартні шляхи до файлів
        public static string AssetsPath => Path.Combine(DataDirectory, "assets.json");
        public static string TransactionsPath => Path.Combine(DataDirectory, "transactions.json");
        public static string PortfolioPath => Path.Combine(DataDirectory, "portfolio.json");
        public static string UsersPath => Path.Combine(DataDirectory, "users.json");

        private static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private static string ResolvePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            // Якщо шлях уже абсолютний, повернути як є
            if (Path.IsPathRooted(path))
                return path;

            // Якщо шлях починається з "Data", вважати його відносним до базової директорії додатка
            if (path.StartsWith("Data" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("Data/", StringComparison.OrdinalIgnoreCase))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            // За замовчуванням: вважати як ім'я файлу всередині DataDirectory
            return Path.Combine(DataDirectory, path);
        }

        public static void Save<T>(string path, T data)
        {
            string fullPath = ResolvePath(path);
            string directory = Path.GetDirectoryName(fullPath) ?? DataDirectory;

            try
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string json = JsonSerializer.Serialize(data, DefaultOptions);

                // Безпечне записування: спочатку записати у тимчасовий файл, потім замінити
                string tmpPath = fullPath + ".tmp";
                File.WriteAllText(tmpPath, json);
                File.Copy(tmpPath, fullPath, true);
                File.Delete(tmpPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"JsonService.Save failed for '{fullPath}': {ex}");
                throw;
            }
        }

        public static T Load<T>(string path)
        {
            string fullPath = ResolvePath(path);

            try
            {
                if (!File.Exists(fullPath))
                    return default;

                string json = File.ReadAllText(fullPath);
                if (string.IsNullOrWhiteSpace(json))
                    return default;

                return JsonSerializer.Deserialize<T>(json, DefaultOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"JsonService.Load failed for '{fullPath}': {ex}");
                return default;
            }
        }

        // Зручні методи для стандартних файлів
        public static void SaveAssets<T>(T data) => Save(AssetsPath, data);
        public static T LoadAssets<T>() => Load<T>(AssetsPath);
        public static void SaveTransactions<T>(T data) => Save(TransactionsPath, data);
        public static T LoadTransactions<T>() => Load<T>(TransactionsPath);
        public static void SavePortfolio<T>(T data) => Save(PortfolioPath, data);
        public static T LoadPortfolio<T>() => Load<T>(PortfolioPath);
        public static void SaveUsers<T>(T data) => Save(UsersPath, data);
        public static T LoadUsers<T>() => Load<T>(UsersPath);
    }
}