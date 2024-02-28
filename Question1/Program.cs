using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        List<ITrade> portfolio = new List<ITrade>
        {
            new Trade { Value = 2000000, ClientSector = "Private" }
            ,new Trade { Value = 400000, ClientSector = "Public" }
            ,new Trade { Value = 500000, ClientSector = "Public" }
            ,new Trade { Value = 3000000, ClientSector = "Public" }
            //,new Trade { Value = 300000, ClientSector = "Private" }
        };

        TradeCategoryProcessor processor = new TradeCategoryProcessor();
        IEnumerable<string> tradeCategories = processor.Process(portfolio);

        foreach (var category in tradeCategories)
        {
            Console.WriteLine(category);
        }
    }
}
