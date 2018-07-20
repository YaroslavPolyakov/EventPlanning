using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EventPlanning.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EventPlanning.Controllers
{
    public class EventController : Controller
    {
        EventContext db = new EventContext();

        // GET: Event
        public ActionResult Visitor()
        {
            IEnumerable<Visitor> visitors = db.Visitors;
            ViewBag.Events = visitors;
            return View(db.Visitors.ToList());
        }

        public ActionResult Index()
        {
            IEnumerable<Event> events = db.Events;
            ViewBag.Events = events;
            return View(db.Events.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {

            var events = db.Events.FirstOrDefault(x => x.Id == id);
            var visitors = db.Visitors.Where(x => x.EventId == id && x.ConfirmedEmail == true).ToList();
            var eventVisitors = new EventVisitor() { Events = events, Visitors = visitors };
            return View(eventVisitors);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameEvent,Address,Description,DateEvent")]
            Event eventCreate)
        {
            if (ModelState.IsValid && eventCreate.DateEvent>=DateTime.Now)
            {
                db.Events.Add(eventCreate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventCreate);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event eventEdit = db.Events.Find(id);
            if (eventEdit == null)
            {
                return HttpNotFound();
            }

            return View(eventEdit);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameEvent,Address,Description")]
            Event eventEdit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventEdit);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event eventDelete = db.Events.Find(id);
            if (eventDelete == null)
            {
                return HttpNotFound();
            }

            return View(eventDelete);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event eventDelete = db.Events.FirstOrDefault(x => x.Id == id);

            db.Events.Remove(eventDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Confirm(string Email)
        {
            ViewBag.Message = "To the postal address " + Email + " you received further instructions on completing the registration";
            return View();
        }

        public ViewResult Error(string error)
        {
            ViewBag.Message = error;
            return View("Error");
        }
        public ActionResult ViewConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrationOnEvent(int id)
        {
            ViewBag.EventId = id;
            return View();
        }

        public bool SendMessage(string mail, string token, int? eventid)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("testtask404@gmail.com", "TestTask404!");

            MailMessage message = new MailMessage("testtask404@gmail.com", mail);
            message.Subject = "Verify your email";
            message.Body = string.Format("Thanks for Registering.<br>Please verify your email by clicking the link <br/> http://localhost:53697" + "{0}", Url.Action("ConfirmMail", "Event", new { Email = mail, Token = token, EventId = eventid }));
            message.IsBodyHtml = true;
            smtp.Send(message);

            return true;
        }

        [HttpPost]
        public ActionResult RegistrationOnEvent(Visitor visitor, int? id)
        {
            if (ModelState.IsValid)
            {
                //var result = db.Visitors.FirstOrDefault(x => x.EventId == id && x.ConfirmedEmail == false && x.Email != visitor.Email);
                var result = db.Visitors.FirstOrDefault(x=> x.Email == visitor.Email && x.EventId==id && x.ConfirmedEmail == true);
                if (result == null)
                {
                    visitor.EventId = id;
                    visitor.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    bool Confirm = SendMessage(visitor.Email, visitor.Token, visitor.EventId);

                    if (Confirm == true)
                    {
                        db.Visitors.Add(visitor);
                        db.SaveChanges();
                        return RedirectToAction("Confirm", "Event",
                            new {Email = visitor.Email, Token = visitor.Token, EventId = visitor.EventId});
                    }
                    else return RedirectToAction("Error", "Event", new {Error = "Error in sending message, please try again later" });
                }
                else return RedirectToAction("Error", "Event", new { Error = "Member with this email already registered" });
            }
            else return View();
        }
        public ActionResult ConfirmMail([Bind(Prefix = "Email")]string email, [Bind(Prefix = "Token")]string token, [Bind(Prefix = "EventId")] int? eventid)
        {
            if (!string.IsNullOrEmpty(email) && eventid != null && !string.IsNullOrEmpty(token))
            {
                var visitorRequest =
                    db.Visitors.FirstOrDefault(x => x.EventId == eventid && x.Email == email && x.Token == token && x.ConfirmedEmail != true);

                if (visitorRequest != null)
                {
                    visitorRequest.ConfirmedEmail = true;
                    db.Entry(visitorRequest).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("ViewConfirmEmail", "Event");
                }
                else return RedirectToAction("Error", "Event");

            }
            else return RedirectToAction("Error", "Event");
        }

        [HttpPost]
        public ActionResult Contact()
        {
            if (ModelState.IsValid)
            {
                Event poll = new Event();
                //poll.NameEvent = poll_name;
                db.Events.Add(poll);
                db.SaveChanges();
                decimal id = poll.Id;
                //que_name = new List<string>();
                //for (int i = 0; i < que_name.Count; i++)
                {
                    //eVE pq = new Polls_Questions();
                    //pq.que_poll_id = id;
                    //pq.que_name = Convert.ToString(que_name);
                    //db.Polls_Questions.Add(pq);
                    //db.SaveChanges();
                }
            }
            return View();
        }
    }
}
