using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Firebase.Database.Query;
using System.Threading.Tasks;
using ConnectWiseSDK;
using FerrumChromeDev.Models;
namespace FerrumChromeDev.Controllers
{

    public class HomeController : Controller
    {
        private static ContactApi _contactApi;
        private static CompanyApi _companyApi;
        private static ActivityApi _activityApi;
        private static MemberApi _memberApi;

        //   amandataEntities db = new Models.amandataEntities();
        public async Task<ActionResult> CaptureCall(string PhoneNo, string ExtNo)
        {
            //if (Session["Call"] == null)
            //{
                Session["Call"] = PhoneNo;
                Session["CallTime"] = DateTime.Now;
                FerrumItModel model = new FerrumItModel();
                model.PhoneNo = PhoneNo;
                model.PhoneExt = ExtNo;

                var firebase = new FirebaseClient("https://ferrumit-91817.firebaseio.com/");
                var dino = await firebase
      .Child("")
      .PostAsync(model);
           // }
            
            string text3 = "<?xml version='1.0' encoding='UTF-8' ?><response><result><call_url/></result></response>";
            return base.Content(text3, "text/xml");
        }
    //    public ActionResult EndCall(string PhoneNo, string Ext)
    //    {
    //        if (Session["Call"] != null)
    //        {

    //            if (Session["Call"].ToString() == PhoneNo)
    //            {

    //                string Name = "";
    //                string conditions = "Phone = '" + PhoneNo + "'";
    //                List<ContactFindResult> list3 = _contactApi.FindContacts(conditions, "", new int?(1000), new int?(0), "", new List<string>
    //{
    //    "Id",
    //    "FirstName",
    //    "LastName",
    //    "Type",
    //    "CompanyId",
    //    "CompanyName"
    //});
    //                if (list3.Count > 0)
    //                {
    //                    Name = list3[0].FirstName + " " + list3[0].LastName;
    //                }
    //                TimeSpan duration = DateTime.Now - DateTime.Parse(Session["CallTime"].ToString());

    //                CallHistory tableobj = new Models.CallHistory();

    //                tableobj.CallDate = DateTime.Now;
    //                tableobj.ContactNo = PhoneNo;
    //                tableobj.Name = Name;
    //                tableobj.UserExtension = Ext;
    //                tableobj.CallTime = duration.Hours.ToString() + ":" + duration.Minutes.ToString() + ":" + duration.Seconds.ToString();
    //                db.CallHistories.Add(tableobj);
    //                db.SaveChanges();


    //                Session["Call"] = null;
    //            }
    //        }
    //        string text3 = "<?xml version='1.0' encoding='UTF-8' ?><response><result><call_url/></result></response>";
    //        return base.Content(text3, "text/xml");

    //        //return View();
    //    }
        


        public ActionResult Index(string ExtNo, string callerID = "2242307730")
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
            obj.Phone = callerID;
            string conditions = "Phone = '" + callerID + "'";
            List<ContactFindResult> list3 = _contactApi.FindContacts(conditions, "", new int?(1000), new int?(0), "", new List<string>
    {
        "Id",
        "FirstName",
        "LastName",
        "Type",
        "CompanyId",
        "CompanyName"
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
                    obj.CompanyName = current2.CompanyName;
                }
            }
           // var phoneno = "2242307730";
            conditions = "PhoneNumber= '" + callerID + "'";
            _activityApi = new ActivityApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            List<ActivityFindResult> activitylist = _activityApi.FindActivities(conditions, "", new int?(100000), new int?(0), new List<string>
            { "Id",
        "Subject",
        "Notes",
        "AssignToResource",
        "DueDate",
        "ActivityTypeDescription",
        "StartTime"
            });
            foreach (ActivityFindResult currentactivity in activitylist)
            {
                obj.ActivityList.Add(new ActivityModel
                {
                    ActivityTypeDescription = currentactivity.ActivityTypeDescription,
                    AssignTo = currentactivity.AssignToResource,
                    Notes = currentactivity.Notes,
                    DueDate = currentactivity.StartTime,
                    Subject = currentactivity.Subject
                });
            }
           
           // ViewBag.CallHistory = db.CallHistories.Where(x => x.UserExtension == ExtNo).OrderBy(x => x.ID).Take(10).ToList();

            return View(obj);
        }

        public ActionResult AddContact(string CallerID)
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
                Id = 0,
                CompanyIdentifier = obj.companyIdentifier
            };
            var result = _contactApi.AddOrUpdateContact(_contact);



            return RedirectToAction("Index", new { callerID = obj.Phone });
        }


        public ActionResult SaveActivity(ActivityModel model)
        {
            ContactInformation contactModel = new ContactInformation();
            contactModel.ContactId = model.ContactId;
            _activityApi = new ActivityApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            Activity activityModel = new Activity();
            activityModel.CompanyIdentifier = model.CompanyIdentifier;
            activityModel.Contact = contactModel;
            activityModel.Type = model.ActivityType;
            activityModel.Subject = model.Subject;
            activityModel.AssignTo = model.AssignTo;
            activityModel.Notes = model.Notes;
            activityModel.Id = 0;




            var result = _activityApi.AddOrUpdateActivity(activityModel);
            return RedirectToAction("Index", new { callerID = model.Phone });
        }

        public ActionResult AddActivity(string Phone,int ID)
        {
            _companyApi = new CompanyApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            _activityApi = new ActivityApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            
           string conditions = "ID="+ID;

            List<CompanyFindResult> list2 = _companyApi.FindCompanies(conditions, "CompanyName asc", new int?(100000), new int?(0), new List<string>
            {
                "Id",
                "CompanyName",
                "CompanyIdentifier"
            });
            _memberApi = new MemberApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");

            List<MemberFindResult> MembersList = _memberApi.FindMembers("", "FirstName asc", new int?(1000), new int?(0), new List<string>
            {
                "Id",
                "MemberIdentifier",
                "FirstName",
                "LastName"
            });
            var membersQuery = MembersList.Select(a => new
            {
                MemberIdentifier = a.MemberIdentifier,
                Name = a.FirstName + " " + a.LastName
            }).ToList();
            ViewBag.MembersList = new SelectList(membersQuery.AsEnumerable(), "MemberIdentifier", "Name");


            List<ActivityFindResult> ActivityTypeList = _activityApi.FindActivities("", "Id asc", new int?(1000), new int?(0), new List<string>
            {
                "ActivityTypeDescription"
            });
            var query = ActivityTypeList.Where(a => a.ActivityTypeDescription != null).Select(a => new
            {
                a.ActivityTypeDescription
            }).Distinct().ToList();
           
            ViewBag.ActivityList = new SelectList(query.AsEnumerable(), "ActivityTypeDescription", "ActivityTypeDescription");

            ActivityModel model = new ActivityModel();
            model.Phone = Phone;
            model.ContactId = GetContactsId(Phone);
            model.CompanyIdentifier = list2.FirstOrDefault().CompanyIdentifier;
            model.CompanyName = list2.FirstOrDefault().CompanyName;
            return View("AddActivity", model);

        }


        public ActionResult GetContactsDropDown(string CompanyIdentifier)
        {

            string companyCondition = "CompanyIdentifier = '" + CompanyIdentifier + "'";

            _contactApi = new ContactApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            _companyApi = new CompanyApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");


            List<CompanyFindResult> list2 = _companyApi.FindCompanies(companyCondition, "Id asc", new int?(100000), new int?(0), new List<string>
            {
                "Id",
                "CompanyName",
                "CompanyIdentifier"
            });
            int companyId = list2.FirstOrDefault().Id;

            string conditions = "CompanyId = " + companyId;


            List<ContactFindResult> Contactlst = _contactApi.FindContacts(conditions, "", new int?(1000), new int?(0), "", new List<string>
    {
        "Id",
        "FirstName",
        "LastName",
        "Type",
        "CompanyId"
    });

            var query = Contactlst.Select(a => new
            {
                Id = a.Id,
                Name = a.FirstName + " " + a.LastName
            }).ToList();


            return Json(new SelectList(query.ToArray(), "Id",
                         "Name"), JsonRequestBehavior.AllowGet);

        }



        //public ActionResult Test()
        //{
        //    // _memberApi = new MemberApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
        //    // List<MemberFindResult> list2 = _memberApi.FindMembers("", "FirstName asc", new int?(1000), new int?(0), new List<string>());
        //    ServiceTicketApi _serviceTicketApi;
        //    _serviceTicketApi = new ServiceTicketApi("https://api-eu.myconnectwise.net", "novaram", "callcenter", "Test123!", "NovaramCred");
        //    var result = _serviceTicketApi.FindServiceTickets("","",new int?(1000),new int?(0),false ,new List<string>());


        //    return View();
        //}

        public int GetContactsId(string Contact)
        {

            string conditions = "Phone = '" + Contact + "'";

            _contactApi = new ContactApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            //_companyApi = new CompanyApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");


            //List<CompanyFindResult> list2 = _companyApi.FindCompanies(companyCondition, "Id asc", new int?(100000), new int?(0), new List<string>
            //{
            //    "Id",
            //    "CompanyName",
            //    "CompanyIdentifier"
            //});
            //int companyId = list2.FirstOrDefault().Id;

            //string conditions = "CompanyId = " + companyId;


            List<ContactFindResult> Contactlst = _contactApi.FindContacts(conditions, "", new int?(1000), new int?(0), "", new List<string>
    {
        "Id",
        "FirstName",
        "LastName",
        "Type",
        "CompanyId"
    });

            var query = Contactlst.Select(a => new
            {
                Id = a.Id,
                Name = a.FirstName + " " + a.LastName
            }).ToList();


            return query.FirstOrDefault().Id;
        }

        public CompanyObj GetCompanyDetailsViaId(int CompanyId)
        {

          //  string conditions = "PhoneNumber = '" + 2242307730 + "'";            
            _companyApi = new CompanyApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
            string conditions = "ID=" + CompanyId;

            List<CompanyFindResult> list2 = _companyApi.FindCompanies(conditions, "CompanyName asc", new int?(100000), new int?(0), new List<string>
            {
                "Id",
                "CompanyName",
                "CompanyIdentifier"
            });
            CompanyObj obj = new CompanyObj();
            obj.CompanyId = list2.FirstOrDefault().Id;
            obj.CompanyIdentifier = list2.FirstOrDefault().CompanyIdentifier;
            return obj;
        }
    }

    public class FerrumItModel
    {
        public string PhoneNo { get; set; }
        public string PhoneExt { get; set; }
    }

    public class CompanyObj
    {
        public int? CompanyId { get; set; }
        public string CompanyIdentifier { get; set; }
    }


    

}