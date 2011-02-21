namespace utility
{
    public delegate Out FactoryDelegate<In, Out>(In input);

    public delegate Out FactoryDelegate<Out>();
}