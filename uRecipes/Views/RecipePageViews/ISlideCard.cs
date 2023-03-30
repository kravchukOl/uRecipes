using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uRecipes.Views.RecipePageViews
{
    public interface ISlideCard
    {
        public event EventHandler CardTaped;

        public bool IsOpen { get; }
        public Task SlideUp();

        public Task SlideDown();
    }
}
