namespace uRecipes.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        protected INavigator pageNavigator;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotBusy))]
        bool isBusy;

        [ObservableProperty]
        public string title;

        // Design Mode flag
        [ObservableProperty]
        public bool isDesignMode;

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
       
    }
}
