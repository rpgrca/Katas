## Movie Rental Kata

You are given a legacy codebase for a movie rental store. The codebase has a **Customer** class that represents a customer of the store and a **Movie** class that represents a movie available for rental. The **Customer** class has methods for renting and returning movies, calculating rental charges, and generating a rental statement.

The goal of the kata is to refactor the existing codebase using appropriate design patterns and techniques to make it more flexible, maintainable, and testable. Some potential areas for improvement include:

- Removing duplication and improving code organization.
- Simplifying complex conditional logic.
- Applying design patterns such as Strategy, Template Method, or State to handle different movie types and rental calculations.
- Decoupling the rental statement generation from the Customer class using a separate class or template engine.

```csharp
using System;
using System.Collections.Generic;

namespace MovieRentalKata
{
    public class Customer
    {
        public string Name { get; }
        private readonly List<MovieRental> _rentals;

        public Customer(string name)
        {
            Name = name;
            _rentals = new List<MovieRental>();
        }

        public void RentMovie(Movie movie, int daysRented)
        {
            _rentals.Add(new MovieRental(movie, daysRented));
        }

        public string GenerateStatement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string statement = "Rental Statement for " + Name + "\n";

            foreach (var rental in _rentals)
            {
                double rentalAmount = 0;

                // Calculate the rental amount based on movie type and rental duration
                switch (rental.Movie.Type)
                {
                    case MovieType.Regular:
                        rentalAmount += 2;
                        if (rental.DaysRented > 2)
                            rentalAmount += (rental.DaysRented - 2) * 1.5;
                        break;
                    case MovieType.NewRelease:
                        rentalAmount += rental.DaysRented * 3;
                        break;
                    case MovieType.Childrens:
                        rentalAmount += 1.5;
                        if (rental.DaysRented > 3)
                            rentalAmount += (rental.DaysRented - 3) * 1.5;
                        break;
                }

                frequentRenterPoints++;

                // Add bonus points for new release rentals for more than one day
                if (rental.Movie.Type == MovieType.NewRelease && rental.DaysRented > 1)
                    frequentRenterPoints++;

                // Add rental amount to the total
                totalAmount += rentalAmount;

                // Add rental details to the statement
                statement += rental.Movie.Title + "\t" + rentalAmount + "\n";
            }

            // Add footer lines to the statement
            statement += "Total Amount: " + totalAmount + "\n";
            statement += "Frequent Renter Points: " + frequentRenterPoints;

            return statement;
        }
    }

    public class Movie
    {
        public string Title { get; }
        public MovieType Type { get; }

        public Movie(string title, MovieType type)
        {
            Title = title;
            Type = type;
        }
    }

    public enum MovieType
    {
        Regular,
        NewRelease,
        Childrens
    }

    public class MovieRental
    {
        public Movie Movie { get; }
        public int DaysRented { get; }

        public MovieRental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new customer
            var customer = new Customer("John Doe");

            // Rent some movies
            var movie1 = new Movie("The Matrix", MovieType.Regular);
            var movie2 = new Movie("Inception", MovieType.NewRelease);
            var movie3 = new Movie("Toy Story", MovieType.Childrens);

            customer.RentMovie(movie1, 3);
            customer.RentMovie(movie2, 2);
            customer.RentMovie(movie3, 4);

            // Generate and print the statement
            var statement = customer.GenerateStatement();
            Console.WriteLine(statement);

            // Wait for user input
            Console.ReadLine();
        }
    }
}
```

(As given by ChatGPT, also known as _Video Store Kata_)
