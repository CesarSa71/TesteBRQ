
// Concrete handler for MEDIUMRISK category
class MediumRiskHandler : TradeCategoryAbstractHandler
{
    public MediumRiskHandler() { }

    public MediumRiskHandler(ITradeCategoryHandler nextHandler) : base(nextHandler) { }

    public override string Handle(ITrade trade)
    {
        if (trade.Value > 1000000 && trade.ClientSector.ToLower() == "public")
        {
            return "MEDIUMRISK";
        }
        return base.Handle(trade);
    }
}
