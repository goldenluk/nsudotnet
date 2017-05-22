using System;
using System.Collections.Generic;

namespace Khadzhiev.Nsudotnet.Calendar
{
    internal class Program
    {
        private static DateTime _dateTemp;
        private static DateTime _dateValue;


        private static ConsoleColor SetColor()
        {
            if (_dateTemp == DateTime.Today)
            {
                return ConsoleColor.Gray;
            }
            if (_dateTemp == _dateValue)
            {
                return ConsoleColor.Blue;
            }
            if (_dateTemp.DayOfWeek == DayOfWeek.Saturday || _dateTemp.DayOfWeek == DayOfWeek.Sunday)
            {
                return ConsoleColor.Red;
            }
            return ConsoleColor.White;
        }

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter date...");
            var dateString = Console.ReadLine();


            if (DateTime.TryParse(dateString, out _dateValue))
            {
                var date = new DateTime();

                for (var i = 0; i < 7; ++i)
                {
                    Console.Write("{0:ddd}\t", date); 
                    date = date.AddDays(1);
                    if (i == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.White;

                var daysInMonth = System.DateTime.DaysInMonth(_dateValue.Year, _dateValue.Month);
                var workingDays = daysInMonth;

                for (var i = 1; i <= daysInMonth; ++i)
                {
                    _dateTemp = new DateTime(_dateValue.Year, _dateValue.Month, i);

                    if (_dateTemp.DayOfWeek == DayOfWeek.Saturday || _dateTemp.DayOfWeek == DayOfWeek.Sunday)
                    {
                        workingDays--;
                    }

                    Console.ForegroundColor = SetColor();
                    if (i == 1)
                    {
                        for (var day = new DateTime(); day.DayOfWeek != _dateTemp.DayOfWeek; day = day.AddDays(1))
                        {
                            Console.Write("\t");
                        }
                        Console.Write("1\t");
                    }
                    else
                    {
                        if (_dateTemp.DayOfWeek == DayOfWeek.Monday)
                        {
                            Console.WriteLine("");
                        }
                        Console.Write(i+"\t");
                    }
                }
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("In this month "+workingDays+ " working days");
            }
            else
            {
                Console.WriteLine("Incorrect Input");
            }
            Console.ReadKey();

        }
    }
}