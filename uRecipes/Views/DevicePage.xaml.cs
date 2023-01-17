using uRecipes.ViewModels;

namespace uRecipes.Views;

public partial class DevicePage : BasePage
{
    public DevicePage( DeviceViewModel viewModel ) : base( viewModel )
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}