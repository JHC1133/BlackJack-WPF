using BlackJack.MVVM.ViewModel;
using EL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack.MVVM.View
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();

            DataContext = new StatsViewModel();
        }

        private void gamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected game from the DataGrid
            Game selectedGame = (Game)gamesDataGrid.SelectedItem;

            ((StatsViewModel)DataContext).SelectedGame = selectedGame;

            // Update the SelectedGamePlayerStatisticsIntermediary based on the selected game
            ((StatsViewModel)DataContext).SelectedGamePlayerStatisticsIntermediary =
                new ObservableCollection<GamePlayerStatisticsIntermediary>(
                    selectedGame.GamePlayerStatisticsIntermediary);


        }
    }
}
