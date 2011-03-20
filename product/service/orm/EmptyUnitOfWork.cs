using System;

namespace solidware.financials.service.orm
{
    public class EmptyUnitOfWork : UnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void commit()
        {
            throw new NotImplementedException();
        }
    }
}