using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeinerSales.Models;

namespace WeinerSales.Data
{
    public class DbInitializer
    {
        public static void Initialize(WeinerDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }


            var roles = new IdentityRole[]
            {
                new IdentityRole() { Name = "Manager" },
                new IdentityRole() { Name = "Sales Associate" }

            };

            var items = new Item[]
            {
                new Item() { Name = "Bun", Cost = 1, Amount = 100 },
                new Item() { Name = "Weiner", Cost = 2, Amount = 100}
            };



            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            foreach (var i in items)
            {
                context.Item.Add(i);
            }





            context.SaveChanges();
        }
    }
}