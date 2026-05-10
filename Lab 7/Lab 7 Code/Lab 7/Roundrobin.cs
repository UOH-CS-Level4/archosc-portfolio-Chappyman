// Section 2b - Round-Robin Scheduling
// Preemptive: each process gets a fixed time slice (TQ).
// If a process isn't finished it goes back to the end of the queue.

using System;
using System.Collections.Generic;
using System.Linq;

class RoundRobin
{
    public static void Run(List<Process> source, int tq)
    {
        Console.WriteLine($"Algorithm: Round-Robin  |  TQ = {tq}\n");

        var procs = ProcessData.Copy(source);
        var queue = new Queue<Process>();
        var added = new HashSet<string>();
        var gantt = new List<GanttBlock>();
        int time = 0;
        int done = 0;

        // add any process that has arrived by the current time
        void Enqueue()
        {
            foreach (var p in procs.OrderBy(p => p.ArrivalTime).ThenBy(p => p.Name))
                if (p.ArrivalTime <= time && p.Remaining > 0 && !added.Contains(p.Name))
                {
                    queue.Enqueue(p);
                    added.Add(p.Name);
                }
        }

        Enqueue();

        while (done < procs.Count)
        {
            if (queue.Count == 0)
            {
                time++;
                Enqueue();
                continue;
            }

            var p = queue.Dequeue();
            if (p.StartTime == -1) p.StartTime = time;

            int slice = Math.Min(tq, p.Remaining);
            gantt.Add(new GanttBlock { ProcessName = p.Name, Start = time, End = time + slice });

            time += slice;
            p.Remaining -= slice;

            Enqueue(); // let new arrivals join before re-queuing current

            if (p.Remaining == 0)
            {
                p.FinishTime = time;
                done++;
            }
            else
            {
                queue.Enqueue(p); // not done yet, goes back to end of queue
            }
        }

        Output.MetricsTable(procs);
        Output.GanttChart(gantt, $"Round-Robin TQ={tq}");
        Output.Averages(procs);
    }
}