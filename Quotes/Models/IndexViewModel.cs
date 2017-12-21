using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class IndexViewModel
    {
        public List<Quote> QuotesList { get; set; }

        public IndexViewModel(List<Quote> quotes)
        {
            QuotesList = quotes;
        }
    }
}