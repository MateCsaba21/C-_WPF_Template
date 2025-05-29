using Pilots_Console;

Console.Write("5. feladat: Adja meg egy név részletét: ");
string name = Console.ReadLine();
List<string> searchedPilots = Solution.SearchPilots(name);
if (searchedPilots.Count != 0)
{
    foreach (var pilot in searchedPilots)
    {
        Console.WriteLine(pilot);
    }
}
else
{
    Console.WriteLine($"Nem található pilóta a(z) {name} névrészlettel!");
}

Console.WriteLine($"6. feladat: {Solution.FileWrite()}");
