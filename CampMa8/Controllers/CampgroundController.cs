using CampMa8.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
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
            WebRequest request = WebRequest.Create("http://api.amp.active.com/camping/" + "campgrounds/?" + url + apiKey);
            WebResponse response = request.GetResponse();
            XDocument doc = XDocument.Load(response.GetResponseStream());
            var newDoc = new XDocument(new XElement("resultset",
                from p in doc.Element("resultset").Elements("result")
                select p));
            var nodes = newDoc.Element("resultset");
            XNode currentnode;
            XNode holder;
            do
            {
                currentnode = nodes.FirstNode;
                XElement node = nodes.Element("result");
                Campground apiCamp = new Campground()
                {
                    contractID = node.Attribute("contractID").Value,
                    contractType = node.Attribute("contractType").Value,
                    facilityID = node.Attribute("facilityID").Value,
                    facilityName = node.Attribute("facilityName").Value,
                    faciltyPhoto = node.Attribute("faciltyPhoto").Value,
                    sitesWithPetsAllowed = node.Attribute("sitesWithPetsAllowed").Value,
                    sitesWithWaterfront = node.Attribute("sitesWithWaterfront").Value,
                    hookups = node.Attribute("sitesWithAmps").Value
                };
                campgrounds.Add(apiCamp);
                holder = currentnode.NextNode;
                currentnode.Remove();
            } while (holder != null);
            return campgrounds;
        }
    }
}