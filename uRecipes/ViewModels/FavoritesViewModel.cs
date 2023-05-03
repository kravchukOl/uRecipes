using uRecipes.Services.LocalRepository;
using uRecipes.Views.Demo;
using uRecipes.Views;


namespace uRecipes.ViewModels
{
    [QueryProperty (nameof(NewFavItem), "Recipe")]
    public partial class FavoritesViewModel : BaseViewModel
    {
        [ObservableProperty]
        string headerImage;

        [ObservableProperty]
        string emptyListMassage;


        public Recipe NewFavItem { get; set; } = null;

        private IRecipeLocalRepository localRepository;

        public ObservableCollection<Recipe> FavRecipes { get; set; }

        public FavoritesViewModel(IRecipeLocalRepository localRepository, INavigator navigator)
        {
            IsDesignMode = false;
            pageNavigator = navigator;

            this.localRepository = localRepository;

            FavRecipes = new ObservableCollection<Recipe>();

            HeaderMessage = "Loading tasty recipes...";
            HeaderImage = "salad.png";

        }

        private async Task AddItem(Recipe recipe)
        {
            int id;
            id = await localRepository.AddItem(recipe);
            recipe.Id = id;
            FavRecipes.Add(recipe);
            NewFavItem = null;
            // Reapply Filters here

            UpdateHeaderMesange();

        }
        private async Task LoadData ()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var recipes = await localRepository.GetRecipesAsync();

                recipes.ForEach((x) => FavRecipes.Add(x));

                UpdateHeaderMesange();

            }
            catch (Exception Ex)
            {
                await Shell.Current.DisplayAlert("Error:",
                    $"Unable to load recipes from local database{Ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void UpdateHeaderMesange()
        {
            if (FavRecipes is null)
                return;

            if(FavRecipes.Count == 0)
                HeaderMessage = $"Here you can store recipes you like";


            int NumberOfUncompletedRecipes = 0;

            foreach(Recipe recipe in FavRecipes)
                if (!recipe.IsCompleted)
                    NumberOfUncompletedRecipes++;

            HeaderMessage = $"You have {NumberOfUncompletedRecipes} recipes that you haven’t tried yet.";
        }


        [RelayCommand]
        public override async Task Initialize()
        {
            if (NewFavItem is not null)
            {
                await AddItem(NewFavItem);
                NewFavItem = null;
            }

            if (FavRecipes.Count == 0)
                await LoadData();
        }

        [RelayCommand]
        private async Task ShowFilters()
        {
            await pageNavigator.MakeToast("This command is still not implemented yet");
        }

        [RelayCommand]
        public async Task DeleteRecipe (Recipe item)
        {
            if (IsBusy)
                return;
            if (item is null)
                return;

            IsBusy = true;

            FavRecipes.Remove(item);

            await localRepository.RemoveItem(item);

            UpdateHeaderMesange();

            IsBusy = false;
        }
        [RelayCommand]
        private async Task GoToRecipe( Recipe item )
        {
            await pageNavigator.GoToPagePassObj(nameof(RecipePage), "Recipe", item);
        }

        [RelayCommand]
        private  void ShowCompleted()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var CompletedRecipes = new ObservableCollection<Recipe>(
                FavRecipes.Where(x => x.IsCompleted == true));
            FavRecipes.Clear();

            foreach (var item in CompletedRecipes)
                FavRecipes.Add(item);

            IsBusy = false;
        }
        [RelayCommand]
        private async Task ShowAll()
        {
            if (IsBusy == true)
                return;

            FavRecipes.Clear();
            await LoadData();

        }

        // Design mode Commands:
        [RelayCommand]
        private void EnableDesignMode()
        {
            IsDesignMode = !IsDesignMode;

            if (IsDesignMode)
                pageNavigator.MakeToast("Design mode has been Enabled");
            else
                pageNavigator.MakeToast("Design mode has been Disabled");
        }

        [RelayCommand]
        private void SaveToRepository()
        {
            if (IsBusy)
                return;

            List<Recipe> RecipeList = FavRecipes.ToList<Recipe>();

            RecipeList.ForEach(async (x) => await localRepository.AddItem(x));

            IsBusy = false;
        }
        [RelayCommand]
        private async Task GoToAddRecipe()
        {
            await pageNavigator.GoToPage(nameof(AddRecipePage));
        }

        [RelayCommand]
        private async Task AddDemoRecipes()
        {
            if (IsBusy)
                return;

            if (FavRecipes.Count != 0)
                return;

            IsBusy = true;


           await localRepository.LoadDemoRecipes();

            List<Recipe> RecipeList = await localRepository.GetRecipesAsync();

            foreach (var item in RecipeList)
                FavRecipes.Add(item);

            UpdateHeaderMesange();

            IsBusy = false;

        }

        [RelayCommand]
        private async Task DeleteAllFavRecipes()
        {
            foreach (var item in FavRecipes)
                await localRepository.RemoveItem(item);

            FavRecipes.Clear();
        }
    }
}
