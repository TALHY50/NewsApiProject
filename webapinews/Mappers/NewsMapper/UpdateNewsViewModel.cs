namespace webapinews.Mappers.NewsMapper
{
    public class UpdateNewsViewModel
    {
        public string Title { get; set; } = null!;

        public string Aurthor { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
