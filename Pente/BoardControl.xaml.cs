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

namespace Pente
{
    /// <summary>
    /// Interaction logic for BoardControl.xaml
    /// </summary>
    public partial class BoardControl : UserControl
    {

        public BoardControl()
        {
            InitializeComponent();
        }

        public void InitializeBoard(int size)
        {
            Board board = Manager.instance.board;
            board.Initialize(Manager.instance.p1, Manager.instance.p2, size);

            int xLength = board.Grid.GetLength(0);
            int yLength = board.Grid.GetLength(1);

            ugrdGrid.Columns = xLength;
            ugrdGrid.Rows = yLength;

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    PieceControl piece = new PieceControl(new Vec2 { x = x, y = y });
                    piece.DataContext = piece;

                    ugrdGrid.Children.Add(piece);
                }
            }
        }
    }
}
