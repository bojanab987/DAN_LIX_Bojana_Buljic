using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for GameMenuView.xaml
    /// </summary>
    public partial class GameMenuView : UserControl
    {
        public GameMenuView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raising event Perform of clicking the slide/card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slide_Clicked(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            var button = sender as Button;
            game.ClickedCard(button.DataContext);
        }

        /// <summary>
        /// Raising event Performing of clicking Play again button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayAgain_C(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            game.Restart();
        }

        
    }
}
