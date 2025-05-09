using System;
using System.Collections.Generic;
using TestMaster.Core.Common;

namespace TestMaster.Core.TestManagement.Entities
{
    /// <summary>
    /// Represents a test case in the system
    /// </summary>
    public class TestCase : BaseEntity
    {
        /// <summary>
        /// Title of the test case
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed description of the test case
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Steps to execute the test case
        /// </summary>
        public string Steps { get; set; }

        /// <summary>
        /// Expected results of the test case
        /// </summary>
        public string ExpectedResults { get; set; }

        /// <summary>
        /// Pre-conditions that must be met before executing the test case
        /// </summary>
        public string PreConditions { get; set; }

        /// <summary>
        /// Priority level of the test case
        /// </summary>
        public TestCasePriority Priority { get; set; }

        /// <summary>
        /// Current status of the test case
        /// </summary>
        public TestCaseStatus Status { get; set; }

        /// <summary>
        /// The ID of the test suite this test case belongs to
        /// </summary>
        public Guid TestSuiteId { get; set; }

        /// <summary>
        /// Estimated execution time in minutes
        /// </summary>
        public int EstimatedTimeInMinutes { get; set; }

        /// <summary>
        /// Tags associated with the test case
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Indicates whether the test case is automated
        /// </summary>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// Reference to automation script if the test case is automated
        /// </summary>
        public string AutomationReference { get; set; }
    }

    /// <summary>
    /// Priority levels for test cases
    /// </summary>
    public enum TestCasePriority
    {
        /// <summary>
        /// Low priority
        /// </summary>
        Low = 0,

        /// <summary>
        /// Medium priority
        /// </summary>
        Medium = 1,

        /// <summary>
        /// High priority
        /// </summary>
        High = 2,

        /// <summary>
        /// Critical priority
        /// </summary>
        Critical = 3
    }

    /// <summary>
    /// Status options for test cases
    /// </summary>
    public enum TestCaseStatus
    {
        /// <summary>
        /// Draft status
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Ready for review
        /// </summary>
        ReadyForReview = 1,

        /// <summary>
        /// Approved and ready for execution
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Deprecated test case
        /// </summary>
        Deprecated = 3
    }
} 