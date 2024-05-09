using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp
{
    public class XMLHelper
    {
        
        private XmlWriterSettings _settings;

        public XMLHelper(XmlWriterSettings settings)
        {
            _settings = settings;
        }


        public void CreateBooksXml(IEnumerable<Book> books, string path)
        {
            
            using (XmlWriter writer = XmlWriter.Create(path, _settings))
            {
                writer.WriteStartElement("books");
                foreach (Book book in books)
                {
                    writer.WriteStartElement("book");
                        writer.WriteElementString("id", book.Id.ToString());
                        writer.WriteElementString("authorId", book.AuthorId.ToString());
                        writer.WriteElementString("publisherId", book.PublisherId.ToString());

                        writer.WriteElementString("name", book.Name);
                        writer.WriteElementString("price", book.Price.ToString());
                        writer.WriteElementString("year", book.Year.ToString());

                        writer.WriteStartElement("numbers");
                            foreach (var num in book.Inventory)
                            {
                                writer.WriteElementString("number", num.ToString());
                            }
                        writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public void CreateAuthorsXml(IEnumerable<Author> authors, string path)
        {
            
            using (XmlWriter writer = XmlWriter.Create(path, _settings))
            {
                writer.WriteStartElement("authors");
                    foreach (Author author in authors)
                    {
                        writer.WriteStartElement("author");
                            writer.WriteElementString("id", author.Id.ToString());
                            writer.WriteElementString("name", author.Name.ToString());
                            writer.WriteElementString("surname", author.Surname.ToString());
                        writer.WriteEndElement();
                    }
                writer.WriteEndElement();
            }
        }

        public void CreatePublishersXml(IEnumerable<Publisher> publishers, string path)
        {
            using (XmlWriter writer = XmlWriter.Create(path, _settings))
            {
                writer.WriteStartElement("publishers");
                foreach (Publisher publisher in publishers)
                {
                    writer.WriteStartElement("publisher");
                        writer.WriteElementString("id", publisher.Id.ToString());
                        writer.WriteElementString("name", publisher.Name);
                        writer.WriteElementString("country", publisher.Country);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }


        public void PrintXmlDoc(string path)
        {
            XDocument xmlDoc = XDocument.Load(path);
            Console.WriteLine("Document: "+xmlDoc.Root.Name);
            Console.WriteLine("=======");
            foreach (XElement elements in xmlDoc.Root.Elements())
            {
                foreach (var subElement in elements.Elements())
                {
                    if (subElement.HasElements)
                    {
                        Console.Write(subElement.Name+": ");
                        foreach (var subSubElement in subElement.Elements())
                        {
                            Console.Write(subSubElement.Value + ", ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("{0} : {1}", subElement.Name, subElement.Value);
                    }
                }
                Console.WriteLine();
            }
        }
        public static void PrintXElement(XElement element)
        {
            
                foreach (var subElement in element.Elements())
                {
                    if (subElement.HasElements)
                    {
                        Console.Write(subElement.Name + ": ");
                        foreach (var subSubElement in subElement.Elements())
                        {
                            Console.Write(subSubElement.Value + ", ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("{0} : {1}", subElement.Name, subElement.Value);
                    }
                }
                Console.WriteLine();
            
        }

    }
}
