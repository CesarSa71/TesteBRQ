
// Handler interface
interface ITradeCategoryHandler
{
    string Handle(ITrade trade);
    ITradeCategoryHandler SetNextHandler(ITradeCategoryHandler nextHandler);
}
