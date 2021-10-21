using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Domain.Services;
using Api.Authorization;
using Api.Database.Models;

namespace Api.Controllers {
    [ApiController, Route("records")]
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
        public async Task<ActionResult<IEnumerable<RecordResponse>>> PostAsync([FromForm] RecordUploadRequest request) {
            var records = new List<RecordResponse>(request.Files.Count);

            foreach (var file in request.Files) {
                using (var stream = file.OpenReadStream()) {
                    var result = await Service.PostAsync(
                        stream,
                        file.ContentType,
                        file.FileName
                    );

                    records.Add(new RecordResponse(result));
                }
            }

            return Ok(records);
        }

        [HttpDelete("{id}"), Authorize(UserRoleId.Admin)]
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