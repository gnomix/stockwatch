using Db4objects.Db4o;

namespace solidware.financials.service.orm
{
    public interface Connection : IObjectContainer, Disposable
    {
    }
}