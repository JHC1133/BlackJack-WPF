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

        //private static PlayViewModel _instance;

        //#region Singleton
        //public static PlayViewModel Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new PlayViewModel();
        //        }
        //        return _instance;
        //    }
        //}


        //#endregion

        GameManager _gameManager;
        Dealer _dealer;

        public PlayViewModel()
        {
            _gameManager = GameManager.Instance;
            _dealer = _gameManager.Dealer;
        }

        public Dealer Dealer { get => _dealer; set => _dealer = value; }
        public GameManager GameManager { get => _gameManager; }
    }
}
