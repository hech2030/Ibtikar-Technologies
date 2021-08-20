using System;
using System.Collections.Generic;

#nullable disable

namespace ECommerceApp.Models
{
    public partial class Command
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
