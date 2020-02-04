using CampMa8.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CampMa8.Controllers
{
    public class CampersController : Controller
    {
        ApplicationDbContext db;
        public CampersController()
        {
            db = new ApplicationDbContext();
        }
        //GET: Campers
        public ActionResult Index()
        {
            var userId = GetAppId();
            var camper = GetCamperByAppId(userId);
            return View(camper);
        }

        public ActionResult Search()
        {
            Campground campground = new Campground();
            return View(campground);
        }

        [HttpPost]
        public ActionResult Search(Campground campground)
        {
            return RedirectToAction("Index", "Campground", campground);
        }

        // GET: Campers/Details/5
        public ActionResult Details()
        {
            var userid = GetAppId();
            var camper = GetCamperByAppId(userid);
            return View(camper);
        }

        // GET: Camper/Create
        public ActionResult Create()
        {
            Camper camper = new Camper();
            //////ViewBag.SkillLevel = new SelectList(campexp);
            //////var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
            //////ViewBag.SportsInterest = new SelectList(SportsInterest);
            return View(camper);
        }

        // POST: Camper/Create
        [HttpPost]
        public ActionResult Create(Camper camper)
        {
            try
            {
                // TODO: Add insert logic here

                camper.ApplicationId = GetAppId();

                db.Camper.Add(camper);
                db.SaveChanges();

                return RedirectToAction("LogOut", "Account");
            }
            catch
            {
                return View();
            }
        }

        //GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            var camper = GetCamperByCamperId(id);
            //var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            //ViewBag.SkillLevel = new SelectList(skilllevel);
            //var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
            //ViewBag.SportsInterest = new SelectList(SportsInterest);
            return View(camper);
        }

        //POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Camper camper)
        {

            try
            {
                // TODO: Add update logic here
                //var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
                //ViewBag.SportsInterest = new SelectList(SportsInterest);
                //var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
                //ViewBag.SkillLevel = new SelectList(skilllevel);
                var camperedit = GetCamperByCamperId(id);
                camperedit.FirstName = camper.FirstName;
                camperedit.LastName = camper.LastName;
                camperedit.ApplicationUser.UserName = camper.ApplicationUser.UserName;
                camperedit.ApplicationUser.Email = camper.ApplicationUser.Email;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public async System.Threading.Tasks.Task<ActionResult> CampgroundLocationsAsync()
        //{
        //    var id = GetAppId();
        //    Camper camper = GetCamperByAppId(id);
        //    List<Campground> campgrounds = new List<Campground>();
        //    string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=park+in+" + camper.ZipCode + "&key=" + GooglePlacesKey.Key;
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    string jsonResult = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        PlacesJSONResult postManJSON = JsonConvert.DeserializeObject<PlacesJSONResult>(jsonResult);
        //        foreach (var result in postManJSON.results)
        //        {
        //            Campground campground = new Campground();
        //            campground.Latitude = result.geometry.location.lat;
        //            campground.Longitude = result.geometry.location.lng;
        //            campground.CampgroundName = result.name;
        //            campgrounds.Add(campground);
        //        }
        //        return View(campgrounds);
        //    }
        //    return View();
        //}
        //GET: Campers/Delete/5
        public ActionResult Delete(int id)
        {
            var camper = GetCamperByCamperId(id);

            return View(camper);
        }

        //POST: Campers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Camper camper)
        {
            try
            {
                // TODO: Add delete logic here
                camper = GetCamperByCamperId(id);
                var userdelete = db.Users.SingleOrDefault(c => c.Id == camper.ApplicationId);
                camper.ApplicationId = null;
                db.Camper.Remove(camper);
                db.Users.Remove(userdelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //rating the experience of the trip/campground after trip completion
        public ActionResult RateCampExperience()
        {
            var CampExperience = db.Experience.Select(e => e.Experience).ToList();
            ViewBag.CampExperience = new SelectList(CampExperience);
            var userid = GetAppId();
            var camper = GetCamperByAppId(userid);
            string yesterday = DateTime.Today.AddDays(-1).ToString("mm/dd/yyyy");
            var campcompleted = db.Event.Include("Camper").Where(e => e.DateOfEvent == yesterday).ToList();
            List<Camper> campers = new List<Camper>();

            foreach (var item in campcompleted)
            {
                var campersforevent = db.CampEvent.Include("Camper").Include("Event").Where(c => c.EventId == item.EventId && c.CamperId == camper.CamperId).Select(c => c.Camper).ToList();
                campers.Concat(campersforevent);
            }
            if (campers.Count == 0)
            {
                return RedirectToAction("MyEvents", "Events");
            }
            else
            {
                return View(campers);
            }

        }

       
        [HttpPost]
        public ActionResult RateCampExperience(List<Camper> campers)
        {
            try
            {
                foreach (var item in campers)
                {
                    var camper = db.Camper.Include("ApplicationUser").Where(c => c.CamperId == item.CamperId).FirstOrDefault();
                    if (camper.CampExperience == 0)
                    {
                        camper.CampExperience += item.CampExperience;
                        db.SaveChanges();
                    }
                    else
                    {
                        camper.CampExperience = Math.Round(((camper.CampExperience + item.CampExperience) / 2), 2);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("MyEvents", "Events");
            }
            catch (Exception)
            {

                return RedirectToAction("MyEvents", "Events");
            }


        }

        //public List<Campsite> CampsiteAPIStringCall()
        //{
        //    string state = "";
        //    string rv = "2001";
        //    string tent = "2003";
        //    string electric = "";
        //    string url = "";
        //    if (state != "")
        //    {
        //        url = "ContractCode=" + state;
        //    }
        //    if (rv != "" && state != "")
        //    {
        //        url += "&siteType=" + rv;
        //    }
        //    else
        //    {
        //        url = "siteType=" + rv;
        //    }
        //    //"http://api.amp.active.com/camping/campgrounds/?pstate=WI&siteType=10001&amenity=4004&hookup=3002&pets=3010&api_key=393hzezzah7m97qapvvfqy5h";
        //    List<Campground> campgrounds = new List<Campground>();
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("http://api.amp.active.com/camping/");
        //    var campgroundQuery = httpClient.GetAsync("campgrounds/?" + url);
        //    campgroundQuery.Wait();
        //    var campResult = campgroundQuery.Result;
        //    if (campResult.IsSuccessStatusCode)
        //    {
        //        var read = campResult.Content.ReadAsStringAsync();
        //        read.Wait();
        //        var campground = read.Result;
        //        JArray jArray = JArray.Parse(campground);
        //        foreach (var item in jArray)
        //        {
        //            var campAttributes = new Campground()
        //            {

        //                facilityID = (string)item["facilityID"],
        //                facilityName = (string)item["facilityName"],
        //                faciltyPhoto = (string)item["facilityPhoto"],
        //                siteType = (string)item["siteType"],
        //                sitesWithPetsAllowed = (string)item["sitesWithPetsAllowed"],
        //                sitesWithAmps = (string)item["sitesWithAmps"],
        //                sitesWithWaterfront = (string)item["sitesWithAmps"],
        //                Latitude = (float)item["latitude"],
        //                Longitude = (float)item["longitude"]
        //            };
        //            campgrounds.Add(campAttributes);
        //        }
        //    }
        //    //return campgrounds;
        //}

        public string GetAppId()
        {
            var userid = User.Identity.GetUserId();
            return userid;
        }

        public Camper GetCamperByAppId(string userid)
        {
            var camper = db.Camper.Include("ApplicationUser").Where(c => c.ApplicationId == userid).FirstOrDefault();
            return camper;
        }
        public Camper GetCamperByCamperId(int id)
        {
            var camper = db.Camper.Include("ApplicationUser").Where(c => c.CamperId == id).FirstOrDefault();
            return camper;
        }

    }
}
