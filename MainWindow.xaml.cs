using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace CardGameUI
{
    public partial class MainWindow : Window
    {
        private Game game;
        private Stopwatch stopwatch;
        private DispatcherTimer dispatcherTimer;
        private Score scoreManager = new Score();

        public MainWindow()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100); // update every 0.1s
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Live update while the game is running
            Timer.Text = $"Time: {stopwatch.Elapsed:mm\\:ss\\.ff}";
        }

        private async void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            game = new Game();

            stopwatch.Restart();
            dispatcherTimer.Start();
            Timer.Text = $"Time: {stopwatch.Elapsed:mm\\:ss\\.ff}";

            // Capture output from the game
            StringBuilder outputBuilder = new StringBuilder();
            var originalOut = Console.Out;
            using (var writer = new StringWriter(outputBuilder))
            {
                try
                {
                    Console.SetOut(writer);

                    // Run the game asynchronously so the UI/timer can update
                    await Task.Run(() => game.Start());
                }
                finally
                {
                    Console.SetOut(originalOut); // Always restore, even if exception
                }
            }

            stopwatch.Stop();
            dispatcherTimer.Stop();

            // Update game output
            txtBkOutput.Text = outputBuilder.ToString();

            // Display the final elapsed time
            Timer.Text = $"Time taken: {stopwatch.Elapsed:mm\\:ss\\.ff}";

            // Record and display the score
            scoreManager.AddScore(game.WinnerName, game.Score);
            txtBkScore.Text = scoreManager.ToString();
            Log.LogGameResult(game.WinnerName, stopwatch.Elapsed.ToString(@"mm\:ss\.ff"));
        }
    }
}