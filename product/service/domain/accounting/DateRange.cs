namespace solidware.financials.service.domain.accounting
{
    static public class DateRange
    {
        static public Range<Date> up_to(Date date)
        {
            return Date.First.to(date);
        }

        static public Range<Date> to(this Date start, Date end)
        {
            return new SequentialRange<Date>(start, end);
        }
    }
}