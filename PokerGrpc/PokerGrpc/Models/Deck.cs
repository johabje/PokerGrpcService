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

        public Stack<Card> cardStack { get; }

        public Random rand = new Random();



        public Deck()
        {
            //mby have a singleton for a complete deck instead of generating?
            // ^if yes, use a copy of the singleton list when randomizing stack
            this.cards = new List<Card>(GenerateDeck());
            this.cardStack =  new Stack<Card>(GenerateRandomizedStack(cards));

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


        public Stack<Card> GenerateRandomizedStack(List<Card> cards) {
            int cardsLeft = cards.Count;
            Stack<Card> stack = new Stack<Card>();
            while (cardsLeft > 0) {
                int nextCardIndex = rand.Next(cardsLeft);
                stack.Push(cards[nextCardIndex]);
                cards.RemoveAt(nextCardIndex);
                cardsLeft = cards.Count;
            }
            return stack;
        }


        public Card DealCardSingle() {
            Card card = cardStack.Pop();
            return card;
        }

        /* might not need
        public List<Card> DealCardMultiple(int cardCount) {
            List<Card> cardsToDeal = new List<Card>();
            for (int i = 0; i < cardCount; i++) {
                cardsToDeal.Add(cardStack.Pop());
            }
            return cardsToDeal;
        }
        */

    }
}
