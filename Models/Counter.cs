using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Models
{
    class Counter
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public string SaveButtonText => "Save";
        public string DeleteButtonText => "Delete";
    }
}
