using CampMa8.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace CampMa8.Controllers
{
    public class CampgroundController : Controller
    {
        // GET: Campground
        public ActionResult Index(Campground campground)
        {
            var camps = CampgroundAPIStringCall(campground);
            return View(camps);
        }

        public List<Campground> CampgroundAPIStringCall(Campground campground)
        {
            string state = campground.contractID;
            string siteType = campground.siteType;
            string amenity = campground.amenities;
            string hookup = campground.hookups;
            string url = "pstate=" + state;
            string apiKey = "&api_key=393hzezzah7m97qapvvfqy5h";
            if (siteType != null)
            {
                url += "&siteType=" + siteType;
            }
            if (amenity != null)
            {
                url += "&amenity=" + amenity;
            }
            if (hookup != null)
            {
                url += "&hookup=" + hookup;
            }
            List<Campground> campgrounds = new List<Campground>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://api.amp.active.com/camping/");
            var campgroundQuery = httpClient.GetAsync("campgrounds/?" + url + apiKey);
            campgroundQuery.Wait();
            var campResult = campgroundQuery.Result;
            if (campResult.IsSuccessStatusCode)
            {
                var read = campResult.Content.ReadAsStringAsync();
                read.Wait();
                var camp = read.Result;
                Type type = typeof(Campground);
                XmlDocument document = new XmlDocument();
                var strReader = new StringReader(camp);
                var serializer = new XmlSerializer((type), new XmlRootAttribute("result"));
                var xmlReader = new XmlTextReader(strReader);
                var obj = serializer.Deserialize(xmlReader);
                //foreach (var item in obj)
                //{
                //    var campAttributes = new Campground()
                //    {
                //        contractID = (string)item["contractID"],
                //        contractType = (string)item["contractType"],
                //        facilityID = (string)item["facilityID"],
                //        facilityName = (string)item["facilityName"],
                //        faciltyPhoto = (string)item["facilityPhoto"],
                //        siteType = (string)item["siteType"],
                //        sitesWithPetsAllowed = (string)item["sitesWithPetsAllowed"],
                //        hookups = (string)item["sitesWithAmps"],
                //        sitesWithWaterfront = (string)item["sitesWithAmps"],
                //        Latitude = (float)item["latitude"],
                //        Longitude = (float)item["longitude"]
                //    };
                //    campgrounds.Add(campAttributes);
                //}
            }
            return campgrounds;
        }
    }
}