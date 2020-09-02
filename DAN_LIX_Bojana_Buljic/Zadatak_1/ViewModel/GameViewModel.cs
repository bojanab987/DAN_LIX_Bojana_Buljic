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
        #region Constructor
        public GameViewModel(SlideCategories category)
        {
            Category = category;
            SetupGame(category);
        }
        #endregion

        #region Properties
        //Collection of slides we are playing with
        public SlideCollectionViewModel Slides { get; private set; }
        //Game information scores, attempts etc
        public GameInfoViewModel GameInfo { get; private set; }
        //Game timer for elapsed time
        public TimerViewModel Timer { get; private set; }
        //Category we are playing in
        public SlideCategories Category { get; private set; }
        #endregion

        //Initialize game essentials
        private void SetupGame(SlideCategories category)
        {

           
            //Slides have been updated
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        

    }
}
