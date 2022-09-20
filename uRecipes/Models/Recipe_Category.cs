using SQLite;

namespace uRecipes.Models
{
    [Table("recipe_category")]
    public class Recipe_Category
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
    }
}
