using System;

namespace TestMaster.Core.Common.Exceptions
{
    /// <summary>
    /// Base exception class for all custom exceptions in the application
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Error code for the exception
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Constructor with message only
        /// </summary>
        /// <param name="message">Exception message</param>
        protected BaseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with message and error code
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="errorCode">Error code for the exception</param>
        protected BaseException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        protected BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor with message, error code, and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="errorCode">Error code for the exception</param>
        /// <param name="innerException">Inner exception</param>
        protected BaseException(string message, string errorCode, Exception innerException) 
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
} 