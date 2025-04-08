using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btl_web_nc.ViewModel
{
    public class SubscriptionsViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NewsletterName { get; set; }
        public string Frequency { get; set; }
        public string UserEmail { get; set; }
    }
}