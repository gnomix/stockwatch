using System;

namespace solidware.financials.service.orm
{
    public class LastOpened
    {
        public DateTime Now { get; private set; }

        public LastOpened(DateTime now)
        {
            Now = now;
        }
    }
}