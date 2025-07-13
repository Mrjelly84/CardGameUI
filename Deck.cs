using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameUI
{
    public class Deck
    {
        private List<Card> cards;
        private static Random rng = new Random(); // Add this line

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            Dictionary<string, int> ranks = new Dictionary<string, int>
            {
                {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6},
                {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10},
                {"J", 11}, {"Q", 12}, {"K", 13}, {"A", 14}
            };

            foreach (string suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card
                    {
                        Suit = suit,
                        Rank = rank.Key,
                        Value = rank.Value
                    });
                }
            }
        }

        public int Count => cards.Count;

        public void Shuffle()//using fisher-yates shuffle algorithm
        {
            int n = cards.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1); // 0 <= j <= i
                // Swap cards[i] and cards[j]
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public List<Card> DrawRandomCards(int count)
        {
            // Defensive: don't draw more than available
            count = Math.Min(count, cards.Count);

            Random rand = new Random();
            int n = cards.Count;

            // Partial Fisher-Yates shuffle
            for (int i = 0; i < count; i++)
            {
                int j = rand.Next(i, n); // pick random index from i to n-1
                                         // Swap cards[i] and cards[j]
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            // Take the first 'count' cards as the hand
            List<Card> hand = cards.Take(count).ToList();

            // Remove the drawn cards from the deck
            cards.RemoveRange(0, count);

            return hand;
        }
    }
}