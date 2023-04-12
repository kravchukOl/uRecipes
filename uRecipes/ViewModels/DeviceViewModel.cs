using uRecipes.Services.LocalisationSevice;

namespace uRecipes.ViewModels
{
    public partial class DeviceViewModel : BaseViewModel
    {

        [ObservableProperty]
        private int selectedViewModelIndex;


        [ObservableProperty]
        bool isOffline;

        public DeviceViewModel( IConnectivity connectivity, INavigator navigator, 
            ILocalizationResourceManager localization) 
        {
            isDesignMode = false;
            isOffline = false;

            pageNavigator = navigator;
            this.localization = localization;

            this.HeaderMessage = (string) this.localization["deviceDefaultMessage"] 
                ?? "Here, you can adjust your device's settings";
        }

        public override Task Initialize()
        {
            SelectedViewModelIndex = 0;
            return Task.CompletedTask;
        }

        [RelayCommand]
        public async Task ConnectDevice()
        {
            await pageNavigator.MakeToast("This command is still not implemented yet");
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
