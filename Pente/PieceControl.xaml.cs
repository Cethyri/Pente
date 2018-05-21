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
        public Vec2 Position { get; private set; }
        public string ImagePath { get {


                string imagePath = "Images/";
                Board board = Manager.instance.board;

                if (board.Grid == null)
                {
                    return "";
                }

                switch (board.Grid.Get(Position))
                {
                    case Piece.P1:
                        imagePath += "P1";
                        break;
                    case Piece.P2:
                        imagePath += "P2";
                        break;
                    case Piece.EMPTY:
                        imagePath += "Empty";
                        break;
                }

                imagePath += Position.x == 0 ? "-x" : Position.x == board.Grid.GetLength(0) - 1 ? "+x" : "";

                imagePath += Position.y == 0 ? "-y" : Position.y == board.Grid.GetLength(1) - 1 ? "+y" : "";

                imagePath += ".png";

                return imagePath;
            } }
        public PieceControl(Vec2 position)
        {
            InitializeComponent();

            Position = position;
        }

        public PieceControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.instance.board.PlacePiece(Position);
            imgNotWorking.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
        }
    }
}
