/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:47 PM
 */
using System;
using BBCodes;

namespace ConsoleTests
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("BBCode: ");
                string b = Console.ReadLine();
                BBCodeParser p = new BBCodeParser(true);
                p.Parse(b);
                Console.WriteLine(p.ToHTML());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}