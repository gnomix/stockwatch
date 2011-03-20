namespace solidware.financials.service.orm
{
    public class EmptyUnitOfWork : UnitOfWork
    {
        public void Dispose()
        {
        }

        public void commit()
        {
        }
    }
}