using uRecipes.Services.LocalisationSevice;
using uRecipes.Services.LocalRepository;
using uRecipes.Views;

namespace uRecipes.ViewModels
{
    public partial class ExploreViewModel : BaseViewModel
    {
        /* User's property needed !!! */

        // public User User { get; set; }


        private IRecipeLocalRepository localRepository;

        private  IConnectivity connectivity;

        [ObservableProperty]
        string greatingMessage;
        [ObservableProperty]
        string headerImage;

        public ObservableCollection<Recipe> TrendingRecipes { get; set; } = new();
        public ObservableCollection<Recipe> SeasonRecipes { get; set; } = new();
        public ObservableCollection <Category> PopCategories { get; set; } = new();
        public ObservableCollection<Recipe> LatestRecipes { get; set; } = new(); // Not implemented control in XAML !!!

        public EventHandler Search { get; set; }

        //public Command SearchFor { get; set; }


        public ExploreViewModel (IRecipeLocalRepository localRepository,
            IConnectivity connectivity, INavigator navigator, ILocalizationResourceManager localization)
        {
            isDesignMode = false;

            pageNavigator = navigator;
            this.localRepository = localRepository;
            this.connectivity = connectivity;
            this.localization = localization;

            HeaderMessage = (String) localization["exploreDeafaultMessage"] 
                ?? "What are we going to cook today?";

            GreatingMessage = "Hello May,"; // Enhance !!!
        }

        private async Task LoadPopCategories()
        {
            IsBusy = true;

            //List<Category> catList = await localRepository.GetAllCategories();
            //catList = new(catList.Where(x => x.CategoryTag.Equals("Popular")));

            List<Category> catList = new (await localRepository.GetCategoriesByTag("Popular"));
            List<Category> catList1 = new(await localRepository.GetCategoriesByTag("RightNow"));

            PopCategories.Clear();

            foreach (var item in catList)
                PopCategories.Add(item);

            foreach( var item in catList1) 
                PopCategories.Add(item);

            IsBusy = false;
        }

        private async Task LoadDemoRecipes()
        {
            IsBusy = true;

            if (TrendingRecipes.Count > 0) return;
            if (SeasonRecipes.Count > 0) return;
            if (LatestRecipes.Count > 0) return;

            try
            {

                using var stream = await FileSystem.OpenAppPackageFileAsync("recipes_demo.json");
                using var reader = new StreamReader(stream);

                var contents = await reader.ReadToEndAsync();
                List<Recipe> demoRecipes = JsonSerializer.Deserialize<List<Recipe>>(contents);

                Random rand = new Random();

                for (int i = 0; i < 5; i++)
                    TrendingRecipes.Add(demoRecipes[rand.Next(demoRecipes.Count)]);

                for (int i = 0; i < 5; i++)
                    SeasonRecipes.Add(demoRecipes[rand.Next(demoRecipes.Count)]);

                IsBusy = false;
            }

            catch (Exception ex) 
            {
                
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public override async Task Initialize()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Do stuff if it's no internet connection on device
                await pageNavigator.MakeToast("You have not Internet connection");
            }

            await LoadPopCategories();

            await LoadDemoRecipes();

            IsBusy = false;
        }

        [RelayCommand]
        private async Task ShowAll (ObservableCollection <Recipe> recipes)
        {
           await pageNavigator.MakeToast("This command is still not implementated yet ");
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
            IsDesignMode = !IsDesignMode;

            if (IsDesignMode)
                pageNavigator.MakeToast("Design mode has been Enabled");
            else
                pageNavigator.MakeToast("Design mode has been Disabled");
        }
    }
}
