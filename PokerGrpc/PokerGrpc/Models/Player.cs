using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public enum Action
    {
        fold = 0,
        check = 1,
        bet = 2,
        call = 3,
        raise = 4,

    }
    public class Player
    {
        public Guid id { get; set; }
        public double wallet { get; set; }
        public String name { get; set; }
        public Boolean isRoomOwner { get; set; }
        public List<Card> Hand { get; set; }
        public String bestCombo { get; set; }
        public bool folded { get; set; }
        public int action { get; set; }
        //etc etc

        public Player()
        {
        }

        public override string ToString()
        {
            return "player:"+ this.id.ToString() + this.wallet+this.name+this.bestCombo;
        }
    }
}
