
namespace uRecipes.Views.RecipePageViews
{
    public class CardAnimationController
    {
        private readonly List <ISlideCard> slideCards = new List <ISlideCard> ();

        private readonly CardAnimation animation = new CardAnimation ();

        public uint Duration
        {
            get { return animation.Length; }

            set { animation.Length = value; }
        }

        public CardAnimationController() => Duration = 500;

        public void CardTaped (object sender, EventArgs e)
        {
            if (sender is ISlideCard card)
            {
                if (card.IsOpen)
                    CloseSlideCard(card);
                else 
                    OpenSlideCard(card);
            }
        }

        public void AddCard(ISlideCard card)
        {
            card.CardTaped += CardTaped;
            slideCards.Add(card);
        }

        public void OpenSlideCard( ISlideCard card )
        {
            bool cardFlag = false;

            foreach ( var item in slideCards ) 
            {
                if (item == card)
                {
                    cardFlag = true;
                    continue;
                }

                if(cardFlag)
                {
                    item.SlideDown();
                }

            }
        }

        public void CloseSlideCard ( ISlideCard card ) 
        {
            foreach (var item in slideCards)
            {
                if (item == card)
                    item.SlideDown();
            }
        }
    }
}
