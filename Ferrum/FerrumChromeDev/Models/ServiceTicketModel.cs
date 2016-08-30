using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerrumChromeDev.Models
{
    public class ServiceTicketModel
    {
        public int? CompanyId { get; set; }
        public string tktSummary { get; set; }
        public string probDesc { get; set; }
        public string PriorityTxt { get; set; }
        public int? ContactId { get; set; }

        public string CompanyIdentifier { get; set; }
        public string Notes { get; set; }
        public string contactno { get; set; }
    }
}