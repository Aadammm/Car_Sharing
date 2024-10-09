namespace Car_Sharing.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? Rented_Car_Id { get; set; }
        public Car? Car { get; set; }  
    }
}

