using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Film
    {
        public Film(string title, string director, string writer, string genre, int year)
        {
            Title = title;
            Director = director;
            Writer = writer;
            Genre = genre;
            Year = year;
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
