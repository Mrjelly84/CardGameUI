using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CardGameUI
{
    public class Game
    {
        private Player[] players;
        public int Score { get; set; }
        public string WinnerName { get; set; }

        public void Start()
        {
            Thread.Sleep(1000); // Simulate some delay before starting the game
            Deck deck = new Deck();
            deck.Shuffle();

            int numPlayers = 4;
            int cardsPerPlayer = Math.Max(1, deck.Count / numPlayers);

            if (deck.Count < numPlayers)
            {
                Console.WriteLine("Not enough cards in the deck for all players!");
                Score = 0;
                WinnerName = "No Game";
                return;
            }

            // Initialize players
            players = new Player[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                players[i] = new Player($"Player {i + 1}", deck.DrawRandomCards(cardsPerPlayer));
            }

            int round = 0;
            while (players.All(p => p.Hand.Count > 0) && round < 30)
            {
                Console.WriteLine($"\n--- Round {round + 1} ---");
                PlayRound();
                round++;
            }

            Console.WriteLine("\nGame Over");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} cards: {player.Hand.Count}");
            }

            // Find the highest card count
            int maxCards = players.Max(p => p.Hand.Count);
            var winners = players.Where(p => p.Hand.Count == maxCards).ToList();

            if (winners.Count == 1)
            {
                var winner = winners[0];
                Console.WriteLine($"🏆 {winner.Name} Wins!");
                Score = winner.Hand.Count;
                WinnerName = winner.Name;
            }
            else
            {
                Console.WriteLine("🤝 It's a draw!");
                Score = 0;
                WinnerName = "Draw";
            }
        }

        private void PlayRound()
        {
            // Each player plays a card
            var playedCards = players.Select(p => p.PlayCard()).ToArray();

            // Show what each player played
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine($"{players[i].Name} plays: {playedCards[i]}");
            }

            // Find the highest card value
            int maxValue = playedCards.Max(c => c.Value);
            var roundWinners = playedCards
                .Select((card, idx) => new { Card = card, Index = idx })
                .Where(x => x.Card.Value == maxValue)
                .Select(x => x.Index)
                .ToList();

            if (roundWinners.Count == 1)
            {
                int winnerIdx = roundWinners[0];
                Console.WriteLine($"{players[winnerIdx].Name} wins this round\n");
                // Winner takes all played cards
                players[winnerIdx].AddCards(playedCards);
            }
            else
            {
                Console.WriteLine("It's a draw – cards lost\n");
                // Optionally: cards are discarded
            }
        }
    }
}