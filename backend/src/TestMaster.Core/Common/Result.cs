using System;
using System.Collections.Generic;

namespace TestMaster.Core.Common
{
    /// <summary>
    /// Represents the result of an operation, containing success status, error messages, and data
    /// </summary>
    /// <typeparam name="T">Type of data returned by the operation</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The data returned by the operation
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Error message if the operation failed
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// List of detailed error messages
        /// </summary>
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Private constructor to enforce usage of factory methods
        /// </summary>
        private Result()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Creates a successful result with data
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <returns>A successful result</returns>
        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        /// <summary>
        /// Creates a successful result with data and message
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <param name="message">Success message</param>
        /// <returns>A successful result</returns>
        public static Result<T> Success(T data, string message)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed result with an error message
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>A failed result</returns>
        public static Result<T> Failure(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed result with multiple error messages
        /// </summary>
        /// <param name="errors">List of error messages</param>
        /// <returns>A failed result</returns>
        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }

        /// <summary>
        /// Creates a failed result with a message and multiple error messages
        /// </summary>
        /// <param name="message">Main error message</param>
        /// <param name="errors">List of detailed error messages</param>
        /// <returns>A failed result</returns>
        public static Result<T> Failure(string message, List<string> errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// Non-generic version of Result for operations that don't return data
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Error message if the operation failed
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// List of detailed error messages
        /// </summary>
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Private constructor to enforce usage of factory methods
        /// </summary>
        private Result()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        /// <returns>A successful result</returns>
        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        /// <summary>
        /// Creates a successful result with a message
        /// </summary>
        /// <param name="message">Success message</param>
        /// <returns>A successful result</returns>
        public static Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed result with an error message
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>A failed result</returns>
        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed result with multiple error messages
        /// </summary>
        /// <param name="errors">List of error messages</param>
        /// <returns>A failed result</returns>
        public static Result Failure(List<string> errors)
        {
            return new Result
            {
                IsSuccess = false,
                Errors = errors
            };
        }

        /// <summary>
        /// Creates a failed result with a message and multiple error messages
        /// </summary>
        /// <param name="message">Main error message</param>
        /// <param name="errors">List of detailed error messages</param>
        /// <returns>A failed result</returns>
        public static Result Failure(string message, List<string> errors)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }
} 