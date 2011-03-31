using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace solidware.financials.windows.ui.views.controls
{
    public class SmartGrid : DataGrid
    {
        static SmartGrid()
        {
            IsReadOnlyProperty.OverrideMetadata(typeof (SmartGrid), new FrameworkPropertyMetadata((o, e) => ((SmartGrid) o).ConfigureColumns()));
            CommandManager.RegisterClassCommandBinding(typeof (SmartGrid), new CommandBinding(ApplicationCommands.Paste, (o, e) => ((SmartGrid) o).OnExecutedPaste()));
        }

        public SmartGrid()
        {
            SelectionUnit = DataGridSelectionUnit.CellOrRowHeader;
            ContextMenu = new ContextMenu
                          {
                              Items =
                                  {
                                      new MenuItem {Command = ApplicationCommands.Copy},
                                      new MenuItem {Command = ApplicationCommands.Paste}
                                  }
                          };
        }

        void ConfigureColumns()
        {
            foreach (var column in Columns)
            {
                column.IsReadOnly = IsReadOnly;
            }
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            if (newValue == null) return;

            var enumerator = newValue.GetEnumerator();
            if (!enumerator.MoveNext()) return;

            var firstRow = enumerator.Current as Row;
            if (firstRow == null) return;

            AutoGenerateColumns = false;
            Columns.Clear();
            foreach (var pair in firstRow)
            {
                Columns.Add(new ExtendedTextColumn
                            {
                                Header = pair.Key,
                                Binding = new Binding("[" + pair.Key + "].Value")
                            });
            }
            ConfigureColumns();
        }

        protected virtual void OnExecutedPaste()
        {
            OnBeginningEdit(new DataGridBeginningEditEventArgs(Columns.First(), new DataGridRow(), new RoutedEventArgs()));
            var rowData = ClipboardHelper.ParseClipboardData();

            var minColumnDisplayIndex = SelectedCells.Min(x => x.Column.DisplayIndex);
            var maxColumnDisplayIndex = SelectedCells.Max(x => x.Column.DisplayIndex);
            var minRowIndex = SelectedCells.Min(y => Items.IndexOf(y.Item));
            var maxRowIndex = SelectedCells.Max(y => Items.IndexOf(y.Item));

            // If single cell select, then use as a starting cell rather than limiting the paste
            if (minColumnDisplayIndex == maxColumnDisplayIndex && minRowIndex == maxRowIndex)
            {
                maxColumnDisplayIndex = Columns.Count - 1;
                maxRowIndex = Items.Count - 1;
            }

            var rowDataIndex = 0;
            for (var i = minRowIndex; i <= maxRowIndex && rowDataIndex <= rowData.Count() - 1; i++, rowDataIndex++)
            {
                var columnDataIndex = 0;
                for (var j = minColumnDisplayIndex; j <= maxColumnDisplayIndex && columnDataIndex <= rowData[rowDataIndex].Length - 1; j++, columnDataIndex++)
                {
                    var column = ColumnFromDisplayIndex(j);
                    if (column.IsReadOnly) continue;
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    foreach (var cell in SelectedCells.Where(x => !x.Column.IsReadOnly))
                    {
                        // N.B. Passing in an integer value of zero results in the Gas (MCF) column updating,
                        //      but no other column updating. Using a string "0" results in all values
                        //      updating properly. Very odd behaviour, but insufficient time to investigate why.
                        cell.Column.OnPastingCellClipboardContent(cell.Item, "0");
                    }
                    OnBeginningEdit(new DataGridBeginningEditEventArgs(Columns.First(), new DataGridRow(), new RoutedEventArgs()));
                    break;
                case Key.Enter:
                    OnBeginningEdit(new DataGridBeginningEditEventArgs(Columns.First(), new DataGridRow(), new RoutedEventArgs()));
                    break;
                default:
                    base.OnKeyUp(e);
                    break;
            }
        }
    }
}