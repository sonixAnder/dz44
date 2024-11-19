using System;
using System.Collections.Generic;

public interface IPart
{
    string Name { get; }
    bool IsBuilt { get; set; }
}

public class Basement : IPart
{
    public string Name => "Фундамент";
    public bool IsBuilt { get; set; } = false;
}

public class Wall : IPart
{
    public string Name => "Стена";
    public bool IsBuilt { get; set; } = false;
}

public class Door : IPart
{
    public string Name => "Дверь";
    public bool IsBuilt { get; set; } = false;
}

public class Window : IPart
{
    public string Name => "Окно";
    public bool IsBuilt { get; set; } = false;
}

public class Roof : IPart
{
    public string Name => "Крыша";
    public bool IsBuilt { get; set; } = false;
}

public interface IWorker
{
    void Work(House house);
}

public class House
{
    public List<IPart> Parts { get; } = new List<IPart>();

    public House()
    {
        Parts.Add(new Basement());
        for (int i = 0; i < 4; i++) Parts.Add(new Wall());
        Parts.Add(new Door());
        for (int i = 0; i < 4; i++) Parts.Add(new Window());
        Parts.Add(new Roof());
    }

    public bool IsComplete()
    {
        foreach (var part in Parts)
        {
            if (!part.IsBuilt) return false;
        }
        return true;
    }

    public void Display()
    {
        Console.WriteLine("  Дом построен!");
        Console.WriteLine("       /\\");
        Console.WriteLine("      /  \\");
        Console.WriteLine("     /____\\");
        Console.WriteLine("    |  []  |");
        Console.WriteLine("    |______|");
    }
}

public class Worker : IWorker
{
    public void Work(House house)
    {
        foreach (var part in house.Parts)
        {
            if (!part.IsBuilt)
            {
                part.IsBuilt = true;
                Console.WriteLine($"Строитель построил: {part.Name}");
                return;
            }
        }
    }
}

public class TeamLeader : IWorker
{
    public void Work(House house)
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine("Отчёт о ходе строительства:");
        foreach (var part in house.Parts)
        {
            Console.WriteLine($"{part.Name}: {(part.IsBuilt ? "Построено" : "Не построено")}");
        }
    }
}

public class Team
{
    private List<IWorker> workers = new List<IWorker>();

    public void AddWorker(IWorker worker)
    {
        workers.Add(worker);
    }

    public void BuildHouse(House house)
    {
        while (!house.IsComplete())
        {
            foreach (var worker in workers)
            {
                worker.Work(house);
                if (house.IsComplete()) break;
            }
        }

        Console.WriteLine("Строительство завершено!");
        house.Display();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        House house = new House();
        Team team = new Team();
        team.AddWorker(new Worker());
        team.AddWorker(new Worker());
        team.AddWorker(new TeamLeader());

        team.BuildHouse(house);
    }
}
