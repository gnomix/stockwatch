namespace solidware.financials.service.orm
{
    public interface UnitOfWork : Disposable
    {
        void commit();
    }
}