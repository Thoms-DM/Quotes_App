using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class Quote
    {
        public int ID;

        [Required(ErrorMessage = "Enter quote")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Enter author")]
        public string Author { get; set; }
        public int Points { get; set; }

    }
}