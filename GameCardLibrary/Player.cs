﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace GameCardLibrary
{
    public class Player
    {
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
        private int _playerID;

        private Hand _hand;


        public bool IsFinished { get; set; }
        public bool Winner { get; set; }
        public bool IsBust { get; set; }
        public string Name { get => _name; }
        public int PlayerID { get => _playerID; }
        public Hand Hand { get => _hand; set => _hand = value; }
        public string StateText { get => _stateText; set => _stateText = value; }

        public Player(Hand hand, string name)
        {
            _name = name;
            _playerID++;
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
