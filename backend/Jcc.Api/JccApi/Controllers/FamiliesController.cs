using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/families")]
    public class FamiliesController : ControllerBase
    {
        private readonly ICreateFamilyUseCaseAsync _createFamilyUseCase;
        private readonly IUpdateFamilyUseCaseAsync _updateFamilyUseCaseAsync;
        private readonly IGetFamiliesUseCaseAsync _getFamiliesUseCaseAsync;
        private readonly IGetFamilyUseCaseAsync _getFamilyUseCaseAsync;

        public FamiliesController(
            ICreateFamilyUseCaseAsync createFamilyUseCase,
            IUpdateFamilyUseCaseAsync updateFamilyUseCaseAsync,
            IGetFamiliesUseCaseAsync getFamiliesUseCaseAsync,
            IGetFamilyUseCaseAsync getFamilyUseCaseAsync)
        {
            _createFamilyUseCase = createFamilyUseCase;
            _updateFamilyUseCaseAsync = updateFamilyUseCaseAsync;
            _getFamiliesUseCaseAsync = getFamiliesUseCaseAsync;
            _getFamilyUseCaseAsync = getFamilyUseCaseAsync;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<FamilyResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFamilies()
        {
            var families = await _getFamiliesUseCaseAsync.Execute();
            var result = new ApiResult<IEnumerable<FamilyResponse>>(families);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResult<FamilyByIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFamily([FromRoute] Guid id)
        {
            var family = await _getFamilyUseCaseAsync.Execute(id);
            if (family is null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyRequest request)
        {
            try
            {
                var guid = await _createFamilyUseCase.Execute(request);
                return CreatedAtAction(nameof(GetFamily), new { id = guid }, request);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var error = new ApiResult<IEnumerable<string>>(ex.Errors.Select(e => e.ErrorMessage).Distinct());
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFamily([FromRoute] Guid id, [FromBody] UpdateFamilyRequest request)
        {
            try
            {
                request.Id = id;
                await _updateFamilyUseCaseAsync.Execute(request);
                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var error = new ApiResult<IEnumerable<string>>(ex.Errors.Select(e => e.ErrorMessage).Distinct());
                return BadRequest(error);
            }
        }
    }
}
