namespace Application.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string? PathPicture { get; set; }
        public int IdUser { get; set; }
        public int? IdProduct { get; set; }
    }
}
