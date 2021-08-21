using System;
using Api.Services.Result;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    public class ServiceController : ControllerBase {
        protected ActionResult<O> MakeResponse<O, I>(ServiceResult<I> result, Func<I, O> mapper) {
            return result.Status switch {
                ServiceResultStatus.Success => Ok(mapper(result.Data)),
                ServiceResultStatus.NotFound => NotFound(result.Message),
                ServiceResultStatus.Conflict => Conflict(result.Message),
                ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
                ServiceResultStatus.Forbid => Forbid(result.Message),
                _ => BadRequest(result.Message),
            };
        }
    }
}