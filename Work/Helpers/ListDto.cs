namespace Work.Helpers
{
    public class ListDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
