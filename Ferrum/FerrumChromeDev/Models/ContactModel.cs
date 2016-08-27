using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerrumChromeDev.Models
{
    public class ContactModel
    {

        public ContactModel()
        {
            ActivityList = new List<ActivityModel>();
        }
        public int? ContactId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

      public string companyIdentifier { get; set; }
        public int? CompanyID { get; set; }
        public string Type { get; set; }
        public string NickName { get;  set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public List<ContactModel> ContactLst
        {
            get;
            set;
        }
        public List<ActivityModel> ActivityList { get; set; }
    }
}