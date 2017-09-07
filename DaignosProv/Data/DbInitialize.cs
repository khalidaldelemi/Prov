using DaignosProv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaignosProv.Data
{
    public class DbInitialize
    {
        public static void Initilaize(ApplicationDbContext context)
        {
                var TV = new ProductCategory { Name = "Tv" };
                var DVD = new ProductCategory { Name = "Dvd" };
                var VHS = new ProductCategory { Name = "Vhs" };
            context.Categories.Add(TV);
            context.Add(DVD);
            context.Add(VHS);
                
                context.SaveChanges();
            
        }
    }
}
