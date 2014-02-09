using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GmailNotifier
{
    public class Email
    {
        public string ID { set; get; }
        public string Sender { set; get; }
        public string From { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public DateTime Date { set; get; }
        public string Link { set; get; }
        public DateTime Modified { set; get; }
    }
}