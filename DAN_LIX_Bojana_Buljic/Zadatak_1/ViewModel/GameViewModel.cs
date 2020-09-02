using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Enum values for type of game
    /// </summary>
    public enum SlideCategories
    {
        Animals,
        Birds,
        Food
    }
    public class GameViewModel:ViewModelBase
    {
        public GameViewModel(SlideCategories category)
        {
            //Category = category;
            //SetupGame(category);
        }
    }
}
