namespace Car_Sharing.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required int Company_Id { get; set; }
        public int? Customer_Id { get; set; }
    }
}
