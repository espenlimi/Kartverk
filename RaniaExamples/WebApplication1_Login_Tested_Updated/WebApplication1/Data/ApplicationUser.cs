﻿using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
