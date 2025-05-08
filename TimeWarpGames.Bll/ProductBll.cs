using System;
using System.Collections.Generic;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Bll
{
    public static class ProductBll
    {
        public static List<Product> ReadAll()
        {
            List<Product> lstProducts = TimeWarpGames.Dal.ProductDal.ReadAll();
            if (lstProducts == null)
            {
                throw new Exception("Geen producten gevonden");
            }
            return lstProducts;
        }
    }
}