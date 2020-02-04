using CampMa8.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

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
        // getting events current camper created

        public ActionResult MyEvents()
        {
            var userid = GetAppId();
            var camper = GetPlayerByAppId(userid);
            ViewBag.UserId = camper.CamperId;
            try
            {
                var eventsjoined = db.CampEvent.Include("Camper").Include("Event").Where(e => e.CamperId == camper.CamperId && e.Event.CamperId != camper.CamperId).Select(e => e.Event).ToList();
                var myevents = db.Event.Include("Camper").Include("ApplicationUser").Where(e => e.CamperId == camper.CamperId).ToList();
                var allevents = myevents.Concat(eventsjoined);
                return View(allevents.ToList());
            }
            catch (Exception)
            {
                return View();
            }

        }

        //Join Event
        public ActionResult JoinEvent(int id)
        {
            var eventjoin = GetEventById(id);
            return View(eventjoin);
        }
        [HttpPost]
        public ActionResult JoinEvent(int id, Event joinevent)
        {
            try
            {
                var userid = GetAppId();
                var camper = GetPlayerByAppId(userid);
                var eventnow = GetEventById(id);
                if (eventnow.IsFull == false && eventnow.CurrentCampers != eventnow.MaximumCampers)
                {
                    eventnow.CurrentCampers += 1;
                    if (eventnow.CurrentCampers == eventnow.MaximumCampers)
                    {
                        eventnow.IsFull = true;
                    }
                    CampEvent campEvent = new CampEvent();
                    campEvent.CamperId = camper.CamperId;
                    campEvent.EventId = eventnow.EventId;
                    db.CampEvent.Add(campEvent);
                    db.SaveChanges();
                    return RedirectToAction("MyEvents", "Events");
                }
                else
                {
                    MessageBox.Show("Sorry, Event is full");
                    return RedirectToAction("MyEvents", "Events");
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            Event eventcreate = new Event();

            return View(eventcreate);
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(Event eventOne)
        {
            try
            {
                // TODO: Add insert logic here
                var userid = GetAppId();
                var camper = GetPlayerByAppId(userid);
                eventOne.CamperId = camper.CamperId;
                string dateofevent = GetDateFormat(eventOne.DateOfEvent);
                string timeofevent = GetTimeFormat(eventOne.TimeOfEvent);
                eventOne.TimeOfEvent = timeofevent;
                eventOne.DateOfEvent = dateofevent;
                eventOne.CurrentCampers = 1;
                db.Event.Add(eventOne);
                db.SaveChanges();
                var eventid = db.Event.Include("Camper").Include("ApplicationUser").Where(e => e.EventName == eventOne.EventName).Select(e => e.EventId).FirstOrDefault();
                CampEvent camperEvent = new CampEvent();
                camperEvent.CamperId = camper.CamperId;
                camperEvent.EventId = eventid;
                db.CampEvent.Add(camperEvent);
                db.SaveChanges();

                return RedirectToAction("MyEvents", "Events");
            }
            catch
            {
                return RedirectToAction("Details", "Players");
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            var eventedit = GetEventById(id);
            return View(eventedit);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event eventedit)
        {
            try
            {
                // TODO: Add update logic here
                var EventName = db.CampEvent.Select(c => c.Event).ToList();
                ViewBag.EventName = new SelectList(EventName);
                var eventoriginal = db.Event.Include("Camper").FirstOrDefault(e => e.EventId == id);
                eventoriginal.EventName = eventedit.EventName;
                eventoriginal.CampgroundAddress = eventedit.CampgroundAddress;
                eventoriginal.City = eventedit.City;
                eventoriginal.IsFull = eventedit.IsFull;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            var eventdelete = GetEventById(id);
            return View(eventdelete);
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event eventdelete)
        {
            try
            {
                // TODO: Add delete logic here
                eventdelete = GetEventById(id);
                eventdelete.CamperId = 0;
                db.Event.Remove(eventdelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public void DeleteOldEvents()
        {
            string yesterday = DateTime.Today.AddDays(-1).ToString("mm/dd/yyyy");
            var oldevents = db.Event.Include("Camper").Where(e => e.DateOfEvent == yesterday).ToList();

            foreach (var item in oldevents)
            {
                item.CamperId = 0;
                db.Event.Remove(item);
            }
            db.SaveChanges();
        }

        public string GetAppId()
        {
            var userid = User.Identity.GetUserId();
            return userid;
        }

        public Camper GetPlayerByAppId(string userid)
        {
            var player = db.Camper.Include("ApplicationUser").Where(p => p.ApplicationId == userid).FirstOrDefault();
            return player;
        }
        public Event GetEventById(int id)
        {
            var eventnow = db.Event.Include("Camper").Include("ApplicationUser").FirstOrDefault(e => e.EventId == id);
            return eventnow;
        }
        public string GetDateFormat(string date)
        {
            DateTime dateformat = DateTime.ParseExact(date, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            string dateofevent = dateformat.ToString("mm/dd/yyyy");
            return dateofevent;
        }
        public string GetTimeFormat(string time)
        {
            DateTime timeformat = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
            string timeofevent = timeformat.ToString("h:mm tt");
            return timeofevent;
        }


       
        
    }
}

