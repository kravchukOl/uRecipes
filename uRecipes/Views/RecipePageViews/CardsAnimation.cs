namespace uRecipes.Views.RecipePageViews
{
    public class CardAnimation : CommunityToolkit.Maui.Animations.BaseAnimation
    {
        Animation OpenCard(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => view.TranslationY = f, -500, 0);

            return animation;
        }

        Animation CloseCard(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => view.TranslationY = f, 0, -500);

            return animation;
        }

        public override Task Animate(VisualElement view)
        {
            view.IsVisible = true;
            view.Animate("Card", OpenCard(view), 16, Length);
            return Task.CompletedTask;
        }

        public Task AnimateBack(VisualElement view) 
        {
            view.Animate("Card", CloseCard(view), 16, Length);
            view.IsVisible = false;
            return Task.CompletedTask;
        }
    }
}
