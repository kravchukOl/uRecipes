using uRecipes.ViewModels;
namespace uRecipes;

public partial class ExplorePage : ContentPage
{

	public ExplorePage( ExploreViewModel viewModel )
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	
}

