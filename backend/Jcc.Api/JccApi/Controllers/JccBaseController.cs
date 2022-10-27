using FluentValidation;
using JccApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JccApi.Controllers
{
    public abstract class JccBaseController : ControllerBase
    {
        protected ApiResult<IEnumerable<string>> GetValidationErrors(ValidationException ex)
        {
            return new ApiResult<IEnumerable<string>>(ex.Errors.Select(e => e.ErrorMessage).Distinct());
        }
    }
}
