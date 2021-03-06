using System;
using System.Collections.Generic;

namespace GrpcConsoleClient
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
        public string Hand { get; set; }
        public String bestCombo { get; set; }
        public int action { get; set; }
        //etc etc

        public Player()
        {
        }
    }
}
