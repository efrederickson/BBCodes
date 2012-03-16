/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:47 PM
 */
using System;
using BBCodes;
using BBCodes.Visitors;

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
                Console.WriteLine("HTML");
                Console.WriteLine(p.ToHTML());
                Console.WriteLine("BBCode");
                Console.WriteLine(new CodeGenerator().Generate(p.Output));
                Console.WriteLine("XML");
                Console.WriteLine(new XMLGenerator().Generate(p.Output));
                Console.WriteLine("XML Tree");
                Console.WriteLine(new XmlTreeGenerator().Generate(p.Output));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}