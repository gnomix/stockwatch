using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.views
{
    public partial class TaxSummaryTab : Tab<TaxSummaryPresenter>
    {
        public TaxSummaryTab()
        {
            InitializeComponent();
        }

        public void bind_to(TaxSummaryPresenter presenter)
        {
            DataContext = presenter;
            //((LineSeries)Chart.Series[0]).ItemsSource = Data();
            //Chart.Series.Add(new BarSeries
            //                 {
            //                     Title = "Federal Taxes",
            //                     IndependentValueBinding = new Binding("Key"),
            //                     DependentValueBinding = new Binding("Value"),
            //                     ItemsSource = Data()
            //                 });
            //((PieSeries)Chart.Series[0]).ItemsSource = PieData();
        }

        //IEnumerable PieData()
        //{
        //    yield return new KeyValuePair<string, decimal>("Federal", 12345.67m);
        //    yield return new KeyValuePair<string, decimal>("Provincial", 2345.67m);
        //    yield return new KeyValuePair<string, decimal>("Income", 345.67m);
        //}

        //IEnumerable<KeyValuePair<DateTime, decimal>> Data()
        //{
        //    yield return new KeyValuePair<DateTime, decimal>(new DateTime(2011, 01, 01), 12345.67m);
        //    yield return new KeyValuePair<DateTime, decimal>(new DateTime(2011, 02, 01), 2345.67m);
        //    yield return new KeyValuePair<DateTime, decimal>(new DateTime(2011, 03, 01), 345.67m);
        //}
    }
}