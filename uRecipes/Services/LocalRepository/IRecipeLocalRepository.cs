using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uRecipes.Models;

namespace uRecipes.Services.LocalRepository
{
    public interface IRecipeLocalRepository
    {
        event EventHandler<Recipe> OnItemAdded;
        event EventHandler<Recipe> OnItemUpdated;
        event EventHandler<Recipe> OnItemDeleted;

        // Recipe operated methods (async):
        Task <List<Recipe>> GetRecipesAsync();
        Task<List<Category>> GetAllCategories();

        Task<Recipe> GetRecipeAsync(int id);

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
        Task <int> SetIngredients(List<Ingredient> categories, Recipe item);
        Task<List<Ingredient>> GetIngredients(Recipe item);

        // Ingredients GET; SET;
        Task <int> SetInstruction(List<Instruction> categories, Recipe item);
        Task<List<Instruction>> GetInstructions(Recipe item);


        // DEMO METHODS
        Task LoadDemoRecipes();


    }
}
