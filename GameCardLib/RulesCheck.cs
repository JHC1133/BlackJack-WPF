using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    internal class RulesCheck
    {
        private const int bustValue = 22;
        private const int blackjackValue = 21;

        /// <summary>
        /// Returns true if a hand is bust
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public bool IsBust(Hand hand)
        {
            if (hand.Score > bustValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if a hand has the value of 21 with only two cards, namely a Blackjack
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public bool IsBlackjack(Hand hand)
        {
            if (hand.NumberOfCards == 2 && hand.Score == blackjackValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if Player score is greater than the dealer score
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public bool PlayerWon(Player player, Dealer dealer)
        {
            if (player.Hand.Score > dealer.Hand.Score)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if player and dealer score are equal
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public bool IsTie(Player player, Dealer dealer)
        {
            if (player.Hand.Score == dealer.Hand.Score)
            {
                return true;
            }
            return false;
        }

    }
}
