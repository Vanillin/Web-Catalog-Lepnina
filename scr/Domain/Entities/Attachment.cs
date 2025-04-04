namespace Domain.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdProduct { get; set; }
    }
}
