
// Concrete handler for LOWRISK category
class LowRiskHandler : TradeCategoryAbstractHandler
{
    public LowRiskHandler() { }

    public LowRiskHandler(ITradeCategoryHandler nextHandler) : base(nextHandler) { }

    public override string Handle(ITrade trade)
    {
        if (trade.Value < 1000000 && trade.ClientSector.ToLower() == "public")
        {
            return "LOWRISK";
        }
        return base.Handle(trade);
    }
}
