using System.Collections.Generic;
using Nest;

namespace Api.FullTextSearch.Models {
    public class FTSProduct {
        public long Id { get; set; }
        [Keyword]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal Price { get; set; } 
        public string Image { get; set; } 
        public List<long> Categories { get; set; }
        public List<long> Sections { get; set; } 
    }
}