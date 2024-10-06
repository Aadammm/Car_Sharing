namespace Car_Sharing.Dto
{
    public class CarDtoWithCompanyAndCustomer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       
        public CompanyDto Company { get; set; }

        public CustomerDto? Customer { get; set; }
    }
}
