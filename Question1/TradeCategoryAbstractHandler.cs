
// Abstract class for handlers
abstract class TradeCategoryAbstractHandler : ITradeCategoryHandler
{
    private ITradeCategoryHandler? _nextHandler;

    public TradeCategoryAbstractHandler()
    {
        _nextHandler = null;
    }

    public TradeCategoryAbstractHandler(ITradeCategoryHandler nextHandler)
    {
        this._nextHandler = nextHandler;
    }

    public ITradeCategoryHandler SetNextHandler(ITradeCategoryHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    public virtual string Handle(ITrade trade)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.Handle(trade);
        }
        // No handler found to process the trade
        return "NOTCATEGORIZED";
    }
}
