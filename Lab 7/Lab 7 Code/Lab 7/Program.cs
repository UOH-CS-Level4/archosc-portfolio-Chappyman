// OS Scheduling Lab - Module 441102, University of Hull
// Run: dotnet run

using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Output.Header("SECTION 1 — Long-Term Scheduling");
        LongTermScheduling.Run(ProcessData.Table1(), threshold: 5);

        Output.Header("SECTION 2a — FCFS");
        FCFS.Run(ProcessData.Table2());

        Output.Header("SECTION 2b — Round-Robin");
        foreach (int tq in new[] { 1, 3, 4, 6 })
            RoundRobin.Run(ProcessData.Table2(), tq);

        Output.Header("SECTION 3 — Context Switching (Priority Round-Robin)");
        foreach (int tq in new[] { 1, 6 })
            ContextSwitching.Run(ProcessData.Table1(), tq);

        Output.Header("INDEPENDENT — Priority Scheduling");
        IndependentLearning.RunPriority(ProcessData.Table1());

        Output.Header("INDEPENDENT — Shortest Job Next (SJN)");
        IndependentLearning.RunSJN(ProcessData.Table2());

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}