namespace Car_Sharing.Dto
{
    public class CompanyWithCarsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<CarDto>? Cars { get; set; }

    }
}
