using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<TypeResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var types = await _genreRepository.GetAll();
            var result = new ApiResult<IEnumerable<TypeResponse>>(types.Select(t => new TypeResponse(t.Id, t.Description)));
            return Ok(result);
        }
    }
}
