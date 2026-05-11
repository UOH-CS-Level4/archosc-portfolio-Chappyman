using System;
using System.Collections.Generic;

class TLBEntry
{
    public int VPN;
    public int PPN;
}

class TLB
{
    private List<TLBEntry> entries = new List<TLBEntry>();
    private const int capacity = 4;
+_
    public int? Lookup(int vpn)
    {
        foreach (var entry in entries)
        {
            if (entry.VPN == vpn)
                return entry.PPN;
        }
        return null;
    }

    public void Insert(int vpn, int ppn)
    {
        if (entries.Count >= capacity)
            entries.RemoveAt(0);
        entries.Add(new TLBEntry { VPN = vpn, PPN = ppn });
    }
}

class Program
{
    static void Main()
    {
        TLB tlb = new TLB();

        Dictionary<int, int> pageTable = new Dictionary<int, int>
        {
            { 0, 7 }, { 1, 9 }, { 2, 0 }, { 3, 5 },
            { 4, 5 }, { 5, 3 }, { 6, 2 }, { 7, 4 }
        };

        int[] vpnAccesses = { 1, 6, 3, 1, 6, 2, 4, 1, 6, 3 };

        int hits = 0;
        int misses = 0;

        Console.WriteLine("TLB Simulation");
        Console.WriteLine("==============");

        foreach (int vpn in vpnAccesses)
        {
            int? ppn = tlb.Lookup(vpn);

            if (ppn != null)
            {
                hits++;
                Console.WriteLine($"VPN {vpn} -> TLB HIT  -> PPN {ppn}");
            }
            else
            {
                misses++;
                if (pageTable.ContainsKey(vpn))
                {
                    int newPpn = pageTable[vpn];
                    tlb.Insert(vpn, newPpn);
                    Console.WriteLine($"VPN {vpn} -> TLB MISS -> PPN {newPpn} (loaded from page table)");
                }
            }
        }

        double hitRatio = (double)hits / (hits + misses) * 100;
        Console.WriteLine("\n--- Results ---");
        Console.WriteLine($"Total Accesses : {hits + misses}");
        Console.WriteLine($"TLB Hits       : {hits}");
        Console.WriteLine($"TLB Misses     : {misses}");
        Console.WriteLine($"Hit Ratio      : {hitRatio:F1}%");
    }
}