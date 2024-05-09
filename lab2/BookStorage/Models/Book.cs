﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStorage.Interfaces;

namespace BookStorage.Models
{
    public class Book : IModel
    {
        public int Id { get; }
        public int AuthorId { get; }
        public int PublisherId { get;}

        public string Name { get;}
        
        public decimal Price
        {
            get;
            set;
        }
        public int Year { get; }
        public List<int> Inventory;
        

        public Book(int Id, string name, Author author, decimal price, int year, Publisher publisher, List<int> inventory)
        {
            this.Id = Id;
            this.AuthorId = author.Id;
            this.PublisherId = publisher.Id;

            this.Name = name;
            this.Price = price;
            this.Year = year;
            this.Inventory = inventory;
        }

        public Book(int Id, string name, int authorId, decimal price, int year, int publisherId, List<int> inventory)
        {
            this.Id = Id;
            this.AuthorId = authorId;
            this.PublisherId = publisherId;

            this.Name = name;
            this.Price = price;
            this.Year = year;
            this.Inventory = inventory;
        }

        

        public override string ToString()
        {
            return string.Format(" `{0}`, {1} : {2}$, Inventory numbers: {3}", Name, Year, Price,  string.Join(",", Inventory.Select(n => n.ToString()).ToArray()));
        }

    }
}
