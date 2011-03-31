using System.Collections.Generic;
using System.Linq;
using gorilla.utility;

namespace solidware.financials.windows.ui.views.controls
{
    public class Row : Dictionary<string, Observable> 
    {
        DataGridTable table;

        public Row(DataGridTable table)
        {
            this.table = table;
        }

        public virtual T ValueStoredIn<T>(Column<T> column)
        {
            return this[column].As<T>();
        }

        public virtual void AddToCell<T>(Column<T> column, T value)
        {
            AddToCell(column, (Observable)new ObservableProperty<T>(value));
        }

        public virtual void AddToCell<T>(Column<T> column, Observable value)
        {
            this[column] = value;
            table.each(row =>
            {
                if (!row.HasValueStoredIn(column))
                    row[column] = new ObservableProperty<T>(default(T));
            });
        }

        public virtual bool HasValueStoredIn<T>(Column<T> column)
        {
            return ContainsKey(column);
        }

        public virtual Column<T> FindColumn<T>(string name)
        {
            return Keys.FirstOrDefault(x => x == name);
        }
    }
}