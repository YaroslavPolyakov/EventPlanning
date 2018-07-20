using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using  EventPlanning.Models;

namespace EventPlanning.Controllers
{
    public class HomeController : Controller
    {
        /*EventContext db = new EventContext();
        public ActionResult Index()
        {
            IEnumerable<Event> events = db.Events;
            ViewBag.Events = events;
            return View();
        }
        */
        public ActionResult About()
        {
            //bool result = User.IsInRole("Admin");
            ViewBag.Message = "Your application description page.";
            //ViewBag.Message = result;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpGet]
        //public ActionResult Event(int id)
        //{
        //    ViewBag.EventId = id;
        //    return View();
        //}
        /*
        [HttpPost]
        public ActionResult Event(Visitor visitor)
        {
            db.Visitors.Add(visitor);
            db.SaveChanges();
            ViewBag.Message = "Waiting for you, " + visitor.FirstName + " on meetup :)"; 
            //return "Waiting for you," + visitor.FirstName + " on meetup :)";
            return new View();
        }*/
    }
}