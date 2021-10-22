using System;
using System.Linq;
using System.Collections.Generic;
using Api.Database;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Domain.Services;

namespace Api.Controllers {
    [ApiController, Route("lite/products")]
    public class LiteProductsController : ControllerBase {
        private readonly LiteProductsService Service;

        public LiteProductsController(LiteProductsService service)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiteProductResponse>> GetAsync(long id) {
            var result = await Service.GetAsync(id);

            if (result is not null) {
                return Ok(new LiteProductResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<LiteProductResponse>> GetListAsync(BulkRequest request) {
            var result = await Service.ListAsync(request.Ids);
            return Ok(from model in result select new LiteProductResponse(model));
        }
    }
}