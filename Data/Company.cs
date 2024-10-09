namespace Car_Sharing.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
