using System.Text.Json;
using Counter.Models;

namespace Counter.Storage
{
    public static class CounterStorage
    {
        private static string _fileName = Path.Combine
            (FileSystem.AppDataDirectory, "counters.json");

        public static List<CounterItem> counters = new List<CounterItem>();

        static CounterStorage()
        {
            if (File.Exists(_fileName))
            {
                string json = File.ReadAllText(_fileName);

                counters = JsonSerializer.Deserialize<List<CounterItem>>(json);

                if (counters == null)
                {
                    counters = new List<CounterItem>();
                }
            }
        }

        public static void SaveCounters()
        {
            string json = JsonSerializer.Serialize(counters);
            File.WriteAllText(_fileName, json);
        }
    }
}