using Android.Webkit;
using SQLite;
using uRecipes.Models;

namespace uRecipes.Services.LocalRepository
{
    public class RecipeLocalRepository : IRecipeLocalRepository
    {
        private SQLiteAsyncConnection connection;

        private List<Category> categories;

        public event EventHandler<Recipe> OnItemAdded;
        public event EventHandler<Recipe> OnItemUpdated;
        public event EventHandler<Recipe> OnItemDeleted;



        // Create connection to SQLite DB
        private async Task CreateConnection()
        {
            if (connection != null)
                return;

            var documentPath = Environment.GetFolderPath(
                               Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "myrecipes.db");

            connection = new SQLiteAsyncConnection(databasePath);

            await connection.CreateTablesAsync
                <Recipe, Recipe_Category, NutritionInfo, Ingredients, Instructions>();
        }
        // Initializes Category's List 
        // by reading from file categories.json
        private async Task InintializeCategories()
        {
            if (categories != null)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("categories.json");
            using var reader = new StreamReader(stream);

            var contents = await reader.ReadToEndAsync();
            categories = JsonSerializer.Deserialize<List<Category>>(contents);
        }


        // Add new Recipe object to SQLite DB
        public async Task<int> AddItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            int id;
            await CreateConnection();
            id = await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
            return id;
        }

        // Add new Recipe object to SQLite DB and attaches:
        // cattegories, nutrition info, ingredients, instructions
        public async Task<int> AddItem(Recipe item, List<Category> categories)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (categories == null) throw new ArgumentNullException(nameof(categories));

            int recipeId;

            recipeId = await AddItem(item);

            foreach (var entry in categories)
                await connection.InsertAsync(
                    new Recipe_Category { RecipeId = recipeId, CategoryId = entry.Id });

            return recipeId;
        }

        public async Task<int> AddItem(
            Recipe item, List<Category> categories = null, NutritionInfo nutrition = null,
            List<Ingredient> ingredients = null, List<Instruction> instructions = null)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (categories == null) throw new ArgumentNullException(nameof(categories));

            int recipeId;

            recipeId = await AddItem(item);

            if (categories != null)
                foreach (var entry in categories)
                    await connection.InsertAsync(
                        new Recipe_Category { RecipeId = recipeId, CategoryId = entry.Id });

            // Nutrition Info setting is not implementated

            // Ingredidints setting is not implementated

            // Instruction setting is not implementated

            return recipeId;
        }


        // Updates existing DB records or adds new one 
        public async Task AddOrUpdate(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (item.Id == 0)
            {
                await AddItem(item);
                OnItemAdded?.Invoke(this, item);
            }
            else
            {
                await UpdateItem(item);
            }

        }


        // Retrives Recipe instance from DB by it's ID
        public async Task<Recipe> GetRecipeAsync(int id)
        {
            if (id <= 0) throw new Exception("SQLite DB:(recipes): Invalid recipe ID");

            await CreateConnection();
            return await connection.Table<Recipe>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        // Retrives List of Recipes from DB 
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            await CreateConnection();

            return await connection.Table<Recipe>().ToListAsync();
        }

        // Returns List of loaded categories
        public async Task<List<Category>> GetAllCategories()
        {
            await InintializeCategories();

            return categories;
        }


        // Removes Recipe from DB
        public async Task RemoveItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();
            await connection.DeleteAsync(item);
            OnItemDeleted?.Invoke(this, item);
        }

        // Updates Recipe record in DB
        public async Task UpdateItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }



        // Retrives List of categories by selected Category Tag
        public async Task<List<Category>> GetCategoriesByCategoryTag(string tag)
        {
            await InintializeCategories();

            return categories.FindAll(x => x.CategoryTag.Equals(tag.ToString()));
        }


        // Translates list of records (RecipeID-CategoryID) to list of Categories
        private async Task<List<Category>> TranslateToCategories(List<Recipe_Category> entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            if (entry.Count == 0) return new List<Category>();

            await InintializeCategories();

            List<Category> catList = new();

            foreach (var item in entry)
            {
                Category category = categories.Find(x => x.Id == item.CategoryId);

                if (category.CategoryName == null)
                    throw new Exception("SQLite DB:(recipe_category): Invalid category Id");

                catList.Add(category);
            }
            return catList;
        }


        public async Task SetCategories(List<Category> categories, Recipe item)
        {
            if (categories is null) throw new ArgumentNullException(nameof(categories));
            if (categories.Count == 0) return;
            if (item is null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();

            if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
                throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

            foreach (Category category in categories)
                await connection.InsertAsync(
                    new Recipe_Category() { RecipeId = item.Id, CategoryId = category.Id });
        }

        public async Task<List<Category>> GetCategories(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();

            if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
                throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

            List<Recipe_Category> recipe_categories;

            recipe_categories = await connection.Table<Recipe_Category>()
                .Where((x) => x.RecipeId == item.Id).ToListAsync();

            if (recipe_categories.Count == 0)
                return new List<Category>();

            return await TranslateToCategories(recipe_categories);
        }

        public async Task<int> SetNutrition(NutritionInfo nutrition, Recipe item)
        {
            if(item == null) throw new ArgumentNullException(nameof(item));
            if(nutrition == null) throw new ArgumentNullException(nameof(nutrition));

            await CreateConnection();

            if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
                throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

            int nutritonId = await connection.InsertAsync(nutrition);

            item.NutritionId = nutritonId;

            await UpdateItem(item);

            return nutritonId;
        }

        public async Task<NutritionInfo> GetNutrition(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.NutritionId == 0) return null;

            await CreateConnection();

            return await connection.Table<NutritionInfo>()
                .FirstOrDefaultAsync(x => x.Id == item.NutritionId);
        }

        public async Task<int> SetIngredients(List<Ingredient> categories, Recipe item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ingredient>> GetIngredients(Recipe item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SetInstruction(List<Instruction> categories, Recipe item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Instruction>> GetInstructions(Recipe item)
        {
            throw new NotImplementedException();
        }

        // If Recipe Table is empty, Populates it with Recipes from recipes_demo.json
        public async Task LoadDemoRecipes()
        {
            await CreateConnection();

            if (await connection.Table<Recipe>().CountAsync() != 0)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("recipes_demo.json");
            using var reader = new StreamReader(stream);

            var contents = await reader.ReadToEndAsync();
            List<Recipe> demoRecipes = JsonSerializer.Deserialize<List<Recipe>>(contents);

            demoRecipes.ForEach(async recipe => await AddItem(recipe));
        }
    }

}
