using System;
using System.Runtime.CompilerServices;
using static cinemaApp.Hall;
using static System.Net.Mime.MediaTypeNames;

namespace cinemaApp
{
    internal class Program
    {

        static List<Hall> halls = new List<Hall>();
        //private List<Movie> selectedMovies = new List<Movie>();
        static void Main(string[] args)
        {


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("                                                                                    |-----------Please click the button------------|");
                Console.WriteLine("                                                                                    |                                              |");
                Console.WriteLine("                                                                                    | 0- Exit program                              |");
                Console.WriteLine("                                                                                    |                                              |");
                Console.WriteLine("                                                                                    | 1- Add Hall                                  |");
                Console.WriteLine("                                                                                    |                                              |");
                Console.WriteLine("                                                                                    | 2- Add Movie                                 |");
                Console.WriteLine("                                                                                    |                                              |");
                Console.WriteLine("                                                                                    | 3- Order a ticket                            |");
                Console.WriteLine("                                                                                    |                                              |");
                Console.WriteLine("                                                                                    | 4- List of sales                             |");
                Console.WriteLine("                                                                                    |----------------------------------------------|");




                string button = Console.ReadLine();

                switch (button)
                {
                    case "0":
                        exit = true;
                        Console.WriteLine("Program is closed");
                        break;
                    case "1":
                        AddHall(halls);
                        break;

                    case "2":
                        AddMovie();
                        break;
                    case "3":
                        OrderTicket();
                        break;
                    
                    case "4":
                        Console.WriteLine("Please select the hall (Enter the Hall Id):");
                        foreach (Hall hall in halls)
                        {
                            Console.WriteLine($"Hall Id: {hall.Id}, Hall Name: {hall.Name}");
                        }

                        int selectedHallId = Convert.ToInt32(Console.ReadLine());
                        Hall selectedHall = halls.FirstOrDefault(h => h.Id == selectedHallId);
                        if (selectedHall == null)
                        {
                            Console.WriteLine("The entered Hall Id does not exist. Please enter a valid Hall Id.");
                            break;
                        }

                        Console.WriteLine("Please select the movie (Enter the Movie Id):");
                        foreach (Movie movie in selectedHall.Movies)
                        {
                            Console.WriteLine($"Movie Id: {movie.Id}, Movie Name: {movie.Name}");
                        }

                        int selectedMovieId = Convert.ToInt32(Console.ReadLine());
                        Movie selectedMovie = selectedHall.Movies.FirstOrDefault(m => m.Id == selectedMovieId);
                        if (selectedMovie == null)
                        {
                            Console.WriteLine("The entered Movie Id does not exist. Please enter a valid Movie Id.");
                            break;
                        }

                        List<Ticket> sales = selectedMovie.Tickets;
                        Console.WriteLine($"Sales for Movie: {selectedMovie.Name}");
                        foreach (Ticket ticket in sales)
                        {
                            Console.WriteLine($"Ticket Id: {ticket.Id}, Name: {ticket.Name}, Surname: {ticket.surName}");
                        }
                        break;

                        break;
                    default: Console.WriteLine("You clicked the wrong button, please try again!");
                        break;
                }
            }
            //Environment.Exit(0);



            static void AddHall(List<Hall> halls)
            {
                try
                {
                    //Console.Write("PLs add the id of hall: ");
                    //int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Please add the name of hall: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter the number of rows: ");
                    int row = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter the number of column:");
                    int column = Convert.ToInt32(Console.ReadLine());

                    Hall hall = new Hall(name, row, column);
                    hall.InitializeSeat();
                    halls.Add(hall);

                    Console.WriteLine("The new hall is added.");

                }

                catch (FormatException)
                {
                    Console.WriteLine("The information you entered is in the wrong format. The hall was not added.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The number you entered is too large. The hall was not added.");
                }
            }





            
            static void AddMovie()
            {
                try
                {
                    Console.Write("Please enter the movie's name: ");
                    string name=Console.ReadLine();
                    bool isDuplicate= halls.Any(h => h.Movies.Any(m => m.Name == name));
                    if (isDuplicate == true)
                    {
                        Console.WriteLine("This movie name already exist, so please try different one!");
                        return;
                    }

                    Console.Write("Please enter the IMDb Rating: ");
                    double imdb=Convert.ToDouble(Console.ReadLine());
                    if(imdb<0 && imdb > 10)
                    {
                        Console.WriteLine("Invalid Imdb Rating! It must be in interval 1-10.");
                        return;
                    }
                    Console.Write("Please enter the start time: ");
                   
                    string startTimeInput = Console.ReadLine();
                    string[] startTimeParts = startTimeInput.Split(':');
                    int startHour = Convert.ToInt32(startTimeParts[0]);
                    int startMinute = Convert.ToInt32(startTimeParts[1]);
                    TimeOnly startTime = new TimeOnly(startHour, startMinute);
                    

                    Console.Write("Please Enter the end time : ");
                    string endTimeInput = Console.ReadLine();
                    
                    string[] endTimeParts = endTimeInput.Split(':');
                    int endHour = Convert.ToInt32(endTimeParts[0]);
                    if (startHour > endHour)
                    {
                        Console.WriteLine("Wrong format!");
                        return;
                    }
                    int endMinute = Convert.ToInt32(endTimeParts[1]);
                    TimeOnly endTime = new TimeOnly(endHour, endMinute);
                    


                    Console.WriteLine("Please seleect the hall(id number): ");
                    int hallId=Convert.ToInt32(Console.ReadLine()); 
                    Hall selectedHall= halls.FirstOrDefault(h=>h.Id==hallId);
                    if (selectedHall==null)
                    {
                        Console.WriteLine("The entered Hall Id does not exist. Please enter a valid Hall Id.");
                        return;

                       
                    }


                    bool hasOverlap = selectedHall.Movies.Any(m => m.Name != name && (startTime >= m.StartTime && startTime <= m.EndTime || endTime >= m.StartTime && endTime <= m.EndTime));

                    if (hasOverlap)
                    {
                        Console.WriteLine("Another movie is already scheduled during the selected time in the same hall. Please choose a different time or hall.");
                        return;
                    }


                    Movie newMovie = new Movie(name, imdb, startTime, endTime);
                    selectedHall.AddMovie(newMovie);

                  
                    Console.WriteLine("Movie successfully added.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format for input data. Movie was not added.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The entered number is too large. Movie was not added.");
                }
            }










            static void OrderTicket()
            {
                try
                {
                    Console.Write("Please enter first name: ");
                    string name=Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("This field must be filled");
                        return;
                    }
                    Console.Write("Please enter your last name: ");
                    string LastName=Console.ReadLine();

                    Console.Write("Select the id of hall: ");
                    foreach (Hall hall in halls)
                    {
                        Console.WriteLine($"Hall Id= {hall.Id} Hall Name= {hall.Name}");

                    }

                    int hallId=Convert.ToInt32(Console.ReadLine());
                    Hall selectedHall = halls.FirstOrDefault(h => h.Id == hallId);
                    if (selectedHall==null)
                    {
                        Console.WriteLine("The entered Hall Id does not exist. Please try again.");
                        return;
                    }



                   

                    Console.WriteLine("Available Movies:");
                    foreach (var movie in selectedHall.Movies)
                    {
                        Console.WriteLine($"Movie Id: {movie.Id}, Name: {movie.Name}");
                    }

                    Console.Write("Select the movie (Enter the Movie Id): ");
                    int movieId = int.Parse(Console.ReadLine());

                    Movie selectedMovie = selectedHall.Movies.FirstOrDefault(m => m.Id == movieId);
                    if (selectedMovie == null)
                    {
                        Console.WriteLine("The entered Movie Id does not exist. Please enter a valid Movie Id.");
                        return;
                    }

                    Console.WriteLine($"Selected Movie: {selectedMovie.Name}");








                    Console.Write("Enter your name: ");
                    string name1 = Console.ReadLine();

                    Console.Write("Enter your surname: ");
                    string surname = Console.ReadLine();

                 
                    Console.WriteLine("Available Seats:");
                    for (int i = 0; i < selectedHall.Row; i++)
                    {
                        for (int j = 0; j < selectedHall.Column; j++)
                        {
                            Seat seat = selectedHall.Seats[i, j];
                            //nsole.Write(seat.SeatStatus);
                            Console.Write($"{seat.SeatStatus} ");
                        }

                        Console.WriteLine("\n");
                    }

                    Console.Write("Enter the row number: ");
                    int row = int.Parse(Console.ReadLine());


                    Console.Write("Enter the column number: ");
                    int column = int.Parse(Console.ReadLine());

                    Seat selectedSeat = selectedHall.Seats[row - 1, column - 1];
                    if (selectedSeat.SeatStatus == Seat.Status.Reserved)
                    {
                        Console.WriteLine("This seat is already reserved. Please select a different seat.");
                        return;
                    }

                    selectedSeat.SeatStatus = Seat.Status.Reserved;
                    selectedSeat.HallId = hallId;



                    Ticket newTicket = new Ticket(name, surname, selectedHall, selectedMovie, selectedSeat);
                    selectedMovie.Tickets.Add(newTicket);

                    Console.WriteLine("Ticket successfully ordered.");










                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format for input data. Ticket order was not completed.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The entered number is too large. Ticket order was not completed.");
                }










            }

        }

       
    }
}