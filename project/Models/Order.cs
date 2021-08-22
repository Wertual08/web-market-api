using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Authorization;

namespace Api.Models {
    public class Order {
        [Key]
        public long Id { get; init; }

        public OrderStateId State { get; set; }

        public long? UserId { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;    

        public DateTime RequestedAt { get; set; }    

        public DateTime FinishedAt { get; set; }   

        [Required]
        public string Email { get; set; } 

        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Address { get; set; }

        public string PromoCode { get; set; }

        public string Description { get; set; }


        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}