namespace webapinews.Mappers.NewsMapper
{
    public class AddNewsViewModel
    {
        public string Title { get; set; } = null!;

        public string Aurthor { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }
}
