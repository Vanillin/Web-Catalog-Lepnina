namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int IdPicture { get; set; }
        public int IdSection { get; set; }
    }
}
