using uRecipes.Services.LocalisationSevice;
using uRecipes.Services.LocalRepository;
using uRecipes.ViewModels;
using uRecipes.Views;
using uRecipes.Views.Demo;
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

        
        //public bool IsBusy
        //{
        //    get => isBusy;
        //    set
        //    {
        //        if (isBusy == value)
        //            return;
        //        isBusy = value;
        //        OnPropertyChanged();
        //        // Also raise the IsNotBusy property changed
        //        OnPropertyChanged(nameof(IsNotBusy));
        //    }
        //}

        //public bool IsNotBusy => !IsBusy;

        public virtual Task Initialise()
        {
            return Task.CompletedTask;
        }
    }
}
