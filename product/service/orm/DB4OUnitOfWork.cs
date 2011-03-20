using gorilla.utility;

namespace solidware.financials.service.orm
{
    public class DB4OUnitOfWork : UnitOfWork
    {
        readonly Connection connection;
        readonly Context context;
        bool was_committed;

        public DB4OUnitOfWork(Connection connection, Context context)
        {
            this.connection = connection;
            this.context = context;
        }

        public void commit()
        {
            connection.Commit();
            was_committed = true;
        }

        public void Dispose()
        {
            context.remove(new TypedKey<Connection>());
            if (!was_committed) connection.Rollback();
            //connection.Dispose();
        }
    }
}