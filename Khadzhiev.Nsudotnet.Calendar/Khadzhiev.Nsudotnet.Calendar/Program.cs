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
                Console.Write("Mon\tTue\tWed\tThu\tFri\t");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Sat\tSun\n");
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
                        switch (_dateTemp.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                Console.Write("  1\t");
                                break;
                            case DayOfWeek.Tuesday:
                                Console.Write("   \t  1\t");
                                break;
                            case DayOfWeek.Wednesday:
                                Console.Write("   \t   \t  1\t");
                                break;
                            case DayOfWeek.Thursday:
                                Console.Write("   \t   \t   \t  1\t");
                                break;
                            case DayOfWeek.Friday:
                                Console.Write("   \t   \t   \t   \t  1\t");
                                break;
                            case DayOfWeek.Saturday:
                                Console.Write("   \t   \t   \t   \t   \t  1\t");
                                break;
                            case DayOfWeek.Sunday:
                                Console.Write("   \t   \t   \t   \t   \t   \t  1\t");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        if (_dateTemp.DayOfWeek == DayOfWeek.Monday)
                        {
                            Console.WriteLine("");
                        }
                        if (_dateTemp.Day / 10 == 0)
                        {
                            Console.Write("  "+i+"\t");
                        }
                        else
                        {
                            Console.Write(" "+i+"\t");
                        }
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