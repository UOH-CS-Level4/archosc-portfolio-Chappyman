using System.Collections.Generic;
using System.Linq;

static class ProcessData
{
    // Table 1 - used by long-term scheduling and context switching
    public static List<Process> Table1() => new List<Process>
    {
        new Process { Name="A", ArrivalTime=0,  ExecutionTime=3, Priority=5  },
        new Process { Name="B", ArrivalTime=2,  ExecutionTime=6, Priority=4  },
        new Process { Name="C", ArrivalTime=5,  ExecutionTime=5, Priority=8  },
        new Process { Name="D", ArrivalTime=6,  ExecutionTime=3, Priority=6  },
        new Process { Name="E", ArrivalTime=8,  ExecutionTime=6, Priority=10 },
        new Process { Name="F", ArrivalTime=9,  ExecutionTime=2, Priority=3  },
        new Process { Name="G", ArrivalTime=10, ExecutionTime=6, Priority=7  },
    };

    // Table 2 - used by FCFS and round robin
    public static List<Process> Table2() => new List<Process>
    {
        new Process { Name="A", ArrivalTime=0,  ExecutionTime=3 },
        new Process { Name="B", ArrivalTime=2,  ExecutionTime=6 },
        new Process { Name="C", ArrivalTime=5,  ExecutionTime=5 },
        new Process { Name="D", ArrivalTime=6,  ExecutionTime=3 },
        new Process { Name="E", ArrivalTime=8,  ExecutionTime=6 },
        new Process { Name="F", ArrivalTime=9,  ExecutionTime=2 },
        new Process { Name="G", ArrivalTime=10, ExecutionTime=6 },
    };

    // returns fresh copies so each algorithm starts clean
    public static List<Process> Copy(List<Process> src) =>
        src.Select(p => new Process
        {
            Name = p.Name,
            ArrivalTime = p.ArrivalTime,
            ExecutionTime = p.ExecutionTime,
            Priority = p.Priority,
            StartTime = -1,
            FinishTime = -1,
            Remaining = p.ExecutionTime,
        }).ToList();
}