namespace utility
{
    public class MapKey<Input, Output> : MapKey
    {
        public bool Equals(MapKey<Input, Output> obj)
        {
            return !ReferenceEquals(null, obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MapKey<Input, Output>)) return false;
            return Equals((MapKey<Input, Output>) obj);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public interface MapKey
    {
    }
}