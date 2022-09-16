using uRecipes.ViewModels;
namespace uRecipes.Views;

public partial class ChefPage : ContentPage
{
	public ChefPage( ChefViewModel viewmodel )
	{
		InitializeComponent();

		BindingContext = viewmodel; 
	}
}