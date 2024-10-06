namespace Car_Sharing.Dto
{
    public class CustomerDtoWithCar
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public CarDto? Car { get; set; }
    }
}
