﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Console.Models
{
    public class MailSettings
    {
        public string DisplayName { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string EmailPassword { get; set; }
        public string Host { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
    }
}