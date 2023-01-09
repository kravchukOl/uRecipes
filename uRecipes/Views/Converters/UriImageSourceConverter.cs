namespace uRecipes.Views.Converters
{
    public class UriImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Uri uri)
            {
                ImageSource imageSource = ImageSource.FromUri(uri);
                return imageSource;
            }

            if(value is String strUri)
            {
                ImageSource imageSource = ImageSource.FromUri(new Uri(strUri));
                return imageSource;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
