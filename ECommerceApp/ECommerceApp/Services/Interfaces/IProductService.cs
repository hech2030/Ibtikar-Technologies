using ECommerceApp.Models;
using System.Collections.Generic;

namespace ECommerceApp.Services.Interfaces
{
    public interface IProductService
    {
        void initProducts();
        Product saveProduct(Product value);
        List<Product> FindProduct(int id = 0, string name = null);
        bool SubmitOrder(Orderhi order, List<Command> commands);
    }
}
