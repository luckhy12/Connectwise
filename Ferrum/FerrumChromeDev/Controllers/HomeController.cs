using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Firebase.Database.Query;
using System.Threading.Tasks;
using ConnectWiseSDK;
using FerrumChromeDev.Models;

namespace FerrumChromeDev.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> CaptureCall(string PhoneNo, string ExtNo)
        {
            FerrumItModel model = new FerrumItModel();
            model.PhoneNo = PhoneNo;
            model.PhoneExt = ExtNo;


            var firebase = new FirebaseClient("https://ferrumit-91817.firebaseio.com/");
            var dino = await firebase
  .Child("")
  .PostAsync(model);

            string text3 = "<?xml version='1.0' encoding='UTF-8' ?><response><result><call_url/></result></response>";
            return base.Content(text3, "text/xml");

            //return View();
        }
        private static ContactApi _contactApi;
        private static CompanyApi _companyApi;
        public ActionResult Index(string callerID)
        {

            if (callerID.StartsWith(" 1"))
            {
                callerID = callerID.Remove(0, 2);
            }
            else if (callerID.StartsWith("1"))
            {
                callerID = callerID.Remove(0, 1);
            }
            else if (callerID.StartsWith("%2B1"))
            {
                callerID = callerID.Remove(0, 3);
            }
            else if (callerID.StartsWith("+1"))
            {
                callerID = callerID.Remove(0, 2);
            }
            else if (callerID.StartsWith(" "))
            {
                callerID = callerID.Remove(0, 0);
            }
            string arg_80_0 = string.Empty;
            string str = string.Empty;
            _contactApi = new ContactApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            new List<ContactModel>();
            ContactModel obj = new ContactModel();
            obj.Phone =callerID;
            string conditions = "Phone = '" + callerID + "'";
            List<ContactFindResult> list3 = _contactApi.FindContacts(conditions, "", new int?(1000), new int?(0), "", new List<string>
	{
		"Id",
		"FirstName",
		"LastName",
		"Type",
		"CompanyId"
	});
            if (list3.Count > 0)
            {
                ViewBag.Contact = 1;

                foreach (ContactFindResult current2 in list3)
                {
                    obj.CompanyID = current2.CompanyId;
                    obj.FirstName = current2.FirstName;
                    obj.LastName = current2.LastName;
                    obj.Type = current2.Type;
                }
            }
      
            return View(obj);
        }

        public ActionResult AddContact( string CallerID)
        {


            ContactModel obj = new ContactModel();
            obj.Phone = CallerID;

            _companyApi = new CompanyApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");

            List<CompanyFindResult> list2 = _companyApi.FindCompanies("", "CompanyName asc", new int?(1000), new int?(0), new List<string>
            {
                "Id",
                "CompanyName",
                "CompanyIdentifier"
            });
            ViewBag.CompaniesList = new SelectList(list2.AsEnumerable(), "CompanyIdentifier", "CompanyName");


            return View(obj);
        }
     
public ActionResult SaveContact(ContactModel obj)
        {
            _contactApi = new ContactApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            Contact _contact = new Contact()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                NickName = obj.NickName,
                Title = obj.Title,
                Phone = obj.Phone,
                Fax = obj.Fax,
                Email = obj.Email,
                Id=0
            };
       var result= _contactApi.AddOrUpdateContact(_contact);

            
            return RedirectToAction("Index", new {callerID =  obj.Phone});
        }
       
    }

    public class FerrumItModel
    {
        public string PhoneNo { get; set; }
        public string PhoneExt { get; set; }
    }
  
}