using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Models
{
    class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; set; } = new ObservableCollection<Counter>();

        public string AddButtonText => "Add";

        public AllCounters()
        {
            LoadCounters();
        }

        public void LoadCounters()
        {
            Counters.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Counter> counters = Directory.EnumerateFiles(appDataPath, "*.counters.txt").Select(filename => 
            {
                string[] data = File.ReadAllLines(filename);

                return new Counter()
                {
                    Filename = filename,
                    Name = data[0],
                    Value = int.Parse(data[1])
                };

            });

            foreach (Counter counter in counters)
            {
                Counters.Add(counter);
            }
        }
    }
}
