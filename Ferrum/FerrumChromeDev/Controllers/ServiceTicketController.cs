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
            

            string conditions = "CompanyId= '" + 23232 + "'";
                 ServiceTicketApi   _ServiceTicketApi      = new ServiceTicketApi("https://control.mysupport247.net", "Mysupport247", "SwitchvoxAPI", "mH5219b2vri0KUa", "NovaramCred1");
           List<TicketFindResult> ServiceTicketlist = _ServiceTicketApi.FindServiceTickets(conditions,"",null,null,true,new List<string>);    //conditions, "", new int?(1000), new int?(0), new List<string>
            
            return Json(0);
        }
    }
}