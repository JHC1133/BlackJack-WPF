using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace GameCardLib
{
    internal class Deck
    {

        ListManager<Card> cards;

        Random randomArranger;

        public bool GameIsDone { get; set; }

        public Deck(List<Card> cardList)
        {
            
        }

        public Deck(List<Card> cardList, List<Card> secondCardList)
        {
            
        }

        private void AddCard(Card card)
        {

        }

        private void RemoveCard(int pos)
        {

        }

        private void DiscardCards()
        {

        }

        private List<Card> GetTwoCards()
        {

        }

        private int SumOfCards()
        {

        }

        private int NumberOfCards()
        {

        }

        private Card GetAt(int position)
        {

        }

        private void InitializeDeck(List<Card> cardList)
        {

        }

        private void OnShuffle(object source, EventArgs e)
        {

        }


    }
}
