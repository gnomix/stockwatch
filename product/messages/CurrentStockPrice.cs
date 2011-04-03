using System.Text;
using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.messages
{
    public class CurrentStockPrice : ValueType<CurrentStockPrice>, Event
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }

        public string Change { get; set; }
        public string ChangePercentage { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("{0} {1:C}".format(Symbol.ToUpperInvariant(), Price));
            builder.AppendLine("{0} / {1}%".format(Change, ChangePercentage));
            //builder.AppendLine("O:{0:C} H:{1:C} L:{2:C}".format(Open, High, Low));
            return builder.ToString();
        }
    }
}