using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class BulkRequest {
        public List<long> Ids { get; set; }
    }
}