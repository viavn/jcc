using JccApi.Application.Abstractions;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Infrastructure.Repository.Abstractions;
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

        public FamiliesController(ICreateFamilyUseCaseAsync createFamilyUseCase)
        {
            _createFamilyUseCase = createFamilyUseCase;
        }

        [HttpGet("{id}")]
        // [ProducesResponseType(typeof(ApiResult<IEnumerable<TypeResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFamily([FromRoute] Guid id)
        {
            return Ok(id);
        }

        [HttpPost]
        // [ProducesResponseType(typeof(ApiResult<IEnumerable<TypeResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyRequest request)
        {
            var guid = await _createFamilyUseCase.Execute(request);
            return CreatedAtAction(nameof(GetFamily), new { id = guid }, request);
        }
    }
}
