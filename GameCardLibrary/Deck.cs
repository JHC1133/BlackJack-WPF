﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace GameCardLibrary
{
    internal class Deck
    {

        private List<Card> _cards;

        Random randomArranger;

        public bool GameIsDone { get; set; }

        public Deck(List<Card> cardList)
        {
            _cards = cardList;
        }

        public Card DrawCard()
        {
            if (_cards.Count > 0)
            {
                Card drawnCard = _cards[0];
                _cards.RemoveAt(0);
                return drawnCard;
            }
            else
            {
                throw new InvalidCastException("The deck is empty");
            }
        }
        private void DiscardCards()
        {

        }

        /// <summary>
        /// Using a Fisher-Yates style shuffle
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();

            int n = _cards.Count;

            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card temp = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = temp;
            }
        }

        private int NumberOfCards()
        {
            return _cards.Count;
        }

        //private Card GetAt(int position)
        //{
            
        //}

        private void InitializeDeck(List<Card> cardList)
        {

        }

        private void OnShuffle(object source, EventArgs e)
        {

        }


    }
}