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
            await Task.Delay(1000);
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
            GPlayer markus = new GPlayer()
            {
                Name = "markus",
                Action = 0,
                BestCombo = "0",
                Hand = "hh",
                IsRoomOwner = false,
                Wallet = 1000

            };

            //var startReply = client.CreateNewGame(new NewGameRequest { Gplayer = johan, GamePin = 666});
            //Console.WriteLine(startReply);
            var join1 = client.JoinGame(new JoinGameRequest { Gplayer = fredrik, GamePin = 666 });
            Console.WriteLine(join1);
            await Task.Delay(1000);
            var join2 = client.JoinGame(new JoinGameRequest { Gplayer = syver, GamePin = 666 });
            Console.WriteLine(join2);

            var start = client.StartGame(new StartGameRequest { Gamepin = 666, PlayerName = "johan" });
            Console.WriteLine("Starting "+start);
            //For action, the values are:
            // -1 -> no action yet
            // 0 -> fold
            // 1 -> check
            // 2 -> bet
            // 3 -> call

            //write code for starting game
            var action1 = client.Action(new ActionRequest
            {
                Action = 3,
                Bet = 0,
                GamePin = 666,
                Name = "johan"
            });
            Console.WriteLine("action1:"+action1);
            await Task.Delay(1000);

            var action2 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });
            Console.WriteLine(action2);
            await Task.Delay(1000);

            var action3 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "syver"
            });
            Console.WriteLine(action3);
            await Task.Delay(1000);

            var action4 = client.Action(new ActionRequest
            {
                Action = 2,
                Bet = 200,
                GamePin = 666,
                Name = "fredrik"
            });
            Console.WriteLine(action4);
            await Task.Delay(1000);

            var action5 = client.Action(new ActionRequest
            {
                Action = 2,
                Bet = 400,
                GamePin = 666,
                Name = "syver"
            });
            Console.WriteLine(action5);

            await Task.Delay(1000);

            var action6 = client.Action(new ActionRequest {
                Action = 0,
                Bet = 0,
                GamePin = 666,
                Name = "johan"
            });
            Console.WriteLine(action6);
            await Task.Delay(1000);

            var action7 = client.Action(new ActionRequest {
                Action = 3,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });
            Console.WriteLine(action7);
            await Task.Delay(1000);

            var action8 = client.Action(new ActionRequest {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });
            Console.WriteLine(action8);
            await Task.Delay(1000);

            var action9 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "syver"
            });
            Console.WriteLine(action9);
            await Task.Delay(1000);

            var action10 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "fredrik"
            });
            Console.WriteLine(action10);
            await Task.Delay(1000);
            
            var action11 = client.Action(new ActionRequest
            {
                Action = 1,
                Bet = 0,
                GamePin = 666,
                Name = "syver"
            });
            Console.WriteLine(action11);
            await Task.Delay(1000);
            



            using (var call = client.StartStream(new JoinGameRequest { GamePin = 666, Gplayer = fredrik }))
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
