namespace Car_Sharing.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required int Company_Id { get; set; }
        public Company Company { get; set; }
        public int? Customer_Id { get; set; }

        public Customer? Customer { get; set; }
    }
}
