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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		HowToPlay howToPlayWindow;
		GameWindow gameWindow;
        public MainWindow()
        {
            InitializeComponent();

		}

		//~MainWindow()
		//{
		//	if(howToPlayWindow != null)
		//	howToPlayWindow.Close();

		//}
			
        //PopupWindow for mode, Select mode, true/false for mode select, popup name window, open actual game window
        public void Start()
        {

		}

		public void Quit()
        {
            Application.Current.Shutdown();
        }

		private void btnPlay_Click(object sender, RoutedEventArgs e)
		{
			gameWindow = new GameWindow(this);
			this.Hide();
			gameWindow.Show();
		}

		private void btnHowToPlay_Click(object sender, RoutedEventArgs e)
		{
			howToPlayWindow = new HowToPlay();
			howToPlayWindow.Show();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Quit();
		}
	}
}
