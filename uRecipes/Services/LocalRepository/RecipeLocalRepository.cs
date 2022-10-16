using SQLite;
using System.IO;
using System.Linq;
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


        private async Task CreateConnection()
        {
            if (connection != null)
                return;

            var documentPath = Environment.GetFolderPath(
                               Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "myrecipes.db");

            connection = new SQLiteAsyncConnection(databasePath);

            await connection.CreateTablesAsync<Recipe, Category, Recipe_Category>();
            await connection.CreateTablesAsync<NutritionInfo, Ingredients, Instructions>();

            await InintializeCategories();
        }
        private async Task InintializeCategories()
        {
            if (categories != null)
                return;

            categories = await connection.Table<Category>().ToListAsync();

            if(categories.Count == 0)
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("categories.json");
                using var reader = new StreamReader(stream);

                var contents = await reader.ReadToEndAsync();
                categories = JsonSerializer.Deserialize<List<Category>>(contents);

                await connection.InsertAllAsync(categories);
            }
        }
        public async Task<int> AddItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
            return item.Id;
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
        public async Task<Recipe> GetRecipeAsync(int id)
        {
            if (id <= 0) throw new Exception("SQLite DB:(recipes): Invalid recipe ID");

            await CreateConnection();
            return await connection.Table<Recipe>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            await CreateConnection();

            return await connection.Table<Recipe>().ToListAsync();
        }
        public async Task<List<Recipe>> GetRecipesByCategories(List<Category> categories)
        {
            await CreateConnection();

            List<Recipe> recipes = new List<Recipe>();
            List<Recipe_Category> rcp_ctg = new List<Recipe_Category>();

            foreach (Category item in categories)
                rcp_ctg.AddRange(
                    await connection.Table<Recipe_Category>().Where(x => x.CategoryId == item.Id).ToListAsync());

            recipes = await connection.Table<Recipe>().ToListAsync();

            recipes = new(recipes.Join(rcp_ctg, r => r.Id, rc => rc.RecipeId, (r, rc) => new Recipe
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Author = r.Author,
                ImageUrl = r.ImageUrl,
                VideoUrl = r.VideoUrl,
                TotalTime = r.TotalTime,
                PersonServ = r.PersonServ,
                IsCompleted = r.IsCompleted,

                AuthorId = r.AuthorId,
                NutritionId = r.NutritionId,
                IngredientId = r.IngredientId,
            }));


            return recipes;

        }
        public async Task RemoveItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();

            await connection.Table<Recipe_Category>().DeleteAsync(x => x.RecipeId == item.Id);

            if(item.IngredientId != 0)
                await connection.Table<Ingredients>().DeleteAsync(x => x.Id == item.IngredientId);
            if(item.NutritionId != 0)
                await connection.Table<NutritionInfo>().DeleteAsync(x => x.Id == item.NutritionId);
            if(item.InstructionId != 0)
                await connection.Table<Instructions>().DeleteAsync(x => x.Id == item.InstructionId);

            await connection.DeleteAsync(item);
            OnItemDeleted?.Invoke(this, item);
        }
        public async Task UpdateItem(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }


        public async Task<List<Category>> GetAllCategories()
        {
            await CreateConnection();

            return categories;
        }
        public async Task<List<Category>> GetCategoriesByTag(string tag)
        {
            await CreateConnection();

            return categories.FindAll(x => x.CategoryTag.Equals(tag));
        }
        public async Task SetCategories(List<Category> categories, Recipe item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            await CreateConnection();

            //if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
            //    throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

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


            List<Recipe_Category> recipe_categories =
                await connection.Table<Recipe_Category>().Where(x => x.RecipeId == item.Id).ToListAsync();

            if (recipe_categories.Count == 0)
                return new List<Category>();

            List<Category> result = new(categories.Join(
                recipe_categories, c => c.Id, rc => rc.CategoryId,
                (c, rc) => new Category
                {
                    Id = rc.CategoryId,
                    CategoryName = c.CategoryName,
                    CategoryTag = c.CategoryTag,
                    Image = c.Image,
                }));

            return result;
        }



        public async Task<int> SetNutrition(NutritionInfo nutrition, Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (nutrition == null) throw new ArgumentNullException(nameof(nutrition));

            await CreateConnection();

            //if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
            //    throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

            await connection.InsertAsync(nutrition);

            item.NutritionId = nutrition.Id;

            await UpdateItem(item);

            return nutrition.Id;
        }
        public async Task<NutritionInfo> GetNutrition(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.NutritionId == 0) return null;

            await CreateConnection();

            return await connection.Table<NutritionInfo>()
                .FirstOrDefaultAsync(x => x.Id == item.NutritionId);
        }


        public async Task<int> SetIngredients(List<Ingredient> ingredients, Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (ingredients == null) throw new ArgumentNullException(nameof(ingredients));

            await CreateConnection();

            //if (await connection.Table<Recipe>().ElementAtAsync(item.Id) == null)
            //    throw new Exception("SQLite DB:(Recipe): Recipe item is not found in DB");

            Ingredients res = new Ingredients
            {
                IngredientsJSON = JsonSerializer.Serialize(ingredients),
            };

            await connection.InsertAsync(res);

            item.IngredientId = res.Id;

            await UpdateItem(item);

            return item.IngredientId;
        }
        public async Task<List<Ingredient>> GetIngredients(Recipe item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.IngredientId == 0) return null;

            await CreateConnection();

            Ingredients ing = await connection.Table<Ingredients>().
                Where(x => x.Id == item.IngredientId).FirstOrDefaultAsync();

            return new List<Ingredient>(JsonSerializer.Deserialize<List<Ingredient>>(ing.IngredientsJSON));
        }


        public async Task<int> SetInstruction(List<Instruction> instructions, Recipe item)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Instruction>> GetInstructions(Recipe item)
        {
            throw new NotImplementedException();
        }



        // Demo methods:

        public async Task LoadDemoRecipes()
        {
            await CreateConnection();

            if (await connection.Table<Recipe>().CountAsync() != 0)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("recipes_demo.json");
            using var reader = new StreamReader(stream);

            var contents = await reader.ReadToEndAsync();
            List<Recipe> demoRecipes = JsonSerializer.Deserialize<List<Recipe>>(contents);

            demoRecipes.ForEach(async recipe => 
            {
                await AddItem(recipe);
            });

            await AsignRandIngredients(demoRecipes, 10);
            await AsignRandNutritions(demoRecipes);
        }


        private async Task AsignRandIngredients(List <Recipe> recipes, int count)
        {
            if (recipes == null) throw new ArgumentNullException(nameof(recipes));
            if (count <= 0 || count > 40) return;

            await CreateConnection();

            using var stream = await FileSystem.OpenAppPackageFileAsync("ingredients_demo.json");
            using var reader = new StreamReader(stream);
            
            var contents = await reader.ReadToEndAsync();
            List<Ingredient> demoIngredients = JsonSerializer.Deserialize<List<Ingredient>>(contents);


            Random random = new Random();

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (Recipe recipe in recipes)
            {
                ingredients.Clear();

                for(int i = 0; i < count; i++)
                    ingredients.Add(demoIngredients[random.Next(demoIngredients.Count)]);

                await SetIngredients(ingredients, recipe);
            }

            
        }
        private async Task AsignRandNutritions(List<Recipe> recipes)
        {
            if (recipes == null) throw new ArgumentNullException(nameof(recipes));

            Random rand_int = new();

            foreach(Recipe recipe in recipes)
            {
                NutritionInfo nutrition = new NutritionInfo
                {
                    Calories_kcal = rand_int.Next(50, 600),
                    Energy_kJ = rand_int.Next(50, 500),
                    Fat_g = rand_int.Next(0, 80),
                    Carbohydrate_g = rand_int.Next(0, 80),
                    Sugar_g = rand_int.Next(0, 20),
                    Fiber_g = rand_int.Next(0, 20),
                    Protein_g = rand_int.Next(0, 80),
                    Cholesterol_mg = rand_int.Next(0,250),
                    Sodium_mg = rand_int.Next(0,600)
                };

                await SetNutrition(nutrition, recipe);
            }
        }
    }

}
