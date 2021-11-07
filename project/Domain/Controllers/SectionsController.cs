using System;
using System.Linq;
using System.Collections.Generic;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;

namespace Api.Controllers {
    [ApiController, Route("api/sections")]
    public class SectionsController : ControllerBase {
        private readonly SectionsService Service;

        public SectionsController(SectionsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionResponse>>> GetAsync() {
            var result = await Service.GetAsync();

            return Ok(from item in result select new SectionResponse(item));
        }
    }
}