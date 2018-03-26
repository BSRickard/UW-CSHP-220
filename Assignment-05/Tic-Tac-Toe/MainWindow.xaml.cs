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
// Date:    2018-03-26

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const bool   IS_A_WINNER = true;
        const int TILES_PER_SIDE = 3;
        static int[,]      array = new int[TILES_PER_SIDE, TILES_PER_SIDE];
        static int     turnCount = -1;
        enum PLAYER          { X = 1,
                               O = TILES_PER_SIDE + 1 };

        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
            for (int x = 0; x < array.Length; x++)
            {
                TextBlock label = new TextBlock()
                {
                    FontSize = 20,
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
            array = new int[3,3];
            foreach (UIElement element in uxGrid.Children)
            {
                element.IsEnabled = true;
            }
            TextBlock label;
            int index;
            for (index = 0; index < array.Length; ++index)
            {
                label = (TextBlock)uxGrid.Children[array.Length + index];
                label.Text = "";
            }
            turnCount = -1;
            ShowStatus(!IS_A_WINNER);
       }

        private void ShowStatus(bool WeHaveAWinner)
        {
            if (WeHaveAWinner)
            {
                uxTurn.Text = string.Format("{0} is a winner",
                    turnCount % 2 == 0 ? PLAYER.X.ToString() : PLAYER.O.ToString());
            }
            else if (++turnCount < array.Length)
            {
                uxTurn.Text = string.Format("{0}'s turn",
                    turnCount % 2 == 0 ? PLAYER.X.ToString() : PLAYER.O.ToString());
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
            Button button = (Button)sender;
            int row    = int.Parse(button.Tag.ToString().Substring(0, 1));
            int column = int.Parse(button.Tag.ToString().Substring(2, 1));
            if (0 != array[row, column]) return;
            TextBlock label = (TextBlock)uxGrid.Children[array.Length + row * 3 + column];
            if (turnCount % 2 == 0)
            {
                array[row, column] = (int)PLAYER.X;
                label.Text = PLAYER.X.ToString();
            }
            else
            {
                array[row, column] = (int)PLAYER.O;
                label.Text = PLAYER.O.ToString();
            }
            ShowStatus(IsAWinner(row, column));
        }

        private bool IsAWinner(int Row, int Column)
        {
            int scoreToWin  = TILES_PER_SIDE * (int)(turnCount % 2 == 0 ? PLAYER.X : PLAYER.O);
            int scoreRow    = 0;
            int scoreColumn = 0;
            int index;
            for (index = 0; index < TILES_PER_SIDE; ++index)
            {
                scoreRow    += array[index, Column];
                scoreColumn += array[Row,    index];
            }
            if (scoreRow == scoreToWin || scoreColumn == scoreToWin) return IS_A_WINNER;
            // If placed at corner or centre...
            if ((Row * TILES_PER_SIDE + Column) % (TILES_PER_SIDE - 1) == 0)
            {
                // ...Check diagonals
                int scoreDiag = 0;
                for (index = 0; index < array.Length; index += TILES_PER_SIDE + 1)
                {
                    scoreDiag += array[index / TILES_PER_SIDE, index % TILES_PER_SIDE];
                }
                if (scoreDiag == scoreToWin) return IS_A_WINNER;
                scoreDiag = 0;
                for (index = TILES_PER_SIDE - 1; 
                    index < (array.Length - (TILES_PER_SIDE - 1)); 
                    index += TILES_PER_SIDE - 1)
                {
                    scoreDiag += array[index / TILES_PER_SIDE, index % TILES_PER_SIDE];
                }
                if (scoreDiag == scoreToWin) return IS_A_WINNER;
            }
            return !IS_A_WINNER;
        }

        private void uxExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
