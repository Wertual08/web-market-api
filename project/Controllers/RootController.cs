using System;
using System.Collections.Generic;
using api.Database;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers {
    [ApiController]
    [Route("")]
    public class RootController : ControllerBase {
        private readonly ApplicationDbContext Context;

        public RootController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> Get() {
            //Context.Database.Migrate();

            return new Dictionary<string, string> {
                { "database", "data" },
                { "jopa", "omg" },
                { "key", "value" },
            };
        }
    }
}