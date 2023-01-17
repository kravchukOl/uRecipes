using uRecipes.Services.LocalisationSevice;

namespace uRecipes.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        protected INavigator pageNavigator;
        protected ILocalizationResourceManager localization;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotBusy))]
        bool isBusy;

        // Design Mode flag
        [ObservableProperty]
        public bool isDesignMode;

        [ObservableProperty]
        string headerMessage;

        bool isNotBusy => !IsBusy;


        public virtual Task Initialise()
        {
            return Task.CompletedTask;
        }
    }
}
