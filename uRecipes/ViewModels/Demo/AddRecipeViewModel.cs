using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uRecipes.Views;

namespace uRecipes.ViewModels.Demo
{
    public partial class AddRecipeViewModel : BaseViewModel
    {
        public string Name{ get; set; }
        public string Author { get; set; }
        public string Description{ get; set; }
        public string PhotoURL { get; set; }
        public string VideoURL { get; set; }
        public string TotalTime { get; set; }
        public string PersServ { get; set; }
        public bool IsCompleted { get; set; }

        // Validation field flags
        [ObservableProperty]
        bool nameIsNotValid;
        [ObservableProperty]
        bool authorIsNotValid;
        [ObservableProperty]
        bool descriptionIsNotValid;
        [ObservableProperty]
        bool photoURLIsNotValid;
        [ObservableProperty]
        bool videoURLIsNotValid;
        [ObservableProperty]
        bool totalTimeIsNotValid;
        [ObservableProperty]
        bool persServIsNotValid;

        public AddRecipeViewModel()
        {
            IsCompleted = false;
            pageNavigator = new PageNavigator();
        }

        [RelayCommand]
        public async Task AddRecipe()
        {
            if (Name is null || Author is null || Description is null 
                || PhotoURL is null || VideoURL is null 
                || TotalTime is null || PersServ is null )
                return;

            if (nameIsNotValid || authorIsNotValid || descriptionIsNotValid ||
                photoURLIsNotValid || videoURLIsNotValid || totalTimeIsNotValid
                || persServIsNotValid)
                return;



            Recipe recipe = new Recipe
            {
                Name = this.Name,
                Author = this.Author,
                Description = this.Description,
                ImageUrl =  new Uri(this.PhotoURL),
                VideoUrl = new Uri(this.VideoURL),
                TotalTime = this.TotalTime,
                PersonServ = this.PersServ,
                IsCompleted = this.IsCompleted,

            };

            await pageNavigator.GoToPagePassObj(nameof(FavoritesPage), "Recipe", recipe);
        }
    }
}
