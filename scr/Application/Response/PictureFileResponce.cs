namespace Application.Response
{
    public class PictureFileResponce
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string StoredPath { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long Size { get; set; }
    }
}