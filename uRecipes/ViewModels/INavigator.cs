using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uRecipes.ViewModels
{
    public interface INavigator
    {
        public  Task GoToPage(string route);
        public  Task GoToPagePassObj(string route, string propertyName, object item);
        public  Task GoBackward();
        public Task MakeToast(string message);

        

    }
}
