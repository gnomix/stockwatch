using System;
using System.IO;
using System.Net;
using gorilla.utility;
using Newtonsoft.Json;
using solidware.financials.messages;

namespace solidware.financials.service.handlers
{
    public class GoogleLookupService : StockPriceLookupService
    {
        public CurrentStockPrice FindPriceFor(string symbol)
        {
            //www.google.com/finance/info?infotype=infoquoteall&q=C,JPM,AIG
            try
            {
                dynamic convert = JsonConvert.DeserializeObject(Open("http://www.google.com/finance/info?infotype=infoquoteall&q={0}".format(symbol)).Remove(0, 4));
                var item = convert[0];
                Console.Out.WriteLine(item);
                var price = MapFrom(item);
                price.Symbol = symbol;
                return price;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return new CurrentStockPrice {Symbol = symbol};
            }
        }

        CurrentStockPrice MapFrom(dynamic item)
        {
            return new CurrentStockPrice
                   {
                       Symbol = item.t,
                       Price = item.l,
                       Change = item.c,
                       ChangePercentage = item.cp,
                       High = item.hi,
                       Low = item.lo,
                       Open = item.op,
                   };
        }

        string Open(string url)
        {
            using (var reader = new StreamReader(new WebClient().OpenRead(new Uri(url))))
            {
                return reader.ReadToEnd();
            }
        }

        /*
{
  "id": "666908",
  "t": "ARX",
  "e": "TSE",
  "l": "26.50",
  "l_cur": "CA$26.50",
  "s": "0",
  "ltt": "4:00PM EDT",
  "lt": "Apr 1, 4:00PM EDT",
  "c": "+0.15",
  "cp": "0.57",
  "ccol": "chg",
  "eo": "",
  "delay": "15",
  "op": "26.49",
  "hi": "26.65",
  "lo": "26.31",
  "vo": "426,774.00",
  "avvo": "1.60M",
  "hi52": "28.67",
  "lo52": "18.77",
  "mc": "7.55B",
  "pe": "20.35",
  "fwpe": "",
  "beta": "0.69",
  "eps": "1.30",
  "name": "ARC Resources Ltd",
  "type": "Company"
}
 */
    }
}