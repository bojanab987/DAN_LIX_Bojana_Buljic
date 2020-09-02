using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    public class GameInfoViewModel:ViewModelBase
    {
        private const int _maxAttempts = 10;
        private const int _pointAward = 75;
        private const int _pointDeduction = 15;
        private bool _gameLost;
        private bool _gameWon;
        /// <summary>
        /// No of match attempts
        /// </summary>
        private int _matchAttempts;
        public int MatchAttempts
        {
            get
            {
                return _matchAttempts;
            }
            private set
            {
                _matchAttempts = value;
                OnPropertyChanged("MatchAttempts");
            }
        }

        /// <summary>
        /// Players score
        /// </summary>
        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        /// <summary>
        /// Setting the viisibility for information when game is lost
        /// </summary>        
        public Visibility LostMessage
        {
            get
            {
                if (_gameLost)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Setting the viisibility for information when game is won
        /// </summary>        
        public Visibility WinMessage
        {
            get
            {
                if (_gameWon)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Decision on game status (if its won or lost)
        /// </summary>
        /// <param name="win">boolean value for game status</param>
        public void GameStatus(bool win)
        {
            //if lost show message for lossing
            if (!win)
            {
                _gameLost = true;
                OnPropertyChanged("LostMessage");
            }
            //if lost show message about winning
            if (win)
            {
                _gameWon = true;
                OnPropertyChanged("WinMessage");
            }
        }

        /// <summary>
        /// Clearing the Game Board
        /// </summary>
        public void ClearInfo()
        {
            Score = 0;
            MatchAttempts = _maxAttempts;
            _gameLost = false;
            _gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }

        /// <summary>
        /// Awarding points for wins
        /// </summary>
        public void Award()
        {
            Score += _pointAward;            
        }

        /// <summary>
        /// Penalty points for looses
        /// </summary>
        public void Penalize()
        {
            Score -= _pointDeduction;
            MatchAttempts--;
            
        }
    }
}
