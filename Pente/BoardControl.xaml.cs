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
        Board board = new Board();

        public BoardControl()
        {
            InitializeComponent();
        }

        public void InitializeBoard(/*Player P1, Player P2*/)
        {
            board.Initialize(Manager.instance.p1, Manager.instance.p2);

            for(int y = 0; y < board.Grid.GetLength(1); y++)
            {
                for (int x = 0; x < board.Grid.GetLength(0); x++)
                {
                    PieceControl piece = new PieceControl(new Board.Vec2 { x = x, y = y });
                    Binding b = new Binding("State")
                    {
                        Mode = BindingMode.OneWay,
                        Converter = new PlayerPieceToImageConverter()
                    };


                    ugrdGrid.Children.Add(piece);
                }
            }
        }
    }
}
