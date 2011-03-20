using System;

namespace solidware.financials.service.orm
{
    public class DB40UnitOfWork : UnitOfWork
    {
        public DB40UnitOfWork(Connection connection)
        {
        }

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