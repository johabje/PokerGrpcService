using System;
namespace PokerGrpc.Models
{
    public class Card
    {
        // enum to provide key value pairs for cards from two to ace
        public enum Value
        {
            Two = 2, Three, Four, Five, Six, Seven, Eight,
            Nine, Ten, Jack, Queen, King, Ace
        }
        // enum list of suits
        public enum Suit
        {
            Hearts,
            Spades,
            Diamonds,
            Clubs

        }

        public Value myValue { get; set; }

        public Suit mySuit { get; set; }

    }

}
