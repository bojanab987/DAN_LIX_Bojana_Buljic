using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zadatak_1.Commands;

namespace Zadatak_1.ViewModel
{
    public class StartMenuViewModel
    {
        private MainWindow _mainWindow;
        public StartMenuViewModel(MainWindow main)
        {
            _mainWindow = main;            
        }

        public void StartNewGame(int categoryIndex)
        {
            var category = (SlideCategories)categoryIndex;
            GameViewModel newGame = new GameViewModel(category);
            _mainWindow.DataContext = newGame;
        }

        //private ICommand play;
        //public ICommand Play
        //{
        //    get
        //    {
        //        if (play == null)
        //        {
        //            play = new RelayCommand(param => PlayExecute(), param => CanPlayExecute());
        //        }
        //        return play;
        //    }
        //}

        //public void PlayExecute()
        //{
        //    var startMenu = DataContext as StartMenuViewModel;
        //    startMenu.StartNewGame(categoryBox.SelectedIndex);
        //}

        //public bool CanPlayExecute()
        //{
        //    return true;
        //}
    }
}
