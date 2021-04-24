using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
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
            GPlayer johan = new GPlayer()
            {
                Name = "johan",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000

            };
            GPlayer fredrik = new GPlayer()
            {
                Name = "fredrik",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000

            };
            GPlayer syver = new GPlayer()
            {
                Name = "syver",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000

            };

            var startReply = client.CreateNewGame(new NewGameRequest { Gplayer = johan, Gamepin = 666});
            Console.WriteLine(startReply);
            var join1 = client.JoinGame(new JoinGameRequest { Gplayer = fredrik, GamePin = 666 });
            Console.WriteLine(join1);
            var join2 = client.JoinGame(new JoinGameRequest { Gplayer = syver, GamePin = 666 });
            Console.WriteLine(join2);

            //write code for starting game
            var action1 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "johan"
            });

            var action2 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });

            var action3 = client.Action(new ActionRequest
            {
                Action = 2,
                Bet = 50,
                GamePin = 666,
                Name = "syver"
            });

            var action4 = client.Action(new ActionRequest
            {
                Action = 0,
                Bet = 0,
                GamePin = 666,
                Name = "johan"
            });
            var action5 = client.Action(new ActionRequest
            {
                Action = 3,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });


            using (var call = client.StartStream(new JoinGameRequest { GamePin = 666, Gplayer = syver }))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    GameLobby feature = call.ResponseStream.Current;
                    Console.WriteLine("Received " + feature.ToString());
                    client.Action(new ActionRequest
                    {
                        Action = 0,
                        Bet = 0,
                        GamePin = 666,
                        Name = "johan"
                    });
                }
                Console.WriteLine("why the fuck are you here");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
