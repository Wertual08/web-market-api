using System;
using System.ComponentModel.DataAnnotations;
using Api.Authorization;

namespace Api.Models {
    public enum OrderStateId {
        Requested,
        Accepted,
        Processed,
        Sent,
        Completed,
        Canceled,
    }
}