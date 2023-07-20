namespace CleanArchitecture.Application.Dtos
{
    public class ProductDisplayDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string Email { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}
