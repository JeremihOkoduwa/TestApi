using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model
{
    public class NotificationMetadata
    {
        public NotificationMetadata()
        {
            this.SmtpServer = "smtp-mail.outlook.com";
            this.Sender = "jerryokoduwa@hotmail.co.uk";
            this.Reciever = "jokoduwa@coure-tech.com";
            this.Password = "";
            this.Port = 587;
            this.UserName = "jerryokoduwa@hotmail.co.uk";
        }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string SmtpServer { get; set; } 
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
