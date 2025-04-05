using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAIDomain.Entity
{
    public class BooksEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }

    }
}
