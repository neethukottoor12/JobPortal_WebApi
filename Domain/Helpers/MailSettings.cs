using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class MailSettings
    {
        public string UserMail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string FromMail { get; set; }

       
    }
}
