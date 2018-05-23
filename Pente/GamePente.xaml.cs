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
using System.Windows.Threading;

namespace Pente
{
    /// <summary>
    /// Interaction logic for GamePente.xaml
    /// </summary>
    public partial class GamePente : Page
    {
        public static int timerF = 20;
        string time = "20";
        public static GamePente instance;
        DispatcherTimer dispatcherTimer;

        public GamePente()
        {
            InitializeComponent();
            GamePente.instance = this;
            BoardControl.InitializeBoard();

            Player1.SetPlayer(ref Manager.instance.p1);
            Player2.SetPlayer(ref Manager.instance.p2);

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            time += "s";
            Timer.DataContext = time;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timerF <= 0)
            {
                ResetClock();
            }
            else
            {
                timerF -= dispatcherTimer.Interval.Seconds;
            }

            time = timerF.ToString();
            time = string.Format("{0:0.#}", time);

            time += "s";
            Timer.DataContext = time;

        }

        public void ResetClock()
        {
            MessageBox.Show("Your turn is skipped.", "Time's Up!");

            Manager.instance.board.turnCount++;
            BoardControl.UpdateImages();

            timerF = 20;
        }

        public void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();

            Manager.instance.board = null;

            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        public static void LeaveGame()
        {
            instance.btnMainMenu_Click(null, null);
        }

        public static void Tressra()
        {
            instance.dispatcherTimer.Stop();
            MessageBoxResult result = MessageBox.Show("Tressra!!", "Four in a row", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                instance.dispatcherTimer.Start();
            }
        }

        internal static void Tria()
        {
            instance.dispatcherTimer.Stop();
            MessageBoxResult result = MessageBox.Show("Tria!!", "Three in a row", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                instance.dispatcherTimer.Start();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save?", "Save", MessageBoxButton.YesNo);
            if (result.HasFlag(MessageBoxResult.Yes))
            {
                DataPersistence.Serializer.SaveToFile(Manager.instance.board);
            } 
        }
    }
}
