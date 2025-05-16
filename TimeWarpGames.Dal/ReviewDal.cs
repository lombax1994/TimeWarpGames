using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public static class ReviewDal
    {
        // Haalt alle reviews op voor een specifiek product
        public static List<Review> ReadByProductId(int productId)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                return db.Reviews
                    .Where(r => r.ProductId == productId)
                    .ToList();
            }
        }

        // Maakt een nieuwe review aan
        public static bool Create(Review review)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                try
                {
                    db.Reviews.Add(review);
                    return db.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}