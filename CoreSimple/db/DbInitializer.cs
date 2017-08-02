using CoreSimple.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimple.db
{
    public static class DbInitializer
    {
        public static void Initialize(CoreContext context)
        {
            context.Database.Migrate();
            if (context.City.Any())
            {
                return;
            }
            var cities = new City[]
            {
                new City(){Name="Kiev",Country="Ukraine",Population=4000000},
                new City(){Name="Paris",Country="France",Population=6000000},
                new City(){Name="Berlin",Country="Germany",Population=8000000},
                new City(){Name="Tokio",Country="Japan",Population=11000000}
            };
            foreach (var item in cities)
            {
                context.City.Add(item);
            }
            context.SaveChanges();
        }
    }
}
