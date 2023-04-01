using System.Text;
using uRecipes.Services.CloudRepository;
using uRecipes.Services.LocalisationSevice;
using uRecipes.Services.LocalRepository;

namespace uRecipes.ViewModels
{
    [QueryProperty(nameof(RecipeItem), "Recipe")]
    public partial class RecipeViewModel : BaseViewModel
    {
        private IRecipeLocalRepository localRepository;
        private IRecipeCloudRepository cloudRepository;

        private IConnectivity connectivity;

        public Recipe RecipeItem { get; set; }

        [ObservableProperty]
        string name;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        string author;

        [ObservableProperty]
        string totalTime;
        [ObservableProperty]
        string personServ;

        [ObservableProperty]
        string allergens;

        [ObservableProperty]
        NutritionInfo nutritionInfo;

        [ObservableProperty]
        string imageUrl;

        [ObservableProperty]
        ImageSource imageSource;

        [ObservableProperty]
        string videoUrl;

        [ObservableProperty]
        bool isCompleted;

        [ObservableProperty]
        bool isOffline;

        public int AuthorId { get; private set; }

        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<Instruction> Instructions { get; private set; }
        public ObservableCollection <Ingredient> Ingredients { get; private set; }



        public RecipeViewModel(IRecipeLocalRepository localRepository, IConnectivity connectivity,
            ILocalizationResourceManager localization, INavigator navigator)
        {
            isDesignMode = false;
            isOffline = false;
            pageNavigator = navigator;
            this.localization= localization;

            this.localRepository = localRepository;
            //this.cloudRepository = cloudRepository;
            this.connectivity = connectivity;

            Categories = new ObservableCollection<Category>();
            Ingredients = new ObservableCollection<Ingredient>();
            Instructions = new ObservableCollection<Instruction>();
        }

        [RelayCommand]
        public override async Task Initialise()
        {
            try
            {
                if (RecipeItem is null)
                    throw new Exception("Unable to load recipe data");

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    IsOffline = true;

                    await pageNavigator.MakeToast("You have no internet connection!!!");
                }

                Name = RecipeItem.Name;
                Description = RecipeItem.Description;
                Author = RecipeItem.Author;
                AuthorId = RecipeItem.AuthorId;


                ImageUrl = RecipeItem.ImageUrl.AbsoluteUri;
                ImageSource =  ImageSource.FromUri(RecipeItem.ImageUrl);

                VideoUrl = RecipeItem.VideoUrl.AbsoluteUri;


                TotalTime = RecipeItem.TotalTime;
                PersonServ = RecipeItem.PersonServ;
                isCompleted = RecipeItem.IsCompleted;

                //await GetCategories();
                await GetIngredients();
                await GetNutrition();
                //await GetInstructions();

                Allergens = GetAlergenString();

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                
            }
        }

        private async Task GetNutrition()
        {
            NutritionInfo = await localRepository.GetNutrition(RecipeItem);
        }

        private async Task GetCategories()
        {
            throw new NotImplementedException();
        }

        private async Task GetInstructions()
        {
            throw new NotImplementedException();
        }

        private async Task GetIngredients()
        {
            IsBusy = true;

            List<Ingredient> ingredients = await localRepository.GetIngredients(RecipeItem);
           
            foreach (Ingredient ingredient in ingredients)
                    Ingredients.Add(ingredient);

            IsBusy = false;
        }

        [RelayCommand]
        public async Task ShowRecipesByAuthor(int authorId)
        {
            try
            {
                await pageNavigator.MakeToast("This command in not implemented");
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        [RelayCommand]
        public async Task AddToFavorites()
        {
            await pageNavigator.MakeToast("This command is still not implemented yet");
        }

        [RelayCommand]
        public async Task ShareRecipe()
        {
            await pageNavigator.MakeToast("This command is still not implemented yet");
        }

        protected string GetAlergenString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            HashSet<String> allergensHashSet = new HashSet<String>();

            foreach (var item in Ingredients)
            {
                if(item.Allergens is object)
                {
                    String[] input = item.Allergens.Split(", ");
                    foreach (var inputItem in input) 
                    {
                        if(!allergensHashSet.Contains(inputItem))
                            allergensHashSet.Add(inputItem);
                    }
                }
            }

            String[] allergensArray = allergensHashSet.ToArray();

            foreach(var item in allergensArray)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();

        }


        [RelayCommand]
        public async Task GoBack() => await pageNavigator.GoBackward();

        // Design mode Commands:
        [RelayCommand]
        private void EnableDesignMode()
        {
            IsDesignMode = !isDesignMode;

            if (IsDesignMode)
                pageNavigator.MakeToast("Design mode has been Enabled");
            else
                pageNavigator.MakeToast("Design mode has been Disabled");
        }

        [RelayCommand]
        private async Task SwithcIsCompleted()
        {
            IsCompleted = !IsCompleted;
            await localRepository.UpdateItem(RecipeItem);
        }

    }
}
