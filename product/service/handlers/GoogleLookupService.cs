using System;
using System.IO;
using System.Net;
using gorilla.utility;
using Newtonsoft.Json;

namespace solidware.financials.service.handlers
{
    public class GoogleLookupService : StockPriceLookupService
    {
        public decimal FindPriceFor(string symbol)
        {
            //www.google.com/finance/info?infotype=infoquoteall&q=C,JPM,AIG
            dynamic convert = JsonConvert.DeserializeObject(Open("http://www.google.com/finance/info?infotype=infoquoteall&q={0}".format(symbol)).Remove(0, 4));
            var item = convert[0];
            Console.Out.WriteLine(item);
            return item.l;
        }

        string Open(string url)
        {
            using (var reader = new StreamReader(new WebClient().OpenRead(new Uri(url))))
            {
                return reader.ReadToEnd();
            }
        }

        public class GoogleFeed
        {
            public int id { get; set; }
            public string Symbol { get; set; }
            public string Exchange { get; set; }
            public decimal CurrentPrice { get; set; }
            public string LastTradeTime { get; set; }
            public string Change { get; set; }
            public string ChangePercentage { get; set; }
            public decimal Open { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Volume { get; set; }
            public decimal AverageVolume { get; set; }
            public decimal OneYearHigh { get; set; }
            public decimal OneYearLow { get; set; }
            public string MarketCapital { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
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