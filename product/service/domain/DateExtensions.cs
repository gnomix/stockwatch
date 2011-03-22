namespace solidware.financials.service.domain
{
    static public class DateExtensions
    {
        static public Date plus_years(this Date date, int years)
        {
            return date.to_date_time().AddYears(years);
        }

        static public Date plus_days(this Date date, int days)
        {
            return date.to_date_time().AddDays(days);
        }

        static public Date minus_days(this Date date, int days)
        {
            return date.to_date_time().AddDays(-days);
        }

        static public Date add_months(this Date date, int months)
        {
            return date.to_date_time().AddMonths(months);
        }
    }
}