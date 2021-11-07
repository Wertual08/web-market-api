using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;
using System;

namespace Api.Controllers {
    [ApiController, Route("api/public")]
    public class PublicController : ServiceController {
        private readonly PublicService Service;

        public PublicController(PublicService service)
        {
            Service = service;
        }

        [HttpGet("main/slides")]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync() {
            var result = await Service.GetMainSlidesAsync();

            return MakeResponse(result, models => from item in models select item.Identifier.ToString("N"));
        }
    }
}