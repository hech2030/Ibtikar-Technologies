using ECommerceApp.Models;
using ECommerceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Transactions;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        private static EcommerceContext Context = new EcommerceContext();
        private readonly DbSet<User> DataAccessProvider = Context.Users;

        public User Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                password = Divers.Encrypt(password);
                var query = DataAccessProvider.AsQueryable();
                query = query.Where(a => a.Email == email && a.PasswordHash == password);
                return query.ToList().FirstOrDefault();
            }
            return null;
        }

        public User Register(User value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                if (DataAccessProvider.AsQueryable().Any(a => a.Email == value.Email))
                    throw new Exception("Email already exists");
                value.PasswordHash = Divers.Encrypt(value.PasswordHash);
                DataAccessProvider.Add(value);
                Context.SaveChanges();
                transaction.Complete();
                return value;
            }
        }
    }
}
