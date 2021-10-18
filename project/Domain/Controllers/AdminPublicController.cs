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
    [ApiController, Route("admin/public"), Authorize(UserRoleId.Admin)]
    public class AdminPublicController : ServiceController {
        private readonly AdminPublicService Service;

        public AdminPublicController(AdminPublicService service)
        {
            Service = service;
        }

        [HttpPost("main/slides")]
        public async Task<ActionResult> PostMainSlidesAsync([FromQuery] List<long> records) {
            await Service.UpdateMainSlidesAsync(records);

            return Ok();
        }
    }
}