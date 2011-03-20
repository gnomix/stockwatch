using gorilla.utility;

namespace solidware.financials.service.orm
{
    public class DB40UnitOfWorkFactory : UnitOfWorkFactory
    {
        readonly ConnectionFactory factory;
        readonly Context context;
        Key<Connection> key = new TypedKey<Connection>();

        public DB40UnitOfWorkFactory(ConnectionFactory factory, Context context)
        {
            this.factory = factory;
            this.context = context;
        }

        public UnitOfWork create()
        {
            if( context.contains(key)) return new EmptyUnitOfWork();

            var connection = factory.Open();
            context.add(key, connection);
            return new DB4OUnitOfWork(connection, context);
        }
    }
}