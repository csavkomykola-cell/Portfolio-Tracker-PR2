using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Portfolio_Tracker.Services
{
    public static class JsonService
    {
        public static void Save<T>(string path, T data)
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(data, options);

            File.WriteAllText(path, json);
        }

        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}