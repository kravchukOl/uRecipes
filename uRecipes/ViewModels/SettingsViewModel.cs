using uRecipes.Services.LocalisationSevice;

namespace uRecipes.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        string headerImage;

        public SettingsViewModel( INavigator navigator,
            ILocalizationResourceManager localization) 
        {
            IsDesignMode = false;

            pageNavigator = navigator;

            this.localization = localization;

            HeaderMessage = "This is Settings Page...";
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        // Enables Design Mode
        [RelayCommand]
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
