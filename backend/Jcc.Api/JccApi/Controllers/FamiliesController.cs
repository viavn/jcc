using FluentValidation;
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
        private readonly IDeleteFamilyUseCaseAsync _deleteFamilyUseCaseAsync;
        private readonly IGetFamiliesUseCaseAsync _getFamiliesUseCaseAsync;
        private readonly IGetFamilyUseCaseAsync _getFamilyUseCaseAsync;
        private readonly ICreateFamilyMemberUseCaseAsync _createFamilyMemberUseCaseAsync;
        private readonly IUpdateFamilyMemberUseCaseAsync _updateFamilyMemberUseCaseAsync;
        private readonly IDeleteFamilyMemberUseCaseAsync _deleteFamilyMemberUseCaseAsync;

        public FamiliesController(
            ICreateFamilyUseCaseAsync createFamilyUseCase,
            IUpdateFamilyUseCaseAsync updateFamilyUseCaseAsync,
            IDeleteFamilyUseCaseAsync deleteFamilyUseCaseAsync,
            IGetFamiliesUseCaseAsync getFamiliesUseCaseAsync,
            IGetFamilyUseCaseAsync getFamilyUseCaseAsync,
            ICreateFamilyMemberUseCaseAsync createFamilyMemberUseCaseAsync,
            IUpdateFamilyMemberUseCaseAsync updateFamilyMemberUseCaseAsync,
            IDeleteFamilyMemberUseCaseAsync deleteFamilyMemberUseCaseAsync)
        {
            _createFamilyUseCase = createFamilyUseCase;
            _updateFamilyUseCaseAsync = updateFamilyUseCaseAsync;
            _deleteFamilyUseCaseAsync = deleteFamilyUseCaseAsync;
            _getFamiliesUseCaseAsync = getFamiliesUseCaseAsync;
            _getFamilyUseCaseAsync = getFamilyUseCaseAsync;
            _createFamilyMemberUseCaseAsync = createFamilyMemberUseCaseAsync;
            _updateFamilyMemberUseCaseAsync = updateFamilyMemberUseCaseAsync;
            _deleteFamilyMemberUseCaseAsync = deleteFamilyMemberUseCaseAsync;
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
                return BadRequest(GetValidationErrors(ex));
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
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFamily([FromRoute] Guid id)
        {
            try
            {
                await _deleteFamilyUseCaseAsync.Execute(id);
                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/members")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMember(Guid id, [FromBody] MemberRequest request)
        {
            try
            {
                request.FamilyId = id;
                request.IsCreating = true;
                request.Id = await _createFamilyMemberUseCaseAsync.Execute(request);
                
                return CreatedAtAction(
                    nameof(GetFamily),
                    new { id = request.Id },
                    new { request.Id, request.FamilyId, request.Name, request.Type }
                );
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpPut("{id}/members/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMember(Guid id, Guid memberId, [FromBody] MemberRequest request)
        {
            try
            {
                request.FamilyId = id;
                request.Id = memberId;
                request.IsCreating = false;
                await _updateFamilyMemberUseCaseAsync.Execute(request);

                return NoContent();
            }
            catch (JccException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(GetValidationErrors(ex));
            }
        }

        [HttpDelete("{id}/members/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid id, [FromRoute] Guid memberId)
        {
            try
            {
                await _deleteFamilyMemberUseCaseAsync.Execute(new DeleteMemberRequest
                {
                    FamilyId = id,
                    MemberId = memberId
                });
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                var errs = new ApiResult<IEnumerable<string>>(new List<string> { ex.Message });
                return BadRequest(errs);
            }
            catch (JccException)
            {
                return NotFound();
            }
        }

        private ApiResult<IEnumerable<string>> GetValidationErrors(ValidationException ex)
        {
            return new ApiResult<IEnumerable<string>>(ex.Errors.Select(e => e.ErrorMessage).Distinct());
        }
    }
}
