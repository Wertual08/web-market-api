using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Domain.Services;

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

            var (stream, record) = await Service.GetAsync(guid);

            if (stream is not null) {
                return File(stream, record.ContentType, record.Name);
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