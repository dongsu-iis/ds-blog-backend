using System;

namespace SharedKernel
{
    public abstract class BaseEntity : ValueObject
    {
        public int Id { get; set; }

        [IgnoreMember]
        public DateTime CreatedAt { get; set; }
        [IgnoreMember]
        public DateTime UpdatedAt { get; set; }
    }
}
