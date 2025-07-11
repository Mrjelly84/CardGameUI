using System.Collections.Generic;
using System.Text;

namespace CardGameUI
{
    public class ScoreEntry
    {
        public string WinnerName { get; set; }
        public int Score { get; set; }
    }

    public class Score
    {
        private List<ScoreEntry> scores = new List<ScoreEntry>();

        public void AddScore(string winnerName, int score)
        {
            scores.Add(new ScoreEntry { WinnerName = winnerName, Score = score });
        }

        public override string ToString()
        {
            if (scores.Count == 0)
                return "No scores yet.";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < scores.Count; i++)
            {
                sb.AppendLine($"Game {i + 1}: {scores[i].WinnerName} ({scores[i].Score})");
            }
            return sb.ToString();
        }
    }
}