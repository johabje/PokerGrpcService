using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    enum Suits
    {
        S,    // 0
        H,   // 1
        C,      // 2
        D,      // 3
        
    }
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
            List<Card> newCards;
            /*
            for (int i =1; i<5; i++)
            {
                Suits suits = (Suits)i;
                char suit = (char)suits;
                switch (i)
                {
                    case 1:
                        suit = 'S';
                        for (int j = 2; i < 15; j++)
                        {
                            Card card = new Card(suit, j);
                            newCards.Add(card);

                        }
                        break;
                    case 2:
                        for (int j = 2; i < 15; j++)
                        {
                            Card card = new Card(suit, j);
                            newCards.Add(card);

                        }
                        break;
                    case 3:
                        for (int j = 2; i < 15; j++)
                        {
                            Card card = new Card(suit, j);
                            newCards.Add(card);

                        }
                        break;
                    case 4:
                        for (int j = 2; i < 15; j++)
                        {
                            Card card = new Card(suit, j);
                            newCards.Add(card);

                        }
                        break;
                }*/
            newCards = new List<Card>()
        {
           new Card('H', 2),
           new Card('H', 3),
           new Card('H', 4),
           new Card('H', 5),
           new Card('H', 7),
           new Card('H', 8),
           new Card('H', 9),
           new Card('H', 10),
           new Card('H', 11),
           new Card('H', 12),
           new Card('H', 13),
           new Card('H', 14),


        };
            this.cards = newCards;
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
