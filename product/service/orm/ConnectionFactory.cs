namespace solidware.financials.service.orm
{
    public interface ConnectionFactory
    {
        Connection Open();
        Connection Open(string path);
    }
}
