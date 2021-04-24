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
            PokerGame lastGame = pokerGame;
            await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame));
            
            while (true)
            {
                pokergame2 = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
                //if (!(pokerGame.toAct.Equals(pokergame2.toAct) && pokerGame.players.Equals(pokergame2.players) && pokerGame.bet.Equals(pokergame2.bet)))
                    if (pokergame2.Equals(lastGame))
                {
                    await responseStream.WriteAsync(PokerGameToGameLobby(pokergame2));
                    lastGame = pokergame2;
                }
                   else
                {
                    Console.WriteLine("Did not send resons this itteration");
                }
                
                await Task.Delay(1000); //gotta look bussy
                Console.WriteLine("The bets are: " + pokerGame.bet+ " and " + pokergame2.bet);
            }
        }

        /*
        Merge Tasks: Bet, Check, Raise, Fold
        get action from request.action
        switch(action)
            Bet()...
            ...
        */

        public override Task<BetResponse> PlayerAction(BetRequest request, ServerCallContext context)
        { 
            ActionResponse badActionResponse = new ActionResponse {success = false});
            PokerGame lobby;
            try {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            } catch {
                return Task.FromResult(badActionResponse);
            }

            Player player;

            player = lobby.playersPlaying.Find(p => p.name.Equals(request.name));

            if (player == null || !player.currentBetter) {
                //player not found or not the players turn to act -> return false
                return Task.FromResult(badActionResponse);
            }
            int actionId = request.action;
            switch (actionId) {
                case 0:
                    // action: fold
                    lobby.Fold(player);
                    break;
                case 1:
                    // action: check;
                    // TODO
                    //only if no one has made a bet (lobby.currentBet =null/0 or something)
                    lobby.NextBetter();
                    break;
                case 2:
                    // action: bet (== raise)
                    //TODO
                    // only if player.bet is higher than a potential current bet
                    // AND bet is smaller and wallet
                    // do this logic in PlaceBet();
                    lobby.PlaceBet(player, request.bet);
                    break;
                case 3:
                    // action: call
                    //  assert player.wallet >= the current bet
                    lobby.Call(player);
                    break;
                default:
                    return Task.FromResult(badActionResponse);
                    break;
            }
            return Task.FromResult(new ActionResponse { success = true });
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
