using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp
{
    internal class Movie
    {

        private static int nextId = 1;
        public int Id { get; }
        public int HallId { get; set; }
        public string Name { get; set; }
        public double Imdb { get; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        //public string ChooseHallNumber { get; set; }
        public static List<Movie> SelectedMovies { get; set; } = new List<Movie>();

        public List<Ticket>Tickets { get; set; }

        public Movie(string name,double imdb,TimeOnly startTime,TimeOnly endTime)
        {
            Id = nextId++;
            Name = name;
            Imdb = imdb;
            StartTime = startTime;
            EndTime = endTime;
            
        Tickets = new List<Ticket>();
        }

    }
}
