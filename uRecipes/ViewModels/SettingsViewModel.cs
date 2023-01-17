using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override Task Initialise()
        {
            return base.Initialise();
        }

        // Enables Design Mode
        [RelayCommand]
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
