namespace webapinews.Mappers.BookMarkMapper
{

    public class BookMarksViewModel
    {
        public List<BookMarksViewModel> BookMarks { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string userName { get; set; }
        public int NewsId { get; set; }
        public string Aurthor { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsBookMark { get; set; }

    }

}
