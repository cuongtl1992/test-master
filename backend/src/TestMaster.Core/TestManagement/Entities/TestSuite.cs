using System;
using System.Collections.Generic;
using TestMaster.Core.Common;

namespace TestMaster.Core.TestManagement.Entities
{
    /// <summary>
    /// Represents a test suite that contains a collection of test cases
    /// </summary>
    public class TestSuite : BaseEntity
    {
        /// <summary>
        /// Name of the test suite
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the test suite
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Project ID the test suite belongs to
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Parent test suite ID if this is a child suite, null if it's a root suite
        /// </summary>
        public Guid? ParentTestSuiteId { get; set; }

        /// <summary>
        /// Collection of test cases in this suite
        /// </summary>
        public List<TestCase> TestCases { get; set; } = new List<TestCase>();

        /// <summary>
        /// Child test suites under this suite
        /// </summary>
        public List<TestSuite> ChildTestSuites { get; set; } = new List<TestSuite>();
    }
} 