using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectWiseSDK;
namespace FerrumChromeDev.Controllers
{
    public class ServiceTicketController : Controller
    {
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
        public ActionResult AddNewServiceTicket()
        {


            return View();

        }
    }
}