﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsFinally { get; set; }
        
        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}