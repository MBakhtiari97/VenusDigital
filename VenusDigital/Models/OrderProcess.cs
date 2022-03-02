using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VenusDigital.Models
{
    public class OrderProcess
    {
        [Required]
        public int OrderProcessId { get; set; }
        [Required]
        public bool IsProcessed { get; set; }
        [Required]
        public bool IsDelivered { get; set; }
        [Required]
        public bool IsReferred { get; set; }

        public string? ReferredDescription { get; set; }
        [Required]
        public int OrderId { get; set; }

        //Nav
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
