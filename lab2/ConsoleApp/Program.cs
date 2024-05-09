using BookStorage.Models;
using System;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine();

            DataSource data = new DataSource();


            var fileName1 = "books.xml";
            var fileName2 = "authors.xml";
            var fileName3 = "publishers.xml";

            var booksPath = @"C:\Users\maxga\Робочий стіл\.Net\lab2v2\ConsoleApp\XmlDocuments\" + fileName1;
            var authorsPath = @"C:\Users\maxga\Робочий стіл\.Net\lab2v2\ConsoleApp\XmlDocuments\" + fileName2;
            var publishersPath = @"C:\Users\maxga\Робочий стіл\.Net\lab2v2\ConsoleApp\XmlDocuments\" + fileName3;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            var xmlHelper = new XMLHelper(settings);
            XDocument booksXml = XDocument.Load(booksPath);
            XDocument authorsXml = XDocument.Load(authorsPath);
            XDocument publishersXml = XDocument.Load(publishersPath);

            XmlDataSource dataXml = new XmlDataSource(booksXml, authorsXml, publishersXml);
            Menu menu = new Menu(dataXml, "Лабораторна робота №2, студент Галактіонов Максим. Група ІС-02.", "Введіть 0, щоб закінчити виконання програми");

            // User input 
            // User input validation based on FluentValidator
            // Class instance creation 

            // Handling of models (DTO) is BL issue 
            // rich model dynamic functions of type should use and invoke type data 

            //Author authorInstance = new Author() { Name = "", Surname = "" };
            //authorInstance.Surname = "";
            decimal value = (decimal)20.5;
            menu.RunMainMenu(xmlHelper);
            

            Console.WriteLine();

            Console.ReadKey();
        }
    }

}

