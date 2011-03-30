using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using gorilla.utility;

namespace solidware.financials.windows.ui.views.controls
{
    public class SmartGrid : DataGrid
    {
        static SmartGrid()
        {
            IsReadOnlyProperty.OverrideMetadata(typeof (SmartGrid), new FrameworkPropertyMetadata((o, e) => ((SmartGrid) o).ConfigureColumns()));
            ValueConvertersProperty.OverrideMetadata(typeof (SmartGrid), new FrameworkPropertyMetadata((o, e) => ((SmartGrid) o).ConfigureColumns()));
            CommandManager.RegisterClassCommandBinding(typeof (SmartGrid), new CommandBinding(ApplicationCommands.Paste, (o, e) => ((SmartGrid) o).OnExecutedPaste()));
        }

        static public readonly DependencyProperty ReadOnlyColumnsProperty = DependencyProperty.Register("ReadOnlyColumns", typeof (IEnumerable<string>), typeof (SmartGrid));
        static public readonly DependencyProperty HeaderColumnsProperty = DependencyProperty.Register("HeaderColumns", typeof (IEnumerable<string>), typeof (SmartGrid));

        static public readonly DependencyProperty ValueConvertersProperty = DependencyProperty.Register("ValueConverters", typeof (IEnumerable<KeyValuePair<string, IValueConverter>>),
                                                                                                        typeof (SmartGrid));

        static public readonly DependencyProperty ReadOnlyItemsProperty = DependencyProperty.Register("ReadOnlyItems", typeof (IEnumerable<IDictionary<string, object>>), typeof (SmartGrid));

        static readonly SolidColorBrush HeaderColour = Brushes.LightGray;
        static readonly SolidColorBrush ReadOnlyColour = Brushes.WhiteSmoke;

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

        public virtual IEnumerable<IDictionary<string, object>> ReadOnlyItems
        {
            get { return (IEnumerable<IDictionary<string, object>>) GetValue(ReadOnlyItemsProperty) ?? Enumerable.Empty<IDictionary<string, object>>(); }
            set { SetValue(ReadOnlyItemsProperty, value); }
        }

        public IEnumerable<string> ReadOnlyColumns
        {
            get { return (IEnumerable<string>) GetValue(ReadOnlyColumnsProperty) ?? Enumerable.Empty<string>(); }
            set
            {
                SetValue(ReadOnlyColumnsProperty, value);
                if (Columns != null && Columns.Count > 0)
                {
                    ConfigureColumns();
                }
            }
        }

        public IEnumerable<string> HeaderColumns
        {
            get { return (IEnumerable<string>) GetValue(HeaderColumnsProperty) ?? Enumerable.Empty<string>(); }
            set
            {
                SetValue(HeaderColumnsProperty, value);
                if (Columns != null && Columns.Count > 0)
                {
                    ConfigureColumns();
                }
            }
        }

        public IEnumerable<KeyValuePair<string, IValueConverter>> ValueConverters
        {
            get { return (IEnumerable<KeyValuePair<string, IValueConverter>>) GetValue(ValueConvertersProperty) ?? Enumerable.Empty<KeyValuePair<string, IValueConverter>>(); }
            set
            {
                SetValue(ValueConvertersProperty, value);
                if (Columns != null && Columns.Count > 0)
                {
                    ConfigureColumns();
                }
            }
        }

        void ConfigureColumns()
        {
            foreach (var column in Columns)
            {
                var header = column.Header.ToString();
                column.IsReadOnly = IsReadOnly || ReadOnlyColumns.Contains(header);

                Brush background = null;
                if (HeaderColumns.Contains(header))
                {
                    background = HeaderColour;
                }
                else if (ReadOnlyColumns.Contains(header))
                {
                    background = ReadOnlyColour;
                }
                if (background != null)
                {
                    if (column.CellStyle == null) column.CellStyle = new Style();
                    SetPropertyIfNotPresent(column, BackgroundProperty, background);
                    SetPropertyIfNotPresent(column, ForegroundProperty, Brushes.Black);
                }

                var textColumn = column as DataGridTextColumn;
                if (textColumn == null) continue;
                var binding = textColumn.Binding as Binding;
                if (binding == null) continue;
                if (binding.Converter != null) continue; // Converter has already been bound and cannot be changed.

                var valueConverter = ValueConverters.SingleOrDefault(x => x.Key == header).Value;

                if (valueConverter != null)
                {
                    binding.Converter = valueConverter;
                }
            }
        }

        static void SetPropertyIfNotPresent(DataGridColumn column, DependencyProperty dependencyProperty, Brush brush)
        {
            var existing = column.CellStyle.Setters.OfType<Setter>().FirstOrDefault(x => x.Property == dependencyProperty);
            if (existing == null)
            {
                column.CellStyle.Setters.Add(new Setter(dependencyProperty, brush));
            }
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            if (newValue == null) return;

           var enumerator = newValue.GetEnumerator();
            if (!enumerator.MoveNext()) return;

            var firstRow = enumerator.Current as IDictionary<string, IObservable>;

            if (firstRow == null) return;

            AutoGenerateColumns = false; 
            Columns.Clear();
            foreach (var pair in firstRow)
            {
                Columns.Add(new ExtendedTextColumn {Header = pair.Key, Binding = new Binding("[" + pair.Key + "].Value")});
            }
            ConfigureColumns();
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            if (ReadOnlyItems != null)
            {
                e.Row.IsEnabled = !IsItemReadOnly(e.Row.Item);
                if (!e.Row.IsEnabled)
                {
                    e.Row.Background = HeaderColour;
                }
            }

            base.OnLoadingRow(e);
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
                    if (IsItemReadOnly(Items[i])) continue;
                    column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                }
            }
        }

        bool IsItemReadOnly(object item)
        {
            return ReadOnlyItems.Any(x => ReferenceEquals(x, item));
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