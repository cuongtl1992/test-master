using System;

namespace TestMaster.Core.Common
{
    /// <summary>
    /// Base entity that all domain entities inherit from
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier for the entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Date when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// User ID who created the entity
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date when the entity was last modified
        /// </summary>
        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// User ID who last modified the entity
        /// </summary>
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Flag indicating if the entity is active or deleted (soft delete)
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
} 