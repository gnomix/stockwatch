using System;
using System.IO;
using System.Net;
using gorilla.utility;
using Newtonsoft.Json;

namespace solidware.financials.service.handlers
{
    public interface StockPriceLookupService
    {
        decimal FindPriceFor(string symbol);
    }

    public class StubLookupService : StockPriceLookupService
    {
        public decimal FindPriceFor(string symbol)
        {
            return Convert.ToDecimal(new Random().NextDouble());
        }
    }

    public class GoogleLookupService : StockPriceLookupService
    {
        public decimal FindPriceFor(string symbol)
        {
            //www.google.com/finance/info?infotype=infoquoteall&q=C,JPM,AIG
            var url = "http://www.google.com/finance/info?infotype=infoquoteall&q={0}".format(symbol);
            dynamic convert = JsonConvert.DeserializeObject(Open(url).Remove(0, 4));
            dynamic item = convert[0];
            var current_price = item.l;
            return current_price;
        }

        string Open(string url)
        {
            using (var reader = new StreamReader(new WebClient().OpenRead(new Uri(url))))
            {
                return reader.ReadToEnd();
            }
        }
    }
}