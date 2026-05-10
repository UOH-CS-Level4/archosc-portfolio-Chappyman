// Section 2a - First-Come First-Served (FCFS)
// Non-preemptive - processes run in arrival order, each to completion

using System;
using System.Collections.Generic;
using System.Linq;

class FCFS
{
    public static void Run(List<Process> source)
    {
        Console.WriteLine("Algorithm: First-Come First-Served\n");

        var procs = ProcessData.Copy(source);
        procs = procs.OrderBy(p => p.ArrivalTime).ThenBy(p => p.Name).ToList();

        int time = 0;
        var gantt = new List<GanttBlock>();

        foreach (var p in procs)
        {
            if (time < p.ArrivalTime)
                time = p.ArrivalTime;

            p.StartTime = time;
            p.FinishTime = time + p.ExecutionTime;
            gantt.Add(new GanttBlock { ProcessName = p.Name, Start = p.StartTime, End = p.FinishTime });
            time = p.FinishTime;
        }

        Output.MetricsTable(procs);
        Output.GanttChart(gantt, "FCFS");
        Output.Averages(procs);
    }
}