using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/god-parents")]
    public class GodParentController : JccBaseController
    {
        private readonly IUpdateGodParentUseCaseAsync _updateGodParentUseCaseAsync;

        public GodParentController(IUpdateGodParentUseCaseAsync updateGodParentUseCaseAsync)
        {
            _updateGodParentUseCaseAsync = updateGodParentUseCaseAsync;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGodParentRequest request)
        {
            try
            {
                request.Id = id;
                await _updateGodParentUseCaseAsync.Execute(request);

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
    }
}
