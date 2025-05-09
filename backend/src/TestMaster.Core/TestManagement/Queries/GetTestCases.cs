using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestMaster.Core.Common;
using TestMaster.Core.Common.Interfaces;
using TestMaster.Core.TestManagement.Entities;

namespace TestMaster.Core.TestManagement.Queries
{
    /// <summary>
    /// Query to get test cases with pagination
    /// </summary>
    public class GetTestCases
    {
        /// <summary>
        /// Test case data transfer object
        /// </summary>
        public class TestCaseDto
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
            public List<string> Tags { get; set; }

            /// <summary>
            /// Indicates whether the test case is automated
            /// </summary>
            public bool IsAutomated { get; set; }

            /// <summary>
            /// Date when the test case was created
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// User who created the test case
            /// </summary>
            public string CreatedBy { get; set; }
        }

        /// <summary>
        /// Paged result of test cases
        /// </summary>
        public class PagedTestCasesResult
        {
            /// <summary>
            /// List of test cases
            /// </summary>
            public List<TestCaseDto> TestCases { get; set; }

            /// <summary>
            /// Total count of test cases matching the filter
            /// </summary>
            public int TotalCount { get; set; }

            /// <summary>
            /// Current page number
            /// </summary>
            public int PageNumber { get; set; }

            /// <summary>
            /// Page size
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// Total number of pages
            /// </summary>
            public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

            /// <summary>
            /// Indicates if there is a previous page
            /// </summary>
            public bool HasPreviousPage => PageNumber > 1;

            /// <summary>
            /// Indicates if there is a next page
            /// </summary>
            public bool HasNextPage => PageNumber < TotalPages;
        }

        /// <summary>
        /// Query request with pagination parameters
        /// </summary>
        public class Query : IRequest<Result<PagedTestCasesResult>>
        {
            /// <summary>
            /// Test suite ID to filter by (optional)
            /// </summary>
            public Guid? TestSuiteId { get; set; }

            /// <summary>
            /// Status to filter by (optional)
            /// </summary>
            public TestCaseStatus? Status { get; set; }

            /// <summary>
            /// Priority to filter by (optional)
            /// </summary>
            public TestCasePriority? Priority { get; set; }

            /// <summary>
            /// Search term to filter by title or description (optional)
            /// </summary>
            public string SearchTerm { get; set; }

            /// <summary>
            /// Page number (1-based)
            /// </summary>
            public int PageNumber { get; set; } = 1;

            /// <summary>
            /// Page size
            /// </summary>
            public int PageSize { get; set; } = 10;
        }

        /// <summary>
        /// Handler for the GetTestCases query
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<PagedTestCasesResult>>
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
            /// Handles the GetTestCases query
            /// </summary>
            /// <param name="request">Query request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Result with paged test cases</returns>
            public async Task<Result<PagedTestCasesResult>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    // Build filter expression
                    System.Linq.Expressions.Expression<Func<TestCase, bool>> filterExpression = tc => true;

                    if (request.TestSuiteId.HasValue)
                    {
                        var testSuiteId = request.TestSuiteId.Value;
                        filterExpression = tc => tc.TestSuiteId == testSuiteId;
                    }

                    if (request.Status.HasValue)
                    {
                        var status = request.Status.Value;
                        var previousExpression = filterExpression;
                        filterExpression = tc => previousExpression.Compile()(tc) && tc.Status == status;
                    }

                    if (request.Priority.HasValue)
                    {
                        var priority = request.Priority.Value;
                        var previousExpression = filterExpression;
                        filterExpression = tc => previousExpression.Compile()(tc) && tc.Priority == priority;
                    }

                    if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        var previousExpression = filterExpression;
                        filterExpression = tc => previousExpression.Compile()(tc) && 
                            (tc.Title.ToLower().Contains(searchTerm) || 
                             (tc.Description != null && tc.Description.ToLower().Contains(searchTerm)));
                    }

                    // Get paged results
                    var (testCases, totalCount) = await _unitOfWork.Repository<TestCase>()
                        .GetPagedAsync(
                            filterExpression,
                            tc => tc.CreatedAt,
                            request.PageNumber,
                            request.PageSize,
                            cancellationToken);

                    // Get test suites for the retrieved test cases
                    var testSuiteIds = testCases.Select(tc => tc.TestSuiteId).Distinct().ToList();
                    var testSuites = await _unitOfWork.Repository<TestSuite>()
                        .GetAsync(ts => testSuiteIds.Contains(ts.Id), cancellationToken);

                    // Map to DTOs
                    var testCaseDtos = testCases.Select(tc =>
                    {
                        var testSuite = testSuites.FirstOrDefault(ts => ts.Id == tc.TestSuiteId);
                        return new TestCaseDto
                        {
                            Id = tc.Id,
                            Title = tc.Title,
                            Priority = tc.Priority,
                            Status = tc.Status,
                            TestSuiteId = tc.TestSuiteId,
                            TestSuiteName = testSuite?.Name ?? "Unknown",
                            EstimatedTimeInMinutes = tc.EstimatedTimeInMinutes,
                            Tags = tc.Tags,
                            IsAutomated = tc.IsAutomated,
                            CreatedAt = tc.CreatedAt,
                            CreatedBy = tc.CreatedBy
                        };
                    }).ToList();

                    // Create result
                    var result = new PagedTestCasesResult
                    {
                        TestCases = testCaseDtos,
                        TotalCount = totalCount,
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize
                    };

                    return Result<PagedTestCasesResult>.Success(result);
                }
                catch (Exception ex)
                {
                    return Result<PagedTestCasesResult>.Failure($"Error retrieving test cases: {ex.Message}");
                }
            }
        }
    }
} 