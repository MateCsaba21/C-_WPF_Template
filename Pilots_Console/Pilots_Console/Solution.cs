using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pilots_Console
{
    public static class Solution
    {
        public static List<Pilot>? Pilots { get; set; } = Pilot.LoadFromJSON("pilots.json");

        public static List<string> SearchPilots(string Name)
        {
            List<string> searchedPilots = new List<string>();
            foreach (var pilot in Pilots)
            {
                string[] nameSplit = pilot.name.Split(' ');
                for (int i = 0; i < nameSplit.Length; i++)
                {
                    if (nameSplit[i].ToLower().StartsWith(Name.ToLower()))
                    {
                        searchedPilots.Add($"\t{pilot.id}\t{pilot.name}\t{(pilot.birthdate != null ? pilot.birthdate.Replace("-", ". ") : "")}\t{pilot.gender}\t{pilot.nation}");
                        break;
                    }
                }
            }
            return searchedPilots;
        }

        public static string FileWrite()
        {
            List<string> nations = Pilots.Select(x => x.nation).Order().ToList();
            string file = "";
            foreach (var nation in nations)
            {
                int nationCount = Pilots.Count(x => x.nation == nation);
                file += $"{nation} ({nationCount}) fő:\n";
                foreach (var pilot in Pilots)
                {
                    if (pilot.nation == nation)
                    {
                        file += $"\t{pilot.name} - {pilot.birthdate}\n";
                    }
                }
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter("pilots2.txt", false);
                streamWriter.Write(file);
                streamWriter.Close();
                return "dwadwa";
            }
            catch (Exception)
            {
                return "Hiba";
            }
        }

    }
}
