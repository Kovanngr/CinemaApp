using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinemaApp
{
    internal class Hall
    {
        private static int nextId = 1;
        public int Id { get; }

        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public  Seat[,] Seats {get; set;}
        public List<Movie> Movies { get; set; }


        public Hall(string name, int row, int column)
        {
            Id = nextId++;
            Name = name;
            Row = row;
            Column = column;
            Seats= new Seat[row, column];
            Movies = new List<Movie>();

        }
        public enum Status
        {
            Empty,
            Reserved
        }

      
        public void InitializeSeat()
        {
            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Seats[i, j] = new Seat();
                }

            }
        }

        
        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

       



    }

    
}
