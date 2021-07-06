using System;
using System.Collections.Generic;
using api.Contexts;
using app.Models;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace api.Controllers {
    [ApiController]
    [Route("")]
    public class RootController : ControllerBase {
        private readonly ApplicationDbContext Context;
        private readonly IElasticClient ElasticClient;

        public RootController(ApplicationDbContext context, IElasticClient elasticClient)
        {
            Context = context;
            ElasticClient = elasticClient;
        }

        [HttpGet("shit")]
        public ActionResult<IEnumerable<Product>> Get() {
            return Ok(ElasticClient.Search<Product>(
                s => s.Index("products").From(0).Size(2000).MatchAll()
            ).Documents);
        }
    }
}