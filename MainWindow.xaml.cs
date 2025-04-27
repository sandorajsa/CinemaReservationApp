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
            ShowMoviesInGR();
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

        private void ShowMoviesInGR()
        {
            foreach (var movie in MoviesToShow)
            {
                RowDefinition row = new()
                {
                    Height = new GridLength(300, GridUnitType.Pixel)
                };
                movies_GR.RowDefinitions.Add(row);
                Border border = new()
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(255, 213, 79)),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(10),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(255, 248, 225))
                };
                Grid grid = new Grid()
                {
                    ColumnDefinitions = {
                        new ColumnDefinition() { Width = new GridLength(200, GridUnitType.Pixel) },
                        new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) },
                        new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    RowDefinitions = {
                        new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) },
                        new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                        new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) }
                    }
                };
                Image image = new()
                {
                    Source = new BitmapImage(new Uri(movie.poster, UriKind.Relative)),
                    Margin = new Thickness(10)
                };
                Grid.SetRowSpan(image, 3);
                grid.Children.Add(image);
                TextBlock title = new()
                {
                    Text = movie.title,
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10,0,10,0),
                    Foreground = new SolidColorBrush(Color.FromRgb( 198, 40, 40)),
                };
                Grid.SetColumn(title, 1);
                grid.Children.Add(title);
                TextBlock desc = new()
                {
                    Text = movie.description,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 16,
                    Margin = new Thickness(10, 0, 10, 0),
                    Foreground = new SolidColorBrush(Color.FromRgb(168, 112, 63)),
                    
                };
                Grid.SetRow(desc, 1);
                Grid.SetColumn(desc, 1);
                grid.Children.Add(desc);
                StackPanel stackPanel = new()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10, 0, 10, 0)
                };
                Grid.SetRow(stackPanel, 2);
                Grid.SetColumn(stackPanel, 1);
                TextBlock textBlock = new()
                {
                    Text = "Age limit: ",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(198, 40, 40)),
                };
                stackPanel.Children.Add(textBlock);
                TextBlock ageLimit = new()
                {
                    Text = movie.age_limit,
                    Margin = new Thickness(5, 0, 5, 0),
                };
                stackPanel.Children.Add(ageLimit);
                TextBlock textBlock2 = new()
                {
                    Text = "| Length: ",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(198, 40, 40)),
                    Margin = new Thickness(10, 0, 10, 0),
                };
                stackPanel.Children.Add(textBlock2);
                TextBlock length = new()
                {
                    Text = movie.length_minutes.ToString() + " min",
                    Margin = new Thickness(5, 0, 5, 0),
                };
                stackPanel.Children.Add(length);
                TextBlock textBlock3 = new()
                {
                    Text = "| Genre: ",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(198, 40, 40)),
                    Margin = new Thickness(10, 0, 10, 0),
                };
                stackPanel.Children.Add(textBlock3);
                TextBlock genre = new()
                {
                    Text = movie.genre,
                    Margin = new Thickness(5, 0, 5, 0),
                };
                stackPanel.Children.Add(genre);
                grid.Children.Add(stackPanel);

                StackPanel dateBTNS = new()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10, 0, 10, 0)
                };
                Grid.SetRow(dateBTNS, 3);
                Grid.SetColumn(dateBTNS, 2);
                foreach (var date in movie.play_dates)
                {
                    Button dateBTN = new()
                    {
                        Content = date,
                        Style = (Style)FindResource("categoryButtons"),
                        Margin = new Thickness(5, 0, 5, 0),
                    };
                    dateBTNS.Children.Add(dateBTN);
                }
                grid.Children.Add(dateBTNS);
                border.Child = grid;
                Grid.SetRow(border, movies_GR.RowDefinitions.Count - 1);
                movies_GR.Children.Add(border);
            }
        }
    }
}