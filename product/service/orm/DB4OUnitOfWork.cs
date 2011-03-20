namespace solidware.financials.service.orm
{
    public class DB4OUnitOfWork : UnitOfWork
    {
        readonly Connection connection;
        bool was_committed;

        public DB4OUnitOfWork(Connection connection)
        {
            this.connection = connection;
        }

        public void Dispose()
        {
            if (!was_committed) connection.Rollback();
            connection.Dispose();
        }

        public void commit()
        {
            connection.Commit();
            was_committed = true;
        }
    }
}