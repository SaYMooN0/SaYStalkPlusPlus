using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SaYStalkPlusPlus.src
{
    public static class UserStorage
    {
        private static readonly string filePath = Program.FileToSaveIds;
        static public List<long> IDs { get; private set; } = new List<long>();

        public static void Init()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string fileContent = File.ReadAllText(filePath);
                    IDs = JsonConvert.DeserializeObject<List<long>>(fileContent) ?? new List<long>();
                }
                catch { return; }
            }
        }

        public static void SaveToFile()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
                string json = JsonConvert.SerializeObject(IDs, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch { return; }
        }

        public static void SaveIfNeeded(long id)
        {
            if (!IDs.Contains(id))
            {
                IDs.Add(id);
                SaveToFile();
            }
        }

        private static string GetEncryptedString() { return ""; }
    }
}
