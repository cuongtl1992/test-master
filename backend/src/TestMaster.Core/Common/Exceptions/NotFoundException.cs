using System;

namespace TestMaster.Core.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of NotFoundException with specified message
        /// </summary>
        /// <param name="message">The error message</param>
        public NotFoundException(string message) 
            : base(message, "NotFound")
        {
        }

        /// <summary>
        /// Initializes a new instance of NotFoundException with a formatted message for an entity
        /// </summary>
        /// <param name="entityName">Name of the entity that wasn't found</param>
        /// <param name="id">ID that was searched for</param>
        public NotFoundException(string entityName, object id)
            : base($"{entityName} with id {id} was not found.", "NotFound")
        {
        }

        /// <summary>
        /// Initializes a new instance of NotFoundException with a specified message and inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, "NotFound", innerException)
        {
        }
    }
} 