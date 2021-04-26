using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcStreamListener
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch(
            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://127.0.0.1:8080");
            var client = new Game.GameClient(channel);

            GPlayer johan = new GPlayer()
            {
                Name = "johan",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000
            };

            var makelobby = client.CreateNewGame(new NewGameRequest { Gplayer = johan, GamePin = 666 });
            Console.WriteLine(makelobby);


            using (var call = client.StartStream(new JoinGameRequest { GamePin = 666, Gplayer = johan }))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    GameLobby feature = call.ResponseStream.Current;
                    Console.WriteLine("Received " + feature.ToString());

                }
                Console.WriteLine("why the fuck are you here");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
