using System;
using System.Windows;
using TournamentGame.ViewModel;

namespace TournamentGame
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        public Scores(GameEngine main)
        {
            InitializeComponent();
            PlayerScores.ItemsSource = main.CurrentScores;
        }
    }
}
