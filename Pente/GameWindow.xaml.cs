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
using System.Windows.Shapes;

namespace Pente
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		Window parent;
		public GameWindow(Window window)
		{
			parent = window;
			InitializeComponent();
            BoardControl.InitializeBoard();
		}
		//~GameWindow()
		//{
		//	parent.Visibility = Visibility.Visible;
		//}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            parent.Visibility = Visibility.Visible;
        }
	}
}
