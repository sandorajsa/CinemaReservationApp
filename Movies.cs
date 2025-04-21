using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationApp
{
    public class Movie
    {
        public string title { get; set; }
        public string description { get; set; }
        public string poster { get; set; }
        public string[] play_dates { get; set; }
        public int length_minutes { get; set; }
        public string age_limit { get; set; }
        public string genre { get; set; }
    }

}
