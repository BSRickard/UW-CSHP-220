using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Course:  UW CSHP 220 C (2018 Winter)
// Project: Assignment 05
// Author:  RICKARD, Brian
// Date:    2018-03-27

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum                PLAYER { X, O };
        const int   TILES_PER_SIDE = 3;
        const int TOTAL_TILE_COUNT = TILES_PER_SIDE * TILES_PER_SIDE;
        const bool     IS_A_WINNER = true;
        static int       turnCount = 0;
        static string     playerUp;

        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
            for (int x = 0; x < TOTAL_TILE_COUNT; x++)
            {
                TextBlock label = new TextBlock()
                {
                    FontSize            = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };
                uxGrid.Children.Add(label);
                Grid.SetRow   (label, x / TILES_PER_SIDE);
                Grid.SetColumn(label, x % TILES_PER_SIDE);
            }
            ShowStatus(!IS_A_WINNER);
        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            int elementIndex = 0;
            foreach (UIElement element in uxGrid.Children)
            {
                element.IsEnabled = true;
                // The second half of elements group are the labels
                if (elementIndex++ >= TOTAL_TILE_COUNT)
                {
                    ((TextBlock)element).Text = "";
                    ((TextBlock)element).FontWeight = FontWeights.Normal;
                }
            }
            turnCount = 0;
            ShowStatus(!IS_A_WINNER);
       }

        private void ShowStatus(bool WeHaveAWinner)
        {
            playerUp = turnCount % 2 == 0 ? PLAYER.O.ToString() : PLAYER.X.ToString();
            if (WeHaveAWinner)
            {
                uxTurn.Text = string.Format("{0} is a winner", playerUp);
            }
            else if (turnCount++ < TOTAL_TILE_COUNT)
            {
                playerUp = turnCount % 2 == 0 ? PLAYER.O.ToString() : PLAYER.X.ToString();
                uxTurn.Text = string.Format("{0}'s turn", playerUp);
                return;
            }
            else
            {
                uxTurn.Text = ("Cat's game; no winner");
            }
            foreach (UIElement element in uxGrid.Children)
            {
                element.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button   = (Button)sender;
            int row         = int.Parse(button.Tag.ToString().Substring(0, 1));
            int column      = int.Parse(button.Tag.ToString().Substring(2, 1));
            TextBlock label = (TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + row * TILES_PER_SIDE + column];
            if ("" != label.Text) return;
            label.Text = playerUp;
            ShowStatus(IsAWinner(row, column));
        }

        private bool IsAWinner(int Row, int Column)
        {
            int scoreRow    = 0;
            int scoreColumn = 0;
            for (int index  = 0; index < TILES_PER_SIDE; ++index)
            {
                if (playerUp == ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + Row * TILES_PER_SIDE + index]).Text)
                {
                    scoreRow++;
                }
                if (playerUp == ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + index * TILES_PER_SIDE + Column]).Text)
                {
                    scoreColumn++;
                }
            }
            if (scoreRow == TILES_PER_SIDE)
            {
                for (int index = 0; index < TILES_PER_SIDE; ++index)
                {
                    ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + Row * TILES_PER_SIDE + index]).FontWeight
                        = FontWeights.Bold;
                }
                return IS_A_WINNER;
            }
            if (scoreColumn == TILES_PER_SIDE)
            {
                for (int index = 0; index < TILES_PER_SIDE; ++index)
                {
                    ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + index * TILES_PER_SIDE + Column]).FontWeight
                        = FontWeights.Bold;
                }
                return IS_A_WINNER;
            }
            // If placed at corner or centre...
            if ((Row * TILES_PER_SIDE + Column) % (TILES_PER_SIDE - 1) == 0)
            {
                // ...Check diagonals
                int scoreDiag = 0;
                for (int index = 0; index < TOTAL_TILE_COUNT; index += TILES_PER_SIDE + 1)
                {
                    if (playerUp == ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + 
                        (int)(index / TILES_PER_SIDE) * TILES_PER_SIDE + index % TILES_PER_SIDE]).Text) scoreDiag++;
                }
                if (scoreDiag == TILES_PER_SIDE)
                {
                    for (int index = 0; index < TOTAL_TILE_COUNT; index += TILES_PER_SIDE + 1)
                    {
                        ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + 
                            (int)(index / TILES_PER_SIDE) * TILES_PER_SIDE + index % TILES_PER_SIDE]).FontWeight 
                            = FontWeights.Bold;
                    }
                    return IS_A_WINNER;
                }
                scoreDiag = 0;
                for (int index = TILES_PER_SIDE - 1; 
                    index < (TOTAL_TILE_COUNT - (TILES_PER_SIDE - 1)); 
                    index += TILES_PER_SIDE - 1)
                {
                    if (playerUp == ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT + 
                        (int)(index / TILES_PER_SIDE) * TILES_PER_SIDE + index % TILES_PER_SIDE]).Text) scoreDiag++;
                }
                if (scoreDiag == TILES_PER_SIDE)
                {
                    for (int index = TILES_PER_SIDE - 1;
                        index < (TOTAL_TILE_COUNT - (TILES_PER_SIDE - 1));
                        index += TILES_PER_SIDE - 1)
                    {
                        ((TextBlock)uxGrid.Children[TOTAL_TILE_COUNT +
                            (int)(index / TILES_PER_SIDE) * TILES_PER_SIDE + index % TILES_PER_SIDE]).FontWeight
                            = FontWeights.Bold;
                    }
                    return IS_A_WINNER;
                }
            }
            return !IS_A_WINNER;
        }

        private void uxExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
