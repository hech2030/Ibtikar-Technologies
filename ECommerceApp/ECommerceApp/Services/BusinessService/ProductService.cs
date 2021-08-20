using ECommerceApp.Models;
using ECommerceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ECommerceApp.Services.BusinessService
{
    public class ProductService : IProductService
    {
        private static EcommerceContext Context = new EcommerceContext();
        private readonly DbSet<Product> ProductDataAccessProvider = Context.Products;
        private readonly DbSet<Command> CommandDataAccessProvider = Context.Commands;
        private readonly DbSet<Orderhi> OrderDataAccessProvider = Context.Orderhis;


        public List<Product> FindProduct(int id, string name)
        {
            var query = ProductDataAccessProvider.AsQueryable();
            if (id > 0)
                query = query.Where(a => a.Id == id);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(a => a.Name == name);
            return query.ToList();
        }

        public void initProducts()
        {
            if (FindProduct(0, null).Count == 0)
            {
                this.saveProduct(new Product
                {
                    Name = "product 1 ",
                    Category = "First categ",
                    Image = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80"
                });
                this.saveProduct(new Product
                {
                    Name = "product 2 ",
                    Category = "First categ",
                    Image = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80"
                });
                this.saveProduct(new Product
                {
                    Name = "product 3 ",
                    Category = "Second categ",
                    Image = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80"
                });
                this.saveProduct(new Product
                {
                    Name = "product 4 ",
                    Category = "First categ",
                    Image = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80"
                });
            }
        }

        public Product saveProduct(Product value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                ProductDataAccessProvider.Add(value);
                Context.SaveChanges();
                transaction.Complete();
                return value;
            }
        }

        public bool SubmitOrder(Orderhi order, List<Command> commands)
        {

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                OrderDataAccessProvider.Add(order);
                Context.SaveChanges();
                transaction.Complete();
                commands.ForEach(a => a.OrderId = order.Id);
                CommandDataAccessProvider.AddRange(commands);
                return true;
            }
            return false;
        }
    }
}
