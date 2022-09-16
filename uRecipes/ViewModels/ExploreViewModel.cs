using AndroidX.Navigation;
using uRecipes.Services.LocalRepository;
using uRecipes.Views;

namespace uRecipes.ViewModels
{
    public partial class ExploreViewModel : BaseViewModel
    {
        /* User's property needed !!! */

        // public User User { get; set; }


        private IRecipeLocalRepository localRepository;
        private IConnectivity connectivity;

        [ObservableProperty]
        string greatingMessage;
        [ObservableProperty]
        string headerMessage;
        [ObservableProperty]
        string headerImage;

        public ObservableCollection <Recipe> TrendingRecipes { get; set; }
        public ObservableCollection <Recipe> SeasonRecipes { get; set; }
        public ObservableCollection <Category> PopCategories { get; set; }
        public ObservableCollection<Recipe> LatestRecipes { get; set; } // Not implemented control in XAML !!!

        public EventHandler Search { get; set; }

        //public Command SearchFor { get; set; }


        public ExploreViewModel (IRecipeLocalRepository localRepository, IConnectivity connectivity, INavigator navigator)
        {
            isDesignMode = false;
            pageNavigator = navigator;

            this.localRepository = localRepository;
            this.connectivity = connectivity;

            TrendingRecipes = new ObservableCollection <Recipe> ();
            SeasonRecipes = new ObservableCollection <Recipe> ();
            LatestRecipes = new ObservableCollection <Recipe> ();

            PopCategories = new ObservableCollection <Category> ();

            HeaderMessage = "What are we going to cook today?"; // Enhance !!!
            GreatingMessage = "Hello May,"; // Enhance !!!
        }


        private async Task LoadPopCategories()
        {
            List<Category> catList = await localRepository.GetAllCategories();
            catList = new(catList.Where(x => x.CategoryTag.Equals("Popular")));

            PopCategories.Clear();
            foreach (var item in catList)
                PopCategories.Add(item);
        }

        private async Task LoadDemoRecipes()
        {
            if (TrendingRecipes.Count > 0) return;
            if (SeasonRecipes.Count > 0) return;
            if (LatestRecipes.Count > 0) return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("recipes_demo.json");
            using var reader = new StreamReader(stream);

            var contents = await reader.ReadToEndAsync();
            List<Recipe> demoRecipes = JsonSerializer.Deserialize<List<Recipe>>(contents);

            Random rand = new Random();

            for(int i = 0; i < 5; i++)
                TrendingRecipes.Add(demoRecipes[rand.Next(demoRecipes.Count)]);

            for (int i = 0; i < 5; i++)
                SeasonRecipes.Add(demoRecipes[rand.Next(demoRecipes.Count)]);
        }

        [RelayCommand]
        private async Task Initialise()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Do stuff if it's no internet connection on device
                await pageNavigator.MakeToast("You have not Internet connection");
            }

            await LoadPopCategories();

            await LoadDemoRecipes();
        }

        [RelayCommand]
        private async Task GoToRecipe(Recipe item)
        {
            await pageNavigator.GoToPagePassObj(nameof(RecipePage), "Recipe", item);
        }

        [RelayCommand]
        // Enables Design Mode
        private void EnableDesignMode()
        {
            IsDesignMode = !isDesignMode;

            if (IsDesignMode)
                pageNavigator.MakeToast("Design mode has been Enabled");
            else
                pageNavigator.MakeToast("Design mode has been Disabled");
        }
    }
}
