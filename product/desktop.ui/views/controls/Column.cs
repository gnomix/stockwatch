using System;

namespace solidware.financials.windows.ui.views.controls
{
    public interface IColumn
    {
        string Title { get; }
    }

    public class Column<T> : IEquatable<Column<T>>, IColumn
    {
        public string Title { get; private set; }

        public Column(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }

        static public implicit operator Column<T>(string title)
        {
            return new Column<T>(title);
        }

        static public implicit operator string(Column<T> column)
        {
            return column.Title;
        }

        public bool Equals(Column<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Title, Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Column<T>)) return false;
            return Equals((Column<T>) obj);
        }

        public override int GetHashCode()
        {
            return (Title != null ? Title.GetHashCode() : 0);
        }

        static public bool operator ==(Column<T> left, Column<T> right)
        {
            return Equals(left, right);
        }

        static public bool operator !=(Column<T> left, Column<T> right)
        {
            return !Equals(left, right);
        }
    }
}