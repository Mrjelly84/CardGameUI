using System;
using System.IO;

namespace CardGameUI
{
    public static class Log
    {
        private static readonly string LogFilePath = "GameResults.txt";

        /// <summary>
        /// Logs the winner and elapsed time of a game to a text file.
        /// </summary>
        /// <param name="winnerName">The name of the game winner.</param>
        /// <param name="elapsedTime">The elapsed time as a formatted string.</param>
        public static void LogGameResult(string winnerName, string elapsedTime)
        {
            string logEntry = $"   {winnerName} , {elapsedTime}";
            try
            {
                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Optionally handle/log exceptions elsewhere
                // For now, just ignore to avoid crashing the app
            }
        }
    }
}