using SQLite;

namespace uRecipes.Models
{
    [Table("ingredients")]
    public class Ingredients
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string IngredientsJSON { get; set; }
    }
}
