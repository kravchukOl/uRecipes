using Sharpnado.Tabs;
using uRecipes.ViewModels;


namespace uRecipes.Views;
public partial class RecipePage : BasePage
{
	public RecipePage(RecipeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();

	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        IngredientSheet.LoadView();
        NutritionTable.LoadView();
        InstructionList.LoadView();
    }
}