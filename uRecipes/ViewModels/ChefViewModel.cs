﻿using uRecipes.Services.LocalRepository;
using uRecipes.Views.Demo;

namespace uRecipes.ViewModels
{
    public partial class ChefViewModel : BaseViewModel
    {
        /* User's property needed !!! */
        // public User User { get; set; }

        [ObservableProperty]
        string headerMessage;
        [ObservableProperty]
        string headerImage;


        private IRecipeLocalRepository localRepository;

        // Design-mode properties:

        public ObservableCollection <Category> AllCategories { get; private set; }


        public ChefViewModel (IRecipeLocalRepository localRepository) 
        {
            IsDesignMode = false;

            this.localRepository = localRepository;

            pageNavigator = new PageNavigator();

            HeaderMessage = "This is Chef Page...";

            AllCategories = new ObservableCollection<Category> ();

            Task.Run(async () => await GetAllCategories());
        }

        [RelayCommand]
        public async Task GetAllCategories()
        {
            if(IsBusy) return;

            try
            {
                IsBusy = true;
                List<Category> catList = new(await localRepository.GetAllCategories());

                foreach (Category item in catList)
                    AllCategories.Add(item);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Design mode Commands:



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