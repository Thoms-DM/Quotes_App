using System;
using System.Web.Http;
using Quotes.Models;

namespace Quotes.Controllers
{
    public class QuoteController : ApiController
    {
        [HttpGet]
        //api/quote
        public IHttpActionResult GetAllQuotes(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "top")
                {
                    var temp = DB.DB.GetTop10();
                    if (temp.QuotesList.Count == 0)
                        return NotFound();
                    return Ok(temp);
                }
                if (type == "recent")
                {
                    var temp = DB.DB.Get10MostRecent();
                    if (temp.QuotesList.Count == 0)
                        return NotFound();
                    return Ok(temp);
                }
            }
        return NotFound();
        }

    public IHttpActionResult PostQuote(string Text, string Author)
    {
        if (!string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(Author))
        {
            //gem nyt qoute til db
            DB.DB.CreateNewQuote(Text, Author);
            return Ok();
        }
            return NotFound();
    }

        public IHttpActionResult PutVote(int ID, string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                try
                {
                    //hent quote med id op fra db
                    var tempQuote = DB.DB.GetQuoteById(ID);
                    if (type == "up")
                    {
                        //votedown
                        tempQuote.Points = ++tempQuote.Points;
                    }
                    else if (type == "down")
                    {
                        tempQuote.Points = --tempQuote.Points;
                    }
                    //save to db
                    DB.DB.ChangeQuoteById(tempQuote);
                }
                catch (Exception e)
                {
                    NotFound();
                    throw;
                }
            }
           

            return Ok();

        }
    }
}