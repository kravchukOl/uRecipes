namespace uRecipes.Views.RecipePageViews;

public partial class IngredientsSheet : ContentView , ISlideCard
{
    public event EventHandler CardTaped;

    public bool IsOpen { get; private set; }

    public IngredientsSheet()
	{
		InitializeComponent();

        IsOpen = false;
	}

    public Task SlideDown()
    {
        throw new NotImplementedException();
    }

    public Task SlideUp()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnCardTap( )
    {
        CardTaped?.Invoke(this, EventArgs.Empty);
    }
}