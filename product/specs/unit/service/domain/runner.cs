using Machine.Specifications;

namespace specs.unit.service.domain
{
    public class runner<T>
    {
        static protected T sut;

        protected virtual T create_sut()
        {
            return default(T);
        }
    }
}