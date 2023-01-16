using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uRecipes.Services.LocalisationSevice;

namespace uRecipes.ViewModels
{
    public partial class DeviceViewModel : BaseViewModel
    {

        [ObservableProperty]
        bool isOffline;

        public DeviceViewModel( IConnectivity connectivity, INavigator navigator, 
            ILocalizationResourceManager localization) 
        {
            isDesignMode = false;
            isOffline = false;

            pageNavigator = navigator;
            this.localization = localization;

            HeaderMessage = (string) this.localization["deviceDefaultMessage"] 
                ?? "Here, you can adjust your device's settings";
        }

        [RelayCommand]
        public async Task ConnectDevice()
        {
            await pageNavigator.MakeToast("This command is still not implemented yet");
        }
    }
}
