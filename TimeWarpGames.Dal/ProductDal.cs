using System;
using System.Collections.Generic;
using System.Linq;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public static class ProductDal
    {
        public static List<Product> ReadAll()
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                List<Product> lstProducts = db.Products.ToList();
                return lstProducts;
            }
        }
    }
}