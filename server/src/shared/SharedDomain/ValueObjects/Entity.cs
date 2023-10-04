using FluentValidation.Results;

namespace MyApp.SharedDomain.ValueObjects
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //public Guid Id { get; } = Guid.NewGuid();


        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public abstract bool Valid(out ValidationResult validationResult);

        public virtual bool Valid()
        {
            return Valid(out _);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
