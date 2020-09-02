using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    public class SlideCollectionViewModel:ViewModelBase
    {      
        #region Constants
        //Interval for how long a user peeks at selected card
        private const int _peekSeconds = 3;
        //Interval for how long a user has to memorize slides
        private const int _openSeconds = 5;
        #endregion

        #region Properties
        //Collection of cards with pictures for slides
        public ObservableCollection<CardViewModel> MemorySlides { get; private set; }

        //Selected cards for matching
        private CardViewModel SelectedCard1;
        private CardViewModel SelectedCard2;

        //Timers for peeking at cards and initial display for memorizing them
        private DispatcherTimer _peekTimer;
        private DispatcherTimer _openingTimer;        

        //Can user select a card
        public bool canSelect { get; private set; }

        //Are selected slides still being displayed
        public bool areSlidesActive
        {
            get
            {
                if (SelectedCard1 == null || SelectedCard2 == null)
                    return true;

                return false;
            }
        }

        //Have all slides been matched
        public bool AllSlidesMatched
        {
            get
            {
                foreach (var slide in MemorySlides)
                {
                    if (!slide.isMatched)
                        return false;
                }

                return true;
            }
        }
        #endregion

        #region Constructor
        public SlideCollectionViewModel()
        {
            _peekTimer = new DispatcherTimer();
            _peekTimer.Interval = new TimeSpan(0, 0, _peekSeconds);
            _peekTimer.Tick += PeekTimer_Tick;

            _openingTimer = new DispatcherTimer();
            _openingTimer.Interval = new TimeSpan(0, 0, _openSeconds);
            _openingTimer.Tick += OpeningTimer_Tick;
        }
        #endregion
               
        /// <summary>
        /// Method creates slides from images in file directory
        /// </summary>
        /// <param name="imagesPath">path of folder with images</param>
        public void CreateSlides(string imagesPath)
        {
            //New list of slides
            MemorySlides = new ObservableCollection<CardViewModel>();
            var models = GetModelsFrom(@imagesPath);

            //Create slides with matching pairs from models
            for (int i = 0; i < 8; i++)
            {
                //Create 2 matching cards
                var newCard = new CardViewModel(models[i]);
                var newCardMatch = new CardViewModel(models[i]);
                //Add new cards to collection
                MemorySlides.Add(newCard);
                MemorySlides.Add(newCardMatch);
                //Initially display images for user
                newCard.PeekAtImage();
                newCardMatch.PeekAtImage();
            }

            ShuffleCards();
            OnPropertyChanged("MemorySlides");
        }

        
        /// <summary>
        /// Selecting a card for match
        /// </summary>
        /// <param name="card"></param>
        public void SelectCard(CardViewModel card)
        {
            card.PeekAtImage();

            if (SelectedCard1 == null)
            {
                SelectedCard1 = card;
            }
            else if (SelectedCard2 == null)
            {
                SelectedCard2 = card;
                HideUnmatched();
            }
            
            OnPropertyChanged("areSlidesActive");
        }
        
        /// <summary>
        /// Checking if selected cards are matching ones
        /// </summary>
        /// <returns></returns>
        public bool CheckIfMatched()
        {
            if (SelectedCard1.Id == SelectedCard2.Id)
            {
                MatchCorrect();
                return true;
            }
            else
            {
                MatchFailed();
                return false;
            }
        }
       
        /// <summary>
        /// Selected cards did not match
        /// </summary>
        private void MatchFailed()
        {
            SelectedCard1.MarkFailed();
            SelectedCard2.MarkFailed();
            ClearSelected();
        }
        
        /// <summary>
        /// Selected cards matched
        /// </summary>
        private void MatchCorrect()
        {
            SelectedCard1.MarkMatched();
            SelectedCard2.MarkMatched();
            ClearSelected();
        }
        
        /// <summary>
        /// Clear selected cards
        /// </summary>
        private void ClearSelected()
        {
            SelectedCard1 = null;
            SelectedCard2 = null;
            canSelect = false;
        }
      
        /// <summary>
        /// Reveal all unmatched cards
        /// </summary>
        public void RevealUnmatched()
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.isMatched)
                {
                    _peekTimer.Stop();
                    slide.MarkFailed();
                    slide.PeekAtImage();
                }
            }
        }

        /// <summary>
        /// Hide all  unmatched cards
        /// </summary>
        public void HideUnmatched()
        {
            _peekTimer.Start();
        }
        
        /// <summary>
        /// Display cards for memorizing
        /// </summary>
        public void Memorize()
        {
            _openingTimer.Start();
        }

        //Get card picture models for creating picture views
        private List<CardModel> GetModelsFrom(string relativePath)
        {
            //List of models for cards
            var models = new List<CardModel>();
            //Get all image URIs in folder
            var images = Directory.GetFiles(relativePath, "*.jpg", SearchOption.AllDirectories);
            //Card id begin at 0
            var id = 0;

            //create and add all card pictures in list of images
            foreach (string i in images)
            {
                models.Add(new CardModel() { Id = id, ImageSource = "/Zadatak_1;component/" + i });
                id++;
            }            
            return models;
        }

        /// <summary>
        /// Randomize the location of the cards in collection
        /// </summary>
        private void ShuffleCards()
        {
            //Randomizing card indexes
            var rnd = new Random();
            //Shuffle memory cards
            for (int i = 0; i < 64; i++)
            {
                MemorySlides.Reverse();
                MemorySlides.Move(rnd.Next(0, MemorySlides.Count), rnd.Next(0, MemorySlides.Count));
            }
        }
       
        /// <summary>
        /// Close cards being memorized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpeningTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                slide.ClosePeek();
                canSelect = true;
            }
            OnPropertyChanged("areSlidesActive");
            _openingTimer.Stop();
        }
        
        /// <summary>
        /// Display selected card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeekTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.isMatched)
                {
                    slide.ClosePeek();
                    canSelect = true;
                }
            }
            OnPropertyChanged("areSlidesActive");
            _peekTimer.Stop();
        }
    }
}
