using Studentica.Common.Interfaces;

namespace Studentica.Services.Models
{
    public abstract class ModelBase<T> : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public T Id { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj is not IEntity<T> model) return false;
            return model.Id.Equals(Id);
        }

        protected bool Equals(ModelBase<T> other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
