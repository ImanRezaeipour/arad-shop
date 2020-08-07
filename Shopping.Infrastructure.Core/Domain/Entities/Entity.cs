using System;
using System.Reflection;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.Infrastructure.Core.Domain.Entities
{
    public abstract class Entity : Entity<Guid>, IEntity
    {
    }
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        protected Entity()
        {
            Validate();
        }

        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TPrimaryKey>))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var other = (Entity<TPrimaryKey>)obj;
            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }
            return Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id == null ? 0 : Id.GetHashCode();
        }

        /// <inheritdoc/>
        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Id.ToString();
        }
        public abstract void Validate();
    }
}