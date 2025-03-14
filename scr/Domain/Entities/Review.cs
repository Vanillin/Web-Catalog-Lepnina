namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string PathPicture { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
    }
}
