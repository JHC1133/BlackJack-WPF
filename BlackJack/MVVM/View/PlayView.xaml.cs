using BlackJack.MVVM.ViewModel;
using GameCardLibrary;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PlayView.xaml
    /// </summary>
    public partial class PlayView : UserControl
    {       

        public PlayView()
        {
            InitializeComponent();

            DataContext = new PlayViewModel();
        }
    }
}
