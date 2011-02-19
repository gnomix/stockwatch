using Rhino.Mocks;

namespace specs
{
    static class The
    {
        public static T dependency<T>() where T : class
        {
            return MockRepository.GenerateMock<T>();
        }
    }
}