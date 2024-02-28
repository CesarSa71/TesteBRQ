
// Class responsible for chaining the handlers
using System.Diagnostics;

class TradeCategoryProcessor
{
    private readonly ITradeCategoryHandler _firstHandler;

    public TradeCategoryProcessor()
    {
        // Construct the chain of responsibility
        var highRiskHandler = new HighRiskHandler();
        var mediumRiskHandler = new MediumRiskHandler(highRiskHandler);
        _firstHandler = new LowRiskHandler(mediumRiskHandler);

        // Alternative way to construct the chain of responsibility
        //_firstHandler = new LowRiskHandler();
        //var highRiskHandler = new HighRiskHandler();
        //var mediumRiskHandler = new MediumRiskHandler();
        //_firstHandler.SetNextHandler(mediumRiskHandler).SetNextHandler(highRiskHandler);
    }

    public string Process(ITrade trade)
    {
        return _firstHandler.Handle(trade);
    }

    public IEnumerable<string> Process(IEnumerable<ITrade> trades)
    {
        return trades.Select(trade => this.Process(trade));
    }
}
