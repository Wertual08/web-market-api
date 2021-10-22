using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class CartUpdateRequest {
        public Dictionary<long, int> Products { get; set; }
    }
}