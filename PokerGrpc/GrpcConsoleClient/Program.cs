using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;


namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch(
            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new Game.GameClient(channel);
            GPlayer player = new GPlayer()
            {
                Name = "johan",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000

            };
            Console.WriteLine(player);
            var reply = client.CreateNewGame(
                              new NewGameRequest { Gplayer = player });
            Console.WriteLine("The thing is hanging");
            Console.WriteLine("Greeting: " + reply);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
