using System;
namespace PokerGrpc.Models
{
    public class Player
    {
        double wallet { get; set; }
        String name { get; set; }
        Boolean isRoomOwner { get; set; }
        //etc etc

        public Player()
        {
        }
    }
}
