using System;
namespace PokerGrpc.Models
{
        public enum CardSuit
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs
        }

        public enum CardValue
        {
            Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
            Ace = 14,
        }

        public class Card
        {
            public CardSuit CardSuit { get; private set; }
            public int CardValue { get; private set; }

            public Card(CardSuit cardSuit, int cardValue)
            {
                CardSuit = cardSuit;
                CardValue = cardValue;
            }
        }

    }
