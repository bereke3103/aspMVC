namespace BulkyBookWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DisplayOrder { get; set; }

        public DateTime CreateedDateTime { get; set; } = DateTime.Now;
    }
}
