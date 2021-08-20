using System;
using System.Collections.Generic;

#nullable disable

namespace ECommerceApp.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
