using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStorage.Models;

namespace ConsoleApp
{
    class XmlToModelConverter
    {
        public static Book ConvertToBook(XElement xmlBook)
        {
            var id = (int)xmlBook.Element("id");
            var authorId = (int)xmlBook.Element("authorId");
            var publisherId = (int)xmlBook.Element("publisherId");

            var name = xmlBook.Element("name").Value;
            var year = (int)xmlBook.Element("year");
            var price = decimal.Parse(xmlBook.Element("price").Value);

            var inventoryNumbers = xmlBook.Element("numbers").Descendants().Select(x=>Int32.Parse(x.Value)).ToList();


            return new Book(id, name, authorId, price, year, publisherId, inventoryNumbers);

        }

        public static Author ConvertToAuthor(XElement xmlAuthor)
        {
            var id = (int)xmlAuthor.Element("id");

            var name = xmlAuthor.Element("name").Value;
            var surname = xmlAuthor.Element("surname").Value; ;
            
            return new Author(id, name, surname);
        }

        public static Publisher ConvertToPublisher(XElement xmlPublisher)
        {
            var id = (int)xmlPublisher.Element("id");

            var name = xmlPublisher.Element("name").Value;
            var country = xmlPublisher.Element("country").Value; 

            return new Publisher(id, name, country);
        }
    }
}
