using System;
using System.Collections.Generic;

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

        public void Shuffle()
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
            List<Card> hand = new List<Card>();
            Random rand = new Random();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                int index = rand.Next(cards.Count);
                hand.Add(cards[index]);
                cards.RemoveAt(index);
            }
            return hand;
        }
    }
}