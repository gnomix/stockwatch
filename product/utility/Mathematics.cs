namespace utility
{
    static public class Mathematics
    {
        static public decimal subtract(this decimal right, decimal left)
        {
            return right - left;
        }

        static public decimal add(this decimal right, decimal left)
        {
            return right + left;
        }

        static public decimal multiply_by(this decimal right, decimal left)
        {
            return right*left;
        }

        static public decimal divided_by(this decimal right, decimal left)
        {
            return right/left;
        }
    }
}