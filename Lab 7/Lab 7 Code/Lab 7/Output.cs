using System;
using System.Collections.Generic;
using System.Linq;

static class Output
{
    public static void Header(string title)
    {
        Console.WriteLine();
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine();
    }

    public static void MetricsTable(List<Process> procs)
    {
        Console.WriteLine($"  {"Process",-10}{"Arrival",-10}{"Exec",-8}{"Start",-8}{"Finish",-10}{"TAT",-8}{"Norm TAT",-10}");
        Console.WriteLine("  " + new string('─', 62));

        foreach (var p in procs.OrderBy(p => p.Name))
        {
            Console.WriteLine(
                $"  {p.Name,-10}{p.ArrivalTime,-10}{p.ExecutionTime,-8}" +
                $"{p.StartTime,-8}{p.FinishTime,-10}" +
                $"{p.TurnaroundTime,-8}{p.NormalisedTAT,-10:F2}"
            );
        }
        Console.WriteLine();
    }

    public static void Averages(List<Process> procs)
    {
        double avgTAT = procs.Average(p => p.TurnaroundTime);
        double avgNTAT = procs.Average(p => p.NormalisedTAT);
        Console.WriteLine($"  Avg Turnaround Time  : {avgTAT:F2}");
        Console.WriteLine($"  Avg Normalised TAT   : {avgNTAT:F2}");
        Console.WriteLine();
    }

    public static void GanttChart(List<GanttBlock> blocks, string title)
    {
        Console.WriteLine($"  Gantt Chart — {title}");

        var names = blocks.Select(b => b.ProcessName).Distinct().OrderBy(n => n).ToList();
        int maxTime = blocks.Max(b => b.End);

        // tick marks along the top
        Console.Write("  " + new string(' ', 4));
        for (int t = 0; t <= maxTime; t++)
            Console.Write(t % 5 == 0 ? "|" : "-");
        Console.WriteLine();

        Console.Write("  " + new string(' ', 4));
        for (int t = 0; t <= maxTime; t++)
            if (t % 5 == 0) Console.Write(t.ToString().PadRight(5));
        Console.WriteLine();

        foreach (string name in names)
        {
            Console.Write($"  {name,2} |");
            for (int t = 0; t < maxTime; t++)
            {
                bool running = blocks.Any(b => b.ProcessName == name && b.Start <= t && b.End > t);
                Console.Write(running ? "█" : " ");
            }
            Console.WriteLine("|");
        }

        Console.Write("  " + new string(' ', 4));
        for (int t = 0; t <= maxTime; t++)
            Console.Write(t % 5 == 0 ? "|" : "-");
        Console.WriteLine("\n");
    }
}