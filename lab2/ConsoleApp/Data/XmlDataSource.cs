using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStorage.Models;

namespace ConsoleApp
{
    class XmlDataSource
    {
        //private static IEnumerable<Book> _books;
        //private static IEnumerable<Author> _authors;
        //private static IEnumerable<Publisher> _publishers;

        private static XDocument _booksXml;
        private static XDocument _authorsXml;
        private static XDocument _publishersXml;

        private Action[] _actions = new Action[]
        {
            () =>XmlQueries.FindByYear(_booksXml),
            () =>XmlQueries.FindByAuthor(_booksXml, _authorsXml),
            () =>XmlQueries.SortByYear(_booksXml),
            () =>XmlQueries.FindSummaryPrice(_booksXml, _authorsXml),
            () =>XmlQueries.FindByInventoryNumber(_booksXml),
            () =>XmlQueries.FindWhereInventoryNumbersMoreThan(_booksXml),
            () =>XmlQueries.FindAuthorsWhoHasMoreThanBooks(_booksXml, _authorsXml),
            () =>XmlQueries.GroupByPublishers(_booksXml, _publishersXml),
            () =>XmlQueries.FindByPublishersCountry(_booksXml, _authorsXml, _publishersXml),
            () =>XmlQueries.FindPublishersWithLetterInName( _publishersXml),
            () =>XmlQueries.GrupByCountry(_booksXml, _publishersXml),
            () =>XmlQueries.FindInventoryByAuthors(_booksXml, _authorsXml),
            () =>XmlQueries.FindMaxAndMinPrice(_booksXml, _publishersXml),
            () =>XmlQueries.FindBooksAndQuantity(_booksXml),
            () =>XmlQueries.CheckIfExists(_booksXml, _authorsXml)
        };

        private string[] _queryInfo = new string[]
        {
            "1. Знайти книгу, що видана у 1995 році",
            "2. Знайти всі книги, що написані автором Joanne Rowling",
            "3. Відстортувати книги за роком видання",
            "4. Вивести загальну ціну за всі книги окремого автора",
            "5. Знайти книги, що мають інвентарні номери 1002 і 3113",
            "6. Знайти книги, що мають більше одного інвентарного номера",
            "7. Вивести авторів, авторства яких книг налічується в бібліотеці більше одної",
            "8. Вивести всі книги, згрупувавши за їх видавництвом",
            "9. Вивести авторів, чиї книги були видані видавництвами, що базуються у Іспанії",
            "10. Знайти видавництва, назва яких починається з букви S, відсортувати за алфавітом по назві країни",
            "11. Згрупувати книги за країною видавництва",
            "12. Вивести всі інвентарні номери, що призначені книгам авторів Rowling і Shakespare, відсортувавши їх за спаданням",
            "13. Вивести видавництва, сумарна ціна за книги яких є найбільшою або найменшою",
            "14. Вивести назви і кількість книг, ціна яких знаходиться в діапазоні від 12$ до 16$",
            "15. Оприділити, чи наявна в бібліотеці книга автора Jack Shakespeare"
        };


        
        public Action[] Actions { get => _actions; }
        public string[] QueryInfo { get => _queryInfo; }


        
        
        public XmlDataSource(XDocument books, XDocument authors, XDocument publishers)
        {
            _booksXml = books;
            _authorsXml = authors;
            _publishersXml = publishers;
            
        }

        
    }
}
