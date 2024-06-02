﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;

namespace connectFour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] BoardSize;
        private string[,] Board;
        private bool red = true;
        private bool yellow = false;
        private Grid DynamicBoard = new Grid();
        private Grid StartingBoard = new Grid();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame newGame = new NewGame();
            newGame.ShowDialog();
            if (newGame.DialogResult == true)
            {
                
                // Create child of the 'Board' grid
                BoardSize = newGame.data;
                BoardSize[1]++;
                InitializeBoard(BoardSize);
                BoardGrid.Children.Clear();
                DynamicBoard = new Grid();
                StartingBoard = new Grid();
                // Define the Columns
                for (int i = 0; i < BoardSize[0]; i++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    StartingBoard.ColumnDefinitions.Add(colDef);
                }

                for (int i = 0; i < BoardSize[0]; i++)
                {
                    FirstRow(StartingBoard, i);
                }

                // Define the Rows
                for (int i = 0; i < BoardSize[1]; i++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    StartingBoard.RowDefinitions.Add(rowDef);
                }
                
                // Add the empty circles to the board with borders
                for (int row = 1; row < BoardSize[1]; row++)
                {
                    for (int col = 0; col < BoardSize[0]; col++)
                    {
                        CreateCircles(StartingBoard, row, col);
                    }
                }

                DynamicBoard = StartingBoard;
                BoardGrid.Children.Add(StartingBoard);
            }
        }

        private void Reset_click(object sender, RoutedEventArgs e)
        {
            BoardGrid.Children.Clear();
        }

        private void CreateCircles(Grid grid, int row, int column)
        {
            Border border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Child = new Ellipse
                {
                    Height = 25,
                    Width = 25,
                    Stroke = new SolidColorBrush(Colors.Black),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            
            grid.Children.Add(border);
        }

        private void FirstRow(Grid grid, int column)
        {
            Button button = new Button()
            {
                Height = 30,
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "\u2193",
            };
            button.Click += Move_Click;
            Grid.SetColumn(button, column);
            grid.Children.Add(button);
        }

        private void Move_Click(Object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int column = Grid.GetColumn(button);
            
            if (red)
            {
                for (int i = BoardSize[1]-1; i > 0; i--)
                {
                    if (Board[column, i] == "o")
                    {
                        Board[column, i] = "r";
                        red = false;
                        yellow = true;
                        
                        // Find the ellipse in the grid and change its color to red
                        var border = FindChildAt(DynamicBoard, i, column) as Border;
                        if (border != null && border.Child is Ellipse ellipse)
                        {
                            ellipse.Fill = new SolidColorBrush(Colors.Red);
                        }

                        BoardGrid.Children.Clear();
                        BoardGrid.Children.Add(DynamicBoard);
                        break;
                    }
                }

                if (red)
                {
                    MessageBox.Show("Invalid move!");
                }
            }
            else
            {
                for (int i = BoardSize[1]-1; i < BoardSize[1]; i--)
                {
                    if (Board[column, i] == "o")
                    {
                        Board[column, i] = "y";
                        red = true;
                        yellow = false;

                        var border = FindChildAt(DynamicBoard, i, column) as Border;
                        if (border != null && border.Child is Ellipse ellipse)
                        {
                            ellipse.Fill = new SolidColorBrush(Colors.Yellow);
                        }
                        
                        BoardGrid.Children.Clear();
                        BoardGrid.Children.Add(DynamicBoard);
                        break;
                    }
                }

                if (yellow)
                {
                    MessageBox.Show("Invalid move!");
                }
            }
        }

        private void CheckForWin()
        {
            
        }

        private void InitializeBoard(int[] BoardSize)
        {
            Board = new string[BoardSize[0], BoardSize[1]];
            for (int i = 0; i < BoardSize[0]; i++)
            {
                for (int j = 0; j < BoardSize[1]; j++)
                {
                    Board[i, j] = "o";
                }
            }
        }
        
        private UIElement FindChildAt(Grid grid, int row, int column)
        {
            return grid.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
        }
    }
}