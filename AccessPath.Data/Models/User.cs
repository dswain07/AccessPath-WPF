using System;
using System.Collections.Generic;
using System.Text;

namespace AccessPath.Data.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Theme { get; set; } = string.Empty;

        public string UserRole { get; set; } = string.Empty;
    }
}
