using System.Globalization;

namespace uRecipes.Services.LocalisationSevice
{
    public interface ILocalizationResourceManager
    {
        object this[string resourceKey] { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void SetCulture(CultureInfo culture);
    }
}