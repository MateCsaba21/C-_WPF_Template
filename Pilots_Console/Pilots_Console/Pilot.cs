using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pilots_Console
{
    public class Pilot
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string nation { get; set; }

        public static List<Pilot>? LoadFromJSON(string fileName)
        {
            string jsonStr = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Pilot>>(jsonStr);
        }
    }

}
