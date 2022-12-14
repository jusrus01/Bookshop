﻿namespace Bookshop.Contracts.DataTransferObjects.Clients
{
    public class EditClientDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string SelectedRole { get; set; }

        public string Role { get; set; }
    }
}
