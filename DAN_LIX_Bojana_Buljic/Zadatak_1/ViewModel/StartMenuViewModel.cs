using System;

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

    }
}
