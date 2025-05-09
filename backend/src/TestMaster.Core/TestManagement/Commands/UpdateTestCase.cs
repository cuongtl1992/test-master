using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestMaster.Core.Common;
using TestMaster.Core.Common.Interfaces;
using TestMaster.Core.TestManagement.Entities;

namespace TestMaster.Core.TestManagement.Commands
{
    /// <summary>
    /// Command to update an existing test case
    /// </summary>
    public class UpdateTestCase
    {
        /// <summary>
        /// Command request with updated test case data
        /// </summary>
        public class Command : IRequest<Result<Guid>>
        {
            /// <summary>
            /// ID of the test case to update
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
        /// Validator for the UpdateTestCase command
        /// </summary>
        public class Validator : AbstractValidator<Command>
        {
            /// <summary>
            /// Initializes a new instance of the validator
            /// </summary>
            public Validator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Test case ID is required");

                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title is required")
                    .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

                RuleFor(x => x.Steps)
                    .NotEmpty().WithMessage("Steps are required");

                RuleFor(x => x.ExpectedResults)
                    .NotEmpty().WithMessage("Expected results are required");

                RuleFor(x => x.TestSuiteId)
                    .NotEmpty().WithMessage("Test suite ID is required");

                RuleFor(x => x.EstimatedTimeInMinutes)
                    .GreaterThan(0).WithMessage("Estimated time must be greater than 0");

                RuleFor(x => x.AutomationReference)
                    .NotEmpty().When(x => x.IsAutomated)
                    .WithMessage("Automation reference is required when the test case is automated");
            }
        }

        /// <summary>
        /// Handler for the UpdateTestCase command
        /// </summary>
        public class Handler : IRequestHandler<Command, Result<Guid>>
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
            /// Handles the UpdateTestCase command
            /// </summary>
            /// <param name="request">Command request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Result with the ID of the updated test case</returns>
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Get existing test case
                    var testCase = await _unitOfWork.Repository<TestCase>()
                        .GetByIdAsync(request.Id, cancellationToken);

                    if (testCase == null)
                    {
                        return Result<Guid>.Failure($"Test case with ID {request.Id} not found");
                    }

                    // Verify test suite exists
                    var testSuiteExists = await _unitOfWork.Repository<TestSuite>()
                        .ExistsAsync(ts => ts.Id == request.TestSuiteId, cancellationToken);

                    if (!testSuiteExists)
                    {
                        return Result<Guid>.Failure($"Test suite with ID {request.TestSuiteId} not found");
                    }

                    // Update test case properties
                    testCase.Title = request.Title;
                    testCase.Description = request.Description;
                    testCase.Steps = request.Steps;
                    testCase.ExpectedResults = request.ExpectedResults;
                    testCase.PreConditions = request.PreConditions;
                    testCase.Priority = request.Priority;
                    testCase.Status = request.Status;
                    testCase.TestSuiteId = request.TestSuiteId;
                    testCase.EstimatedTimeInMinutes = request.EstimatedTimeInMinutes;
                    testCase.Tags = request.Tags;
                    testCase.IsAutomated = request.IsAutomated;
                    testCase.AutomationReference = request.AutomationReference;

                    await _unitOfWork.Repository<TestCase>().UpdateAsync(testCase, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result<Guid>.Success(testCase.Id, "Test case updated successfully");
                }
                catch (Exception ex)
                {
                    return Result<Guid>.Failure($"Error updating test case: {ex.Message}");
                }
            }
        }
    }
} 