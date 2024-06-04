namespace Studentica.Common.Interfaces
{
    public interface IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public T Id { get; set; }
    }
}
