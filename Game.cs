using System;
using System.Threading;

namespace CardGameUI
{
    public class Game
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private Player player4;

        public void Start()
        {
            Deck deck = new Deck();
            deck.Shuffle();

            int numPlayers = 4;
            // Deal as many cards as possible, but at least 1 per player
            int cardsPerPlayer = Math.Max(1, deck.Count / numPlayers);

            // If you want a maximum of 3 cards per player, use this instead:
            // int cardsPerPlayer = Math.Min(3, deck.Count / numPlayers);

            // Ensure there are enough cards for all players to get at least 1
            if (deck.Count < numPlayers)
            {
                Console.WriteLine("Not enough cards in the deck for all players!");
                return;
            }

            player1 = new Player("Player 1", deck.DrawRandomCards(cardsPerPlayer));
            player2 = new Player("Player 2", deck.DrawRandomCards(cardsPerPlayer));
            player3 = new Player("Player 3", deck.DrawRandomCards(cardsPerPlayer));
            player4 = new Player("Player 4", deck.DrawRandomCards(cardsPerPlayer));

            int round = 0;
            while (player1.Hand.Count > 0 && player2.Hand.Count > 0 && player3.Hand.Count > 0 && player4.Hand.Count > 0 && round < 30)
            {
                Console.WriteLine($"\n--- Round {round + 1} ---");
                PlayRound();
                //Thread.Sleep(100);
                round++;
            }

            Console.WriteLine("\nGame Over");
            Console.WriteLine($"{player1.Name} cards: {player1.Hand.Count}");
            Console.WriteLine($"{player2.Name} cards: {player2.Hand.Count}");
            Console.WriteLine($"{player3.Name} cards: {player3.Hand.Count}");
            Console.WriteLine($"{player4.Name} cards: {player4.Hand.Count}");

            // Winner logic for 4 players
            if (player1.Hand.Count > player2.Hand.Count && player1.Hand.Count > player3.Hand.Count && player1.Hand.Count > player4.Hand.Count)
            {
                Console.WriteLine("🏆 Player 1 Wins!");
            }
            else if (player2.Hand.Count > player1.Hand.Count && player2.Hand.Count > player3.Hand.Count && player2.Hand.Count > player4.Hand.Count)
            {
                Console.WriteLine("🏆 Player 2 Wins!");
            }
            else if (player3.Hand.Count > player1.Hand.Count && player3.Hand.Count > player2.Hand.Count && player3.Hand.Count > player4.Hand.Count)
            {
                Console.WriteLine("🏆 Player 3 Wins!");
            }
            else if (player4.Hand.Count > player1.Hand.Count && player4.Hand.Count > player2.Hand.Count && player4.Hand.Count > player3.Hand.Count)
            {
                Console.WriteLine("🏆 Player 4 Wins!");
            }
            else
            {
                Console.WriteLine("🤝 It's a draw!");
            }
        }

        private void PlayRound()
        {
            Card card1 = player1.PlayCard();
            Card card2 = player2.PlayCard();
            Card card3 = player3.PlayCard();
            Card card4 = player4.PlayCard();

            Console.WriteLine($"{player1.Name} plays: {card1}");
            Console.WriteLine($"{player2.Name} plays: {card2}");
            Console.WriteLine($"{player3.Name} plays: {card3}");
            Console.WriteLine($"{player4.Name} plays: {card4}");

            // Find the highest card value
            int maxValue = Math.Max(Math.Max(card1.Value, card2.Value), Math.Max(card3.Value, card4.Value));
            int winners = 0;
            if (card1.Value == maxValue) winners++;
            if (card2.Value == maxValue) winners++;
            if (card3.Value == maxValue) winners++;
            if (card4.Value == maxValue) winners++;

            if (winners == 1)
            {
                if (card1.Value == maxValue)
                {
                    Console.WriteLine($"{player1.Name} wins this round\n");
                    player1.AddCards(card1, card2, card3, card4);
                }
                else if (card2.Value == maxValue)
                {
                    Console.WriteLine($"{player2.Name} wins this round\n");
                    player2.AddCards(card2, card1, card3, card4);
                }
                else if (card3.Value == maxValue)
                {
                    Console.WriteLine($"{player3.Name} wins this round\n");
                    player3.AddCards(card3, card1, card2, card4);
                }
                else if (card4.Value == maxValue)
                {
                    Console.WriteLine($"{player4.Name} wins this round\n");
                    player4.AddCards(card4, card1, card2, card3);
                }
            }
            else
            {
                Console.WriteLine("It's a draw – cards lost\n");
            }
        }
    }
}