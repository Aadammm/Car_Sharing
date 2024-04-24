

namespace Car_Sharing.Dtos
{
    public class CompanyWithCarsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<CarBasicDto>? CompanyCars { get; set; }

    }
}
