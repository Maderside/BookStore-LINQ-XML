using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStorage.Interfaces;

namespace ConsoleApp
{
    public static class ConsoleHelper
    {
        public static int ReadInt(string message)
        {
            Console.WriteLine();
            Console.Write(message);

            var str = Console.ReadLine();

            try
            {
                return int.Parse(str);
            }
            catch (FormatException)
            {
                return ReadInt("Enter an integer value: ");
            }
        }

        public static void PrintModels(IEnumerable<IModel> models)
        {

            foreach (var model in models)
            {
                Console.WriteLine(model.ToString());
            }
        }

    }
}
