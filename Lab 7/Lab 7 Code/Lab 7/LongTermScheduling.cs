// Section 1 - Long-Term Scheduling
// Simulates a job queue and admits jobs based on priority threshold

using System;
using System.Collections.Generic;
using System.Linq;

class LongTermScheduling
{
    public static void Run(List<Process> jobs, int threshold)
    {
        Console.WriteLine($"Admitting jobs with Priority > {threshold}\n");

        var admitted = jobs
            .Where(j => j.Priority > threshold)
            .OrderBy(j => j.ArrivalTime)
            .ToList();

        var rejected = jobs
            .Where(j => j.Priority <= threshold)
            .ToList();

        Console.WriteLine("  ADMITTED:");
        foreach (var j in admitted)
            Console.WriteLine($"    Job {j.Name} | Priority {j.Priority,2} | Arrival {j.ArrivalTime} | Exec {j.ExecutionTime}");

        Console.WriteLine("\n  REJECTED:");
        foreach (var j in rejected)
            Console.WriteLine($"    Job {j.Name} | Priority {j.Priority,2}");

        Console.WriteLine();
    }
}