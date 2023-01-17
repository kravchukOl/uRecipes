using uRecipes.ViewModels;
namespace uRecipes;

public partial class ExplorePage : BasePage
{

	public ExplorePage( ExploreViewModel viewModel ):base(viewModel)
	{
		InitializeComponent();
	}
}