using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using TestMaster.Core.TestManagement.Commands;
using TestMaster.Core.TestManagement.Entities;
using TestMaster.Core.TestManagement.Queries;

namespace TestMaster.Api.Controllers.V1
{
    /// <summary>
    /// Controller for test case operations
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestCasesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TestCasesController> _logger;

        /// <summary>
        /// Initializes a new instance of the controller
        /// </summary>
        /// <param name="mediator">Mediator for sending commands and queries</param>
        /// <param name="logger">Logger</param>
        public TestCasesController(IMediator mediator, ILogger<TestCasesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new test case
        /// </summary>
        /// <param name="command">Create test case command</param>
        /// <returns>ID of the created test case</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTestCase.Command command)
        {
            try
            {
                _logger.LogInformation("Creating new test case with title: {Title}", command.Title);
                
                var result = await _mediator.Send(command);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to create test case: {Message}", result.Message);
                    return BadRequest(result);
                }

                _logger.LogInformation("Test case created successfully with ID: {Id}", result.Data);
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test case");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the test case");
            }
        }

        /// <summary>
        /// Gets a test case by ID
        /// </summary>
        /// <param name="id">Test case ID</param>
        /// <returns>Test case details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation("Getting test case with ID: {Id}", id);
                
                var query = new GetTestCaseById.Query { Id = id };
                var result = await _mediator.Send(query);
                
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Test case not found with ID: {Id}", id);
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test case with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the test case");
            }
        }

        /// <summary>
        /// Gets a paged list of test cases with optional filtering
        /// </summary>
        /// <param name="testSuiteId">Test suite ID to filter by (optional)</param>
        /// <param name="status">Status to filter by (optional)</param>
        /// <param name="priority">Priority to filter by (optional)</param>
        /// <param name="searchTerm">Search term to filter by title or description (optional)</param>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paged list of test cases</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid? testSuiteId = null,
            [FromQuery] TestCaseStatus? status = null,
            [FromQuery] TestCasePriority? priority = null,
            [FromQuery] string searchTerm = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Getting test cases with filters: TestSuiteId={TestSuiteId}, Status={Status}, Priority={Priority}, SearchTerm={SearchTerm}, Page={Page}, PageSize={PageSize}", 
                    testSuiteId, status, priority, searchTerm, pageNumber, pageSize);
                
                var query = new GetTestCases.Query
                {
                    TestSuiteId = testSuiteId,
                    Status = status,
                    Priority = priority,
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test cases");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving test cases");
            }
        }

        /// <summary>
        /// Updates an existing test case
        /// </summary>
        /// <param name="id">Test case ID</param>
        /// <param name="command">Update test case command</param>
        /// <returns>Result of the update operation</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTestCase.Command command)
        {
            try
            {
                if (id != command.Id)
                {
                    _logger.LogWarning("ID mismatch: URL ID={UrlId}, Body ID={BodyId}", id, command.Id);
                    return BadRequest("ID in the URL must match the ID in the request body");
                }

                _logger.LogInformation("Updating test case with ID: {Id}", id);
                
                var result = await _mediator.Send(command);
                if (!result.IsSuccess)
                {
                    if (result.Message.Contains("not found"))
                    {
                        _logger.LogWarning("Test case not found with ID: {Id}", id);
                        return NotFound(result);
                    }
                    
                    _logger.LogWarning("Failed to update test case: {Message}", result.Message);
                    return BadRequest(result);
                }

                _logger.LogInformation("Test case updated successfully with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test case with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the test case");
            }
        }

        /// <summary>
        /// Deletes a test case
        /// </summary>
        /// <param name="id">Test case ID</param>
        /// <returns>Result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting test case with ID: {Id}", id);
                
                var command = new DeleteTestCase.Command { Id = id };
                var result = await _mediator.Send(command);
                
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Test case not found with ID: {Id}", id);
                    return NotFound(result);
                }

                _logger.LogInformation("Test case deleted successfully with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test case with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the test case");
            }
        }
    }
} 