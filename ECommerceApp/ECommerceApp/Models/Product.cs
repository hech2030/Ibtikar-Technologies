using System;
using System.Collections.Generic;

#nullable disable

namespace ECommerceApp.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
    }
}
