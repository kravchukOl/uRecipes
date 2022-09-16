using uRecipes.ViewModels;

namespace uRecipes.Views;

public partial class FavoritesPage : ContentPage
{
	public FavoritesPage (FavoritesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;		
	}

	private void ShowCompButton_Clicked(object sender, EventArgs e)
	{

		Application.Current.Resources.TryGetValue("GreenAccent2", out var GreenAccent);
		ShowCompButton.TextColor = (Color)GreenAccent;

        Application.Current.Resources.TryGetValue("Gray450", out var Gray);
        ShowAllButton.TextColor = (Color)Gray;

    }

	private void ShowAllButton_Clicked(object sender, EventArgs e)
	{
        Application.Current.Resources.TryGetValue("GreenAccent2", out var GreenAccent);
        ShowAllButton.TextColor = (Color)GreenAccent;

        Application.Current.Resources.TryGetValue("Gray450", out var Gray);
        ShowCompButton.TextColor = (Color)Gray;

    }
}