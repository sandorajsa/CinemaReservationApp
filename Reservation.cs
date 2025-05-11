using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationApp
{
    public class Reservation
    {
        public string MovieName { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<string> Seats { get; set; } = new List<string>();
    }
}
