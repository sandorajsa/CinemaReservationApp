using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;

namespace CinemaReservationApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Movie> Movies { get; set; }
        private ObservableCollection<Movie> moviesToShow;

        public ObservableCollection<Movie> MoviesToShow
        {
            get { return moviesToShow; }
            set { moviesToShow = value;OnPropertyChanged(nameof(MoviesToShow)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string tulajdonsagNev)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            FileRead();
            CatButtons();
        }
        private void FileRead()
        {
            string jsonStr = File.ReadAllText("./Resources/cinema_movies.json");
            Movies = JsonSerializer.Deserialize<ObservableCollection<Movie>>(jsonStr);
            MoviesToShow = JsonSerializer.Deserialize<ObservableCollection<Movie>>(jsonStr);

        }
        private void CatButtons()
        {
            StackPanel stp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            foreach (var movie in Movies)
            {
                Button btn = new Button()
                {
                    Content = movie.genre.ToUpper(),
                    Style = (Style)FindResource("categoryButtons") 
                };
                stp.Children.Add(btn);
            }
            search_GR.Children.Add(stp);
        }
    }
}