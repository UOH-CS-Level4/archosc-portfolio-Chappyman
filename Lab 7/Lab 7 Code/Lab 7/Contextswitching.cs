// Section 3 - Context Switching: Priority-based Round-Robin
// Same as Round-Robin but the ready list is re-sorted by priority
// before each scheduling decision (higher priority value = runs first)

using System;
using System.Collections.Generic;
using System.Linq;

class ContextSwitching
{
    public static void Run(List<Process> source, int tq)
    {
        Console.WriteLine($"Algorithm: Priority Round-Robin  |  TQ = {tq}\n");

        var procs = ProcessData.Copy(source);
        var readyList = new List<Process>();
        var added = new HashSet<string>();
        var gantt = new List<GanttBlock>();
        int time = 0;
        int done = 0;

        void Enqueue()
        {
            foreach (var p in procs.OrderBy(p => p.ArrivalTime).ThenBy(p => p.Name))
                if (p.ArrivalTime <= time && p.Remaining > 0 && !added.Contains(p.Name))
                {
                    readyList.Add(p);
                    added.Add(p.Name);
                }
        }

        Enqueue();

        while (done < procs.Count)
        {
            if (readyList.Count == 0)
            {
                time++;
                Enqueue();
                continue;
            }

            // pick highest priority each time (ties go to earlier arrival)
            readyList.Sort((a, b) =>
                b.Priority != a.Priority
                    ? b.Priority.CompareTo(a.Priority)
                    : a.ArrivalTime.CompareTo(b.ArrivalTime));

            var p = readyList[0];
            readyList.RemoveAt(0);

            if (p.StartTime == -1) p.StartTime = time;

            int slice = Math.Min(tq, p.Remaining);
            gantt.Add(new GanttBlock { ProcessName = p.Name, Start = time, End = time + slice });

            time += slice;
            p.Remaining -= slice;

            Enqueue();

            if (p.Remaining == 0)
            {
                p.FinishTime = time;
                done++;
            }
            else
            {
                readyList.Add(p);
            }
        }

        Output.MetricsTable(procs);
        Output.GanttChart(gantt, $"Priority Round-Robin TQ={tq}");
        Output.Averages(procs);
    }
}