using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public class Deck
    {
        const int numberOfCards = 52;
        private List<Card> cards { get; set; }

        public Deck()
        {
            cards = new List<Card>();
        }

        public void GenerateDeck()
        {
            //generat on of each card and shuffel to random order.
        }

        public List<Card> DealHand()
        {
            List<Card> hand = new List<Card>();
            hand.Add(this.cards[0]);
            cards.RemoveAt(0);
            hand.Add(this.cards[0]);
            cards.RemoveAt(0);
            return hand;
        }

        public List<Card> DealFlop()
        {
            //deal the flop, burn one card
            return null;
        }

        public List<Card> DealTurn()
        {
            //deal the turn, burn one card
            return null;
        }

        public Card DealRiver()
        {
            //Deal river, burn card
            return null;
        }



    }
}
