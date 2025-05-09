using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestMaster.Core.Common;
using TestMaster.Core.Common.Exceptions;
using TestMaster.Core.Common.Interfaces;
using TestMaster.Core.TestManagement.Entities;

namespace TestMaster.Core.TestManagement.Queries
{
    /// <summary>
    /// Query to get a test case by ID
    /// </summary>
    public class GetTestCaseById
    {
        /// <summary>
        /// Detailed test case data transfer object
        /// </summary>
        public class TestCaseDetailDto
        {
            /// <summary>
            /// Unique identifier of the test case
            /// </summary>
            public Guid Id { get; set; }

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
            /// Test suite name
            /// </summary>
            public string TestSuiteName { get; set; }

            /// <summary>
            /// Estimated execution time in minutes
            /// </summary>
            public int EstimatedTimeInMinutes { get; set; }

            /// <summary>
            /// Tags associated with the test case
            /// </summary>
            public System.Collections.Generic.List<string> Tags { get; set; }

            /// <summary>
            /// Indicates whether the test case is automated
            /// </summary>
            public bool IsAutomated { get; set; }

            /// <summary>
            /// Reference to automation script if the test case is automated
            /// </summary>
            public string AutomationReference { get; set; }

            /// <summary>
            /// Date when the test case was created
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// User who created the test case
            /// </summary>
            public string CreatedBy { get; set; }

            /// <summary>
            /// Date when the test case was last modified
            /// </summary>
            public DateTime? LastModifiedAt { get; set; }

            /// <summary>
            /// User who last modified the test case
            /// </summary>
            public string LastModifiedBy { get; set; }
        }

        /// <summary>
        /// Query request with test case ID
        /// </summary>
        public class Query : IRequest<Result<TestCaseDetailDto>>
        {
            /// <summary>
            /// ID of the test case to retrieve
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for the GetTestCaseById query
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<TestCaseDetailDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="unitOfWork">Unit of work</param>
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            /// <summary>
            /// Handles the GetTestCaseById query
            /// </summary>
            /// <param name="request">Query request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Result with test case details</returns>
            public async Task<Result<TestCaseDetailDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    // Get test case
                    var testCase = await _unitOfWork.Repository<TestCase>()
                        .GetByIdAsync(request.Id, cancellationToken);

                    if (testCase == null)
                    {
                        return Result<TestCaseDetailDto>.Failure($"Test case with ID {request.Id} not found");
                    }

                    // Get test suite
                    var testSuite = await _unitOfWork.Repository<TestSuite>()
                        .GetByIdAsync(testCase.TestSuiteId, cancellationToken);

                    // Map to DTO
                    var testCaseDto = new TestCaseDetailDto
                    {
                        Id = testCase.Id,
                        Title = testCase.Title,
                        Description = testCase.Description,
                        Steps = testCase.Steps,
                        ExpectedResults = testCase.ExpectedResults,
                        PreConditions = testCase.PreConditions,
                        Priority = testCase.Priority,
                        Status = testCase.Status,
                        TestSuiteId = testCase.TestSuiteId,
                        TestSuiteName = testSuite?.Name ?? "Unknown",
                        EstimatedTimeInMinutes = testCase.EstimatedTimeInMinutes,
                        Tags = testCase.Tags,
                        IsAutomated = testCase.IsAutomated,
                        AutomationReference = testCase.AutomationReference,
                        CreatedAt = testCase.CreatedAt,
                        CreatedBy = testCase.CreatedBy,
                        LastModifiedAt = testCase.LastModifiedAt,
                        LastModifiedBy = testCase.LastModifiedBy
                    };

                    return Result<TestCaseDetailDto>.Success(testCaseDto);
                }
                catch (Exception ex)
                {
                    return Result<TestCaseDetailDto>.Failure($"Error retrieving test case: {ex.Message}");
                }
            }
        }
    }
} 