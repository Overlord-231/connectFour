using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace connectFour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[] BoardSize;
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
                BoardSize = newGame.data;
                Grid DynamicBoard = new Grid();
                //DynamicBoard.ShowGridLines = true;
                Board.Children.Clear();

                // Define the Columns

                SolidColorBrush circleOutline = new SolidColorBrush(Colors.Black);
                Border cellBorder = new Border();
                cellBorder.BorderBrush = circleOutline;
                cellBorder.BorderThickness = new Thickness(1);

                for (int i = 0; i < BoardSize[0]; i++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    DynamicBoard.ColumnDefinitions.Add(colDef);

                }

                // Define the Rows
                for (int i = 0; i < BoardSize[1]; i++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    DynamicBoard.RowDefinitions.Add(rowDef);
                }




                for (int row = 0; row < BoardSize[1]; row++)
                {
                    for (int col = 0; col < BoardSize[0]; col++)
                    {

                        Ellipse circle = new Ellipse();
                        circle.Height = 10;
                        circle.Width = 10;
                        circle.Stroke = circleOutline;
                        circle.HorizontalAlignment = HorizontalAlignment.Center;
                        circle.VerticalAlignment = VerticalAlignment.Center;
                        Grid.SetRow(circle, row);
                        Grid.SetColumn(circle, col);
                        DynamicBoard.Children.Add(circle);
                    }
                }

                Board.Children.Add(DynamicBoard);
            }
        }
    }
}