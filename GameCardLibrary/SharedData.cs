using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class SharedData
    {

        int numberOfPlayers;
        int numberOfDecks;

        private SharedData()
        {
            
        }


        #region Singleton
        private static SharedData _instance;

        public static SharedData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SharedData();
                }

                return _instance;
            }
        }
        #endregion

        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public int NumberOfDecks { get => numberOfDecks; set => numberOfDecks = value; }
    }
}
