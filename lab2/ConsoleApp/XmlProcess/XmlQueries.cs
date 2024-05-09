using System;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp
{
    public class XmlQueries
    {
        //1. Знайти книгу, що видана у 1995 році
        public static void FindByYear(XDocument books)
        {
            var result = from book in books.Root.Elements()
                         where book.Element("year").Value == "1995"
                         select book;

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));
            ConsoleHelper.PrintModels(booksModels);
        }

        //2. Знайти всі книги, що написані автором Joanne Rowling
        public static void FindByAuthor(XDocument books, XDocument authors)
        {
            var result = from b in books.Root.Elements()
                         join a in authors.Root.Elements() on (int)b.Element("authorId") equals (int)a.Element("id")
                         where a.Element("surname").Value == "Rowling"
                         select b;

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));
            ConsoleHelper.PrintModels(booksModels);

        }

        //3. Відстортувати книги за роком видання
        public static void SortByYear(XDocument books)
        {
            var result = from book in books.Root.Elements()
                         orderby (int)book.Element("year")
                         select book;

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));
            ConsoleHelper.PrintModels(booksModels);
        }

        //4. Вивести загальну ціну за всі книги окремого автора
        public static void FindSummaryPrice(XDocument books, XDocument authors)
        {
            var result = from b in books.Root.Elements()
                         join a in authors.Root.Elements() on (int)b.Element("authorId") equals (int)a.Element("id")
                         group b by new { aId = (int)b.Element("authorId"), aName = (string)a.Element("surname") } into g
                         select new { AuthorSurname = g.Key.aName, Books = g, SummaryPrice = g.Elements("price").Sum(x => decimal.Parse(x.Value) * x.Parent.Elements("numbers").Elements().Count()) };

            foreach (var group in result)
            {
                Console.WriteLine("Author: " + group.AuthorSurname);
                Console.WriteLine("Summary price: " + group.SummaryPrice);


                Console.WriteLine("Books:");
                foreach (var book in group.Books)
                {
                    XMLHelper.PrintXElement(book);
                }
            }

        }

        //5. Знайти книги, що мають інвентарні номери 1002 і 3113
        public static void FindByInventoryNumber(XDocument books)
        {
            var result = from book in books.Root.Elements()
                         where book.Element("numbers").Elements().Select(x => Int32.Parse(x.Value)).ToList().Contains(1002) || book.Element("numbers").Elements().Select(x => Int32.Parse(x.Value)).ToList().Contains(3112)
                         select book;

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));

            ConsoleHelper.PrintModels(booksModels);
        }

        //6. Знайти книги, що мають більше одного інвентарного номера
        public static void FindWhereInventoryNumbersMoreThan(XDocument books)
        {
            var result = from book in books.Root.Elements()
                         where book.Element("numbers").Elements().Count() > 1
                         select book;

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));

            ConsoleHelper.PrintModels(booksModels);
        }

        //7. Вивести авторів, авторства яких книг налічується в бібліотеці більше одної
        public static void FindAuthorsWhoHasMoreThanBooks(XDocument books, XDocument authors)
        {
            var result = from a in authors.Root.Elements()
                         join b in books.Root.Elements() on (int)a.Element("id") equals (int)b.Element("authorId")
                         group b by new { aId = (int)b.Element("authorId"), aName = (string)a.Element("surname") } into g
                         where g.Select(x => x.Elements("book")).Count() > 1
                         select new { AuthorName = g.Key.aName, NumberOfBooks = g.Select(x => x.Elements("book")).Count() };
            foreach (var group in result)
            {
                Console.WriteLine(group.AuthorName + ", кількість книг:" + group.NumberOfBooks);
            }
        }

        //8. Вивести всі книги, згрупувавши за їх видавництвом
        public static void GroupByPublishers(XDocument books, XDocument publishers)
        {
            var result = from b in books.Root.Elements()
                         join p in publishers.Root.Elements()
                           on (int)b.Element("publisherId") equals (int)p.Element("id")
                         group b by new { pId = (int)b.Element("publisherId"), pName = (string)p.Element("name") } into g
                         select new { PublisherName = g.Key.pName, Books = g };

            foreach (var group in result)
            {
                Console.WriteLine(group.PublisherName);
                foreach (var book in group.Books)
                {
                    Console.WriteLine(XmlToModelConverter.ConvertToBook(book).ToString());
                }
            }
        }

        //9. Вивести авторів, чиї книги були видані видавництвами, що базуються у Іспанії
        public static void FindByPublishersCountry(XDocument books, XDocument authors, XDocument publishers)
        {
            var result = (from a in authors.Root.Elements()
                          join b in books.Root.Elements()
                            on (int)a.Element("id") equals (int)b.Element("authorId")
                          join p in publishers.Root.Elements()
                            on (int)b.Element("publisherId") equals (int)p.Element("id")
                          where p.Element("country").Value == "Spain"
                          select a).Distinct();

            var authorsModels = result.Select(x => XmlToModelConverter.ConvertToAuthor(x));

            ConsoleHelper.PrintModels(authorsModels);
        }

        //10. Знайти видавництва, назва яких починається з букви S, відсортувати за алфавітом по назві країни
        public static void FindPublishersWithLetterInName(XDocument publishers)
        {
            var result = publishers.Root.Elements()
                .Where(a => a.Element("name").Value.StartsWith("S"))
                .OrderBy(a => a.Element("name").Value)
                .ThenBy(a => a.Element("country").Value)
                ;

            var publisherModels = result.Select(x => XmlToModelConverter.ConvertToPublisher(x));
            ConsoleHelper.PrintModels(publisherModels);
        }

        //11. Згрупувати книги за країною видавництва
        public static void GrupByCountry(XDocument books, XDocument publishers)
        {
            var result = books.Root.Elements()
                .Join(publishers.Root.Elements(),
                book => book.Element("publisherId").Value,
                publisher => publisher.Element("id").Value,
                (book, publisher) => new { book, publisher })
                .GroupBy(x => new { Country = x.publisher.Element("country").Value });

            foreach (var group in result)
            {
                Console.WriteLine(group.Key.Country);
                foreach (var book in group)
                {
                    Console.WriteLine(XmlToModelConverter.ConvertToBook(book.book).ToString());
                }
            }
        }

        //12. Вивести всі інвентарні номери, що призначені книгам авторів Rowling і Shakespare, відсортувавши їх за спаданням
        public static void FindInventoryByAuthors(XDocument books, XDocument authors)
        {
            var result = books.Root.Elements()
                .Join(authors.Root.Elements(),
                book => book.Element("authorId").Value,
                author => author.Element("id").Value,
                (book, author) => new { book, author })
                .Where(
                x => x.author.Element("surname").Value == "Rowling" || x.author.Element("surname").Value == "Shakespeare"
                )
                .Select(
                x => x.book.Element("numbers").Elements().Select(n => Int32.Parse(n.Value)).ToList()
                )
                .SelectMany(x => x.OrderByDescending(n => n))
                .OrderByDescending(x => x)
                ;

            Console.WriteLine(string.Join(",", result.Select(n => n.ToString()).ToArray()));
        }

        //13. Вивести видавництва, сумарна ціна за книги яких є найбільшою або найменшою
        public static void FindMaxAndMinPrice(XDocument books, XDocument publishers)
        {
            var summirsedPrice = books.Root.Elements()
                .Join(publishers.Root.Elements(),
                book => book.Element("publisherId").Value,
                publisher => publisher.Element("id").Value,
                (book, publisher) => new { book, publisher })
                .GroupBy(x => new { pId = x.book.Element("publisherId").Value, x.publisher })
                .Select(x => new
                {
                    Publisher = x.Key.publisher,
                    TotalSum = x.Sum(n => decimal.Parse(n.book.Element("price").Value) * n.book.Elements("numbers").Elements().Count())
                })
            ;
            var maxSum = summirsedPrice.Max(x => x.TotalSum);
            var minSum = summirsedPrice.Min(x => x.TotalSum);

            var result = summirsedPrice.Where(x => x.TotalSum == maxSum || x.TotalSum == minSum);

            foreach (var group in result)
            {
                Console.WriteLine(XmlToModelConverter.ConvertToPublisher(group.Publisher).ToString() + ". Total sum: " + group.TotalSum);
            }
        }

        //14. Вивести назви і кількість книг, ціна яких знаходиться в діапазоні від 12$ до 16$
        public static void FindBooksAndQuantity(XDocument books)
        {
            var result = books.Root.Elements()
                .Where(x => decimal.Parse(x.Element("price").Value) > 12.0M && decimal.Parse(x.Element("price").Value) < 16.0M);

            var numberOfBooks = result.Count();

            var booksModels = result.Select(x => XmlToModelConverter.ConvertToBook(x));
            ConsoleHelper.PrintModels(booksModels);
            Console.WriteLine("Кількість книг, що відповідає ціновій категорії: " + numberOfBooks.ToString());
        }

        //15. Оприділити, чи наявна в бібліотеці книга автора Jack Shakespeare
        public static void CheckIfExists(XDocument books, XDocument authors)
        {
            var result = books.Root.Elements()
                .Join(authors.Root.Elements(),
                book => book.Element("authorId").Value,
                author => author.Element("id").Value,
                (book, author) => new { book, author })
                .Any(x => x.author.Element("name").Value == "Jack" && x.author.Element("surname").Value == "Shakespeare");

            Console.WriteLine("{0}", result == false ? "Такої книги не наявно" : "Така книга наявна");

        }
    }
}
