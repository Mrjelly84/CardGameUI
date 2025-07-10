using System;
using System.Text;
using System.Windows;
using System.IO;

namespace CardGameUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game; // Class-level field

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            game = new Game();

            // Capture output from the game
            StringBuilder outputBuilder = new StringBuilder();
            using (var writer = new StringWriter(outputBuilder))
            {
                var originalOut = Console.Out;
                Console.SetOut(writer);

                game.Start();

                Console.SetOut(originalOut);
            }

            // Display captured output in the TextBlock
            txtBkOutput.Text = outputBuilder.ToString();
            lblStatus.Content = "Game started!";
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            game = new Game();

            // Capture output from the game
            StringBuilder outputBuilder = new StringBuilder();
            using (var writer = new StringWriter(outputBuilder))
            {
                var originalOut = Console.Out;
                Console.SetOut(writer);

                game.Start();

                Console.SetOut(originalOut);
            }

            // Reset UI elements as needed
            lblStatus.Content = "Game restarted!";
            txtBkOutput.Text = outputBuilder.ToString();
            // Clear card displays, scores, etc. if you have them
        }
    }
}