using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cinemaApp
{
    public class Seat
    {


        public int Row { get; set; }
        public int Column { get; set; }
        public Seat[,] Seats { get; set; }
        public int HallId { get; set; }
        public Seat( int row, int column,int hallId)
        {
            
            Row = row;
            Column = column;
            HallId = hallId;
            Seats = new Seat[row, column];
          

        }
        public enum Status
        {
            Empty,
            Reserved
        }

        public Status SeatStatus { get; set; }
        public Seat()
        {
            SeatStatus = Status.Empty;

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
       
        
    }
}
