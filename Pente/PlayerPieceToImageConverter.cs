using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Pente
{
    public class PlayerPieceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board.Piece piece = (Board.Piece)value;

            string imagePath = "Images/";

            switch (piece)
            {
                case Board.Piece.P1:
                    imagePath += "P1";
                    break;
                case Board.Piece.P2:
                    imagePath += "P2";
                    break;
                case Board.Piece.EMPTY:
                    imagePath += "Empty";
                    break;
            }

            imagePath += ".png";

            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}