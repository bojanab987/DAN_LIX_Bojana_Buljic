using System.Windows.Media;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    public class CardViewModel:ViewModelBase
    {
        //Model for cards
        private CardModel _model;

        #region Constructor
        public CardViewModel(CardModel model)
        {
            _model = model;
            Id = model.Id;
        }
        #endregion

        #region Properties
        //ID of this card/slide
        public int Id { get; private set; }

        //Card/slide status
        private bool _isViewed;
        private bool _isMatched;
        private bool _isFailed;

        //Card is being viewed by user
        public bool isViewed
        {
            get
            {
                return _isViewed;
            }
            private set
            {
                _isViewed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //Card has been matched
        public bool isMatched
        {
            get
            {
                return _isMatched;
            }
            private set
            {
                _isMatched = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //Card has failed to be matched
        public bool isFailed
        {
            get
            {
                return _isFailed;
            }
            private set
            {
                _isFailed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        //User can select this slide/card
        public bool isSelectable
        {
            get
            {
                if (isMatched)
                    return false;
                if (isViewed)
                    return false;

                return true;
            }
        }

        //Image to be displayed
        public string SlideImage
        {
            get
            {
                if (isMatched)
                    return _model.ImageSource;
                if (isViewed)
                    return _model.ImageSource;


                return "/Zadatak_1;component/Assets/questionmark.jpg";
            }
        }        

        //Brush color of border based on status
        public Brush BorderBrush
        {
            get
            {
                if (isFailed)
                    return Brushes.Red;
                if (isMatched)
                    return Brushes.Green;
                if (isViewed)
                    return Brushes.Yellow;

                return Brushes.Black;
            }
        }        

        //Card has been matched
        public void MarkMatched()
        {
            isMatched = true;
        }

        //Mark card when user failed to match
        public void MarkFailed()
        {
            isFailed = true;
        }

        //No longer being viewed
        public void ClosePeek()
        {
            isViewed = false;
            isFailed = false;
            OnPropertyChanged("isSelectable");
            OnPropertyChanged("SlideImage");
        }

        //Let user view
        public void PeekAtImage()
        {
            isViewed = true;
            OnPropertyChanged("SlideImage");
        }
        #endregion
    }
}
