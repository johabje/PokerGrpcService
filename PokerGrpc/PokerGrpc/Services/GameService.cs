using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PokerGrpc.Models;
using PokerGrpc.Singelton;

namespace PokerGrpc.Services
{
    public class GameService : Game.GameBase
    {
        private readonly ILogger<GameService> _logger;
        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
        }

        public override Task<GameLobby> CreateNewGame(NewGameRequest request, ServerCallContext context)
        {
            Player player = new Player();
            player.name = request.Gplayer.Name;
            player.isRoomOwner = false;
            player.Hand = null;
            player.bestCombo = null;
            player.action = 0;
            player.wallet = 0;
            PokerGame lobby = new PokerGame(player, 1, 666, 6);


            lobby.gamePin = 666;
            GPlayer gPlayer = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = true,
                Name = player.name,
                Wallet = player.wallet,
            };
            GameLobby gameLobby = new GameLobby
            {
                GamePin = 666,
                ToAct = gPlayer,
                TableCards = "0",
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            gameLobby.Gplayers.Add(gPlayer);
            foreach (PokerGame game in StorageSingleton.Instance.currentGames)
            {
                if (game.gamePin == lobby.gamePin)
                {
                    return Task.FromResult(gameLobby);
                }
            }
            StorageSingleton.Instance.currentGames.Add(lobby);
            Console.WriteLine(StorageSingleton.Instance.currentGames);
            return Task.FromResult(gameLobby);

        }


        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        { 
            Player player = new Player
            {
                name = request.Gplayer.Name,
                isRoomOwner = false,
                Hand = null,
                bestCombo = null,
                action = 0,
                wallet = 0,
            };
            PokerGame lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            lobby.AddPlayer(player);
            GPlayer gPlayer = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            GameLobby gameLobby = new GameLobby
            {
                GamePin = 888,
                ToAct = gPlayer,
                TableCards = "x",
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            foreach (Player participant in lobby.players)
            {
                if (participant != null)
                {
                    GPlayer gParticipant = new GPlayer
                    {
                        Action = 0,
                        BestCombo = "0",
                        Hand = "0",
                        IsRoomOwner = false,
                        Name = participant.name,
                        Wallet = participant.wallet,
                    };
                    gameLobby.Gplayers.Add(gParticipant);
                }
            }
            return Task.FromResult(gameLobby);
        }

        public override async Task StartStream(JoinGameRequest request, IServerStreamWriter<GameLobby> responseStream, ServerCallContext context)
        {
            PokerGame pokerGame = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            PokerGame pokergame2;
            Console.WriteLine("pokergamePin" + pokerGame.gamePin);
            double lastbet = pokerGame.bet;
            await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame));
            
            while (true)
            {
                pokergame2 = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
                //if (!(pokerGame.toAct.Equals(pokergame2.toAct) && pokerGame.players.Equals(pokergame2.players) && pokerGame.bet.Equals(pokergame2.bet)))
                    if (pokergame2.bet != lastbet)
                {
                    await responseStream.WriteAsync(PokerGameToGameLobby(pokergame2));
                    lastbet = pokergame2.bet;
                }
                   else
                {
                    Console.WriteLine("Did not send resons this itteration");
                }
                
                await Task.Delay(1000); //gotta look bussy
                Console.WriteLine("The bets are: " + pokerGame.bet+ " and " + pokergame2.bet);
            }
        }

        public override Task<BetResponse> Bet(BetRequest request, ServerCallContext context)
        { 
            PokerGame lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin)).PlaceBet(lobby.toAct, request.Amount);
            foreach (var element in StorageSingleton.Instance.currentGames)
            {
                Console.WriteLine("is there more than 1 lobby?");
            }
            return Task.FromResult(new BetResponse
            {
                Succsess = true,
            });
        }

        public override Task<CallResponse> Call(CallRequest request, ServerCallContext context)
        {
            return base.Call(request, context);
        }

        public override Task<FoldResponse> Fold(FoldRequest request, ServerCallContext context)
        {
            return base.Fold(request, context);
        }

        public override Task<RaiseResponse> Raise(RaiseRequest request, ServerCallContext context)
        {
            return base.Raise(request, context);
        }



        public GameLobby PokerGameToGameLobby(PokerGame pokerGame)
        {
            GameLobby gamelobby = new GameLobby
            {
                GamePin = pokerGame.gamePin,
                ToAct = PlayerToGPlayer(pokerGame.toAct),
                TableCards = "0",
                Pot = pokerGame.pot,
                Bet = pokerGame.bet,
                Blind = pokerGame.blind
            };
            foreach (var player in pokerGame.players)
            {
                if (player != null)
                {
                    GPlayer gPlayer = PlayerToGPlayer(player);
                    gamelobby.Gplayers.Add(gPlayer);
                }
            }
            return gamelobby;
        }

        public GPlayer PlayerToGPlayer(Player player)
        {
            GPlayer gParticipant = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            return gParticipant;
        }
    }
}
