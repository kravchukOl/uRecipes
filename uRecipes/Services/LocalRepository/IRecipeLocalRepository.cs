namespace uRecipes.Services.LocalRepository
{
    public interface IRecipeLocalRepository
    {
        event EventHandler<Recipe> OnItemAdded;
        event EventHandler<Recipe> OnItemUpdated;
        event EventHandler<Recipe> OnItemDeleted;

        Task <List<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(int id);
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> GetCategoriesByTag(string tag);

        Task <int> AddItem (Recipe item);
        Task <int> AddItem
            (Recipe item, List<Category> categories = null, NutritionInfo nutrition = null,
            List<Ingredient> ingredients = null, List<Instruction> instructions = null );

        Task UpdateItem(Recipe item);
        Task RemoveItem(Recipe item);
        Task AddOrUpdate(Recipe item);


        // Categories GET; SET;
        Task SetCategories(List<Category> categories, Recipe item);
        Task<List<Category>> GetCategories(Recipe item);

        // Nutrition GET; SET;
        Task <int> SetNutrition(NutritionInfo nutrition, Recipe item);
        Task<NutritionInfo> GetNutrition(Recipe item);

        // Ingredients GET; SET;
        Task <int> SetIngredients(List<Ingredient> ingredients, Recipe item);
        Task<List<Ingredient>> GetIngredients(Recipe item);

        // Instructions GET; SET;
        //Task<int> SetInstruction(List<Instruction> instruction, Recipe item);
        //Task<List<Instruction>> GetInstructions(Recipe item);


        // DEMO METHODS
        Task LoadDemoRecipes();
    }
}
