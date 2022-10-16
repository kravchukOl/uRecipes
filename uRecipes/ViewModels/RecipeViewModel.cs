﻿using uRecipes.Services.CloudRepository;
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
        string videoUrl;

        [ObservableProperty]
        bool isCompleted;

        [ObservableProperty]
        bool isOffline;

        public int AuthorId { get; private set; }

        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<Instruction> Instructions { get; private set; }
        public ObservableCollection <Ingredient> Ingredients { get; private set; }



        public RecipeViewModel(IRecipeLocalRepository localRepository, IConnectivity connectivity, INavigator navigator)
        {
            isDesignMode = false;
            isOffline = false;
            pageNavigator = navigator;

            this.localRepository = localRepository;
            //this.cloudRepository = cloudRepository;
            this.connectivity = connectivity;

            Categories = new ObservableCollection<Category>();
            Ingredients = new ObservableCollection<Ingredient>();
            Instructions = new ObservableCollection<Instruction>();
            NutritionInfo = new NutritionInfo();


        }

        [RelayCommand]
        private async Task Initialise()
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
                VideoUrl = RecipeItem.VideoUrl.AbsoluteUri;

                TotalTime = RecipeItem.TotalTime;
                PersonServ = RecipeItem.PersonServ;
                isCompleted = RecipeItem.IsCompleted;

                //  Categories = new (await localRepository.GetCategoriesOfRecipe(RecipeItem));

                List<Ingredient> ingredients = await localRepository.GetIngredients(RecipeItem);

                foreach(Ingredient ingredient in ingredients)
                    this.Ingredients.Add(ingredient);

                NutritionInfo = await localRepository.GetNutrition(RecipeItem);

            }
            catch (Exception ex)
            {
                // Arrange error view
            }
            finally
            {
                // Initialise Eror view
            }
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