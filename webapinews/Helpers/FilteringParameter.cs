namespace webapinews.Helpers.Paging
{
    public class FilteringParameter 
    { 
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; } = DateTime.Now;
    }
}
