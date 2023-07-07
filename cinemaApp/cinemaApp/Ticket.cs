using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp
{
    internal class Ticket
    {

        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; set; }
        public string surName { get; set; }

        public Hall SelectedHall { get; set; }
        public Movie SelectedMovie { get; set; }
        public Seat SelectedSeat { get; set; }
        public Ticket(string name, string surName, Hall selectedHall, Movie selectedMovie, Seat selectedSeat)
        {
            Id = nextId++;
            Name = name;
            this.surName = surName;
            SelectedHall = selectedHall;
            SelectedMovie = selectedMovie;
            SelectedSeat = selectedSeat;
        }
    }
}
