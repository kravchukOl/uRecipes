using SQLite;

namespace uRecipes.Models
{
    [Table("category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTag { get; set; }
        public string Image { get; set; }
    }
}
