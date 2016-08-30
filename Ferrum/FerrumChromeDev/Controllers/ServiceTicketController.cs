using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectWiseSDK;
using FerrumChromeDev.Models;

namespace FerrumChromeDev.Controllers
{
    public class ServiceTicketController : Controller
    {
        private static ServiceTicketApi _serviceTicketApi;
        // GET: ServiceTicket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckTicket(string Mobile)
        {
           HomeController ctlObj= new HomeController();
         int ContactID=   ctlObj.GetContactsId(Mobile);

            string conditions = "ContactId= " + ContactID ;
                 ServiceTicketApi   _ServiceTicketApi      = new ServiceTicketApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
           List<TicketFindResult> ServiceTicketlist = _ServiceTicketApi.FindServiceTickets(conditions,"",null,null,true,new List<string>  {
               "Id",
        "CompanyName",
        "CompanyId",
        "ContactName",
        "AddressLine1",
        "City"
            });

           return Json(ServiceTicketlist);
        }
        public ActionResult AddNewServiceTicket(string Mobile)
        {
            ServiceTicketModel model = new ServiceTicketModel();
            model.contactno = Mobile;

            return View(model);

        }

        [HttpPost]
        public ActionResult AddNewServiceTicket(ServiceTicketModel model)
        {

          

            ServiceTicket serviceTicket = new ServiceTicket();
            serviceTicket.CompanyId= model.CompanyId;
            serviceTicket.Summary = model.tktSummary;
            serviceTicket.DetailDescription = model.probDesc;
            serviceTicket.StatusName = "";
            serviceTicket.ServiceType = "";
            serviceTicket.ServiceSubType = "";
            serviceTicket.Priority = model.PriorityTxt;
            serviceTicket.ContactId = model.ContactId;
            serviceTicket.Id = 0;
            //serviceTicket.boar
          //  serviceTicket.DetailNotes = note;
            _serviceTicketApi = new ServiceTicketApi("https://api-eu.myconnectwise.net", "novaram", "callcenter", "Test123!", "NovaramCred");
            var result = _serviceTicketApi.AddOrUpdateServiceTicketViaCompanyIdentifier(model.CompanyIdentifier, serviceTicket);

          
            //List<TicketNote> note = new List<TicketNote>();
            //TicketNote noteModel = new TicketNote();
            //noteModel.ContactId = Convert.ToInt32(model.ContactId);
            //noteModel.DateCreated = System.DateTime.Now;
            //noteModel.NoteText = model.Notes;
            //noteModel.Id = 0;
           
            //note.Add(noteModel);
            
            
            return View();
        }
    }
}