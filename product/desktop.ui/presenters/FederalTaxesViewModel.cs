using System;
using solidware.financials.windows.ui.model;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class FederalTaxesViewModel : ObservablePresenter<FederalTaxesViewModel>
    {
        public FederalTaxesViewModel(Guid id)
        {
            Id = id;
            FederalTaxesGrid = CreateSampleTable();
        }

        public Guid Id { get; private set; }
        public decimal FederalTaxes { get; set; }
        public decimal FederalFamilyTaxes { get; private set; }
        public DataGridTable FederalTaxesGrid { get; private set; }

        public void ChangeTotalIncomeTo(decimal totalIncome)
        {
            FederalTaxes = new FederalTaxes().CalculateFederalTaxesFor(totalIncome);
            FederalTaxesGrid.AddRow(x =>
            {
                x.AddToCell(new Column<string>("Name"), "blah");
                x.AddToCell(new Column<decimal>("Tax"), totalIncome);
            });
            update(x => x.FederalTaxes);
        }

        DataGridTable CreateSampleTable()
        {
            var table = new DataGridTable();
            var nameColumn = table.CreateColumn<string>("Name");
            var tax = table.CreateColumn<decimal>("Tax");

            table.AddRow(x =>
            {
                x.AddToCell(nameColumn, "mo");
                x.AddToCell(tax, 12345.67m);
            });
            table.AddRow(x => x.AddToCell(nameColumn, "allison"));
            table.FindRowFor(nameColumn, "allison").AddToCell(tax, 98765.43m);
            return table;
        }
    }
}