
namespace uRecipes.Views.RecipePageViews
{
    public class CardsAnimation : CommunityToolkit.Maui.Animations.BaseAnimation
    {

        Animation OpenCard(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => view.HeightRequest = f, 0, 500);

            return animation;
        }

        Animation CloseCard(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => view.HeightRequest = f, 500, 0);

            return animation;
        }

        public override Task Animate(VisualElement view)
        {
            view.Animate("Card", OpenCard(view), 16, Length);
            return Task.CompletedTask;
        }

        public Task AnimateBack(VisualElement view) 
        {
            view.Animate("Card", CloseCard(view), 16, Length);
            return Task.CompletedTask;
        }
    }
}
