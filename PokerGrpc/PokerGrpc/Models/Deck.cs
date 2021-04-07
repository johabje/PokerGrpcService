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
            for (int i =1; i<5; i++)
            {
                char suit = 'S';
                switch (i)
                {
                    case 1:
                        suit = 'S';
                        break;
                    case 2:
                        suit = 'S';
                        break;
                    case 3:
                        suit = 'S';
                        break;
                    case 4:
                        suit = 'S';
                        break;
                }
                for (int j = 2; i < 15; j++)
                {
                    Card card = new Card(suit, j);
                    this.cards.Add(card);

                }
            }
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
            List<Card> flop = new List<Card>();
            cards.RemoveAt(0);
            flop.Add(this.cards[0]);
            cards.RemoveAt(0);
            flop.Add(this.cards[0]);
            cards.RemoveAt(0);
            flop.Add(this.cards[0]);
            cards.RemoveAt(0);
            return flop;
        }

        public Card DealTurn()
        {
            cards.RemoveAt(0);
            Card turn = this.cards[0];
            cards.RemoveAt(0);
            return turn;
        }

        public Card DealRiver()
        {
            cards.RemoveAt(0);
            Card river = this.cards[0];
            cards.RemoveAt(0);
            return river;
        }



    }
}
