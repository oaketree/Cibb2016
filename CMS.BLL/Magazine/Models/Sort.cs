namespace CMS.BLL.Magazine.Models
{
    public class Sort
    {
        public int id { get; set; }
        public int afterid { get; set; }
    }
    public class CategorySort
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public int SortID { get; set; }
    }
}
