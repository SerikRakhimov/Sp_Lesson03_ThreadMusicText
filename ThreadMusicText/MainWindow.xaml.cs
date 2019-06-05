using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ThreadMusicText
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        public void PlayMusic()
        {
            var windowDop = new WindowDop();

            windowDop.Height = 0;
            windowDop.Width = 0;
            windowDop.Visibility = Visibility.Hidden;

            windowDop.playerDop.LoadedBehavior = MediaState.Manual;
            windowDop.playerDop.Source = new Uri(@"anita_coj_-_neispravimaja_(zf.fm).mp3", UriKind.RelativeOrAbsolute);
            windowDop.playerDop.Play();

            windowDop.Show();
            //MessageBox.Show("Поток PlayMusic закончил работу");
            //Thread.Sleep(10000);
        }

        public void SaveToFile()
        {
            string writePath = "result.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                string text = null;
                Dispatcher.Invoke(() => text = textInput.Text);
                //Thread.Sleep(5000);
                sw.WriteLine(text);
            }
            //MessageBox.Show("Поток SaveToFile закончил работу");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var thread = new Thread(PlayMusic);

            thread.SetApartmentState(System.Threading.ApartmentState.STA);

            thread.IsBackground = true;  //фоновый поток

            thread.Start();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var thread = new Thread(SaveToFile);

            thread.IsBackground = false;  //основной поток

            thread.Start();

            //MessageBox.Show("Главный поток закончил работу");

        }
    }
}
