using Sharpnado.Tabs;
using uRecipes.ViewModels;
using uRecipes.Views.RecipePageViews;

namespace uRecipes.Views;
public partial class RecipePage : BasePage
{
    public bool isIngredientsHided = true;


	public RecipePage(RecipeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();


	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //IngredientSheet.LoadView();
        NutritionTable.LoadView();
        InstructionList.LoadView();
    }

    private void Ingredients_Tapped(object sender, TappedEventArgs e)
    {
        var animation = new CardsAnimation();
        animation.Length = 1000;

        if (isIngredientsHided) 
        {
            IngredientSheet.LoadView();
            animation.Animate(IngredientSheet);
            isIngredientsHided=false;
        }
        else
        {
            animation.AnimateBack(IngredientSheet);
            isIngredientsHided = true;
        }


    }
}