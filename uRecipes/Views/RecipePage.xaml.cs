using Sharpnado.Tabs;
using uRecipes.ViewModels;
using uRecipes.Views.RecipePageViews;

namespace uRecipes.Views;
public partial class RecipePage : BasePage
{
    public bool isIngredientsHiden = true;


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



    private void AnimateCard (DelayedView card)
    {
        var animation = new CardsAnimation();
        animation.Length = 1000;

        if (isIngredientsHiden)
        {
            card.LoadView();
            animation.Animate(card);
            isIngredientsHiden = false;
        }
        else
        {
            animation.AnimateBack(IngredientSheet);
            isIngredientsHiden = true;
        }
    }


    private void Nutrition_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void Ingredients_Tapped(object sender, TappedEventArgs e)
    {
        var animation = new CardsAnimation();
        animation.Length = 1000;

        if (isIngredientsHiden) 
        {
            if(!IngredientSheet.IsLoaded)
                IngredientSheet.LoadView();

            animation.Animate(IngredientSheet);
            isIngredientsHiden=false;
        }
        else
        {
            animation.AnimateBack(IngredientSheet);
            isIngredientsHiden = true;
        }


    }
}