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
        private readonly RecordsService RecordsService;

        public RecordsController(RecordsService recordsService)
        {
            RecordsService = recordsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordResponse>>> GetAsync([FromQuery] int page = 0) {
            var result = await RecordsService.GetAsync(page);

            return Ok(from item in result select new RecordResponse(item));
        }

        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetAsync(string identifier) {
            var result = await RecordsService.GetAsync(identifier);

            if (result is not null) {
                return File(result.Item1, result.Item2);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<RecordResponse>> PostAsync([FromForm] RecordUploadRequest request) {
            using (var stream = request.File.OpenReadStream()) {
                var result = await RecordsService.PostAsync(
                    stream,
                    request.File.ContentType,
                    request.File.FileName
                );

                return Ok(new RecordResponse(result));
            }
        }

        [HttpDelete("{identifier}")]
        public async Task<ActionResult<RecordResponse>> DeleteAsync(string identifier) {
            var result = await RecordsService.DeleteAsync(identifier);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new RecordResponse(result));
            }
        }
    }
}