﻿using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.MVVM.ViewModel
{
    class PlayViewModel : ObservableObject
    {
        GameManager _gameManager = GameManager.Instance;

        public PlayViewModel()
        {
            
        }
    }
}