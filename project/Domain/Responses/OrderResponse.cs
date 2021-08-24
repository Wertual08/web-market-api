using System;
using System.Collections.Generic;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record OrderResponse {
        public long Id { get; init; }
        public string State { get; init; }
        public long? UserId { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Address { get; init; }
        public string PromoCode { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public DateTime RequestedAt { get; init; } 
        public DateTime FinishedAt { get; init; }    

        public OrderResponse(Order model) {
            Id = model.Id;
            State = model.State.ToString();
            UserId = model.UserId;
            Email = model.Email;
            Phone = model.Phone;
            Name = model.Name;
            Surname = model.Surname;
            Address = model.Address;
            PromoCode = model.PromoCode;
            Description = model.Description;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            RequestedAt = model.RequestedAt;
            FinishedAt = model.FinishedAt;
        }
    }
}