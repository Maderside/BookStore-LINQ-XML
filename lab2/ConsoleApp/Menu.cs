using BookStorage.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Menu
    {
        private static XmlDataSource _source;
        public string Header { get; set; }
        public string ExitInfo { get; set; }

        public Menu(XmlDataSource source, string header, string exitInfo)
        {
            _source = source;
            Header = header;
            ExitInfo = exitInfo;
        }


        private void InvokeAction(int actionIndex)
        {

            actionIndex--;
            if (actionIndex < 0 || actionIndex > _source.Actions.Length)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            else
            {
                _source.Actions[actionIndex]?.Invoke();
            }
        }

        public void ExecuteQuery(int number)
        {
            try
            {
                InvokeAction(number);
            }
            catch (IndexOutOfRangeException ex)
            {
                PrintInfo();
                number = ConsoleHelper.ReadInt(ex.Message + ", enter a valid number: ");
                Console.Clear();
                ExecuteQuery(number);
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine(Header);
            foreach (var row in _source.QueryInfo)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine(ExitInfo);
        }

        public void RunQueryMenu()
        {
            PrintInfo();
            int input = ConsoleHelper.ReadInt("Select query to execute:");
            if (input == 0)
            {
                Environment.Exit(0);
            }
            Console.Clear();
            ExecuteQuery(input);

            input = ConsoleHelper.ReadInt("Enter 0 to exit, enter any other number to continue: ");
            if (input != 0)
            {
                Console.Clear();
                RunQueryMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void RunMainMenu(XMLHelper xMLHelper)
        {
            Console.WriteLine(Header);
            Console.WriteLine("1 - Створити власні xml-фали");
            Console.WriteLine("2 - Продовжити працювати з існуючими файлами і даними");
            Console.WriteLine(ExitInfo);
            var input = ConsoleHelper.ReadInt("Введіть значення:");
            switch (input)
            {
                case 1:
                    createXmlDocs(xMLHelper);
                    Console.ReadKey();
                    break;
                case 2:
                    RunQueryMenu();
                    break;
            }
        }

        public void createXmlDocs(XMLHelper xmlHelper)
        {
            Console.WriteLine("Створення xml-документу для книг");


          
            Console.WriteLine("Введіть кількість елементів");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Book[] books = new Book[amount];

            for (int i = 0; i < amount; i++)
            {
                Console.Write("Введіть назву  " + (i + 1) + " книги: ");
                var name = Console.ReadLine();

                Console.Write("Введіть ціну  " + (i + 1) + " книги: ");
                var price = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Рік публікації " + (i + 1) + " книги: ");
                var year = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введіть ID автора книги " + (i + 1) + " книги: ");
                var authorId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введіть ID видавницта " + (i + 1) + " книги: ");
                var publisherId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter amount of inventory numbers: ");
                var amountOfInventoryNumbers = Convert.ToInt32(Console.ReadLine());

                var inventoryNumbers = new List<int>();
                for (int j = 0; j < amountOfInventoryNumbers; j++)
                {
                    Console.Write("Ведіть " + (j + 1) + " інвентарний номер: ");
                    var number = Convert.ToInt32(Console.ReadLine());
                    inventoryNumbers.Add(number);
                }
                books[i] = new Book(i, name, authorId, price, year, publisherId, inventoryNumbers);
                Console.WriteLine("\n\n\n");
            }

            string path = @"C:\Users\maxga\Робочий стіл\.Net\lab2v2\ConsoleApp\XmlDocuments\booksCustom.xml";
            xmlHelper.CreateBooksXml(books, path);

            Console.WriteLine("Вмість створеного файлу:");
            xmlHelper.PrintXmlDoc(path);
        }



    }


}


