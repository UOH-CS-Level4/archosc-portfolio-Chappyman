// Data model shared across all scheduling algorithms

class Process
{
    public string Name { get; set; }
    public int ArrivalTime { get; set; }
    public int ExecutionTime { get; set; }
    public int Priority { get; set; }

    public int StartTime { get; set; } = -1;
    public int FinishTime { get; set; } = -1;
    public int Remaining { get; set; }  // tracks leftover burst for RR

    // turnaround = how long from arrival to completion
    public int TurnaroundTime => FinishTime - ArrivalTime;
    // normalised TAT shows how fairly the process was served
    public double NormalisedTAT => (double)TurnaroundTime / ExecutionTime;
}

// one coloured block on the gantt chart
class GanttBlock
{
    public string ProcessName { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
}