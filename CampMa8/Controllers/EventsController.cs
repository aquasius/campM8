using CampMa8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampMa8.Controllers
{
    public class EventsController : Controller
    {
        ApplicationDbContext db;
        public EventsController()
        {
            
            db = new ApplicationDbContext();
        }
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
    }
}