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
        public int InstructionId { get; set; }
        public int IngredientId { get; set; }


    }

    [Table("ingredients")]
    public class Ingredients
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string IngredientsJSON { get; set; }
    }
    public class Ingredient
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Units { get; set; }

    }

    [Table("nutrition")]
    public class NutritionInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Calories { get; set; }
        public string Fat { get; set; }
        public string Carbs { get; set; }
        public string Fiber { get; set; }
        public string Sugar { get; set; }
        public string Protein { get; set; }
    }

    [Table("instruction")]
    public class Instructions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string InstructionsJSON { get; set; }

        /* !!! Not implemented !!! */
        /* !!! Not defined !!! */ 
    }

    public class Instruction
    {
        /* !!! Not implemented !!! */
        /* !!! Not defined !!! */
    }

    [Table("category")]
    public class Category
    {     
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTag { get; set; }
        public string Image { get; set; }
    }

    [Table("recipe_category")]
    public class Recipe_Category
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
    }



}
