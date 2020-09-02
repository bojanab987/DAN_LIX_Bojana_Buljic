using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;

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

    /// <summary>
    /// Main playing view model
    /// </summary>
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

        #region Methods and events
        //Initialize game essentials
        private void SetupGame(SlideCategories category)
        {
            //Initializing all necessary view models for game play
            Slides = new SlideCollectionViewModel();
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1));
            GameInfo = new GameInfoViewModel();

            //Set attempts to the maximum allowed
            GameInfo.ClearInfo();

            //Create cards(slides) from image folder then display to be memorized
            Slides.CreateSlides("Assets/" + category.ToString());
            Slides.Memorize();

            //Game has started, begin count.
            Timer.Start();

            //Slides have been updated
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }
        
        /// <summary>
        /// Card has been clicked
        /// </summary>
        /// <param name="card"></param>
        public void ClickedCard(object card)
        {
            if (Slides.canSelect)
            {
                var selected = card as CardViewModel;
                Slides.SelectCard(selected);
            }

            if (!Slides.areSlidesActive)
            {
                if (Slides.CheckIfMatched())
                    GameInfo.Award(); //Correct match
                else
                    GameInfo.Penalize();//Incorrect match
            }

            GameStatus();
        }
        
        /// <summary>
        /// Status of the current game
        /// </summary>
        private void GameStatus()
        {
            if (GameInfo.MatchAttempts < 0)
            {
                GameInfo.GameStatus(false);
                Slides.RevealUnmatched();
                Timer.Stop();
            }

            if (Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                Timer.Stop();
            }
        }

        /// <summary>
        /// Restarting the game
        /// </summary>
        public void Restart()
        {            
            SetupGame(Category);
        }

        
        #endregion
    }
}
