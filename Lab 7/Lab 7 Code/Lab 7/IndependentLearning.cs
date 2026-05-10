// Independent Learning - Priority Scheduling and Shortest Job Next
// Both are non-preemptive: once a process starts it runs to completion

using System;
using System.Collections.Generic;
using System.Linq;

class IndependentLearning
{
    // Priority Scheduling - at each step pick the highest priority process
    // that has arrived. Non-preemptive so no interruptions once started.
    public static void RunPriority(List<Process> source)
    {
        Console.WriteLine("Algorithm: Priority Scheduling (non-preemptive)\n");

        var procs = ProcessData.Copy(source);
        var remaining = new List<Process>(procs);
        var gantt = new List<GanttBlock>();
        int time = 0;

        while (remaining.Count > 0)
        {
            var available = remaining.Where(p => p.ArrivalTime <= time).ToList();

            if (available.Count == 0)
            {
                time = remaining.Min(p => p.ArrivalTime);
                continue;
            }

            var p = available
                .OrderByDescending(p => p.Priority)
                .ThenBy(p => p.ArrivalTime)
                .First();

            p.StartTime = time;
            p.FinishTime = time + p.ExecutionTime;
            gantt.Add(new GanttBlock { ProcessName = p.Name, Start = p.StartTime, End = p.FinishTime });

            time = p.FinishTime;
            remaining.Remove(p);
        }

        Output.MetricsTable(procs);
        Output.GanttChart(gantt, "Priority Scheduling");
        Output.Averages(procs);
    }

    // Shortest Job Next - pick whichever arrived process has the smallest burst.
    public static void RunSJN(List<Process> source)
    {
        Console.WriteLine("Algorithm: Shortest Job Next (SJN)\n");

        var procs = ProcessData.Copy(source);
        var remaining = new List<Process>(procs);
        var gantt = new List<GanttBlock>();
        int time = 0;

        while (remaining.Count > 0)
        {
            var available = remaining.Where(p => p.ArrivalTime <= time).ToList();

            if (available.Count == 0)
            {
                time = remaining.Min(p => p.ArrivalTime);
                continue;
            }

            var p = available
                .OrderBy(p => p.ExecutionTime)
                .ThenBy(p => p.ArrivalTime)
                .First();

            p.StartTime = time;
            p.FinishTime = time + p.ExecutionTime;
            gantt.Add(new GanttBlock { ProcessName = p.Name, Start = p.StartTime, End = p.FinishTime });

            time = p.FinishTime;
            remaining.Remove(p);
        }

        Output.MetricsTable(procs);
        Output.GanttChart(gantt, "Shortest Job Next (SJN)");
        Output.Averages(procs);
    }
}