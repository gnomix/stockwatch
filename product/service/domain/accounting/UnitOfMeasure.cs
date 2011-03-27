namespace solidware.financials.service.domain.accounting
{
    public interface UnitOfMeasure
    {
        decimal convert(decimal amount, UnitOfMeasure other);
        string pretty_print(decimal amount);
    }

}