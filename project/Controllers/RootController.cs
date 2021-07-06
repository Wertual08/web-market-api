using System;
using System.Collections.Generic;
using Api.Contexts;
using Api.Models;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Api.Controllers {
    [ApiController]
    [Route("")]
    public class RootController : ControllerBase {
        private readonly ApplicationDbContext DbContext;
        private readonly IElasticClient ElasticClient;

        public RootController(ApplicationDbContext dbContext, IElasticClient elasticClient)
        {
            DbContext = dbContext;
            ElasticClient = elasticClient;
        }

        [HttpGet("shit")]
        public ActionResult<IEnumerable<Product>> Get() {
            var result = ElasticClient.Search<Product>(
                s => s.Index("products").From(0).Size(2000).MatchAll()
            );

            return Ok();
        }
    }
}