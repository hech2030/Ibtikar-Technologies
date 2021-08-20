using ECommerceApp.Models;
using System.Collections.Generic;

namespace ECommerceApp.Common.Request
{
    public class SubmitOrderRequest
    {
        public Orderhi Order { get; set; }
        public List<Command> Commands { get; set; }

    }
}
