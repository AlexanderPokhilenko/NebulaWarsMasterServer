using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static void Main()
    {
        List<int> test = new List<int>{65,19,861,654};
        var test1  = test.Take(10).ToList();
        Console.WriteLine(test1.Count);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        foreach (var VARIABLE in test1)
        {
            Console.WriteLine(VARIABLE);
        }
    }
}