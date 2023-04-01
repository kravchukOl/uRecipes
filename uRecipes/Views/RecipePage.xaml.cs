using Sharpnado.Tabs;
using uRecipes.ViewModels;
using uRecipes.Views.RecipePageViews;

namespace uRecipes.Views;
public partial class RecipePage : BasePage
{
    public bool isIngredientsHiden = true;
    public bool isNutritionHiden = true;
    public bool isInstructionHiden = true;

    public CardAnimation animation = new();


    private CardAnimationController cardAnimationController;


    public RecipePage(RecipeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();

        cardAnimationController = new CardAnimationController();

        //var animation = new CardAnimation();

        animation.Length = 500;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //IngredientSheet.LoadView();
        //NutritionTable.LoadView();
        //InstructionList.LoadView();
    }



    //private void AnimateCard (DelayedView card)
    //{
    //    var animation = new CardAnimation();
    //    animation.Length = 1000;

    //    if (isIngredientsHiden)
    //    {

    //        card.LoadView();
    //        animation.Animate(card);
    //        isIngredientsHiden = false;
    //    }
    //    else
    //    {
    //        animation.AnimateBack(IngredientSheet);
            
    //        isIngredientsHiden = true;
    //    }
    //}


    private void Nutrition_Tapped(object sender, TappedEventArgs e)
    {
        //var animation = new CardAnimation();
        //animation.Length = 500;

        if (isNutritionHiden)
        {
            if (!NutritionTable.IsLoaded)
                NutritionTable.LoadView();

            animation.Animate(NutritionTable);
            isNutritionHiden = false;
        }
        else
        {
            animation.AnimateBack(NutritionTable);
            isNutritionHiden = true;

        }
    }

    private void Ingredients_Tapped(object sender, TappedEventArgs e)
    {
        //var animation = new CardAnimation();
        //animation.Length = 500;

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

    private void Instruction_Tapped(object sender, TappedEventArgs e)
    {
        if (isInstructionHiden)
        {
            if (!InstructionList.IsLoaded)
                InstructionList.LoadView();

            animation.Animate(InstructionList);
            isInstructionHiden = false;
        }
        else
        {
            animation.AnimateBack(InstructionList);
            isInstructionHiden = true;
        }
    }
}