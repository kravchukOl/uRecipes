using uRecipes.ViewModels;

namespace uRecipes.Views;

public partial class SettingsPage : BasePage
{
	public SettingsPage(SettingsViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

}