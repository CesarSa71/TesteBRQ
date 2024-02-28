
// Concrete handler for HIGHRISK category
class HighRiskHandler : TradeCategoryAbstractHandler
{
    public HighRiskHandler() { }

    public HighRiskHandler(ITradeCategoryHandler nextHandler) : base(nextHandler) { }

    public override string Handle(ITrade trade)
    {
        if (trade.Value > 1000000 && trade.ClientSector.ToLower() == "private")
        {
            return "HIGHRISK";
        }
        return base.Handle(trade);
    }
}
