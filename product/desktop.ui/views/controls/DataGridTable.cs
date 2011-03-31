using System;
using System.Linq;
using gorilla.utility;

namespace solidware.financials.windows.ui.views.controls
{
    public class DataGridTable : SmartCollection<Row>
    {
        public virtual Row FindRowFor<ColumnType>(Column<ColumnType> column, ColumnType expectedValue)
        {
            return FindRowMatching(row => row.ValueStoredIn(column).Equals(expectedValue));
        }

        public virtual bool HasRowFor<ColumnType>(Column<ColumnType> column, ColumnType expected)
        {
            return this.Any(row => row.ValueStoredIn(column).Equals(expected));
        }

        public virtual Row FindRowMatching(Func<Row, bool> condition)
        {
            return this.First(condition);
        }

        public virtual void AddRow(Action<Row> configureRow)
        {
            var row = new Row(this);
            configureRow(row);
            Add(row);
        }

        public virtual Column<T> CreateColumn<T>(string columnName)
        {
            var column = new Column<T>(columnName);
            this.each(x => x.AddToCell(column, default(T)));
            return column;
        }

        public virtual Column<T> FindColumn<T>(string columnName)
        {
            return this.Any() ? null : this.First().FindColumn<T>(columnName);
        }
    }
}