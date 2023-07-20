using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Dtos
{
    public class ProductCreateDto : BaseDto 
    {
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}