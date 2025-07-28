namespace ABCCarTraders.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
    }

    public enum CategoryType
    {
        Car,
        Part
    }
}