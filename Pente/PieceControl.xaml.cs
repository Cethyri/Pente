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
    /// Interaction logic for PieceControl.xaml
    /// </summary>
    public partial class PieceControl : UserControl
    {
        public Board.Vec2 Position { get; private set; }
        public Board.Piece State { get; set; } // need to access Board instance and set grid[position] state

        public PieceControl(Board.Vec2 position)
        {
            InitializeComponent();

            Position = position;
        }
    }
}
