using System;

namespace TestMaster.Core.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when a business rule is violated
    /// </summary>
    public class BusinessRuleException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of BusinessRuleException with specified message
        /// </summary>
        /// <param name="message">The error message</param>
        public BusinessRuleException(string message) 
            : base(message, "BusinessRuleViolation")
        {
        }

        /// <summary>
        /// Initializes a new instance of BusinessRuleException with specified message and error code
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="errorCode">The specific error code</param>
        public BusinessRuleException(string message, string errorCode) 
            : base(message, errorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of BusinessRuleException with a specified message and inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public BusinessRuleException(string message, Exception innerException)
            : base(message, "BusinessRuleViolation", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of BusinessRuleException with a specified message, error code, and inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="errorCode">The specific error code</param>
        /// <param name="innerException">The inner exception</param>
        public BusinessRuleException(string message, string errorCode, Exception innerException)
            : base(message, errorCode, innerException)
        {
        }
    }
} 