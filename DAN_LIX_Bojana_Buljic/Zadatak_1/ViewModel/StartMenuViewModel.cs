using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;

namespace Zadatak_1.ViewModel
{
    public class StartMenuViewModel
    {
        private MainWindow _mainWindow;
        #region Constructor
        public StartMenuViewModel(MainWindow main)
        {
            _mainWindow = main;            
        }
        #endregion

        #region Method to Start New Game
        /// <summary>
        /// Opens Game view and starts new game
        /// </summary>
        /// <param name="categoryIndex"></param>
        public void StartNewGame(int categoryIndex)
        {
            var category = (SlideCategories)categoryIndex;
            GameViewModel newGame = new GameViewModel(category);
            _mainWindow.DataContext = newGame;
        }
        #endregion

        private ICommand exitGame;
        public ICommand ExitGame
        {
            get
            {
                if (exitGame == null)
                {
                    exitGame = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exitGame;
            }
        }

        public void ExitExecute()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit the game?", "Check", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _mainWindow.Close();
            }
        }

        public bool CanExitExecute()
        {
            return true;
        }

    }
}
