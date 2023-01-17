using uRecipes.ViewModels;
namespace uRecipes.Views;

public partial class ChefPage : BasePage
{
	public ChefPage( ChefViewModel viewmodel ) : base( viewmodel )
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}