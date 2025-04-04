namespace Application.Dto
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdProduct { get; set; }
    }
}
