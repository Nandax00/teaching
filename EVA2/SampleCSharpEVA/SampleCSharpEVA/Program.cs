using System;
using System.Collections.Generic;

/* Ctrl + Shift + B => Build
 * Ctrl + S => Save
 * Ctrl + R + R => Rename
 * Shift + F12 on selected field => shows references of field
 * F12 on selected field => shows definition of field
 * F5 => Start debugging
 * Ctrl + F5 => Start program without debugging
 * File => New => Project... => Visual C# => Windows Classic Desktop => Windows Forms App / WPF App
 * */

namespace SampleCSharpEVA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");

            // Use var if type is obvious
            var a = 0;
            Int32 b = 0;

            try
            {
                Console.Write("First number: ");
                a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Second number: ");
                b = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Sum: " + AddNumbers(a, b) + "\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number is too long.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not a number.");
            }

            // Basic container and iteration
            List<int> randomNumbers = FillList();
            PrintList(randomNumbers);

            // Passing value by reference
            int value = 4;
            Square(ref value);
            Console.WriteLine("Square of value is: " + value);

            Console.ReadLine();
        }

        // Static functions can only invoke static functions
        static int AddNumbers(int x, int y)
        {
            return (x + y);
        }

        static List<int> FillList()
        {
            Random random = new Random();

            List<int> numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(random.Next(1,50));
            }

            return numbers;
        }

        static void PrintList(List<int> toPrint)
        {
            // foreach is read-only
            foreach (var item in toPrint)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static void Square(ref int val)
        {
            val *= val;
        }
    }
}


