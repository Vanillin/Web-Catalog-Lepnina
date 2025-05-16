namespace Domain.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int IdPicture { get; set; }
        public int IdProduct { get; set; }
    }
}
