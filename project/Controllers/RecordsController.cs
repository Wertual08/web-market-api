using System;
using System.Linq;
using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories;
using Api.Responses;
using System.Threading.Tasks;
using Api.Requests;
using Api.Authorization;
using Api.Services;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Api.Controllers {
    [ApiController]
    [Route("records")]
    public class RecordsController : ControllerBase {
        private readonly RecordsService Service;

        public RecordsController(RecordsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordResponse>>> GetAsync([FromQuery] int page = 0) {
            var result = await Service.GetAsync(page);

            return Ok(from item in result select new RecordResponse(item));
        }

        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetAsync(string identifier) {
            if (!Guid.TryParse(identifier, out Guid guid)) {
                return UnprocessableEntity();
            }

            var result = await Service.GetAsync(guid);

            if (result is not null) {
                return File(result.Item1, result.Item2.ContentType, result.Item2.Name);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<RecordResponse>> PostAsync([FromForm] RecordUploadRequest request) {
            using (var stream = request.File.OpenReadStream()) {
                var result = await Service.PostAsync(
                    stream,
                    request.File.ContentType,
                    request.File.FileName
                );

                return Ok(new RecordResponse(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RecordResponse>> DeleteAsync(long id) {
            var result = await Service.DeleteAsync(id);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new RecordResponse(result));
            }
        }
    }
}