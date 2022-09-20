using SQLite;

namespace uRecipes.Models
{
    [Table("recipes")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Uri ImageUrl { get; set; }
        public Uri VideoUrl { get; set; }
        public string PrepTime { get; set; }
        public string PersonServ { get; set; }
        public bool IsCompleted { get; set; }


        public int AuthorId { get; set; }
        public int NutritionId { get; set; }
        public int IngredientId { get; set; }

    }
}
