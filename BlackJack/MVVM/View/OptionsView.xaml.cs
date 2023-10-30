using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using BlackJack.MVVM.ViewModel;
using GameCardLibrary;
using UtilitiesLib;

namespace BlackJack.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        Helper helper = new Helper();

        public OptionsView()
        {
            InitializeComponent();

            
            DataContext = new OptionsViewModel();
        }

        private bool IsOptionsTextBoxesFilled()
        {
            if (txtNumberOfDecks.Text != null && txtNumberOfPlayers.Text != null)
            {
                return true;
            }
            return false;
        }

        private bool TryParseOptionsTextBoxes(out int numberOfDecks, out int numberOfPlayers)
        {
            numberOfDecks = 0;
            numberOfPlayers = 0;

            return !string.IsNullOrEmpty(txtNumberOfDecks.Text) &&
                   !string.IsNullOrEmpty(txtNumberOfPlayers.Text) &&
                   helper.StringToInt(txtNumberOfDecks.Text, out numberOfDecks) &&
                   helper.StringToInt(txtNumberOfPlayers.Text, out numberOfPlayers);
        }

        private void ClearTextBoxes()
        {
            txtNumberOfDecks.Clear();
            txtNumberOfPlayers.Clear();
        }

        private void btnOptionsSave_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("OptionsSaveButton Clicked");
            //if (IsOptionsTextBoxesFilled())
            //{
            //    Debug.WriteLine("txtPlayers: " + txtNumberOfPlayers.Text);
            //    Debug.WriteLine("txtDecks: " + txtNumberOfDecks.Text);
            //    if (TryParseOptionsTextBoxes(out int numberOfDecks, out int numberOfPlayers))
            //    {
            //        SharedData.Instance.NumberOfDecks = numberOfDecks;
            //        SharedData.Instance.NumberOfPlayers = numberOfPlayers;

            //        ClearTextBoxes();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please use valid integers as input");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please fill in both textboxes");
            //}
            Debug.WriteLine("Button clicked");

            ((OptionsViewModel)DataContext).SaveCommand.Execute(null);

            if (cbNameList != null)
            {

            }

        }
    }
}
