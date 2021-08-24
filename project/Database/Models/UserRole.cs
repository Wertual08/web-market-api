using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Models {
    public class UserRole {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; init; }
        
        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}