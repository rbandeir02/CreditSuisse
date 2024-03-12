using System;
using System.Collections.Generic;

// Interface to represent a trade
interface ITrade
{
    double Value { get; }
    string ClientSector { get; }
}

// Interface defining the strategy for categorizing trades
interface ITradeCategorizationStrategy
{
    string CategorizeTrade(ITrade trade);
}

// Concrete implementation of the low risk trade categorization strategy
class LowRiskCategorizationStrategy : ITradeCategorizationStrategy
{
    public string CategorizeTrade(ITrade trade)
    {
        if (trade.Value < 1000000 && trade.ClientSector == "Public")
            return "LOWRISK";
        else
            return null; // Trade does not fit into this category
    }
}

// Concrete implementation of the medium risk trade categorization strategy
class MediumRiskCategorizationStrategy : ITradeCategorizationStrategy
{
    public string CategorizeTrade(ITrade trade)
    {
        if (trade.Value >= 1000000 && trade.ClientSector == "Public")
            return "MEDIUMRISK";
        else
            return null; // Trade does not fit into this category
    }
}

// Concrete implementation of the high risk trade categorization strategy
class HighRiskCategorizationStrategy : ITradeCategorizationStrategy
{
    public string CategorizeTrade(ITrade trade)
    {
        if (trade.Value >= 1000000 && trade.ClientSector == "Private")
            return "HIGHRISK";
        else
            return null; // Trade does not fit into this category
    }
}

// Context class that applies the categorization strategy
class TradeCategorizer
{
    private readonly Dictionary<string, ITradeCategorizationStrategy> _strategies;

    public TradeCategorizer()
    {
        _strategies = new Dictionary<string, ITradeCategorizationStrategy>
        {
            {"LOWRISK", new LowRiskCategorizationStrategy()},
            {"MEDIUMRISK", new MediumRiskCategorizationStrategy()},
            {"HIGHRISK", new HighRiskCategorizationStrategy()}
        };
    }

    public string CategorizeTrade(ITrade trade)
    {
        foreach (var strategy in _strategies)
        {
            string category = strategy.Value.CategorizeTrade(trade);
            if (category != null)
                return category;
        }

        throw new Exception("No category found for the trade.");
    }
}

//Now, Lets test!
class Program
{
    static void Main(string[] args)
    {
        // Example input trades, as mentioned in the Test email that Elisangela sent me
        List<ITrade> portfolio = new List<ITrade>
        {
            new Trade { Value = 2000000, ClientSector = "Private" },
            new Trade { Value = 400000, ClientSector = "Public" },
            new Trade { Value = 500000, ClientSector = "Public" },
            new Trade { Value = 3000000, ClientSector = "Public" }
        };

        // Categorize trades
        TradeCategorizer categorizer = new TradeCategorizer();
        List<string> tradeCategories = new List<string>();
        foreach (var trade in portfolio)
        {
            string category = categorizer.CategorizeTrade(trade);
            tradeCategories.Add(category);
        }

        // Output categories
        Console.WriteLine("Trade categories:");
        foreach (var category in tradeCategories)
        {
            Console.WriteLine(category);
        }

        Console.WriteLine("I have commented all the classes and interface that i used. I choose stratety design pattern for this test because it provides a clean and flexible way to encapsulate different algorithms or strategies. It was a requirement for this test!");
        Console.ReadLine(); //pause
    }
}

// Concrete implementation of the ITrade interface
class Trade : ITrade
{
    public double Value { get; set; }
    public string ClientSector { get; set; }
}
