using System.Text;
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
        private Grid DynamicBoard;
        private Grid StartingBoard;
        private bool hasItEnded = false;
        private int redPair;
        private int yellowPair;

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

                redPair = 0;
                yellowPair = 0;

                StatusDisplay.Text = "Red starts!";
                DynamicBoard = StartingBoard;
                hasItEnded = false;
                BoardGrid.Children.Add(StartingBoard);
            }
        }

        private void Reset_click(object sender, RoutedEventArgs e)
        {
            StatusDisplay.Text = "Red starts!";
            BoardGrid.Children.Clear();
            InitializeBoard(BoardSize);
            BoardGrid.Children.Add(StartingBoard);
            hasItEnded = false;
            red = true;
            yellow = false;
            redPair = 0;
            yellowPair = 0;
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
            if (!hasItEnded)
            {
                if (red)
                {
                    for (int i = BoardSize[1] - 1; i > 0; i--)
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

                            StatusDisplay.Text = "Yellow's turn!";
                            BoardGrid.Children.Clear();
                            BoardGrid.Children.Add(DynamicBoard);
                            CheckForWin();
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
                    for (int i = BoardSize[1] - 1; i > 0; i--)
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

                            StatusDisplay.Text = "Red's turn!";
                            BoardGrid.Children.Clear();
                            BoardGrid.Children.Add(DynamicBoard);
                            CheckForWin();
                            break;
                        }
                    }

                    if (yellow)
                    {
                        MessageBox.Show("Invalid move!");
                    }
                }
            }
            else
            {
                MessageBox.Show("The game has ended, click on the New Game or Reset button to start a new one.");
            }
        }

        private void CheckForWin()
        {
            CheckVertically();
            CheckHorizontally();
            CheckDiagonally();
        }

        private void CheckDiagonally()
        {
            // ascendingDiagonalCheck 
            for (int i = 3; i < BoardSize[0]; i++)
            {
                for (int j = 0; j < BoardSize[1] - 3; j++)
                {
                    if (Board[i, j] == "r" && Board[i - 1, j + 1] == "r" &&
                        Board[i - 2, j + 2] == "r" && Board[i - 3, j + 3] == "r")
                    {
                        MessageBox.Show("Red wins!");
                        StatusDisplay.Text = "Red wins!";
                        hasItEnded = true;
                    }
                }
            }
            
            for (int i = 3; i < BoardSize[0]; i++)
            {
                for (int j = 0; j < BoardSize[1] - 3; j++)
                {
                    if (Board[i, j] == "y" && Board[i - 1, j + 1] == "y" &&
                        Board[i - 2, j + 2] == "y" && Board[i - 3, j + 3] == "y")
                    {
                        MessageBox.Show("Yellow wins!");
                        StatusDisplay.Text = "Yellow wins!";
                        hasItEnded = true;
                    }
                }
            }

            // descendingDiagonalCheck
            for (int i = 3; i < BoardSize[0]; i++)
            {
                for (int j = 3; j < BoardSize[1]; j++)
                {
                    if (Board[i, j] == "r" && Board[i - 1, j - 1] == "r" &&
                        Board[i - 2, j - 2] == "r" && Board[i - 3, j - 3] == "r")
                    {
                        MessageBox.Show("Red wins!");
                        StatusDisplay.Text = "Red wins!";
                        hasItEnded = true;
                    }
                }
            }
            
            for (int i = 3; i < BoardSize[0]; i++)
            {
                for (int j = 3; j < BoardSize[1]; j++)
                {
                    if (Board[i, j] == "y" && Board[i - 1, j - 1] == "y" &&
                        Board[i - 2, j - 2] == "y" && Board[i - 3, j - 3] == "y")
                    {
                        MessageBox.Show("Yellow wins!");
                        StatusDisplay.Text = "Yellow wins!";
                        hasItEnded = true;
                    }
                }
            }
        }

        private void CheckHorizontally()
        {
            for (int row = 0; row < BoardSize[1]; row++)
            {
                for (int col = 1; col < BoardSize[0]; col++)
                {
                    if (Board[col - 1, row] == "r" && Board[col, row] == "r")
                    {
                        redPair++;
                        if (redPair == 3)
                        {
                            MessageBox.Show("Red Wins!");
                            hasItEnded = true;
                            StatusDisplay.Text = "Red wins!";
                            break;
                        }
                    }
                    else
                    {
                        redPair = 0;
                    }
                }

                for (int col = 1; col < BoardSize[0]; col++)
                {
                    if (Board[col - 1, row ] == "y" && Board[col, row] == "y")
                    {
                        yellowPair++;
                        if (yellowPair == 3)
                        {
                            MessageBox.Show("Yellow Wins!");
                            hasItEnded = true;
                            StatusDisplay.Text = "Yellow wins!";
                            break;
                        }
                    }
                    else
                    {
                        yellowPair = 0;
                    }
                }
            }
        }

        private void CheckVertically()
        {
            for (int col = 0; col < BoardSize[0]; col++)
            {
                for (int row = 1; row < BoardSize[1]; row++)
                {
                    if (Board[col, row - 1] == "r" && Board[col, row] == "r")
                    {
                        redPair++;
                        if (redPair == 3)
                        {
                            MessageBox.Show("Red Wins!");
                            hasItEnded = true;
                            StatusDisplay.Text = "Red wins!";
                            break;
                        }
                    }
                    else
                    {
                        redPair = 0;
                    }
                }

                for (int row = 1; row < BoardSize[1]; row++)
                {
                    if (Board[col, row - 1] == "y" && Board[col, row] == "y")
                    {
                        yellowPair++;
                        if (yellowPair == 3)
                        {
                            MessageBox.Show("Yellow Wins!");
                            hasItEnded = true;
                            StatusDisplay.Text = "Yellow wins!";
                            break;
                        }
                    }
                    else
                    {
                        yellowPair = 0;
                    }
                }
            }
        }

        private void InitializeBoard(int[] BoardSize)
        {
            Board = new string[BoardSize[0], BoardSize[1]];
            BoardGrid.Children.Clear();
            DynamicBoard = new Grid();
            StartingBoard = new Grid();
            for (int i = 0; i < BoardSize[0]; i++)
            {
                for (int j = 0; j < BoardSize[1]; j++)
                {
                    Board[i, j] = "o";
                }
            }

            // Define the Rows
            for (int i = 0; i < BoardSize[1]; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                StartingBoard.RowDefinitions.Add(rowDef);
            }

            // Define the Columns
            for (int i = 0; i < BoardSize[0]; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                StartingBoard.ColumnDefinitions.Add(colDef);
            }

            //Create the buttons in the first row
            for (int i = 0; i < BoardSize[0]; i++)
            {
                FirstRow(StartingBoard, i);
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
        }

        private UIElement FindChildAt(Grid grid, int row, int column)
        {
            return grid.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
        }
    }
}