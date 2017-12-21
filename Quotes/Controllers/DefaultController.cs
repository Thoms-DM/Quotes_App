using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quotes.DB;
using Quotes.Models;

namespace Quotes.Controllers
{
    public class DefaultController : Controller
    {
        Quote tempQuote = new Quote();

        //GET: Default
        public ViewResult Index()
        {
            return View(DB.DB.GetIndexViewModel());
        }

        [HttpGet]
        public ViewResult HighestRated()
        {
            return View(DB.DB.GetTop10());
        }

        [HttpGet]
        public ViewResult MostRecent()
        {
            return View(DB.DB.Get10MostRecent());
        }

        [HttpPost]
        public ActionResult CreateQuote(string txt_text, string txt_author)
        {
            //gem nyt qoute til db
            DB.DB.CreateNewQuote(txt_text, txt_author);

            return View("Index", DB.DB.GetIndexViewModel());
        }

        public ActionResult VoteUp(int quoteID, string currentView)
        {
            //hent quote med id op fra db
            tempQuote = DB.DB.GetQuoteById(quoteID);
            //votedown
            tempQuote.Points = ++tempQuote.Points;
            //save to db
            DB.DB.ChangeQuoteById(tempQuote);
        
            return View(currentView, DB.DB.GetIndexViewModel());
        }
        public ActionResult VoteDown(int quoteID, string currentView)
        {
            //hent quote med id op fra db
            tempQuote = DB.DB.GetQuoteById(quoteID);
            //votedown
            tempQuote.Points = --tempQuote.Points;
            //save to db
            DB.DB.ChangeQuoteById(tempQuote);

            return View(currentView, DB.DB.GetIndexViewModel());
        }

        
        
    }
}