using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStorage.Interfaces;

namespace BookStorage.Models
{
    public class Author: IModel
    {
        public int Id { get; private set; }
        public string Name { get;  set; }
        public string Surname { get;  set; }

        public Author()
        {

        }
       

        public Author(int id, string name, string surname)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
        }

        public override string ToString()
        {
            return string.Format(" {0} {1}", Name, Surname);
        }
    }
}
