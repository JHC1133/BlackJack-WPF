using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace GameCardLibrary
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string[] _nameArray =
        {
            "Joar",
            "Farid",
            "Wilmer",
            "Simon",
            "Marco",
            "Brandon",
            "Kristoffer",
            "Dick",
            "Jose",
            "Kevin",
            "Sofia",
            "Gorm",
            "Zeke",
            "McThundertits"
        };

        private string _name;
        private string _stateText;
        private Hand _hand;

        public int Wins { get; set; }
        public int Blackjacks { get; set; }
        public int Busts { get; set; }
        public int Ties { get; set; }
        public int Losses { get; set; }
        public bool IsFinished { get; set; }
        public bool Winner { get; set; }
        public bool IsBust { get; set; }
        [Key]
        public string Name { get => _name; }
        public Hand Hand { get => _hand; set => _hand = value; }


        public string StateText
        {
            get { return _stateText; }
            set
            {
                if (_stateText != value)
                {
                    _stateText = value;
                    OnPropertyChanged(); // Notifies the UI of the property change
                }
            }
        }

        public Player(Hand hand, string name)
        {
            _name = name;
            _hand = hand;
        }

        private string RandomizeName()
        {
            Random rand = new Random();
            int i = rand.Next(0, _nameArray.Length - 1);
            string s = _nameArray[i];
            return s;
        }

        public void SetPlayerHand(Deck deck)
        {
            
        }


        public override string ToString()
        {
            return string.Empty;
        }

    }
}
