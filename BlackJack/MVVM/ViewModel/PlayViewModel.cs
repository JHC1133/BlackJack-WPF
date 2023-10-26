using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.MVVM.ViewModel
{
    public class PlayViewModel : ObservableObject
    {


        GameManager _gameManager;

        public PlayViewModel()
        {
            _gameManager = GameManager.Instance;
        }


        public GameManager GameManager { get => _gameManager; }
    }
}
