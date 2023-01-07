using System.Globalization;
using uRecipes.Resources.Localization;

namespace uRecipes.Services.LocalisationSevice
{
    public class LocalizationResourceManager : INotifyPropertyChanged, ILocalizationResourceManager
    {
        public LocalizationResourceManager()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }

        public static LocalizationResourceManager Instance { get; } = new();

        public object this[string resourceKey] => AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? Array.Empty<byte>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetCulture(CultureInfo culture)
        {
            AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
