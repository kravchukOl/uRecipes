using uRecipes.ViewModels;

namespace uRecipes.Views;
public partial class RecipePage : ContentPage
{
	public RecipePage(RecipeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

	}

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //}
}
