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

namespace CinemaReservationApp
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }
        public string MovieTitle { get; set; }
        public string Date { get; set; }
        public DataWindow(Reservation reservation,List<Reservation> reservations, string selectedTitle, string date)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Reservation = reservation;
            this.Reservations = reservations;
            MovieTitle = selectedTitle;
            reservation.MovieName = selectedTitle;
            reservation.Date = date;
            Date = date;
            Seats_Generate();
        }

        private void Seats_Generate()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    CheckBox checkBox = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Name = $"seat_{i}_{j}",
                    };
                    foreach (var item in Reservations)
                    {
                        if (item.Seats.Contains(checkBox.Name) && item.MovieName == MovieTitle && item.Date == Date)
                        {
                            checkBox.IsEnabled = false;
                            checkBox.Background = Brushes.Red;
                        }
                    }
                    checkBox.LayoutTransform = new ScaleTransform(1.75, 1.75);
                    checkBox.Click += (s, e) =>
                    {
                        if (checkBox.IsChecked == true)
                        {
                            Reservation.Seats.Add(checkBox.Name);
                            checkBox.Background = Brushes.Green;
                        }
                        else
                        {
                            Reservation.Seats.Remove(checkBox.Name);
                            checkBox.Background = Brushes.Transparent;
                        }
                    };
                    Grid.SetRow(checkBox, i);
                    Grid.SetColumn(checkBox, j);
                    seats_GR.Children.Add(checkBox);
                }
            }
        }
        private bool InputCheck()
        {
            if(String.IsNullOrWhiteSpace(Reservation.Name) || String.IsNullOrWhiteSpace(Reservation.Phone) || String.IsNullOrWhiteSpace(Reservation.Email) || again_TXB.Text == "")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Reservation.Seats.Count == 0)
            {
                MessageBox.Show("Please select at least one seat!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void cancel_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void done_BTN_Click(object sender, RoutedEventArgs e)
        {
            if(InputCheck())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
