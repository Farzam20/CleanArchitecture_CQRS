﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }

        public string CreatedByUserId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public IdentityUser CreatedByUser { get; set; }

        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }
    }
}
