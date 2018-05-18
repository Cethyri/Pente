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
            Piece piece = (Piece)value;

            string imagePath = "Images/";

            switch (piece)
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

            imagePath += ".png";

            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}