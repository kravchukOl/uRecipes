using Sharpnado.Tabs;
using Sharpnado.Tasks;

namespace uRecipes.Views;

public class DelayedView<TView> : LazyView<TView> where TView : View, new()
{
    public int DelayInMilliseconds { get; set; } = 100;

    public override void LoadView()
    {
        TaskMonitor.Create(
            async () =>
            {
                View? view = null;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    await Task.Run(
                        () =>
                        {
                            view = new TView
                            {
                                BindingContext = BindingContext,
                            };
                        });
                }
                else
                {
                    view = new TView
                    {
                        BindingContext = BindingContext,
                    };
                }

                await Task.Delay(DelayInMilliseconds);

                IsLoaded = true;
                Content = view;
            });
    }
}