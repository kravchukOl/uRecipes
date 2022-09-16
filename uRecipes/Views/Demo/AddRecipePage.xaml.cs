using uRecipes.ViewModels.Demo;

namespace uRecipes.Views.Demo;

public partial class AddRecipePage : ContentPage
{
	public AddRecipePage(AddRecipeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

		RecipeImage.Unloaded += RecipeImage_Unloaded;

    }

	private void RecipeImage_Unloaded(object sender, EventArgs e)
	{
		//RecipeImage.Source = ImageSource.
		//	FromResource("uRecipes.Resources.Images.icons_png.image.png");
	}
}