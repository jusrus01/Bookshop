﻿namespace Bookshop.Contracts.DataTransferObjects.Mails
{
    public class MailConfiguration
    {
        public bool UseSmtp4Dev { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }
    }
}
