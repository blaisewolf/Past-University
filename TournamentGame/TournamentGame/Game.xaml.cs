using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TournamentGame.ViewModel;
using System.Collections.Generic;

namespace TournamentGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game(int numberOfPlayers)
        {
            InitializeComponent();
            this.DataContext = new GameEngine(numberOfPlayers);
            SetupIcon();
        }
        void SetupIcon()
        {
            GameEngine game = (GameEngine)this.DataContext;
            for (int i = 0; i < 10; i++) // field iconok
            {
                for (int j = 0; j < 10; j++)
                {
                    Image field = new Image { Height = 40, Width = 40 , ToolTip = "" + (i * 10 + j) };
                    field.SetValue(Grid.RowProperty, i);
                    field.SetValue(Grid.ColumnProperty, j);
                    BitmapImage temp = new BitmapImage();
                    temp.BeginInit();
                    temp.UriSource = new Uri(game.FieldsSource[i * 10 + j], UriKind.Relative);
                    temp.EndInit();
                    field.Source = temp;
                    this.Fields.Children.Add(field);
                }
            }
        }
    }
}
