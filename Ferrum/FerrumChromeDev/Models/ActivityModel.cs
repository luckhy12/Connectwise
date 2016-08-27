using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerrumChromeDev.Models
{
    public class ActivityModel
    {
        public string CompanyIdentifier { get; set; }
        public string Notes { get; set; }
        public string AssignTo { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string ActivityTypeDescription { get; set; }
        public DateTime? DueDate { get; set; }
    }
}