using System;

namespace FunctionalTicketCalculator
{
    class Program
    {
        enum TicketType
        {
            Standard,
            Vip
        }

        enum DayType
        {
            Weekday,
            Weekend
        }

        static decimal ApplyPricingRule(decimal price, Func<decimal, decimal> rule) => rule(price);

        static decimal CustomerCategoryRule(decimal price, int age, bool isStudent)
        {
            if (age < 6)
            {
                return 0m;
            }
            else if (age >= 6 && age <= 12)
            {
                return price * 0.5m;
            }
            else if (isStudent)
            {
                return price * 0.85m;
            }
            else if (age >= 60)
            {
                return price * 0.7m;
            }
            else
            {
                return price;
            }
        }

        static decimal TicketTypeRule(decimal price, TicketType ticketType)
        {
            if (ticketType == TicketType.Standard)
            {
                return price;
            }
            else if (ticketType == TicketType.Vip)
            {
                return price * 1.25m;
            }
            else
            {
                return price;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Price:");
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price >= 0)
            {
                Console.WriteLine($"Price: {price}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                return;
            }

            Console.WriteLine("Enter Age:");
            if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
            {
                Console.WriteLine($"Age: {age}");
            }
            else
            {
                Console.WriteLine("Invalid Input. Please enter a valid int number");
                return;
            }

            Console.WriteLine("Enter whether customer is Student:");
            Console.WriteLine(" * True");
            Console.WriteLine(" * False");
            if (bool.TryParse(Console.ReadLine(), out bool isStudent))
            {
                Console.WriteLine($"Is Student: {isStudent}");
            }
            else
            {
                Console.WriteLine("Invalid Input. Please enter one of the options listed above");
                return;
            }

            Console.WriteLine("Enter Ticket Type:");
            Console.WriteLine(" * Standard");
            Console.WriteLine(" * Vip");
            if (Enum.TryParse(Console.ReadLine(), out TicketType ticketType))
            {
                Console.WriteLine($"Ticket Type: {ticketType}");
            }
            else
            {
                Console.WriteLine("Invalid Input. Please enter one of the options listed above");
                return;
            }

            Console.WriteLine("Enter Day Type:");
            Console.WriteLine(" * Weekday");
            Console.WriteLine(" * Weekend");
            if (Enum.TryParse(Console.ReadLine(), out DayType dayType))
            {
                Console.WriteLine($"Day Type: {dayType}");
            }
            else
            {
                Console.WriteLine("Invalid Input. Please enter one of the options listed above");
                return;
            }

            Func<decimal, decimal> dayTypeRule = p => dayType == DayType.Weekend ? p * 1.1m : p;

            Console.WriteLine("Applying Rules...");
            Console.WriteLine($"Start Price: {price}");

            decimal currentPrice = price;

            currentPrice = ApplyPricingRule(currentPrice, p => CustomerCategoryRule(p, age, isStudent));
            Console.WriteLine($"After applying Customer Category Rule: {currentPrice}");
            currentPrice = ApplyPricingRule(currentPrice, p => TicketTypeRule(p, ticketType));
            Console.WriteLine($"After applying Ticket Type Rule: {currentPrice}");
            currentPrice = ApplyPricingRule(currentPrice, dayTypeRule);
            Console.WriteLine($"After applying Day Type Rule: {currentPrice}");

            decimal finalPrice = Math.Round(currentPrice, 2);

            Console.WriteLine($"Total cost: {finalPrice}");
        }
    }
}