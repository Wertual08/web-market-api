using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;
using System;
using Api.Database.Models;

namespace Api.Controllers {
    [ApiController, Route("api/admin/public"), Authorize(UserRoleId.Admin)]
    public class AdminPublicController : ServiceController {
        private readonly AdminPublicService Service;

        public AdminPublicController(AdminPublicService service)
        {
            Service = service;
        }

        [HttpGet("main/slides")]
        public async Task<ActionResult<IEnumerable<RecordResponse>>> GetMainSlidesAsync() {
            var result = await Service.GetMainSlidesAsync();
            return MakeResponse(result, models => from model in models select new RecordResponse(model));
        }

        [HttpPut("main/slides")]
        public async Task<ActionResult<IEnumerable<RecordResponse>>> PostMainSlidesAsync([FromQuery] List<long> records) {
            var result = await Service.UpdateMainSlidesAsync(records);
            return MakeResponse(result, models => from model in models select new RecordResponse(model));
        }
    }
}