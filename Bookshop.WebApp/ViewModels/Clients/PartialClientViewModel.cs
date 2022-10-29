﻿using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Clients
{
    public class PartialClientViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
