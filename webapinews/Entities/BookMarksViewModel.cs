namespace webapinews.Entities
{
    
        public class BookMarksViewModel
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public int NewsId { get; set; }
            public string Aurthor { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsBookMark { get; set; }

        }
    
}
