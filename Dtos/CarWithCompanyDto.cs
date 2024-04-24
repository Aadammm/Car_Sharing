

namespace Car_Sharing.Dtos
{
    public class CarWithCompanyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CompanyBasicDto? Company{get;set;}
    }
}
