namespace Car_Sharing.Dto
{
    public class CustomerDtoEdit
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? Rented_Car_Id { get; set; }
    }
}
