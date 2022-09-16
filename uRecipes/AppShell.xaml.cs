using uRecipes.Views;
using uRecipes.Views.Demo;

namespace uRecipes;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ExplorePage), typeof(ExplorePage));
		Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
		Routing.RegisterRoute(nameof(ChefPage), typeof(ChefPage));
		Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));


		Routing.RegisterRoute(nameof(AddRecipePage), typeof(AddRecipePage));


	}
}
