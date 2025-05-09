using System;
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
    /// Command to delete a test case
    /// </summary>
    public class DeleteTestCase
    {
        /// <summary>
        /// Command request with test case ID to delete
        /// </summary>
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// ID of the test case to delete
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Handler for the DeleteTestCase command
        /// </summary>
        public class Handler : IRequestHandler<Command, Result>
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
            /// Handles the DeleteTestCase command
            /// </summary>
            /// <param name="request">Command request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Result of the delete operation</returns>
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Get existing test case
                    var testCase = await _unitOfWork.Repository<TestCase>()
                        .GetByIdAsync(request.Id, cancellationToken);

                    if (testCase == null)
                    {
                        return Result.Failure($"Test case with ID {request.Id} not found");
                    }

                    // Soft delete the test case
                    await _unitOfWork.Repository<TestCase>().SoftDeleteAsync(testCase, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result.Success("Test case deleted successfully");
                }
                catch (Exception ex)
                {
                    return Result.Failure($"Error deleting test case: {ex.Message}");
                }
            }
        }
    }
} 