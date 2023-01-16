using uRecipes.ViewModels;

namespace uRecipes.Views;

public partial class DevicePage : ContentPage
{


    public DevicePage( DeviceViewModel viewModel )
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}