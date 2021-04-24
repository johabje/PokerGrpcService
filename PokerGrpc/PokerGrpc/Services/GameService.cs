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
            player.lastAction = 0;
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

        /*
        ADD START GAME FUNCTIONALITY
        */

        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        { 
            Player player = new Player
            {
                name = request.Gplayer.Name,
                isRoomOwner = false,
                Hand = null,
                bestCombo = null,
                lastAction = 0,
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
            await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame, request.Gplayer.Name));
            
            while (true)
            {
                pokergame2 = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
                //if (!(pokerGame.toAct.Equals(pokergame2.toAct) && pokerGame.players.Equals(pokergame2.players) && pokerGame.bet.Equals(pokergame2.bet)))
                    if (!lastGame.toAct.Equals(pokergame2.toAct) || !lastGame.tableCards.Count().Equals(pokergame2.tableCards.Count()))
                {

                    await responseStream.WriteAsync(PokerGameToGameLobby(pokergame2, request.Gplayer.Name));
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

        /*
        TODO change names etc here -> game.proto
        */
        public override Task<ActionResponse> Action(ActionRequest request, ServerCallContext context)
        { 
            ActionResponse badActionResponse = new ActionResponse {Success = false};
            PokerGame lobby;
            try {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            } catch {
                return Task.FromResult(badActionResponse);
            }

            Player player;
            try {
                player = lobby.playersPlaying.Find(p => p.name.Equals(request.Name));
            } catch {
                return Task.FromResult(badActionResponse);
            }

            if (player == null || !player.currentBetter) {
                //player not found or not the players turn to act -> return false
                return Task.FromResult(badActionResponse);
            }
            int actionId = request.Action;
            switch (actionId) {
                case 0:
                    // action: fold
                    lobby.Fold(player);
                    break;
                case 1:
                    // action: check;
                    if (!lobby.Check(player)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                case 2:
                    // action: bet (== raise)
                    if (!lobby.PlaceBet(player, request.Bet)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                case 3:
                    // action: call
                    if(!lobby.Call(player)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                default:
                    return Task.FromResult(badActionResponse);
                    break;
            }
            player.lastAction = actionId;
            lobby.UpdateState();
            return Task.FromResult(new ActionResponse { Success = true });
        }
        public override Task<StartGameResponse> StartGame(StartGameRequest request, ServerCallContext context)
        {
            StartGameResponse badActionResponse = new StartGameResponse { Success = false };
            PokerGame lobby;
            try
            {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.Gamepin));
            }
            catch
            {
                return Task.FromResult(badActionResponse);
            }

            foreach (Player player in lobby.players)
            {
                if (player != null && player.name == request.PlayerName)
                {
                    if (player.isRoomOwner)
                    {
                        lobby.NewRound();
                        return Task.FromResult(new StartGameResponse { Success = true });
                    }
                }
            }
            return Task.FromResult(badActionResponse);

        }

        public GameLobby PokerGameToGameLobby(PokerGame pokerGame, string playerName)
        {
            GameLobby gamelobby = new GameLobby
            {
                GamePin = pokerGame.gamePin,
                ToAct = PlayerToGPlayer(pokerGame.toAct, pokerGame),

                TableCards = pokerGame.GetCards(pokerGame.tableCards),

                Pot = pokerGame.pot,
                Bet = pokerGame.bet,
                Blind = pokerGame.blind
            };
            foreach (var player in pokerGame.players)
            {
                if (player != null)
                {
                    GPlayer gPlayer = PlayerToGPlayer(player, pokerGame);
                    if (gPlayer.Name != playerName)
                    {
                        gPlayer.Hand = "x";
                    }
                    gamelobby.Gplayers.Add(gPlayer);
                }
            }
            return gamelobby;
        }

        public GPlayer PlayerToGPlayer(Player player, PokerGame pokerGame)
        {
            string hand;
            if (player.Hand == null)
            {
                hand = "0";
            }
            else
            {
                hand = pokerGame.GetCards(player.Hand);
            }
            GPlayer gParticipant = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = hand,
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            return gParticipant;
        }
    }
}
