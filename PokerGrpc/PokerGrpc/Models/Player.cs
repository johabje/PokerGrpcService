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
        //public String id { get; set; }

        public String name { get; set; }
        public double wallet { get; set; }
        public Boolean isRoomOwner { get; set; }
        public List<Card> Hand { get; set; }
        public String bestCombo { get; set; }
        public int curentGameBetTotal { get; set; }
        public bool firstToBet = false;
        public bool currentRoundFirstToBet = false;
        public bool currentBetter = false;
        public int lastAction = -1;
        public float bet;

        public Player()
        {
        }

        public override bool Equals(Object obj)
        {
            Player personObj = obj as Player;
            if (personObj == null)
                return false;
            else
                return name.Equals(personObj.name) && wallet.Equals(personObj.wallet);
        }
    }
}
