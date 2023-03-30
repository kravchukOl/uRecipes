using uRecipes.ViewModels;
namespace uRecipes;

public abstract class BasePage : ContentPage
{
    private readonly BaseViewModel _viewModel;

    protected BasePage(BaseViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Initialise();
    }
}