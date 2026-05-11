using System;

class BankersAlgorithm
{
    // Tries to find a safe execution order; returns null if the system can't safely proceed
    static int[]? FindSafeSequence(int[,] need, int[,] alloc, int[] available, int p, int r)
    {
        int[] work = (int[])available.Clone(); // copy of available resources we can modify safely
        bool[] finish = new bool[p];           // keeps track of which processes are done
        int[] safeSeq = new int[p];
        int count = 0;

        while (count < p)
        {
            bool found = false;

            for (int i = 0; i < p; i++)
            {
                if (finish[i]) continue; // skip processes we've already completed

                // check if process i can be satisfied with current available resources
                bool canRun = true;
                for (int j = 0; j < r; j++)
                    if (need[i, j] > work[j]) { canRun = false; break; }

                if (canRun)
                {
                    // simulate finishing the process and releasing its resources
                    for (int j = 0; j < r; j++)
                        work[j] += alloc[i, j];

                    safeSeq[count++] = i;
                    finish[i] = true;
                    found = true;
                }
            }

            // if we couldn't run anything in this pass, we're stuck (unsafe state)
            if (!found) return null;
        }

        return safeSeq;
    }

    static void Main()
    {
        Console.Write("Number of processes: ");
        int p = int.Parse(Console.ReadLine()!);

        Console.Write("Number of resource types: ");
        int r = int.Parse(Console.ReadLine()!);

        int[,] alloc = new int[p, r];
        int[,] maxD = new int[p, r];
        int[,] need = new int[p, r];

        Console.WriteLine("\nEnter allocation matrix:");
        for (int i = 0; i < p; i++)
        {
            Console.Write($"  P{i + 1}: ");
            string[] vals = Console.ReadLine()!.Trim().Split();
            for (int j = 0; j < r; j++)
                alloc[i, j] = int.Parse(vals[j]);
        }

        Console.WriteLine("\nEnter maximum demand matrix:");
        for (int i = 0; i < p; i++)
        {
            Console.Write($"  P{i + 1}: ");
            string[] vals = Console.ReadLine()!.Trim().Split();
            for (int j = 0; j < r; j++)
            {
                maxD[i, j] = int.Parse(vals[j]);
                need[i, j] = maxD[i, j] - alloc[i, j]; // compute remaining need

                if (need[i, j] < 0)
                    throw new Exception($"Allocation exceeds maximum for P{i + 1}");
            }
        }

        Console.Write("\nEnter available resources: ");
        string[] avVals = Console.ReadLine()!.Trim().Split();
        int[] available = new int[r];
        for (int j = 0; j < r; j++)
            available[j] = int.Parse(avVals[j]);

        int[]? seq = FindSafeSequence(need, alloc, available, p, r);

        Console.WriteLine();
        if (seq != null)
        {
            Console.Write("Safe Sequence: ");
            for (int k = 0; k < p; k++)
                Console.Write((k > 0 ? " -> " : "") + $"P{seq[k] + 1}");
            Console.WriteLine("\nSystem is in a safe state.");
        }
        else
        {
            Console.WriteLine("System is in an UNSAFE state - no safe sequence exists.");
        }
    }
}