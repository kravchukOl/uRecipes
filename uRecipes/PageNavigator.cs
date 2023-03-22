using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using uRecipes.ViewModels;

namespace uRecipes
{
    public class PageNavigator : INavigator
    {
        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();
        

        public async Task GoToPage(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToPagePassObj(string route, string propertyName, object item)
        {
            await Shell.Current.GoToAsync(route, true, new Dictionary<string, object>
            {
                {propertyName, item}
            });
        }

        public async Task GoBackward()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        public async Task MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }

    }
}
