using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace CardGameUI
{
   
    public partial class MainWindow : Window
    {
        private Game game; // Class-level field
        private Stopwatch stopwatch; // Stopwatch to measure elapsed time

        public MainWindow()
        {
            InitializeComponent();
            stopwatch = new Stopwatch(); // Initialize the stopwatch
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            game = new Game();

            // Restart the stopwatch
            stopwatch.Restart();

            // Capture output from the game
            StringBuilder outputBuilder = new StringBuilder();
            using (var writer = new StringWriter(outputBuilder))
            {
                var originalOut = Console.Out;
                Console.SetOut(writer);

                game.Start();

                Console.SetOut(originalOut);
            }


            
            // Stop the stopwatch
            stopwatch.Stop();

            // Reset UI elements as needed
            txtBkOutput.Text = outputBuilder.ToString();

            // Display the elapsed time in the Timer text block
            Timer.Text = $"Time taken: {stopwatch.ElapsedMilliseconds} ms";
        }


    }
}